using FirstFloor.ModernUI.Windows.Controls;
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
using FirstFloor.ModernUI.Presentation;

namespace KinoUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Link link=new Link { DisplayName = "light", Source = AppearanceManager.LightThemeSource };
                Uri uri= link.Source;
                AppearanceManager.Current.ThemeSource = uri;
                string server = System.Configuration.ConfigurationSettings.AppSettings["Server"];
                string dbname = System.Configuration.ConfigurationSettings.AppSettings["DbName"];
                string login = System.Configuration.ConfigurationSettings.AppSettings["login"];
                string pass = System.Configuration.ConfigurationSettings.AppSettings["pass"];
                string connstr = String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connect Timeout=300", server, dbname, login, pass);
                KinoDataLibrary.Common.SetSQLConnectionString(connstr);
                
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }
    }
}
