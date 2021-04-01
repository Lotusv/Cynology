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
//using KinoDataModel;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace KinoUI.Content.Directories
{
    /// <summary>
    /// Interaction logic for AgeGroupUC.xaml
    /// </summary>
    public partial class ColorTypeUC : UserControl
    {
        //KinologyModel mod = null;
        KinoDataSet kinoDataSet;
        CollectionViewSource color_typeViewSource;
        CollectionViewSource colortypecolorViewSource;
        public ColorTypeUC()
        {

            InitializeComponent();
            color_typeViewSource = ((CollectionViewSource)(this.FindResource("color_typeViewSource")));
            colortypecolorViewSource = ((CollectionViewSource)(this.FindResource("colortypecolorViewSource")));
            MyCommonDB.ColorDT();
            kinoDataSet = MyCommonDB.KinoDS();
            this.DataContext = kinoDataSet;

        }

        public void gridview_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
            if (drv != null && (drv.Row["color_type"] == System.DBNull.Value || drv.Row["color_type"] == ""))
            {
                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                validationResult.PropertyName = "color_type";
                validationResult.ErrorMessage = "color type is required, please fill unigue value or press Escape for reject.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
            }
        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            SaveChanges();
        }

        void SaveChanges()
        {
            grdcolortype.CommitEdit();
            MessageBoxResult res = MessageBox.Show("There are not saved changes, Would you like to save changes?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                if (!MyCommonDB.SaveColorType()) kinoDataSet.tbl_color_type.RejectChanges();
                if (!MyCommonDB.SaveColorTypeColor()) kinoDataSet.tbl_color_type_color.RejectChanges();
            }
            else
            {
                kinoDataSet.tbl_color_type_color.RejectChanges();
                kinoDataSet.tbl_color_type.RejectChanges();
            }
        }
    

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void grdColors_Loaded(object sender, RoutedEventArgs e)
        {

            KinoDataSet.tbl_color_typeRow tr; //View_TicketBalanceViewSource
            try
            {
                RadGridView dataControl = (RadGridView)sender;
                Telerik.Windows.Controls.GridView.GridViewRow gvr = dataControl.ParentRow;
                DataRowView drv = (DataRowView)gvr.Item;
                if (drv != null)
                {
                    tr = drv.Row as KinoDataSet.tbl_color_typeRow;
                    if (tr != null)
                    {
                        LoadColorsForDetails(tr, dataControl);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in grdColors_Loaded");
            }
        }

        void LoadColorsForDetails(KinoDataSet.tbl_color_typeRow tr, RadGridView dataControl)
        {
            try
            {
                //DataRow[] tsrvr = tr.GetChildRows("FK_tbl_color_type_color_tbl_color_type");
                //dataControl.ItemsSource = tsrvr;
                //dataControl.Tag = tr;

                DataRow[] tsrvr = tr.Gettbl_color_type_colorRows();
                dataControl.ItemsSource = tsrvr;
                dataControl.Tag = tr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in LoadColorsForDetails");
            }
        }

        private void OpenColorsList(object sender, RoutedEventArgs e)
        {
            //KinoDataSet.tbl_pedigree_documentRow seldr;
            //Button btn = (Button)sender;
            //try
            //{
            //    StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
            //    RadGridView documentsgrdv;
            //    documentsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdDocuments");
            //    if (documentsgrdv == null) documentsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdDocumentsRO");
            //    if (documentsgrdv == null) return;

            //    DataRowView drv = documentsgrdv.SelectedItem as DataRowView;
            //    if (drv == null) return;

            //    seldr = drv.Row as KinoDataSet.tbl_pedigree_documentRow;
            //    Common.OpenFileFromDataRow(seldr, "file_name", "PedigreeDocument");

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message + " in btnOpenDocument_Click");
            //}


            CheckBox_List_Dialog dlg = null;// new CheckBox_List_Dialog();
            KinoDataSet.tbl_color_typeRow tr;
            Button btn = (Button)sender;
            try
            {
                

                StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                RadGridView documentsgrdv;
                documentsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "radgrdColors");
                if (documentsgrdv == null) return;

                //RadGridView dataControl = (RadGridView)sender;

                Telerik.Windows.Controls.GridView.GridViewRow gvr = documentsgrdv.ParentRow;


                DataRowView drv = (DataRowView)gvr.Item;

                tr = drv.Row as KinoDataSet.tbl_color_typeRow;// (HotelDataSet.TicketTypeRow)drv.Row;
                if (tr == null) return;
                if (tr.RowState == DataRowState.Detached || tr.RowState == DataRowState.Added)
                {
                    MessageBox.Show("You must save changes before");
                    return;
                }

                dlg = new CheckBox_List_Dialog();
                dlg.DataContext = kinoDataSet.tbl_color;
                KinoDataSet.tbl_color_type_colorRow[] tsrvr = tr.Gettbl_color_type_colorRows();

                if (tsrvr != null && tsrvr.Length > 0)
                {
                    for (int i = 0; i < kinoDataSet.tbl_color.Count; i++)
                    {
                        if (tsrvr.AsEnumerable<KinoDataSet.tbl_color_type_colorRow>().Where(p => p.color_id == kinoDataSet.tbl_color[i].color_id).Count() > 0)
                        {
                            dlg.selItemsList.Add(i);
                        }
                    }
                }


                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    MessageBox.Show("ok");
                    List<KinoDataSet.tbl_colorRow> SelExpDRList = new List<KinoDataSet.tbl_colorRow>();
                    foreach (DataRowView sdrv in dlg.lb_Services.SelectedItems)
                    {
                        DataRow sdr = sdrv.Row;
                        if (tsrvr != null)
                        {
                            KinoDataSet.tbl_colorRow exprow = (KinoDataSet.tbl_colorRow)sdr;
                            SelExpDRList.Add(exprow);
                            if (tsrvr.AsEnumerable<KinoDataSet.tbl_color_type_colorRow>().Where(p => p.color_id == exprow.color_id).Count() == 0)
                            {
                                KinoDataSet.tbl_color_type_colorRow ntickservrow = (KinoDataSet.tbl_color_type_colorRow)kinoDataSet.tbl_color_type_color.NewRow();
                                ntickservrow.color_id = exprow.color_id;
                                ntickservrow.color_type_id = tr.color_type_id;
                                kinoDataSet.tbl_color_type_color.Rows.Add(ntickservrow);
                            }
                            //}
                        }
                    }

                    foreach (KinoDataSet.tbl_color_type_colorRow ttsrvr in tsrvr)
                    {
                        if (SelExpDRList.Where(p => p.color_id == ttsrvr.color_id).Count() == 0)
                        {
                            ttsrvr.Delete();
                        }
                    }
                    tsrvr = tr.Gettbl_color_type_colorRows();
                    documentsgrdv.ItemsSource = tsrvr;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in OpenColorsList");
                if (dlg != null)
                    dlg = null;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }

        private void grdcolortype_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
    //public partial class ColorTypeUC : UserControl
