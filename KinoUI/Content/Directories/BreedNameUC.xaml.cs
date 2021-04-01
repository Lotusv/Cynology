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
    /// Interaction logic for BreedNameUC.xaml
    /// </summary>
    public partial class BreedNameUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;

        CollectionViewSource breed_groupViewSource;
        CollectionViewSource breed_nameViewSource;
        public BreedNameUC()
        {
            
            try
            {
                InitializeComponent();
                breed_groupViewSource = ((CollectionViewSource)(this.FindResource("breed_groupViewSource")));
                breed_nameViewSource = ((CollectionViewSource)(this.FindResource("breed_nameViewSource")));
                breed_groupViewSource.Source = MyCommonDB.BreedGroupDT();
                breed_nameViewSource.Source = MyCommonDB.BreedNameDT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in BreedNameUC ctor");
            }


        }
        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["breed_name"] == System.DBNull.Value || drv.Row["breed_name"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "breed_name";
                validationResult.ErrorMessage = "Breed name is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

            if (drv != null && (drv.Row["group_id"] == System.DBNull.Value || drv.Row["group_id"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "group_id";
                validationResult.ErrorMessage = "Breed group is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
        }

        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            grdbreedname.CommitEdit();
            MyCommonDB.SaveBreedName();
        }

        private void grdbreedname_DataError(object sender, Telerik.Windows.Controls.GridView.DataErrorEventArgs e)
        {

        }

        private void grdbreedname_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}

    //public partial class BreedNameUC : UserControl
 