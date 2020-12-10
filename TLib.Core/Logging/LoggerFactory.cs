using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Core.Logging
{
    public class LoggerFactory
    {
        public static Serilog.ILogger GetSerilogSqlLogger(string connectionString)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "Serilogs", AutoCreateSqlTable = true })
                .CreateLogger();

        }

        public static Serilog.ILogger GetSerilogFileLogger(int retainedFileCountLimit = 2)
        {
            return new LoggerConfiguration()
                .WriteTo.File(
                    path: "logs\\log.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 500000000,//500Mb
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: retainedFileCountLimit)
                .MinimumLevel.Verbose()
                .CreateLogger();
        }

        public static Serilog.ILogger GetSerilogAsyncFileLogger(int retainedFileCountLimit = 2)
        {
            return new LoggerConfiguration()
                .WriteTo.Async(a =>
                {
                    a.File(
                        path: "logs\\log.txt",
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                        rollingInterval: RollingInterval.Day,
                        fileSizeLimitBytes: 500000000,//500Mb
                        rollOnFileSizeLimit: true,
                        retainedFileCountLimit: retainedFileCountLimit);
                })
                .MinimumLevel.Verbose()
                .CreateLogger();
        }
    }
}
