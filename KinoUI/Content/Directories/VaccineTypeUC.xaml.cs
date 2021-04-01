using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KinoDataLibrary;
//using KinoDataModel;


namespace KinoUI.Content.Directories
{
    /// <summary>
    /// Interaction logic for AgeGroupUC.xaml
    /// </summary>
    public partial class VaccineTypeUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource tbl_vaccine_typeViewSource;
        public VaccineTypeUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    tbl_vaccine_typeViewSource = ((CollectionViewSource)(this.FindResource("tbl_vaccine_typeViewSource")));
                    tbl_vaccine_typeViewSource.Source = MyCommonDB.VaccineTypeDT();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in VaccineTypeUC ctor");
            }
        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["vaccine_type_name"] == System.DBNull.Value || drv.Row["vaccine_type_name"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "vaccine_type_name";
                validationResult.ErrorMessage = "vaccine type name is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

            if (drv != null && (drv.Row["showInPedigree"] == System.DBNull.Value || drv.Row["showInPedigree"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "showInPedigree";
                validationResult.ErrorMessage = "Show In Pedigree is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
            //showInPedigree
        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            grdstatuses.CommitEdit();
            MyCommonDB.SaveVaccineTypes();
        }
        private void grdstatuses_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
