using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportReader.Classes
{
    public static class SQLiteWriter
    {
        private static string warningTable = "Warnings";
        private static string errorTable = "Errors";
        private static string connectionString = @"Data Source=C:\Users\Dejan\Desktop\DMSZadatak\Solutions\ReportReader\ReportReader\Results\Reports.db;"; 

        private static SqliteCommand InsertCommand(SqliteConnection connection, string table, Report report)
        {
            SqliteCommand command;
            command = connection.CreateCommand();
            command.CommandText = string.Format($"INSERT INTO {table}  VALUES (@circuitName, @fileContent, @file, @date, @fileState, @logDirectory)");

            command.Parameters.AddWithValue("@circuitName", report.CircuitName);
            command.Parameters.AddWithValue("@fileContent", report.File_Content);
            command.Parameters.AddWithValue("@file", report.File);
            command.Parameters.AddWithValue("@date", report.Date);
            command.Parameters.AddWithValue("@fileState", report.File_State);
            command.Parameters.AddWithValue("@logDirectory", report.Log_Directory);


            return command;
        }

        public static void Write(List<Report> summaryReports, List<Report> transformReports)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var report in transformReports)
                        {
                            var command = InsertCommand(connection, warningTable, report);
                            command.ExecuteNonQuery();
                        }

                        foreach (var report in summaryReports)
                        {
                            var command = InsertCommand(connection, errorTable, report);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
