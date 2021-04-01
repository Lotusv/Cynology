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
    public partial class TrialTypesUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource tbl_trial_typeViewSource;
        public TrialTypesUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    tbl_trial_typeViewSource = ((CollectionViewSource)(this.FindResource("tbl_trial_typeViewSource")));
                    tbl_trial_typeViewSource.Source = MyCommonDB.TrialTypeDT();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in TrialTypesUC ctor");
            }
        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["trial_type_name"] == System.DBNull.Value || drv.Row["trial_type_name"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "trial_type_name";
                validationResult.ErrorMessage = "Trial name is required, please fill unigue value or press Escape for reject.";
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
            MyCommonDB.SaveTrialType();
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
