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
using Telerik.Windows;

namespace KinoUI.Content
{
    /// <summary>
    /// Interaction logic for PersonUC.xaml
    /// </summary>
    public partial class PersonUC : UserControl
    {
        KinoDataSet kinoDataSet;
        CollectionViewSource personViewSource;
        CollectionViewSource sexViewSource;
        CollectionViewSource countryViewSource;
        CollectionViewSource cityViewSource;
        CollectionViewSource nationalityViewSource;

        public PersonUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //kinoDataSet = ((KinoDataSet)(this.FindResource("kinoDataSet")));
                    personViewSource = ((CollectionViewSource)(this.FindResource("personViewSource")));
                    sexViewSource = ((CollectionViewSource)(this.FindResource("sexViewSource")));
                    countryViewSource = ((CollectionViewSource)(this.FindResource("countryViewSource")));

                    cityViewSource = ((CollectionViewSource)(this.FindResource("cityViewSource")));
                    nationalityViewSource = ((CollectionViewSource)(this.FindResource("nationalityViewSource")));


                    MyCommonDB.PersonDT();
                    kinoDataSet = MyCommonDB.KinoDS();

                    sexViewSource.Source = MyCommonDB.SexDT();
                    countryViewSource.Source = MyCommonDB.CountryDT();
                    cityViewSource.Source = MyCommonDB.CityDT();
                    nationalityViewSource.Source = MyCommonDB.NationalityDT();



                    this.DataContext = kinoDataSet;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in PersonUC ctor");
            }
        }
        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["name"] == System.DBNull.Value || drv.Row["name"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "name";
                validationResult.ErrorMessage = "Name is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

            if (drv != null && (drv.Row["surname"] == System.DBNull.Value || drv.Row["surname"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "surname";
                validationResult.ErrorMessage = "Surname is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }

           
            if (drv != null && (drv.Row["passport_no"] == System.DBNull.Value || drv.Row["passport_no"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "passport_no";
                validationResult.ErrorMessage = "Passport number is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
        }

        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            grdPerson.CommitEdit();
            MyCommonDB.SavePerson();
        }

        private void grdPerson_DataError(object sender, Telerik.Windows.Controls.GridView.DataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            Telerik.Windows.Controls.GridView.GridViewRow gvr = e.Row;
            //DataRowView drv = gvr.DataContext as DataRowView;
            //if (drv != null)
            //{
            //    drv.Row.RejectChanges();
            //}
            e.Handled = true;
            e.KeepRowInEditMode=true;
        }

        private void grdPerson_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
