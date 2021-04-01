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
    public partial class EctoparasitTypeUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource tbl_ectoparasit_typeViewSource;
        public EctoparasitTypeUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    tbl_ectoparasit_typeViewSource = ((CollectionViewSource)(this.FindResource("tbl_ectoparasit_typeViewSource")));
                    tbl_ectoparasit_typeViewSource.Source = MyCommonDB.EctoparasitTypeDT();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in EctoparasitTypeUC ctor");
            }
        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["ectoparasit_type_name"] == System.DBNull.Value || drv.Row["ectoparasit_type_name"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "ectoparasit_type_name";
                validationResult.ErrorMessage = "Ectoparasit type name is required, please fill unigue value or press Escape for reject.";
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
            MyCommonDB.SaveEctoparasitTypes();
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
