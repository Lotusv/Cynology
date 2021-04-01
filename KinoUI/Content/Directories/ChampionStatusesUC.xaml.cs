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
    public partial class ChampionStatusesUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource tbl_championStatusesViewSource;
        public ChampionStatusesUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    tbl_championStatusesViewSource = ((CollectionViewSource)(this.FindResource("tbl_championStatusesViewSource")));
                    tbl_championStatusesViewSource.Source = MyCommonDB.ChampionStatusesDT();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in ChampionStatusesUC ctor");
            }
        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["championStatusName"] == System.DBNull.Value || drv.Row["championStatusName"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "championStatusName";
                validationResult.ErrorMessage = "Champion status is required, please fill unigue value or press Escape for reject.";
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
            MyCommonDB.SaveChampionStatuses();
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
