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

namespace KinoUI.Content
{
    /// <summary>
    /// Interaction logic for AddHairTypeDlg.xaml
    /// </summary>
    public partial class AddKennelClubFactorsDlg : Window
    {
        LinearGradientBrush _notSelectedBrush;
        LinearGradientBrush _SelectedBrush;
        CollectionViewSource PersonsViewSource;
        CollectionViewSource Club_PersonsViewSource;
        CollectionViewSource breed_nameViewSource;
        CollectionViewSource Club_breed_nameViewSource;

        KinoDataSet _kinoDataSet;
        Int32 _ClubID;

        public AddKennelClubFactorsDlg(KinoDataSet ds, Int32 ClubID)
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    _kinoDataSet = ds;
                    _ClubID = ClubID;

                    PersonsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("PersonsViewSource")));
                    Club_PersonsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Club_PersonsViewSource")));

                    breed_nameViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("breed_nameViewSource")));
                    Club_breed_nameViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("Club_breed_nameViewSource")));


                    PersonsViewSource.Source = _kinoDataSet.tbl_person;
                    _kinoDataSet.tbl_kennel_club_Person.DefaultView.RowFilter = "kennel_club_id=" + _ClubID;
                    Club_PersonsViewSource.Source = _kinoDataSet.tbl_kennel_club_Person.DefaultView;

                    breed_nameViewSource.Source = _kinoDataSet.tbl_breed_name;
                    _kinoDataSet.tbl_kennel_breedname.DefaultView.RowFilter = "kennel_club_id=" + _ClubID;
                    Club_breed_nameViewSource.Source = _kinoDataSet.tbl_kennel_breedname.DefaultView;


                    _notSelectedBrush = ((LinearGradientBrush)(this.FindResource("notSelectedBrush")));
                    _SelectedBrush = ((LinearGradientBrush)(this.FindResource("SelectedBrush")));
                    btnBreeds.Background = _notSelectedBrush;
                    btnPersons.Background = _notSelectedBrush;

                    mainGrid.Children.Remove(pnlBreeds);
                    mainGrid.Children.Remove(pnlPersons);

                }
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.Message + " in AddKennelClubFactorsDlg ctor");
            }
            
        }

        private void btnBreeds_Click(object sender, RoutedEventArgs e)
        {
            btnBreeds.Background = _SelectedBrush;
            btnPersons.Background = _notSelectedBrush;

            radTransitionControl1.Content = pnlBreeds;
        }


        private void btnPersons_Click(object sender, RoutedEventArgs e)
        {

            btnBreeds.Background = _notSelectedBrush;
            btnPersons.Background = _SelectedBrush;

            radTransitionControl1.Content = pnlPersons;
        }


        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _kinoDataSet.tbl_kennel_club_Person.RejectChanges();
            _kinoDataSet.tbl_kennel_breedname.RejectChanges();
            this.Close();
        }

        private void imgApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
        }

        private void AddPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstPersons.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Person from left list");
                    return;
                }
                DataRowView drv = lstPersons.SelectedItem as DataRowView;
                KinoDataSet.tbl_personRow dr = drv.Row as KinoDataSet.tbl_personRow;

                if (!IsRowExist(_kinoDataSet.tbl_kennel_club_Person, dr.person_id, _ClubID, "person_id"))
                {
                    KinoDataSet.tbl_kennel_club_PersonRow newdr = _kinoDataSet.tbl_kennel_club_Person.NewRow() as KinoDataSet.tbl_kennel_club_PersonRow;
                    newdr.kennel_club_id = _ClubID;
                    newdr.person_id = dr.person_id;
                    newdr.isBreeder = 0;

                    _kinoDataSet.tbl_kennel_club_Person.Rows.Add(newdr);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in AddPersonBtn_Click");
            }


        }

        private void RemovePersonBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstClubPersons.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Person from right list");
                    return;
                }
                DataRowView drv = lstClubPersons.SelectedItem as DataRowView;
                DataRow dr = drv.Row;
                dr.Delete();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in RemovePersonBtn_Click");
            }


        }

        private void AddBreedNameBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstHairTypes.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Breed from left list");
                    return;
                }
                DataRowView drv = lstHairTypes.SelectedItem as DataRowView;
                KinoDataSet.tbl_breed_nameRow dr = drv.Row as KinoDataSet.tbl_breed_nameRow;

                if (!IsRowExist(_kinoDataSet.tbl_kennel_breedname, dr.breed_nameID, _ClubID, "breed_nameID"))
                {
                    KinoDataSet.tbl_kennel_breednameRow newdr = _kinoDataSet.tbl_kennel_breedname.NewRow() as KinoDataSet.tbl_kennel_breednameRow;
                    newdr.kennel_club_id = _ClubID;
                    newdr.breed_nameID = dr.breed_nameID;
                    _kinoDataSet.tbl_kennel_breedname.Rows.Add(newdr);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in AddBreedNameBtn_Click");
            }


        }

        private void RemoveBreedNameBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstKennelBreeds.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Breed from right list");
                    return;
                }
                DataRowView drv = lstKennelBreeds.SelectedItem as DataRowView;
                DataRow dr = drv.Row;
                dr.Delete();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in RemoveBreedNameBtn_Click");
            }


        }

        bool IsRowExist(DataTable dt, Int32 _id, Int32 club_id, string fieldname)
        {
            // TableListUC lstTables = (fluidControl.LargeContent as Border).ChildrenOfType<TableListUC>().Where(b => b.Name == "lstTables").FirstOrDefault(); 
            bool res = false;
            try
            {
                var query = dt.AsEnumerable().Where(r => r.Field<Int32>(fieldname) == _id & r.Field<Int32>("kennel_club_id") == club_id);
                foreach (var item in query)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in IsRowExist");
            }
            return res;
        }
    }
}

