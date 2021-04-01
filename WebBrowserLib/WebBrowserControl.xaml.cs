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

namespace WebBrowserLib
{
    /// <summary>
    /// Interaction logic for WebBrowserControl.xaml
    /// </summary>
    public partial class WebBrowserControl : Window
    {
        private string key = "";
        private string _selfileUrl = "";
        SeacrTypeEnum _searchType = SeacrTypeEnum.NotDefined;
        string wiki_URL;
        public WebBrowserControl(string _key, SeacrTypeEnum _type, string _wikiurl="") :this()
        {
            key = _key;
            if (_wikiurl != "") wiki_URL = _wikiurl;
            _searchType = _type;
            Browser.Address = GetFinalUrl();
        }

        void LoadUrl()
        {
            Browser.Address = GetFinalUrl();
        }

        string GetFinalUrl()
        {
            string _finurl = "";
            //"https://www.google.com/search?tbm=isch&q=" + _key;
            switch (_searchType)
            {
                case SeacrTypeEnum.NotDefined:
                    _finurl = txtAddress.Text;
                    break;
                case SeacrTypeEnum.Google:
                    break;
                case SeacrTypeEnum.GoogleImage:
                    _finurl = "https://www.google.com/search?tbm=isch&q=" + key;
                    break;
                case SeacrTypeEnum.Wiki:
                    _finurl = wiki_URL;
                    break;
                case SeacrTypeEnum.FCI:
                    _finurl = "https://www.google.az/search?q=" + key +"+site:www.fci.be";
                    break;
                default:
                    break;
            }
            _finurl = _finurl.Replace("-", "+");
            _finurl = _finurl.Replace("  ", " ");
            _finurl = _finurl.Replace(" ", "+");
            return _finurl;
        }

        public string SelectedFileUrl()
        {
            return _selfileUrl;
        }
        public WebBrowserControl()
        {
            InitializeComponent();
            MenuHandler mnuhandler = new MenuHandler();
            mnuhandler.ImageSelectedEvent += Mnuhandler_ImageSelectedEvent;
            Browser.MenuHandler = mnuhandler;
        }

        private void Mnuhandler_ImageSelectedEvent(string sampleParam)
        {
            try
            {
                _selfileUrl=sampleParam; 
                this.Dispatcher.Invoke(() =>
                {
                    this.DialogResult = true;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in Mnuhandler_ImageSelectedEvent");
            }

        }

        private void btnGoogleImage_Click(object sender, RoutedEventArgs e)
        {
            _searchType = SeacrTypeEnum.GoogleImage;
            Browser.Address = GetFinalUrl();
        }

        private void btnwiki_Click(object sender, RoutedEventArgs e)
        {
            _searchType = SeacrTypeEnum.Wiki;
            Browser.Address = GetFinalUrl();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            _searchType = SeacrTypeEnum.NotDefined;
            Browser.Address = GetFinalUrl();
        }

        private void btnGoogle_Click(object sender, RoutedEventArgs e)
        {
            _searchType = SeacrTypeEnum.FCI;
            Browser.Address = GetFinalUrl();
            //
        }
    }

    public enum SeacrTypeEnum
    {
        NotDefined,
        Google,
        GoogleImage,
        Wiki,
        FCI
    }
}
