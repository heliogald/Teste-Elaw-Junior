using System.Data.SQLite;
using WebCrawler.Models;

namespace WebCrawler.Services
{
    public class DatabaseLogger
    {
        private readonly string _connectionString;

        public DatabaseLogger(string dbFilePath)
        {
            _connectionString = $"Data Source={dbFilePath};Version=3;";
        }

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS ExecutionLogs (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime DATETIME,
                        EndTime DATETIME,
                        TotalPages INTEGER,
                        TotalRows INTEGER,
                        JsonFilePath TEXT
                    )";
                var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void LogExecution(ExecutionLog log)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = @"
                    INSERT INTO ExecutionLogs (StartTime, EndTime, TotalPages, TotalRows, JsonFilePath)
                    VALUES (@StartTime, @EndTime, @TotalPages, @TotalRows, @JsonFilePath)";
                var command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@StartTime", log.StartTime);
                command.Parameters.AddWithValue("@EndTime", log.EndTime);
                command.Parameters.AddWithValue("@TotalPages", log.TotalPages);
                command.Parameters.AddWithValue("@TotalRows", log.TotalRows);
                command.Parameters.AddWithValue("@JsonFilePath", log.JsonFilePath);
                command.ExecuteNonQuery();
            }
        }
    }
}
