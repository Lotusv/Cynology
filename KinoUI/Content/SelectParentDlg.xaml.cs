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
using KinoDataLibrary;
using System.Data;

namespace KinoUI.Content
{
    /// <summary>
    /// Interaction logic for SelectParentDlg.xaml
    /// </summary>
    public partial class SelectParentDlg : ModernDialog
    {
        KinoDataSet kinoDataSet;
        CollectionViewSource colorSource;
        CollectionViewSource personViewSource;
        CollectionViewSource sexViewSource;
        CollectionViewSource countryViewSource;
        CollectionViewSource cityViewSource;
        CollectionViewSource nationalityViewSource;
        CollectionViewSource pedigreeViewSource;
        CollectionViewSource dogDocumentViewSource;
        int _sir;
        Int32 _breed_nameID;
        Int32 _pedigreeid;
        Int32 _parentID = 0;
        KinoDataSet.tbl_pedigreeDataTable _dt;
        public SelectParentDlg()
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
            this.OkButton.Click += OkButton_Click;
            this.OkButton.IsEnabled = false;
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    pedigreeViewSource = ((CollectionViewSource)(this.FindResource("pedigreeViewSource")));
                    _dt = MyCommonDB.PedigreeDT().Copy() as KinoDataSet.tbl_pedigreeDataTable;
                    pedigreeViewSource.Source=_dt;
                    //kinoDataSet = MyCommonDB.KinoDS().Copy() as KinoDataSet;
                    //this.DataContext = kinoDataSet;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in SelectParentDlg ctor");
            }
        }

        void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //DataTable dt;
                //dt = grdPedigree.ItemsSource as DataTable;
                DataRowView drv = grdPedigree.SelectedItem as DataRowView;
                if (drv != null)
                {
                DataRow dr = drv.Row;
                _parentID = (Int32)dr["pedigree_id"];
                this.DialogResult = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in OkButton_Click");
            }
        }

        public Int32 ParentID()
        {
            return _parentID;
        }

        void filterDogs(bool clearflag=false)
        {
                BindingListCollectionView view = CollectionViewSource.GetDefaultView(pedigreeViewSource.Source) as BindingListCollectionView;
                if (view != null)
                {

                    string fltr = MyCommonDB.GetBreedsByBreedName(_breed_nameID);
                    if (fltr != "")
                        view.CustomFilter = "sex_id=" + _sir + " and breed_id IN (" + fltr + ") and pedigree_id <> " + _pedigreeid;
                    else
                    {
                        view.CustomFilter = "";
                    }
                    if (clearflag) view.CustomFilter = "";
                }
        }

        public SelectParentDlg(int sir, Int32 breed_nameID, Int32 pedigreeid)
            : this()
        {
            _sir = sir;
            _breed_nameID = breed_nameID;
            _pedigreeid = pedigreeid;
            //this.parentUC.SetFilter(_sir, _breed_nameID, _pedigreeid);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            filterDogs();
        }

        private void ModernDialog_Loaded(object sender, RoutedEventArgs e)
        {
            filterDogs();
        }

        private void ModernDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //filterDogs(true);
        }

        private void grdPedigree_SelectionChanging(object sender, Telerik.Windows.Controls.SelectionChangingEventArgs e)
        {
            this.OkButton.IsEnabled = true;
        }
    }
}
