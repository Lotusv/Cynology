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
    public partial class AddBreedFactorsTypeDlg : Window
    {
        LinearGradientBrush _notSelectedBrush;
        LinearGradientBrush _SelectedBrush;
        CollectionViewSource ColorTypesViewSource;
        CollectionViewSource BreedColorTypesViewSource;
        CollectionViewSource HairTypesViewSource;
        CollectionViewSource BreedHairTypesViewSource;
        CollectionViewSource SizeTypesViewSource;
        CollectionViewSource BreedSizeTypesViewSource;
        KinoDataSet _kinoDataSet;
        KinoDataSet.tbl_breedRow _breeddr;
        Int32 _breedID;

        public AddBreedFactorsTypeDlg(KinoDataSet ds, KinoDataSet.tbl_breedRow breeddr)//Int32 breedID)
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                _kinoDataSet = ds;
                _breeddr=breeddr;
                _breedID=_breeddr.breed_id;

                ColorTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ColorTypesViewSource")));
                BreedColorTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("BreedColorTypesViewSource")));

                HairTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("HairTypesViewSource")));
                BreedHairTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("BreedHairTypesViewSource")));

                SizeTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SizeTypesViewSource")));
                BreedSizeTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("BreedSizeTypesViewSource")));


                ColorTypesViewSource.Source=_kinoDataSet.tbl_color_type;
                _kinoDataSet.tbl_breed_colortype.DefaultView.RowFilter = "breed_id=" + _breedID;
                BreedColorTypesViewSource.Source=_kinoDataSet.tbl_breed_colortype.DefaultView;

                HairTypesViewSource.Source = _kinoDataSet.tbl_hair_type;
                _kinoDataSet.tbl_breed_hairtype.DefaultView.RowFilter = "breed_id=" + _breedID;
                BreedHairTypesViewSource.Source = _kinoDataSet.tbl_breed_hairtype.DefaultView;

                SizeTypesViewSource.Source = _kinoDataSet.tbl_size_type;
                _kinoDataSet.tbl_breed_sizetype.DefaultView.RowFilter = "breed_id=" + _breedID;
                BreedSizeTypesViewSource.Source = _kinoDataSet.tbl_breed_sizetype.DefaultView;


                _notSelectedBrush = ((LinearGradientBrush)(this.FindResource("notSelectedBrush")));
                _SelectedBrush = ((LinearGradientBrush)(this.FindResource("SelectedBrush")));
                btnHairType.Background = _notSelectedBrush;
                btnColorType.Background = _notSelectedBrush;
                btnSizeType.Background = _notSelectedBrush;
                mainGrid.Children.Remove(pnlSizeType);
                mainGrid.Children.Remove(pnlHairType);
                mainGrid.Children.Remove(pnlColorType);
 
            }
        }

        private void btnHairType_Click(object sender, RoutedEventArgs e)
        {
            btnHairType.Background = _SelectedBrush;
            btnColorType.Background = _notSelectedBrush;
            btnSizeType.Background = _notSelectedBrush;

            radTransitionControl1.Content = pnlHairType;
        }

        private void btnColorType_Click(object sender, RoutedEventArgs e)
        {

            btnHairType.Background = _notSelectedBrush;
            btnColorType.Background = _SelectedBrush;
            btnSizeType.Background = _notSelectedBrush;

            radTransitionControl1.Content = pnlColorType;
        }

        private void btnSizeType_Click(object sender, RoutedEventArgs e)
        {

            btnHairType.Background = _notSelectedBrush;
            btnColorType.Background = _notSelectedBrush;
            btnSizeType.Background = _SelectedBrush;

            radTransitionControl1.Content = pnlSizeType;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _kinoDataSet.tbl_breed_sizetype.RejectChanges();
            _kinoDataSet.tbl_breed_hairtype.RejectChanges();
            _kinoDataSet.tbl_breed_colortype.RejectChanges();
            this.Close();
        }

        private void imgApply_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (this.UniversalSplitType == UniversalSplitEnum.Split && SplitRow())
            //{
                this.DialogResult = true;
            //}
            
        }

        bool IsRowExist(DataTable dt, Int32 color_type_id, Int32 breed_id, string fieldname)
        {
            // TableListUC lstTables = (fluidControl.LargeContent as Border).ChildrenOfType<TableListUC>().Where(b => b.Name == "lstTables").FirstOrDefault(); 
            bool res=false;
            try
            {
                var query = dt.AsEnumerable().Where(r => r.Field<Int32>(fieldname) == color_type_id & r.Field<Int32>("breed_id") == breed_id);
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

        private void AddColorTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lstColorTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Select color type from left list");
                return;
            }
            DataRowView drv = lstColorTypes.SelectedItem as DataRowView;
            KinoDataSet.tbl_color_typeRow dr = drv.Row as KinoDataSet.tbl_color_typeRow;
            if (!IsRowExist(_kinoDataSet.tbl_breed_colortype, dr.color_type_id, _breedID, "color_type_id"))
            {
                KinoDataSet.tbl_breed_colortypeRow newdr = _kinoDataSet.tbl_breed_colortype.NewRow() as KinoDataSet.tbl_breed_colortypeRow;
                
                newdr.tbl_breedRow = _breeddr;
                newdr.breed_id = _breedID;

                newdr.color_type_id = dr.color_type_id;
                _kinoDataSet.tbl_breed_colortype.Rows.Add(newdr);
            }


        }

        private void RemoveColorTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lstBreedColorTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Select color type from right list");
                return;
            }
            DataRowView drv = lstBreedColorTypes.SelectedItem as DataRowView;
            DataRow dr = drv.Row;
            dr.Delete();

        }

        private void AddHairTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lstHairTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Select Hair type from left list");
                return;
            }
            DataRowView drv = lstHairTypes.SelectedItem as DataRowView;
            KinoDataSet.tbl_hair_typeRow dr = drv.Row as KinoDataSet.tbl_hair_typeRow;

            if (!IsRowExist(_kinoDataSet.tbl_breed_hairtype, dr.hair_type_id, _breedID, "hair_type_id"))
            {
                KinoDataSet.tbl_breed_hairtypeRow newdr = _kinoDataSet.tbl_breed_hairtype.NewRow() as KinoDataSet.tbl_breed_hairtypeRow;
                
                newdr.tbl_breedRow = _breeddr;
                newdr.breed_id = _breedID;
                newdr.hair_type_id = dr.hair_type_id;
                _kinoDataSet.tbl_breed_hairtype.Rows.Add(newdr);
            }


        }

        private void RemoveHairTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lstBreedHairTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Select Hair type from right list");
                return;
            }
            DataRowView drv = lstBreedHairTypes.SelectedItem as DataRowView;
            DataRow dr = drv.Row;
            dr.Delete();
        }

        private void AddSizeTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lstSizeTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Select Size type from left list");
                return;
            }
            DataRowView drv = lstSizeTypes.SelectedItem as DataRowView;
            KinoDataSet.tbl_size_typeRow dr = drv.Row as KinoDataSet.tbl_size_typeRow;

            if (!IsRowExist(_kinoDataSet.tbl_breed_sizetype, dr.size_type_id, _breedID, "size_type_id"))
            {
                KinoDataSet.tbl_breed_sizetypeRow newdr = _kinoDataSet.tbl_breed_sizetype.NewRow() as KinoDataSet.tbl_breed_sizetypeRow;
                
                newdr.tbl_breedRow = _breeddr;
                newdr.breed_id = _breedID;
                newdr.size_type_id = dr.size_type_id;
                _kinoDataSet.tbl_breed_sizetype.Rows.Add(newdr);
            }


        }

        private void RemoveSizeTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lstBreedSizeTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Select Size type from right list");
                return;
            }
            DataRowView drv = lstBreedSizeTypes.SelectedItem as DataRowView;
            DataRow dr = drv.Row;
            dr.Delete();
        }
    }
}
