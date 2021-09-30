using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using ReportReader.Classes;

namespace ReportReaderGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string defaultDirectory = @"C:\Users\Dejan\Desktop\DMSZadatak\Zadatak\Reports\Reports\";
        const string defaultOutput = @"..\..\..\..\ReportReader\Results";
        const string defaultDBPath = @"..\..\..\Results\Reports.db";

        List<Report> reports = new List<Report>();

        public MainWindow()
        {
            InitializeComponent();
            InitWorkingDirectory();
        }

        private void InitWorkingDirectory()
        {
            txtDirectory.Text = defaultDirectory;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtDirectory.Text = dialog.FileName + '\\';
            }
        }

        private void btnParse_Click(object sender, RoutedEventArgs e)
        {
            if (txtDirectory.Text.Trim() == "")
            {
                MessageBox.Show("Directory path cannot be empty!");
                return;
            }

            try
            {
                reports = ReportParser.ParseReports(txtDirectory.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            gridSummaryReports.ItemsSource = reports.Where(x=> x.ReportType == ReportType.Summary);
            gridTransformReports.ItemsSource = reports.Where(x => x.ReportType == ReportType.Transform);
        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (reports.Count == 0)
            {
                MessageBox.Show("No data to save, aborting.");
                return;
            }

            string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), default));
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = path;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                path = dialog.FileName + '\\';
            }
            else
            {
                return;
            }

            ReportParser.SaveReports(reports, ReportParser.ReportOutput.CSV, path);
            MessageBox.Show("Done.");
        }


        private void btnSaveDB_Click(object sender, RoutedEventArgs e)
        {
            if (reports.Count == 0)
            {
                MessageBox.Show("No data to save, aborting.");
                return;
            }

            string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Directory.GetCurrentDirectory(), defaultDBPath));
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = path;
            dialog.IsFolderPicker = false;
            dialog.Filters.Add(new CommonFileDialogFilter("Database file(.db)", "db"));
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                path = dialog.FileName;
                path = System.IO.Path.GetFullPath(path);
                path = string.Format($@"Data Source={path};");
            }
            else
            {
                return;
            }

            ReportParser.SaveReports(reports, ReportParser.ReportOutput.SQLite, path);
            MessageBox.Show("Done.");
        }
    }
}
