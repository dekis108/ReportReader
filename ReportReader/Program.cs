using ReportReader.Classes;
using System;
using System.Collections.Generic;

namespace ReportReader
{
    class Program
    {
        const string directory = @"C:\Users\Dejan\Desktop\DMSZadatak\Zadatak\Reports\Reports\";
        const string outputPath = @"..\..\..\Results\";
        static string connection = @"..\..\..\Results\Reports.db";

        static void Main(string[] args)
        {
            var records =  ReportParser.ParseReports(directory);
            connection = System.IO.Path.GetFullPath(connection);
            string connectionString = string.Format($@"Data Source={connection};");

            ReportParser.SaveReports(records, ReportParser.ReportOutput.CSV, outputPath);
            ReportParser.SaveReports(records, ReportParser.ReportOutput.SQLite, connectionString);


            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
