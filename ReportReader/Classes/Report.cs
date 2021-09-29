﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ReportReader.Classes
{
    public class Report
    {
        public string CircuitName { get; set; }

        public string File_Content { get; set; }

        public string File { get; set; }

        public string Date { get; set; }

        public string File_State { get; set; }

        public string Log_Directory { get; set; }

        public override string ToString()
        {
            return string.Format($"({CircuitName},{File_Content},{File},{Date},{File_State},{Log_Directory})");
        }

    }
}
