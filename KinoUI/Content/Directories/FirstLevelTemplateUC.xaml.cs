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
    /// Interaction logic for FirstLevelTemplateUC.xaml
    /// </summary>
    public partial class FirstLevelTemplateUC : UserControl
    {
        
        KinoDataSet kinoDataSet;

        CollectionViewSource firstLevelViewSource;
        CollectionViewSource exhibCategoryViewSource;
        CollectionViewSource ageGroupViewSource;
        CollectionViewSource sexViewSource;
        CollectionViewSource exhStatusViewSource;

        public FirstLevelTemplateUC()
        {
            InitializeComponent();
            firstLevelViewSource = ((CollectionViewSource)(this.FindResource("firstLevelViewSource")));
            exhibCategoryViewSource = ((CollectionViewSource)(this.FindResource("exhibCategoryViewSource")));
            ageGroupViewSource = ((CollectionViewSource)(this.FindResource("ageGroupViewSource")));
            sexViewSource = ((CollectionViewSource)(this.FindResource("sexViewSource")));
            exhStatusViewSource = ((CollectionViewSource)(this.FindResource("exhStatusViewSource")));

            firstLevelViewSource.Source = MyCommonDB.FirstLevelTemplateDT();
            exhibCategoryViewSource.Source = MyCommonDB.ExhibitionCategoriesDT();
            ageGroupViewSource.Source = MyCommonDB.AgeGroupDT();
            sexViewSource.Source = MyCommonDB.SexDT();
            exhStatusViewSource.Source = MyCommonDB.ExhibitionStatusesDT();

        }
        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            grdFLevelTemplate.CommitEdit();
            MyCommonDB.SaveFirstLevelTemplate();
        }

        private void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;

            if (drv != null && (drv.Row["agegroup_id"] == System.DBNull.Value || drv.Row["agegroup_id"]==""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "agegroup_id";
                validationResult.ErrorMessage = "Age group is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

            if (drv != null && (drv.Row["sex_type_id"] == System.DBNull.Value || drv.Row["sex_type_id"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "sex_type_id";
                validationResult.ErrorMessage = "Sex is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

            if (drv != null && (drv.Row["exh_statusid"] == System.DBNull.Value || drv.Row["exh_statusid"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "exh_statusid";
                validationResult.ErrorMessage = "Exhibition status is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

            if (drv != null && (drv.Row["exhib_categoryID"] == System.DBNull.Value || drv.Row["exhib_categoryID"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "exhib_categoryID";
                validationResult.ErrorMessage = "Exhibition category is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

           
        }

        private void grdFLevelTemplate_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}