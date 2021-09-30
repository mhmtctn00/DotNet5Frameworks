using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Performance.Database
{
    public class DatabasePerformance
    {
        private IConfiguration _configuration;
        public DatabasePerformance()
        {
            Connect();
        }
        public DatabasePerformance(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        SqlConnection connection;
        string connectionString = "";
        SqlCommand cmd;

        public void Connect()
        {
            var _configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();
            connectionString = _configuration.GetConnectionString("ConnectionString");


            connection = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = connection;
        }
        public void Add(string className, string methodName, string time)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                cmd.CommandText = "SELECT CASE WHEN OBJECT_ID('dbo.Performances', 'U') IS NOT NULL THEN 1 ELSE 0 END";
                Int32 count = (Int32)cmd.ExecuteScalar();
                if (count == 0)
                {
                    try
                    {
                        cmd.CommandText = "CREATE TABLE [dbo].[Performances] ([Id][int] IDENTITY(1,1) NOT NULL, [ClassName] [varchar](MAX) NOT NULL," +
                        "[MethodName] [varchar](50) NOT NULL,[Time] [varchar](MAX) NOT NULL)";
                        cmd.ExecuteNonQuery();
                        AddToTable(className, methodName, time);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    AddToTable(className, methodName, time);
                }
            }
        }
        public void AddToTable(string className, string methodName, string time)
        {
            try
            {
                cmd.CommandText = "Insert Into Performances(ClassName,MethodName,Time) Values ('" + className + "','" + methodName + "','" + time + "')";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
