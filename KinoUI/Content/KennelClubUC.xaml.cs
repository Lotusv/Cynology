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
using Telerik.Windows.Controls;

namespace KinoUI.Content
{
    /// <summary>
    /// Interaction logic for KennelClubUC.xaml
    /// </summary>
    public partial class KennelClubUC : UserControl
    {
        KinoDataSet kinoDataSet;
        CollectionViewSource clubViewSource;
        CollectionViewSource countryViewSource;
        CollectionViewSource cityViewSource;
       
        CollectionViewSource kennel_breednameViewSource;
        CollectionViewSource kennel_club_PersonViewSource;

        public KennelClubUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //kinoDataSet = ((KinoDataSet)(this.FindResource("kinoDataSet")));
                    clubViewSource = ((CollectionViewSource)(this.FindResource("clubViewSource")));                    
                    countryViewSource = ((CollectionViewSource)(this.FindResource("countryViewSource")));
                    cityViewSource = ((CollectionViewSource)(this.FindResource("cityViewSource")));

                    kennel_breednameViewSource = ((CollectionViewSource)(this.FindResource("kennel_breednameViewSource")));
                    kennel_club_PersonViewSource = ((CollectionViewSource)(this.FindResource("kennel_club_PersonViewSource")));

                    MyCommonDB.KennelClubsDT();
                    kinoDataSet = MyCommonDB.KinoDS();
  
                    countryViewSource.Source = MyCommonDB.CountryDT();
                    cityViewSource.Source = MyCommonDB.CityDT();
                    this.DataContext = kinoDataSet;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in KennelClubUC ctor");
            }
        }
        public void gridview_RowValidating(object sender, GridViewRowValidatingEventArgs e)
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
        }

        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            grdClubs.CommitEdit();
            MyCommonDB.SaveKennelClubs();
            MyCommonDB.SaveClubBreed();
            MyCommonDB.SaveClubPerson();
        }

        private void btnAddBreedNameType_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            OpenClubsFactorsDialog(btn);
        }

        void OpenClubsFactorsDialog(Button btn)
        {
            try
            {//grdHairs
                StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                RadGridView grdPersons;
                RadGridView grdBreedNames;
                RadGridView sizesgrdv;
                KinoDataSet.tbl_kennel_clubRow tr = null;

                grdPersons = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdpersons");
                if (grdPersons != null)
                {
                    tr = grdPersons.Tag as KinoDataSet.tbl_kennel_clubRow;
                    if (tr != null)
                    {
                        AddKennelClubFactorsDlg frm = new AddKennelClubFactorsDlg(kinoDataSet, (Int32)tr.kennel_club_id);
                        bool? res = frm.ShowDialog();
                        if (res == true)
                        {
                            LoadPersonsForDetails(tr, grdPersons);
                        }
                    }
                }


                grdBreedNames = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdbreednames");
                if (grdBreedNames != null)
                {
                    LoadBreedNamesForDetails(tr, grdBreedNames);
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in OpenClubsFactorsDialog");
            }
        }


        private void grdbreednames_Loaded(object sender, RoutedEventArgs e)
        {
            KinoDataSet.tbl_kennel_clubRow tr; //View_TicketBalanceViewSource
            try
            {
                RadGridView dataControl = (RadGridView)sender;
                Telerik.Windows.Controls.GridView.GridViewRow gvr = dataControl.ParentRow;
                DataRowView drv = (DataRowView)gvr.Item;
                if (drv != null)
                {
                    tr = drv.Row as KinoDataSet.tbl_kennel_clubRow;// (HotelDataSet.TicketTypeRow)drv.Row;
                    if (tr != null)
                    {
                        LoadBreedNamesForDetails(tr, dataControl);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in grdbreednames_Loaded");
            }
        }

        void LoadBreedNamesForDetails(KinoDataSet.tbl_kennel_clubRow tr, RadGridView dataControl)
        {
            try
            {
                DataRow[] tsrvr = tr.GetChildRows("FK_tbl_kennel_breedname_tbl_kennel_club");
                dataControl.ItemsSource = tsrvr;
                dataControl.Tag = tr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in LoadBreedNamesForDetails");
            }
        }

        void LoadPersonsForDetails(KinoDataSet.tbl_kennel_clubRow tr, RadGridView dataControl)
        {
            try
            {
                DataRow[] tsrvr = tr.GetChildRows("FK_Tbl_KennelClub_Person_tbl_kennel_club");
                dataControl.ItemsSource = tsrvr;
                dataControl.Tag = tr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in LoadPersonsForDetails");
            }
        }

        private void btnPerson_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            OpenClubsFactorsDialog(btn);
        }

        private void grdpersons_Loaded(object sender, RoutedEventArgs e)
        {
            KinoDataSet.tbl_kennel_clubRow tr; //View_TicketBalanceViewSource
            try
            {
                RadGridView dataControl = (RadGridView)sender;
                Telerik.Windows.Controls.GridView.GridViewRow gvr = dataControl.ParentRow;
                DataRowView drv = (DataRowView)gvr.Item;
                if (drv != null)
                {
                    tr = drv.Row as KinoDataSet.tbl_kennel_clubRow;
                    if (tr != null)
                    {
                        LoadPersonsForDetails(tr, dataControl);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in grdpersons_Loaded");
            }
        }

        private void grdClubs_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
