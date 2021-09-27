using System;
using System.Collections.Generic;
using System.Text;

namespace ReportReader.Classes
{
    public class Report
    {
        public string CircuitName { get; set; }

        public string File_Content { get; set; }

        public string File { get; set; }

        public DateTime Date { get; set; }

        public string File_State { get; set; }

        public string Log_Directory { get; set; }
    }
}
