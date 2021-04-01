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

namespace KinoUI.Content
{
    /// <summary>
    /// Interaction logic for ParentUC.xaml
    /// </summary>
    public partial class ParentUC : UserControl
    {
        KinoDataSet kinoDataSet;
        CollectionViewSource colorSource;
        CollectionViewSource personViewSource;
        CollectionViewSource sexViewSource;
        CollectionViewSource countryViewSource;
        CollectionViewSource cityViewSource;
        CollectionViewSource nationalityViewSource;
        CollectionViewSource pedigreeViewSource;
        CollectionViewSource dogDocumentViewSource;

        public ParentUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    pedigreeViewSource = ((CollectionViewSource)(this.FindResource("pedigreeViewSource")));
                    MyCommonDB.PedigreeDT();
                    kinoDataSet = MyCommonDB.KinoDS();
                    this.DataContext = kinoDataSet;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in ParentUC ctor");
            }
        }


        public void SetFilter(int _sir, Int32 _breed_nameID, Int32 _pedigreeid)
        {

            BindingListCollectionView view = CollectionViewSource.GetDefaultView(pedigreeViewSource.Source) as BindingListCollectionView;
            if (view != null)
            {

                string fltr = MyCommonDB.GetBreedsByBreedName(_breed_nameID);
                if (fltr != "")
                    view.CustomFilter = "sex_id=" + _sir + " and breed_id IN (" + fltr + ") and pedigree_id <> " + _pedigreeid;
                else
                {
                    view.CustomFilter = "";
                }
            }
        }

        private void grdPedigree_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
