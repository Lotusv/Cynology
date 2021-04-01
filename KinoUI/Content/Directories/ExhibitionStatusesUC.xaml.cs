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
    public partial class ExhibitionStatusesUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource exh_statusViewSource;
        public ExhibitionStatusesUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    exh_statusViewSource = ((CollectionViewSource)(this.FindResource("exh_statusViewSource")));
                    exh_statusViewSource.Source = MyCommonDB.ExhibitionStatusesDT();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in ExhibitionStatusesUC ctor");
            }
        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["exh_status"] == System.DBNull.Value || drv.Row["exh_status"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "exh_status";
                validationResult.ErrorMessage = "Exhibition status is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

            if (drv != null && (drv.Row["IsWinner"] == System.DBNull.Value || drv.Row["IsWinner"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "IsWinner";
                validationResult.ErrorMessage = "IsWinner is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
            //IsWinner
        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            grdstatuses.CommitEdit();
            MyCommonDB.SaveExhibitionStatuses();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            grdstatuses.CommitEdit();
            MyCommonDB.SaveExhibitionStatuses();
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
