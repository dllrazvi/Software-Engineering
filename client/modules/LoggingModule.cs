using Serilog;
using Serilog.Sinks.MSSqlServer;
using Microsoft.Extensions.Logging;
using System.Data;

namespace client.modules
{
    internal class LoggingModule
    {
        private static readonly ILoggerFactory _loggerFactory;

        static LoggingModule()
        {
            DatabaseConnection dbInstance = DatabaseConnection.Instance;
            var conn = dbInstance.GetConnection();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: conn.ConnectionString,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "logs",
                        AutoCreateSqlTable = true,
                    }
                )
                .CreateLogger();

            _loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(dispose: true));
        }

        public static ILogger<T> CreateLogger<T>() => _loggerFactory.CreateLogger<T>();
    }
}
