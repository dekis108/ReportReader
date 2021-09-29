using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReportReader.Classes
{
    public static class ReportParser
    {
        const string outputPath = @"..\..\..\Results\";
        const string warningsFile = "Warnings.csv";
        const string errorsFile = "Errors.csv";
        const string statisticsFile = "Statistics.csv";
        const string summaryFile = "Summary.csv";
        const string transformReport = "CIMToDMSTranformReports.txt";
        const string summaryReport = "SummaryReport.txt";


        private static string ParseDate(string path)
        {
            return path.Split("at_")[1].Split('\\')[0];
        }

        private static string ParseFileState(string path)
        {
            string[] directories = path.Split('\\');
            string currentDirectory = directories[directories.Length - 1];
            string prevDirectory = directories[directories.Length - 2];

            string reason = currentDirectory.Split('_')[2];

            return prevDirectory + " " + reason;
        }


        private static List<Report> ParseSummaryReport(string directory, string fileName, string circuitName)
        {
            string path = directory + '\\' + fileName;

            string errorContent = "";
            List<Report> reports = new List<Report>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');


                        while (words[0] == "Error" && words[1] == "description")
                        {
                            do
                            {
                                errorContent += line + '\n';
                                if ((line = reader.ReadLine()) == null) {
                                    break;
                                }
                                
                                words = line.Split(' ');
                            } while (words[0] != "Error" || words[1] != "description") ;


                            reports.Add(
                                new Report()
                                {
                                    CircuitName = circuitName,
                                    File_Content = errorContent.Trim(),
                                    File = fileName,
                                    Date = ParseDate(path),
                                    Log_Directory = directory,
                                    File_State = ParseFileState(directory),
                                }
                            );

                            errorContent = line;
                        }


                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }



            return reports;
        }

        private static List<Report> ParseTransformReport(string directory, string fileName)
        {
            string path = directory + '\\' + fileName;

            string circuitName = "";
            List<Report> reports = new List<Report>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line; 
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');
                        if (circuitName == "" && words[0] == "Circuit")
                        {
                            circuitName = words[3];
                        }

                        words = line.Split('\t');
                        if (words.Length > 1)
                        {
                            reports.Add(
                                new Report()
                                {
                                    CircuitName = circuitName,
                                    File_Content = words[1],
                                    File = fileName,
                                    Date = ParseDate(path),
                                    Log_Directory = directory,
                                    File_State = ParseFileState(directory),
                                }
                             );

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return reports;
        }

        public static void ParseReports(string directory)
        {
            string circuitName = "";
            string[] reportClass = Directory.GetDirectories(directory);
            foreach(string subdirectory in reportClass)
            {
                string[] reports = Directory.GetDirectories(subdirectory);
                foreach (string report in reports)
                {
                    circuitName = "";
                    var records = ReportParser.ParseTransformReport(report, transformReport);
                    CsvWriter.AppendOrCreate(outputPath, warningsFile, records);

                    if (records.Count > 0)
                    {
                        circuitName = records[0].CircuitName;
                    }

                    records = ReportParser.ParseSummaryReport(report, summaryReport, circuitName);
                    CsvWriter.AppendOrCreate(outputPath, errorsFile, records);
                }
            }
        }
    }
}
