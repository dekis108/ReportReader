using ReportReader.Classes;
using System;
using System.Collections.Generic;

namespace ReportReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = @"C:\Users\Dejan\Desktop\DMSZadatak\Zadatak\Reports\Reports\";

            //ReportParser.ParseReports(directory, ReportParser.ReportType.CSV);

            ReportParser.ParseReports(directory, ReportParser.ReportType.SQLite);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
