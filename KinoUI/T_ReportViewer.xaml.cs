using ReportsClassLib;
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

namespace KinoUI
{
    /// <summary>
    /// Interaction logic for T_ReportViewer.xaml
    /// </summary>
    public partial class T_ReportViewer : Window
    {
        public T_ReportViewer()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //PedigreeReport rep = new PedigreeReport();
            //var typeReportSource = new Telerik.Reporting.TypeReportSource();
            //typeReportSource.TypeName = "";//"Telerik.Reporting.Examples.CSharp.ListBoundReport, CSharp.ReportLibrary";
            //this.reportViewer1.ReportSource =  rep;// typeReportSource;
        }
    }
}
