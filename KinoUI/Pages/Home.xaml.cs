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
using KinoDataLibrary;
using KinoUI.Content;

namespace KinoUI.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        DirectoriesListUC dirs;
        Border imgPersons;
        Border imgDirectories;
        Border imgGlobe;
        Border imgbreeds3;
        Border imgkennelclub;
        Border imgCACIB;
        Border imgPrize;

        GeographyDirectoriesList _geographyDirectoriesList;
        BreedsListUC _breedsListUC;
        KennelsListUC _kennelsListUC;
        CACIBList _CACIBList;
        PrizesListUC _prizesListUC;
        public Home()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {

               // imgPersons = ((Border)(this.FindResource("imgPersons")));
                imgGlobe = ((Border)(this.FindResource("imgGlobe")));
                imgDirectories = ((Border)(this.FindResource("imgDirectories")));
                imgbreeds3 = ((Border)(this.FindResource("imgbreeds3")));
                imgkennelclub = ((Border)(this.FindResource("imgkennelclub")));
                imgCACIB = ((Border)(this.FindResource("imgCACIB")));
                imgPrize = ((Border)(this.FindResource("imgPrize")));
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Image img = sender as Image;
            if (img != null)
            {
                switch (img.Source.ToString())
                {//RadTransitionControlPrizes

                    case "pack://application:,,,/KinoUI;component/Pages/images/Prize.png":
                        if (_prizesListUC == null)
                        {
                            _prizesListUC = new PrizesListUC();
                            _prizesListUC.Name = "_prizesListUC";
                            _prizesListUC.Back += _prizesListUC_Back;
                            _prizesListUC.MouseDoubleClick += _prizesListUC_MouseDoubleClick;
                            _prizesListUC.MouseMove += MenuUC_MouseMove;
                        }
                        _prizesListUC.Width = imgPrize.ActualWidth;
                        _prizesListUC.Height = imgPrize.ActualHeight;
                        RadTransitionControlPrizes.Content = _prizesListUC;
                        break;

                    case "pack://application:,,,/KinoUI;component/Pages/images/cacib_10.jpg":
                        if (_CACIBList == null)
                        {
                            _CACIBList = new CACIBList();
                            _CACIBList.Name = "_CACIBList";
                            _CACIBList.Back += CACIBList_Back;
                            _CACIBList.MouseDoubleClick += CACIBList_MouseDoubleClick;
                            _CACIBList.MouseMove += MenuUC_MouseMove;
                        }
                        _CACIBList.Width = imgCACIB.ActualWidth;
                        _CACIBList.Height = imgCACIB.ActualHeight;
                        RadTransitionControlCACIB.Content = _CACIBList;
                        break;

                    case "pack://application:,,,/KinoUI;component/Pages/images/kennelclub.png":
                        if (_kennelsListUC == null)
                        {
                            _kennelsListUC = new KennelsListUC();
                            _kennelsListUC.Name = "_kennelsListUC";
                            _kennelsListUC.Back += _kennelsListUC_Back;
                            _kennelsListUC.MouseDoubleClick += _kennelsListUC_MouseDoubleClick;
                            _kennelsListUC.MouseMove += MenuUC_MouseMove;
                        }
                        _kennelsListUC.Width = imgkennelclub.ActualWidth;
                        _kennelsListUC.Height = imgkennelclub.ActualHeight;
                        RadTransitionControlKennelClubs.Content = _kennelsListUC;
                        break;

                    case "pack://application:,,,/KinoUI;component/Pages/images/breeds3.png":
                        if (_breedsListUC == null)
                        {
                            _breedsListUC = new BreedsListUC();
                            _breedsListUC.Name = "_breedsListUC";
                            _breedsListUC.Back += _breedsListUC_Back;
                            _breedsListUC.MouseDoubleClick += _breedsListUC_MouseDoubleClick;
                            _breedsListUC.MouseMove += MenuUC_MouseMove;
                        }
                        _breedsListUC.Width = imgbreeds3.ActualWidth;
                        _breedsListUC.Height = imgbreeds3.ActualHeight;
                        RadTransitionControlreeds.Content = _breedsListUC;
                        break;

                    case "pack://application:,,,/KinoUI;component/Pages/images/directory-icon.png":
                        if (dirs == null)
                        {
                            dirs = new DirectoriesListUC();
                            dirs.Name = "dirs";
                            dirs.Back += dirs_Back;
                            dirs.MouseDoubleClick += dirs_MouseDoubleClick;
                            dirs.MouseMove += MenuUC_MouseMove;
                        }
                        dirs.Width = imgDirectories.ActualWidth;
                        dirs.Height = imgDirectories.ActualHeight;
                        RadTransitionControl1.Content = dirs;
                        break;
                    case "pack://application:,,,/KinoUI;component/Pages/images/globe.png":
                        if (_geographyDirectoriesList == null)
                        {
                            _geographyDirectoriesList = new GeographyDirectoriesList();
                            _geographyDirectoriesList.Name = "_geographyDirectoriesList";
                            _geographyDirectoriesList.Back += _geographyDirectoriesList_Back;
                            _geographyDirectoriesList.MouseDoubleClick += _geographyDirectoriesList_MouseDoubleClick;
                            _geographyDirectoriesList.MouseMove += MenuUC_MouseMove;
                        }
                        _geographyDirectoriesList.Width = imgGlobe.ActualWidth;
                        _geographyDirectoriesList.Height = imgGlobe.ActualHeight;
                        RadTransitionControlGlobe.Content = _geographyDirectoriesList;
                        break;
                    //
                    default:
                        break;
                }
            }     
        }

        void _prizesListUC_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BackPrizessList();
        }

        void _prizesListUC_Back(object sender, RoutedEventArgs e)
        {
            BackPrizessList();
        }

        void BackPrizessList()
        {
            RadTransitionControlPrizes.Content = imgPrize;
        }

        private void CACIBList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BackCacib();
        }

        private void CACIBList_Back(object sender, RoutedEventArgs e)
        {
            BackCacib();
        }

        void MenuUC_MouseMove(object sender, MouseEventArgs e)
        {
            UserControl uc = sender as UserControl;
            SetMenuTittle(uc.Name);
        }

        void BackKennelsList()
        {
            RadTransitionControlKennelClubs.Content = imgkennelclub;
        }

        void _kennelsListUC_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BackKennelsList();
        }

        void _kennelsListUC_Back(object sender, RoutedEventArgs e)
        {
            BackKennelsList();
        }

        void _breedsListUC_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BackBreedsList();
        }

        private void _breedsListUC_Back(object sender, RoutedEventArgs e)
        {
            BackBreedsList();
        }

        void BackCacib()
        {
            RadTransitionControlCACIB.Content = imgCACIB;
        }

        void BackBreedsList()
        {
            RadTransitionControlreeds.Content = imgbreeds3;
        }

        void _geographyDirectoriesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BackGeographyDirectory();
        }

        void _geographyDirectoriesList_Back(object sender, RoutedEventArgs e)
        {
            BackGeographyDirectory();
        }

        void BackGeographyDirectory()
        {
            RadTransitionControlGlobe.Content = imgGlobe;
        }

        void BackDirectory()
        {
            RadTransitionControl1.Content = imgDirectories;
        }

        void dirs_Back(object sender, RoutedEventArgs e)
        {
            BackDirectory();
        }

        void dirs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BackDirectory();
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
           
            MyCommonDB.BreedDT();
            KinoDataSet ds=MyCommonDB.KinoDS();
            var dlg = new SelectBreedDlg
            {
                Title = "Breed select",
                DataContext=ds
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            //this.dialogResult.Text = dlg.DialogResult.HasValue ? dlg.DialogResult.ToString() : "<null>";
            //this.dialogMessageBoxResult.Text = dlg.MessageBoxResult.ToString();
        }

        private void SetMenuTittle(string elemname)
        {
            string txt = "";
                switch (elemname)
                {

                    case "brdrFCI":
                        txt = "FCI";
                        break;

                    case "brdrPeeson":
                        txt = "Persons";
                        break;

                    case "brdrGlobe":
                        txt = "Geography Directories";
                        break;
                    case "_geographyDirectoriesList":
                        txt = "Geography Directories";
                        break;

                    case "brdrDeirectories":
                        txt = "Other  Directories";
                        break;
                    case "dirs":
                        txt = "Other  Directories";
                        break;
                    
                    case "brdrBreeds":
                        txt = "Breeds";
                        break;
                    case "_breedsListUC":
                        txt = "Breeds";
                        break;

                    case "brdrKennelClub":
                        txt = "Kennel Clubs, Dogs";
                        break;
                    case "_kennelsListUC":
                        txt = "Kennel Clubs, Dogs";
                        break;


                    case "brdPrize":
                        txt = "Exhibition Statuses Templates";
                        break;
                    case "_prizesListUC":
                        txt = "Exhibition Statuses Templates";
                        break;

                    case "brdrCACIB":
                        txt = "CACIB";
                        break;
                    //
                    default:
                        break;
                }

            txtCurrentMenu.Text = txt;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Border brdr = sender as Border;
            string txt = "";
            if (brdr != null)
            {
                SetMenuTittle (brdr.Name.ToString());
            }
        }
    }
}
