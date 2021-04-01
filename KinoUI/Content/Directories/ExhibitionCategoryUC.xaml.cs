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
    public partial class ExhibitionCategoryUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource exhib_categoryViewSource;
        public ExhibitionCategoryUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    exhib_categoryViewSource = ((CollectionViewSource)(this.FindResource("exhib_categoryViewSource")));
                    exhib_categoryViewSource.Source = MyCommonDB.ExhibitionCategoriesDT();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in ExhibitionCategoryUC ctor");
            }
        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["exhib_category"] == System.DBNull.Value || drv.Row["exhib_category"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "exhib_category";
                validationResult.ErrorMessage = "Exhibition category is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            grdexhcategory.CommitEdit();
            MyCommonDB.SaveExhibitionCategories();
        }

        private void grdexhcategory_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
