using ReportReader.Classes;
using System;
using System.Collections.Generic;

namespace ReportReader
{
    class Program
    {
        const string path = @"..\..\..\Results\";
        const string warningsFile = "Warnings.csv";
        const string errorsFile = "Errors.csv";
        const string statisticsFile = "Statistics.csv";
        const string summary = "Summary.csv";

        static void Main(string[] args)
        {
            List<Report> records = new List<Report>()
            {
                new Report()
                {
                    CircuitName = "c1",
                    File_Content = "ovako onako",
                    File = "ovaj",
                    Date = DateTime.Now,
                    File_State = "Corrupted",
                    Log_Directory = "evo"
                },
                new Report()
                {
                    CircuitName = "c2",
                    File_Content = "inace",
                    File = "inace",
                    Date = DateTime.Now,
                    File_State = "inace",
                    Log_Directory = "inace"
                }
            };



            CsvWriter.AppendOrCreate(path,warningsFile, records);

            Console.WriteLine("Hello World!");
        }
    }
}
