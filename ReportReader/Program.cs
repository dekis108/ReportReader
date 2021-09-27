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
        const string summaryFile = "Summary.csv";
        const string transformReport = "CIMToDMSTranformReports.txt";
        const string summaryReport = "SummaryReport.txt";

        static void Main(string[] args)
        {
            string inputTransformPath = @"C:\Users\Dejan\Desktop\DMSZadatak\Zadatak\Reports\Reports\Rejected\Reports_for_ChangeSet_FRS_10_23_E0101 [Bulk] _created at_2014-03-04_17-40-08\";
            string inputSummaryPath = @"C:\Users\Dejan\Desktop\DMSZadatak\Zadatak\Reports\Reports\Rejected\Reports_for_ChangeSet_BRT_10_57_E0101 [Bulk] _created at_2014-03-04_17-29-48\";

            //var records = ReportParser.ParseTransformReport(inputTransformPath, transformReport);
            //CsvWriter.AppendOrCreate(outputPath, warningsFile, records);

            var records = ReportParser.ParseSummaryReport(inputSummaryPath, summaryReport);
            CsvWriter.AppendOrCreate(outputPath, summaryFile, records);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
