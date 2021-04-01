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

namespace KinoUI.Content.Directories
{
    /// <summary>
    /// Interaction logic for CityUC.xaml
    /// </summary>
    public partial class CityUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;

        CollectionViewSource countryViewSource;
        CollectionViewSource cityViewSource;
        public CityUC()
        {
            InitializeComponent();
            countryViewSource = ((CollectionViewSource)(this.FindResource("countryViewSource")));
            cityViewSource = ((CollectionViewSource)(this.FindResource("cityViewSource")));
            countryViewSource.Source = MyCommonDB.CountryDT();
            cityViewSource.Source = MyCommonDB.CityDT();

        }
        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            grdCity.CommitEdit();
            MyCommonDB.SaveCity();
        }

        private void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["city"] == System.DBNull.Value || drv.Row["city"]==""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "City";
                validationResult.ErrorMessage = "City is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
        }

        private void grdCity_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
