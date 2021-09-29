using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace ReportReader.Classes
{
    public static class CsvWriter
    {
        public static void Write(string directory, string fileName, List<Report> records)
        {
            string path = directory + fileName;

            using (var writer = new StreamWriter(path))
            {
                using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<ReportMap>();

                    csv.WriteRecords(records);
                }
            }
        }

        public static void AppendOrCreate(string directory, string fileName,List<Report> records)
        {
            string path = directory + fileName;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = !File.Exists(path),
            };

            using (var stream = File.Open(path, FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    using (var csv = new CsvHelper.CsvWriter(writer, config))
                    {
                        csv.WriteRecords(records);
                    }
                }
            }

        }
    }
}
