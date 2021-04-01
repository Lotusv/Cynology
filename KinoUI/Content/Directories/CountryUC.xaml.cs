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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KinoDataLibrary;
//using KinoDataModel;
using Telerik.Windows;
using Telerik.Windows.Controls;
using System.Data;

namespace KinoUI.Content.Directories
{
    /// <summary>
    /// Interaction logic for CountryUC.xaml
    /// </summary>
    public partial class CountryUC : UserControl
    {
        //KinologyModel mod=null;
        KinoDataSet kinoDataSet;
        CollectionViewSource countryViewSource;
        public CountryUC()
        {

            InitializeComponent();
            //kinoDataSet = ((KinoDataSet)(this.FindResource("kinoDataSet")));
            countryViewSource = ((CollectionViewSource)(this.FindResource("countryViewSource")));
            //kinoDataSet.Merge(MyCommon.KinoDS());
            countryViewSource.Source = MyCommonDB.CountryDT();
            //if (mod == null)
            //    mod = mod = MyCommon.GetModel(); //new KinologyModel(@"data source=62.212.231.217\\RISK_GEEE_server,9913;initial catalog=Kino;persist security info=True;user id=kino;password=kino");
            //this.DataContext = mod;
        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["country_name"] == System.DBNull.Value || drv.Row["country_name"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "country_name";
                validationResult.ErrorMessage = "Country name is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }


        }

        public void mnuDelete_Click(object sender, RadRoutedEventArgs e)
        {
        }

        public void mnuAdd_Click(object sender, RadRoutedEventArgs e)
        {
           //countryViewSource.View.
        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            grdCountry.CommitEdit();
            MyCommonDB.SaveCountry();
            MyCommonDB.SaveCountryCodes();
            
        }

        private void grdCountry_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void radgrdCountryCodes_Loaded(object sender, RoutedEventArgs e)
        {
            KinoDataSet.tbl_countryRow tr;
            try
            {
                RadGridView dataControl = (RadGridView)sender;
                Telerik.Windows.Controls.GridView.GridViewRow gvr = dataControl.ParentRow;
                DataRowView drv = (DataRowView)gvr.Item;
                if (drv != null)
                {
                    tr = drv.Row as KinoDataSet.tbl_countryRow;
                    if (tr != null)
                    {
                        LoadCountryCodesForDetails(tr, dataControl);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in radgrdCountryCodes_Loaded");
            }
        }

        void LoadCountryCodesForDetails(KinoDataSet.tbl_countryRow tr, RadGridView dataControl)
        {
            try
            {
                DataRow[] tsrvr = tr.Gettbl_country_codeRows();
                dataControl.ItemsSource = tsrvr;
                dataControl.Tag = tr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in LoadCountryCodesForDetails");
            }
        }
    }
}
