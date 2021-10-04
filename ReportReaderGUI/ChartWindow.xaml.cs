using LiveCharts;
using LiveCharts.Wpf;
using ReportReader.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace ReportReaderGUI
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private MainWindow main;

        public ChartWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            main = mainWindow;
        }

        public void LoadChartData(List<Report> reports)
        {
            if (reports == null || reports.Count == 0)
            {
                return;
            }

            List<string> names = new List<string>();
            List<int> errors = new List<int>();
            List<int> warnings = new List<int>();

            int i;
            foreach (Report report in reports)
            {
                i = 0;
                if (!names.Contains(report.CircuitName))
                {
                    names.Add(report.CircuitName);
                    errors.Add(0);
                    warnings.Add(0);

                    i = names.Count - 1;
                }
                else
                {
                    i = names.FindIndex(x => x == report.CircuitName);
                }

                switch (report.ReportType)
                {
                    default:
                    case ReportType.Summary:
                        errors[i]++;
                        break;
                    case ReportType.Transform:
                        warnings[i]++;
                        break;
                }

            }


            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Errors",
                    Values = new ChartValues<int>(errors.ToArray())
                },

                new ColumnSeries
                {
                    Title = "Warnings",
                    Values = new ChartValues<int>(warnings.ToArray())
                }
            };


            Labels = names.ToArray();
            Formatter = value => value.ToString();
            DataContext = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            main.ChartClosed();
        }
    }
}
