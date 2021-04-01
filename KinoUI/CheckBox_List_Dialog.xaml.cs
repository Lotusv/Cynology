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
using System.Windows.Shapes;
using KinoDataLibrary;

namespace KinoUI
{
    /// <summary>
    /// Interaction logic for CheckBox_List_Dialog.xaml
    /// </summary>
    public partial class CheckBox_List_Dialog : Window
    {
        public List<int> selItemsList = new List<int>() ;
        public CheckBox_List_Dialog()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (int itemind in selItemsList)
            {
                lb_Services.SelectedItems.Add(lb_Services.Items[itemind]);
            }
        }

        private void btnAddColor_Click(object sender, RoutedEventArgs e)
        {
            String newColor = txtNewColor.Text;
            KinoDataSet.tbl_colorDataTable dt = this.DataContext as KinoDataSet.tbl_colorDataTable;
            if (dt == null) return;
            var query = dt.AsEnumerable().Where(r => r.color.ToLower() == newColor.ToLower());
            foreach (var item in query)
            {
                MessageBox.Show("There is same color in list");
                return;
            }
            KinoDataSet.tbl_colorRow dr = (KinoDataSet.tbl_colorRow)dt.NewRow();
            dr.color = newColor;
            dt.Rows.Add(dr);
            if (!MyCommonDB.SaveColor()) dt.RejectChanges();
        }
    }
}
