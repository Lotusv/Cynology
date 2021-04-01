using System.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using KinoDataLibrary;

namespace KinoUI.Content
{
    /// <summary>
    /// Interaction logic for SelectBreedDlg.xaml
    /// </summary>
    public partial class SelectBreedDlg : ModernDialog
    {
        CollectionViewSource breedViewSource;
        CollectionViewSource hairtypeViewSource;
        CollectionViewSource colortypeViewSource;
        CollectionViewSource sizetypeViewSource;

        Button _okbtn;
        SolidColorBrush foundBackgroundBrush;
        SolidColorBrush notfoundBackgroundBrush;
        Int32 _selectedBreedID = 0;

        public SelectBreedDlg()
        {
            InitializeComponent();

            // define the dialog buttons
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.Buttons = new Button[] { this.OkButton, this.CancelButton };
                this.OkButton.Click += OkButton_Click;
                this.OkButton.IsEnabled = false;
                breedViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("breedViewSource")));

                hairtypeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hairtypeViewSource")));
                colortypeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("colortypeViewSource")));
                sizetypeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("sizetypeViewSource")));

                foundBackgroundBrush = ((SolidColorBrush)(this.FindResource("foundBackgroundBrush")));
                notfoundBackgroundBrush = ((SolidColorBrush)(this.FindResource("notfoundBackgroundBrush")));
            }

        }

        public Int32 SelectedBreedID()
        {
            return _selectedBreedID;
        }

        void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt;
                dt=grdBreeds.ItemsSource  as DataTable;
                DataRow dr = dt.Rows[0];
                _selectedBreedID = (Int32)dr["breed_id"];
                //this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in OkButton_Click");
            }
        }

        private void cmbBreedName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(_isLoaded)
                {
                    grdHairType.SelectedItems.Clear();
                    grdColorType.SelectedItems.Clear();
                    grdSizeType.SelectedItems.Clear();

                    if (cmbBreedName.SelectedIndex != -1)
                    {
                        DataRowView drv = cmbBreedName.SelectedItem as DataRowView;

                        Int32 _brnm = (Int32)drv.Row["breed_nameID"];
                        FilterHairTypeByBreedName(_brnm);
                        FilterColorTypeByBreedName(_brnm);
                        FilterSizeTypeByBreedName(_brnm);
                        FilterBreeds();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in cmbBreedName_SelectionChanged");
            }  
        }

        void FilterHairTypeByBreedName(Int32 breednameid=0)
        {
            try
            {
                BindingListCollectionView view = CollectionViewSource.GetDefaultView(hairtypeViewSource.Source) as BindingListCollectionView;
                if (view != null)
                {
                    if (breednameid != 0)
                    {
                        string fltr = MyCommonDB.GetBreedsByBreedName(breednameid);
                        if (fltr != "")
                            view.CustomFilter = "breed_id IN (" + fltr + ")";
                        else
                            view.CustomFilter = "breed_id = -99";
                    }
                    else
                    {
                        view.CustomFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message + " in FilterHairTypeByBreedName");
            }
        }

        void FilterColorTypeByBreedName(Int32 breednameid = 0)
        {
            try
            {


                BindingListCollectionView view = CollectionViewSource.GetDefaultView(colortypeViewSource.Source) as BindingListCollectionView;
                if (view != null)
                {
                    if (breednameid != 0)
                    {
                        string fltr = MyCommonDB.GetBreedsByBreedName(breednameid);
                        if (fltr != "")
                            view.CustomFilter = "breed_id IN (" + fltr + ")";
                        else
                            view.CustomFilter = "breed_id = -99";
                    }
                    else
                    {
                        view.CustomFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in FilterColorTypeByBreedName");
            }
        }

        void FilterSizeTypeByBreedName(Int32 breednameid = 0)
        {
            try
            {


                BindingListCollectionView view = CollectionViewSource.GetDefaultView(sizetypeViewSource.Source) as BindingListCollectionView;
                if (view != null)
                {
                    if (breednameid != 0)
                    {
                        string fltr = MyCommonDB.GetBreedsByBreedName(breednameid);
                        if (fltr != "")
                            view.CustomFilter = "breed_id IN (" + fltr + ")";
                        else
                            view.CustomFilter = "breed_id = -99";
                    }
                    else
                    {
                        view.CustomFilter = "";
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in FilterSizeTypeByBreedName");
            }
        }

        private void factorType_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (cmbBreedName.SelectedIndex != -1)   FilterBreeds();            

        }


        public DataTable GetDataTable()
        {
            string where = "";


            try
            {
                if (cmbBreedName.SelectedIndex != -1)
                {
                    DataRowView drv = cmbBreedName.SelectedItem as DataRowView;

                    Int32 _brnm = (Int32)drv.Row["breed_nameID"];
                    where = "WHERE     (dbo.tbl_breed_name.breed_nameID = " + _brnm + ")";
                }
                else return null;

                if (where != "")
                {
                    if (grdSizeType.SelectedItems != null && grdSizeType.SelectedItems.Count > 0)
                    {
                        DataRowView drv = grdSizeType.SelectedItem as DataRowView;

                        where += " and (dbo.tbl_breed_sizetype.size_type_id = " + (Int32)drv.Row["size_type_id"] + ")";
                    }

                    if (grdColorType.SelectedItem != null)
                    {
                        DataRowView drv = grdColorType.SelectedItem as DataRowView;
                        where += " and (dbo.tbl_breed_colortype.color_type_id = " + (Int32)drv.Row["color_type_id"] + ")";
                    }
                    //HairTypeList
                    if (grdHairType.SelectedItem != null)
                    {
                        DataRowView drv = grdHairType.SelectedItem as DataRowView;
                        where += " and (dbo.tbl_breed_hairtype.hair_type_id  = " + (Int32)drv.Row["hair_type_id"] + ")";
                    }
                }

                string query = "SELECT     breed_id, breed_name FROM (SELECT dbo.tbl_breed_name.breed_nameID, dbo.tbl_breed_name.breed_name, dbo.tbl_breed.breed_id, dbo.tbl_breed_colortype.color_type_id, dbo.tbl_breed_hairtype.hair_type_id, " +
                         " dbo.tbl_breed_sizetype.size_type_id, dbo.tbl_color_type.color_type, dbo.tbl_hair_type.hair_type, dbo.tbl_size_type.size_type " +
    " FROM dbo.tbl_breed_sizetype INNER JOIN dbo.tbl_size_type ON dbo.tbl_breed_sizetype.size_type_id = dbo.tbl_size_type.size_type_id RIGHT OUTER JOIN " +
                          " dbo.tbl_hair_type INNER JOIN dbo.tbl_breed_hairtype ON dbo.tbl_hair_type.hair_type_id = dbo.tbl_breed_hairtype.hair_type_id RIGHT OUTER JOIN " +
                          " dbo.tbl_breed INNER JOIN dbo.tbl_breed_name ON dbo.tbl_breed.breed_nameID = dbo.tbl_breed_name.breed_nameID ON dbo.tbl_breed_hairtype.breed_id = dbo.tbl_breed.breed_id ON  " +
                          " dbo.tbl_breed_sizetype.breed_id = dbo.tbl_breed.breed_id LEFT OUTER JOIN dbo.tbl_color_type INNER JOIN " +
                          " dbo.tbl_breed_colortype ON dbo.tbl_color_type.color_type_id = dbo.tbl_breed_colortype.color_type_id ON  " +
                          " dbo.tbl_breed.breed_id = dbo.tbl_breed_colortype.breed_id " + where + ") AS t1 GROUP BY breed_name, breed_id";


                String ConnString = MyCommonDB.ConnString();
                SqlConnection conn = new SqlConnection(ConnString);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, conn);

                DataTable myDataTable = new DataTable();

                conn.Open();
                try
                {
                    adapter.Fill(myDataTable);
                }
                finally
                {
                    conn.Close();
                }

                return myDataTable;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in GetDataTable");
                return null;
            }


            
        }

        void FilterBreeds()
        {
            try
            {

                if (!_isLoaded) return;

                DataTable dt=GetDataTable();
                if (dt == null) return;
                grdBreeds.ItemsSource =dt ;
                if (dt.Rows.Count == 1)
                {
                    grdBreeds.Background = foundBackgroundBrush;
                    this.OkButton.Background = foundBackgroundBrush;
                    this.OkButton.IsEnabled = true;
                }
                else
                {
                    this.OkButton.IsEnabled = false;
                    this.OkButton.Background = notfoundBackgroundBrush;
                    grdBreeds.Background = notfoundBackgroundBrush;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in FilterBreeds");
            }
        }

        private void ModernDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FilterHairTypeByBreedName(0);
            FilterColorTypeByBreedName(0);
            FilterSizeTypeByBreedName(0);
        }

        bool _isLoaded = false;

        private void ModernDialog_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
            cmbBreedName.SelectedIndex = -1;

            BindingListCollectionView htview = CollectionViewSource.GetDefaultView(hairtypeViewSource.Source) as BindingListCollectionView;
            if (htview != null) htview.CustomFilter = "breed_id = -99";

            BindingListCollectionView ctview = CollectionViewSource.GetDefaultView(colortypeViewSource.Source) as BindingListCollectionView;
            if (ctview != null) ctview.CustomFilter = "breed_id = -99";

            BindingListCollectionView stview = CollectionViewSource.GetDefaultView(sizetypeViewSource.Source) as BindingListCollectionView;
            if (stview != null) stview.CustomFilter = "breed_id = -99";


        }
    }
}
