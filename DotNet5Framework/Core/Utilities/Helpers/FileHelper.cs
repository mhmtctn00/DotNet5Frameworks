using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        private static readonly List<string> imageFormats = new() { ".jpeg", ".jpg", ".png" };
        private static readonly List<string> fileFormats = new() { ".pdf", ".docx", ".rtf", ".doc" };
        private static readonly string rootPath = Directory.GetCurrentDirectory();


        public static void ResizeImage(double scaleFactor, string srcPath, string destPath)
        {
            var image = Image.FromFile(srcPath);
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

        // Dosya yolları dışarıadn dinamik alınacak.
        public static void ResizeImagesWithMaxSize(List<string> srcPhats, int maxSize = 500)
        {
            foreach (var path in srcPhats)
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

        public static async Task CopyAsync(string srcPath, string destPath)
        {
            int bufferSize = 1024 * 1024;
            Thread.Sleep(322);
            using (FileStream fileStream = new FileStream(destPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            //using (FileStream fs = File.Open(<file-path>, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                FileStream fs = new FileStream(srcPath, FileMode.Open, FileAccess.ReadWrite);
                fileStream.SetLength(fs.Length);
                int bytesRead = -1;
                byte[] bytes = new byte[bufferSize];

                while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
                {
                    await fileStream.WriteAsync(bytes, 0, bytesRead);
                }
                fs.Close();
            }
        }

        //Hedef dosyaya istenilen isim verilebilmeli. Suan random değer atıyor.
        public static async Task<string> CopyAsync(IFormFile file, string destFolderName)
        {
            var filePath = "";
            var fileExtension = $".{file.ContentType.Split("/")[1]}";
            if (!(fileFormats.Any(x => x == fileExtension) || imageFormats.Any(x => x == fileExtension)))
            {
                return "unsupported_file_format_type";
            }


            var fName = "";
            if (imageFormats.Any(x => x == fileExtension))
                fName = Path.Combine("Images", destFolderName);
            if (fileFormats.Any(x => x == fileExtension))
                fName = Path.Combine("Files", destFolderName);

            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), fName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(fName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                filePath = dbPath;
            }
            return filePath;
        }

        //Hedef dosyaya istenilen isim verilebilmeli. Suan random değer atıyor.
        public static async Task<string> CopyAsync(List<IFormFile> files, string destFolderName)
        {
            var filePaths = new List<string>();
            foreach (var file in files)
            {
                var fileExtension = $".{file.ContentType.Split("/")[1]}";
                if (!(fileFormats.Any(x => x == fileExtension) || imageFormats.Any(x => x == fileExtension)))
                {
                    return "unsupported_file_format_type";
                }
            }
            foreach (var file in files)
            {
                var fileExtension = $".{file.ContentType.Split("/")[1]}";
                var fName = "";
                if (imageFormats.Any(x => x == fileExtension))
                    fName = Path.Combine("Images", destFolderName);
                if (fileFormats.Any(x => x == fileExtension))
                    fName = Path.Combine("Files", destFolderName);

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), fName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(fName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    filePaths.Add(dbPath);
                }
            }
            return JsonConvert.SerializeObject(filePaths);
        }
    }
}
