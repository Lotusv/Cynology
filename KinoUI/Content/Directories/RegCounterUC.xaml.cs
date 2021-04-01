using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class RegCounterUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource registration_counterViewSource;
        public RegCounterUC()
        {

            InitializeComponent();
            registration_counterViewSource = ((CollectionViewSource)(this.FindResource("registration_counterViewSource")));
            registration_counterViewSource.Source = MyCommonDB.Registration_counterDT();

        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["description"] == System.DBNull.Value || drv.Row["description"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "description";
                validationResult.ErrorMessage = "Description is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            grdregistration_counter.CommitEdit();
            MyCommonDB.SaveRegistration_counter();
        }

        private void grdregistration_counter_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
