using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;


namespace Core.Utilities.Helpers
{
    class FileHelper
    {
        public static void ResizeImage(double scaleFactor, string path, string destPath)
        {
            var image = Image.FromFile(path);
            var newWidth = (int)(image.Width * scaleFactor);
            var newHeight = (int)(image.Height * scaleFactor);
            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);
            thumbnailBitmap.Save(destPath, image.RawFormat);

            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
        }
        public static void ResizeImagesWithMaxSize(List<string> paths, int maxSize = 500)
        {
            foreach (var path in paths)
            {
                var destPath = $"{Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)}/sent/{path.Split('\\')[path.Split('\\').Length - 1]}";
                int fileSize = (int)new FileInfo(path).Length / 1024;
                double scale = 1.0;
                if (fileSize <= 500)
                {
                    ResizeImage(scale, path, destPath);
                }
                while (fileSize > 500)
                {
                    if (fileSize < 570)
                        scale -= 0.1;
                    else if (fileSize < 2500)
                        scale -= 0.2;
                    else if (fileSize < 3500)
                        scale -= 0.3;
                    else if (fileSize < 4500)
                        scale -= 0.4;
                    else
                        scale -= 0.5;

                    if (scale < 0)
                        scale = 0.1;

                    ResizeImage(scale, path, destPath);
                    fileSize = (int)new FileInfo(destPath).Length / 1024;
                    Thread.Sleep(100);
                }
                Thread.Sleep(100);
            }
        }
        public static void Copy(string inputFilePath, string outputFilePath)
        {
            int bufferSize = 1024 * 1024;
            Thread.Sleep(322);
            using (FileStream fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            //using (FileStream fs = File.Open(<file-path>, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.ReadWrite);
                fileStream.SetLength(fs.Length);
                int bytesRead = -1;
                byte[] bytes = new byte[bufferSize];

                while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
                {
                    fileStream.Write(bytes, 0, bytesRead);
                }
                fs.Close();
            }
        }
    }
}
