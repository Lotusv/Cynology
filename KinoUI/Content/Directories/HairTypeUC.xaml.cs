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
    public partial class HairTypeUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource hair_typeViewSource;
        public HairTypeUC()
        {

            InitializeComponent();
            hair_typeViewSource = ((CollectionViewSource)(this.FindResource("hair_typeViewSource")));
            hair_typeViewSource.Source = MyCommonDB.HairTypeDT();

        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["hair_type"] == System.DBNull.Value || drv.Row["hair_type"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "hair_type";
                validationResult.ErrorMessage = "Hair type is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            grdhairtype.CommitEdit();
            MyCommonDB.SaveHairType();
            //MyCommon.Save();
        }

        private void grdhairtype_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}

    //public partial class HairTypeUC : UserControl
