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

            var records =  ReportParser.ParseReports(directory);
            ReportParser.SaveReports(records, ReportParser.ReportOutput.CSV);
            ReportParser.SaveReports(records, ReportParser.ReportOutput.SQLite);


            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
