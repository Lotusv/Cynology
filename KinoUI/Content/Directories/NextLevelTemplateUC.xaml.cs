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
    /// Interaction logic for NextLevelTemplateUC.xaml
    /// </summary>
    public partial class NextLevelTemplateUC : UserControl
    {
        KinoDataSet kinoDataSet;

        CollectionViewSource nextLevelViewSource;
        CollectionViewSource exhibCategoryViewSource;
        CollectionViewSource nextexhStatusViewSource;
        CollectionViewSource exhStatusViewSource;

        public NextLevelTemplateUC()
        {
            InitializeComponent();
            nextLevelViewSource = ((CollectionViewSource)(this.FindResource("nextLevelViewSource")));
            exhibCategoryViewSource = ((CollectionViewSource)(this.FindResource("exhibCategoryViewSource")));
            nextexhStatusViewSource = ((CollectionViewSource)(this.FindResource("nextexhStatusViewSource")));
            
            exhStatusViewSource = ((CollectionViewSource)(this.FindResource("exhStatusViewSource")));

            nextLevelViewSource.Source = MyCommonDB.NextLevelTemplateDT();
            exhibCategoryViewSource.Source = MyCommonDB.ExhibitionCategoriesDT();
            nextexhStatusViewSource.Source = MyCommonDB.ExhibitionStatusesDT();
            
            exhStatusViewSource.Source = MyCommonDB.ExhibitionStatusesDT();

        }
        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            grdFLevelTemplate.CommitEdit();
            MyCommonDB.SaveNextLevelTemplate();
        }

        private void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;

            if (drv != null && (drv.Row["next_exh_statusid"] == System.DBNull.Value || drv.Row["next_exh_statusid"]==""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "next_exh_statusid";
                validationResult.ErrorMessage = "Next Status is required, please fill unigue value or press Escape for reject.";
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