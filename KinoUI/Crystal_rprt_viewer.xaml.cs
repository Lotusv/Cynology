using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms.Integration;

namespace KinoUI
{
    /// <summary>
    /// Interaction logic for Crystal_rprt_viewer.xaml
    /// </summary>
    public partial class Crystal_rprt_viewer : Window
    {
    CrystalReportViewer cryreportviewer = null;

    //Rprt As ReportDocument

    public Crystal_rprt_viewer()
    {
        InitializeComponent();

    }

    public Crystal_rprt_viewer(ReportDocument Rprt)
    {
        InitializeComponent();

        CrystalReportViewer crystalReportViewer1;
        WindowsFormsHost host = new WindowsFormsHost();
        crystalReportViewer1 = new CrystalReportViewer();
        crystalReportViewer1.ReportSource = Rprt;
        host.Child = crystalReportViewer1;
        grid1.Children.Add(host);

    }

    public Crystal_rprt_viewer(string rprtpath)
    {
        InitializeComponent();

        CrystalReportViewer crystalReportViewer1;
        WindowsFormsHost host = new WindowsFormsHost();
        crystalReportViewer1 = new CrystalReportViewer();
        crystalReportViewer1.ReportSource = rprtpath;
        host.Child = crystalReportViewer1;
        grid1.Children.Add(host);

    }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (reportViewer == null)
            //{cryreportviewer = new CrystalReportViewer();
            //    reportViewer.Child = cryreportviewer;
            //}
            //// create new report 
            //documentReportDocument report = new ReportDocument();
            //// load report to document
            //report.Load("Report/CryReportDemo.rpt");
            //// set database logon inforation to report document 
            //object.report.SetDatabaseLogon("username", "pass", @"TGS-LT-01\EXPRESS", "dbname");
            ////finally set report object to 
            //viewer.cryreportviewer.ReportSource = report;
        }
    }
}
