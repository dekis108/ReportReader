using ReportReader.Classes;
using System;
using System.Collections.Generic;

namespace ReportReader
{
    class Program
    {
        const string outputPath = @"..\..\..\Results\";
        const string warningsFile = "Warnings.csv";
        const string errorsFile = "Errors.csv";
        const string statisticsFile = "Statistics.csv";
        const string summary = "Summary.csv";
        const string transformFile = "CIMToDMSTranformReports.txt";

        static void Main(string[] args)
        {
            string inputPath = @"C:\Users\Dejan\Desktop\DMSZadatak\Zadatak\Reports\Reports\Rejected\Reports_for_ChangeSet_FRS_10_23_E0101 [Bulk] _created at_2014-03-04_17-40-08\";

            var records = ReportParser.ParseTransformReport(inputPath, transformFile);
            CsvWriter.AppendOrCreate(outputPath, warningsFile, records);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
