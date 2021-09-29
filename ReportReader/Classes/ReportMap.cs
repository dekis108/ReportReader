using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ReportReader.Classes
{
    public class ReportMap : ClassMap<Report>
    {
        public ReportMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.ReportType).Ignore();
        }
    }
}
