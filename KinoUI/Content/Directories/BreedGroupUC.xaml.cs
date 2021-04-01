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
    /// Interaction logic for AgeGroupUC.xaml
    /// </summary>
    public partial class BreedGroupUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource breed_groupViewSource;
        public BreedGroupUC()
        {

            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                breed_groupViewSource = ((CollectionViewSource)(this.FindResource("breed_groupViewSource")));
                breed_groupViewSource.Source = MyCommonDB.BreedGroupDT();
            }


        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["breed_group_name"] == System.DBNull.Value || drv.Row["breed_group_name"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "breed_group_name";
                validationResult.ErrorMessage = "Breed Group name is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                grdbreedgroup.CommitEdit();
                MyCommonDB.SaveBreedGroup();
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message + " in mnuSave_Click");
            }
        }

        private void grdbreedgroup_DataError(object sender, Telerik.Windows.Controls.GridView.DataErrorEventArgs e)
        {
            //e.Source
        }

        private void grdbreedgroup_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
