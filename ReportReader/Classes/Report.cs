using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ReportReader.Classes
{
    public enum ReportType { Summary = 0, Transform }

    public class Report
    {
        public ReportType ReportType { get; set; }

        public string CircuitName { get; set; }

        public string File_Content { get; set; }

        public string File { get; set; }

        public string Date { get; set; }

        public string File_State { get; set; }

        public string Log_Directory { get; set; }


    }
}
