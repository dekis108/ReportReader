using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReportReader.Classes
{
    public static class ReportParser
    {
        public static void ParseSummaryReport()
        {
            throw new NotImplementedException();
        }

        public static List<Report> ParseTransformReport(string path, string fileName)
        {
            path = path + fileName;

            string[] temp = path.Split('\\');
            string directory = temp[temp.Length - 2];

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
                                    Date = path.Split("at_")[1],
                                    Log_Directory = directory,
                                    File_State = "TODO" //TODO
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
    }
}
