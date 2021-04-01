using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using KinoDataLibrary;
//using KinoDataModel;
using KinoUI.Content;
using Telerik.Windows.Controls;
using System.Net;
using static KinoDataLibrary.KinoDataSet;
using System.IO;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;

namespace KinoUI.Pages
{
    /// <summary>
    /// Interaction logic for AgeGroupUC.xaml
    /// </summary>
    public partial class BreedsUC : UserControl
    {
        //KinologyModel mod = null;
        private ScrollViewer myScrollViewer;
        KinoDataSet kinoDataSet;
        CollectionViewSource breedViewSource;
        CollectionViewSource breednameViewSource;
        CollectionViewSource regcounterViewSource;

        CollectionViewSource hairtypeViewSource;
        CollectionViewSource colortypeViewSource;
        CollectionViewSource sizetypeViewSource;

        CollectionViewSource breedhairtypesViewSource;
        CollectionViewSource breedcolorrtypesViewSource;
        CollectionViewSource breedsizetypesViewSource;
        public BreedsUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //kinoDataSet = ((KinoDataSet)(this.FindResource("kinoDataSet")));
                    breedViewSource = ((CollectionViewSource)(this.FindResource("breedViewSource")));
                    breednameViewSource = ((CollectionViewSource)(this.FindResource("breednameViewSource")));
                    regcounterViewSource = ((CollectionViewSource)(this.FindResource("regcounterViewSource")));

                    hairtypeViewSource = ((CollectionViewSource)(this.FindResource("hairtypeViewSource")));
                    colortypeViewSource = ((CollectionViewSource)(this.FindResource("colortypeViewSource")));
                    sizetypeViewSource = ((CollectionViewSource)(this.FindResource("sizetypeViewSource")));

                    breedhairtypesViewSource = ((CollectionViewSource)(this.FindResource("breedhairtypesViewSource")));
                    breedcolorrtypesViewSource = ((CollectionViewSource)(this.FindResource("breedcolorrtypesViewSource")));
                    breedsizetypesViewSource = ((CollectionViewSource)(this.FindResource("breedsizetypesViewSource")));

                    MyCommonDB.BreedDT();
                    kinoDataSet = MyCommonDB.KinoDS();

                    breednameViewSource.Source = MyCommonDB.BreedNameDT();
                    regcounterViewSource.Source = MyCommonDB.Registration_counterDT();
                    
                    //breedViewSource.Source = MyCommon.BreedDT();

                   
                   
                    //breedViewSource.Source = kinoDataSet;
                    //breednameViewSource.Source = kinoDataSet;
                    //regcounterViewSource.Source = kinoDataSet;
                    //hairtypeViewSource.Source = kinoDataSet;
                    //colortypeViewSource.Source = kinoDataSet;
                    //sizetypeViewSource.Source = kinoDataSet;
                    //breedhairtypesViewSource.Source = kinoDataSet;
                    //breedcolorrtypesViewSource.Source = kinoDataSet;
                    //breedsizetypesViewSource.Source = kinoDataSet;
                    this.DataContext = kinoDataSet;
                    
                   
                    //hairtypeViewSource.Source = MyCommon.HairTypeDT();
                    //colortypeViewSource.Source = MyCommon.ColorTypeDT();
                    //sizetypeViewSource.Source = MyCommon.SizeTypeDT();

                    //breedhairtypesViewSource.Source=
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in BreedsUC ctor");
            }
        }

        private void ScrollViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            myScrollViewer = (sender as ScrollViewer);
            ScrollBar verticalScrollBar = myScrollViewer.Template.FindName("PART_VerticalScrollBar", myScrollViewer) as ScrollBar;
            verticalScrollBar.SmallChange = 5;
        }
        public void gridview_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            try
            {
                System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
                if (drv == null) return;
                if  (drv.Row["breed_nameID"] == System.DBNull.Value || drv.Row["breed_nameID"] == "")
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "breed_nameID";
                    validationResult.ErrorMessage = "Breed name is required, please fill unigue value or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }

                if (drv.Row["active"] == System.DBNull.Value || drv.Row["active"] == "")
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "active";
                    validationResult.ErrorMessage = "Active  is required, please set value or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in gridview_RowValidating");
            }

        }

        private void mnuSave_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            SaveChanges();
        }

        void SaveChanges()
        {
            grdbreed.CommitEdit();
            MyCommonDB.SaveBreed();
            MyCommonDB.SaveBreedColorType();
            MyCommonDB.SaveBreedHairType();
            MyCommonDB.SaveBreedSizeType();
            MyCommonDB.FillBreedDescriptions(true);
        }

        private void grdhairtypes_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            //KinoDataSet.tbl_breedRow br = ((System.Data.DataRowView)((Telerik.Windows.Controls.GridView.GridViewRow)e.OwnerGridViewItemsControl.ParentRow).Item).Row as KinoDataSet.tbl_breedRow;
            //if (br != null)
            //{
                
            //}
            //else
            //    MessageBox.Show("Ooups");
        }

        private void grdhairtypes_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            try
            {
                if (e.EditAction == Telerik.Windows.Controls.GridView.GridViewEditAction.Cancel)
                {
                    return;
                }
                if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Insert)
                {
                    //Add the new entry to the data base.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in grdhairtypes_RowEditEnded");
            }

        }

        private void RadMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            //DataRowView drv = grdbreed.CurrentItem as DataRowView;
            //MessageBox.Show(drv.Row["breed_id"].ToString());
            FindTemplate();
        }

        private void grdhairtypes_Loaded(object sender, RoutedEventArgs e)
        {
            //KinoDataSet.tbl_breedRow tr; //View_TicketBalanceViewSource
            //try
            //{
            //    RadGridView dataControl = (RadGridView)sender;
            //    //Telerik.Windows.Controls.GridView.GridViewRow gvr = dataControl.ParentRow;
            //    //DataRowView drv = (DataRowView)gvr.Item;
            //    DataRowView drv = (DataRowView)breedRadDataForm.CurrentItem;
            //    if (drv == null) return;
            //    tr = drv.Row as KinoDataSet.tbl_breedRow;// (HotelDataSet.TicketTypeRow)drv.Row;
            //    if (tr != null)
            //    {
            //        LoadHairTypesForDetails(tr, dataControl);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + " in grdhairtypes_Loaded");
            //}
        }

        private void grdcolortypes_Loaded(object sender, RoutedEventArgs e)
        {
            //KinoDataSet.tbl_breedRow tr;
            //try
            //{
            //    RadGridView dataControl = (RadGridView)sender;
            //    //Telerik.Windows.Controls.GridView.GridViewRow gvr = dataControl.ParentRow;
            //    //DataRowView drv = (DataRowView)gvr.Item;
            //    DataRowView drv = (DataRowView)breedRadDataForm.CurrentItem;
            //    if (drv == null) return;

            //    tr = drv.Row as KinoDataSet.tbl_breedRow;
            //    if (tr != null)
            //    {
            //        LoadColorTypesForDetails(tr, dataControl);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + " in grdcolortypes_Loaded");
            //}
        }

        void LoadHairTypesForDetails(KinoDataSet.tbl_breedRow tr, RadGridView dataControl)
        {
            try
            {
                DataRow[] tsrvr = tr.GetChildRows("FK_tbl_breed_hairtype_tbl_breed");
                dataControl.ItemsSource = tsrvr;
                dataControl.Tag = tr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in LoadHairTypesForDetails");
            }
        }

        void LoadColorTypesForDetails(KinoDataSet.tbl_breedRow tr, RadGridView dataControl)
        {
            try
            {
                DataRow[] tsrvr = tr.GetChildRows("FK_tbl_breed_colortype_tbl_breed");
                dataControl.ItemsSource = tsrvr;
                dataControl.Tag = tr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in LoadColorTypesForDetails");
            }
        }

        void LoadSizeTypesForDetails(KinoDataSet.tbl_breedRow tr, RadGridView dataControl)
        {
            try
            {
                DataRow[] tsrvr = tr.GetChildRows("FK_tbl_breed_sizetype_tbl_breed");
                dataControl.ItemsSource = tsrvr;
                dataControl.Tag = tr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in LoadSizeTypesForDetails");
            }
        }

        private void grdsizetypes_Loaded(object sender, RoutedEventArgs e)
        {
            //KinoDataSet.tbl_breedRow tr;
            //try
            //{
            //    RadGridView dataControl = (RadGridView)sender;
            //    //Telerik.Windows.Controls.GridView.GridViewRow gvr = dataControl.ParentRow;
            //    //DataRowView drv = (DataRowView)gvr.Item;
            //    DataRowView drv = (DataRowView)breedRadDataForm.CurrentItem;
            //    if (drv == null) return;
            //        tr = drv.Row as KinoDataSet.tbl_breedRow;
            //        if (tr != null)
            //        {
            //            LoadSizeTypesForDetails(tr, dataControl);
            //        }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + " in grdsizetypes_Loaded");
            //}
        }

        private void btnAddHairType_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            OpenBreedFactorsDialog(btn);
        }

        void FindTemplate()
        {
            try
            {
                Telerik.Windows.Controls.GridView.GridViewRow myListBoxItem = (Telerik.Windows.Controls.GridView.GridViewRow)(grdbreed.ItemContainerGenerator.ContainerFromItem(grdbreed.Items.CurrentItem));

                // Getting the ContentPresenter of myListBoxItem
                ContentPresenter myContentPresenter = MyCommonDB.FindVisualChild<ContentPresenter>(myListBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                RadGridView myTextBlock = (RadGridView)myDataTemplate.FindName("grdhairtypes", myContentPresenter);

                // Do something to the DataTemplate-generated TextBlock
                MessageBox.Show("The text of the TextBlock of the selected list item: ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in FindTemplate");
            }

        }

        void OpenBreedFactorsDialog(Button btn)
        {
            try
            {//grdHairs
                StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                RadGridView colorsgrdv;
                RadGridView hairsgrdv;
                RadGridView sizesgrdv;
                 KinoDataSet.tbl_breedRow tr=null;


                 DataRowView drv = (DataRowView)breedRadDataForm.CurrentItem;
                 if (drv == null) return;
                 tr = drv.Row as KinoDataSet.tbl_breedRow;
                 if (tr == null) return;


                //colorsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdcolortypes");
                //if (colorsgrdv != null)
                //{
                if(tr.RowState== DataRowState.Detached)
                    kinoDataSet.tbl_breed.Rows.Add(tr);
                 AddBreedFactorsTypeDlg frm = new AddBreedFactorsTypeDlg(kinoDataSet, tr);//(Int32)tr.breed_id);
                 bool? res = frm.ShowDialog();
                 if (res == true)
                 {
                     
                     //LoadColorTypesForDetails(tr, colorsgrdv);
                 }
                //}


                //hairsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdhairtypes");
                //if (hairsgrdv != null)
                //{
                //    LoadHairTypesForDetails(tr, hairsgrdv);
                //}



                //sizesgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdsizetypes");
                //if (sizesgrdv != null)
                //{
                //    LoadSizeTypesForDetails(tr, sizesgrdv);
                //}

            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.Message + " in OpenBreedFactorsDialog");
            }
        }

        private void btnAddColorType_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            OpenBreedFactorsDialog(btn);
            
        }

        private void btnAddSizeType_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            OpenBreedFactorsDialog(btn);
        }

        string GetKeyForSearch(KinoDataSet.tbl_breedRow tr, DataRow tdr)
        {
            string _url = "";
            string _key="";
            try
            {
                _key = tr.tbl_breed_nameRow.breed_name;
                KinoDataLibrary.KinoDataSet.tbl_breed_hairtypeRow bhr = tdr as KinoDataLibrary.KinoDataSet.tbl_breed_hairtypeRow;
                if(bhr!=null)
                {
                    _key += ("+" + bhr.tbl_hair_typeRow.hair_type);
                }
                else
                {
                    if (tr.Gettbl_breed_hairtypeRows() != null && tr.Gettbl_breed_hairtypeRows().Length > 0)
                    {
                        foreach (tbl_breed_hairtypeRow item in tr.Gettbl_breed_hairtypeRows())
                        {
                            _key += ("+" + item.tbl_hair_typeRow.hair_type);
                        }
                    }
                }

                KinoDataLibrary.KinoDataSet.tbl_breed_colortypeRow bcr = tdr as KinoDataLibrary.KinoDataSet.tbl_breed_colortypeRow;
                if(bcr!=null)
                {
                    _key += ("+" + bcr.tbl_color_typeRow.color_type);
                }
                else
                {
                    if (tr.Gettbl_breed_colortypeRows() != null && tr.Gettbl_breed_colortypeRows().Length > 0)
                    {
                        foreach (tbl_breed_colortypeRow item in tr.Gettbl_breed_colortypeRows())
                        {
                            _key += ("+" + item.tbl_color_typeRow.color_type);
                        }
                    }
                }

                KinoDataLibrary.KinoDataSet.tbl_breed_sizetypeRow bsr = tdr as KinoDataLibrary.KinoDataSet.tbl_breed_sizetypeRow;

                if(bsr!=null)
                {
                    _key += ("+" + bsr.tbl_size_typeRow.size_type);
                }
                else
                {
                    if (tr.Gettbl_breed_sizetypeRows() != null && tr.Gettbl_breed_sizetypeRows().Length > 0)
                    {
                        foreach (tbl_breed_sizetypeRow item in tr.Gettbl_breed_sizetypeRows())
                        {
                            _key += ("+" + item.tbl_size_typeRow.size_type);
                        }
                    }                 
                }

                //_url = "https://www.google.com/search?tbm=isch&q=" + _key;
                //_url = _url.Replace("-", "+");
                //_url = _url.Replace("  ", " ");
                //_url = _url.Replace(" ", "+");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in GetKeyForSearch");
            }
            return _key;
        }

        private void btnAddHairTypePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                
                Button btn = (Button)sender;
                StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                KinoDataSet.tbl_breedRow tr = null;
                RadListBox grdv;
                grdv = MyCommonDB.FindVisualChild<RadListBox>(stpnl, "grdhairtypes");
                if (grdv.SelectedItem as DataRowView == null)
                {
                    MessageBox.Show("You must select row in the grid");
                    return;
                }
                KinoDataLibrary.KinoDataSet.tbl_breed_hairtypeRow tdr = ((DataRowView)grdv.SelectedItem).Row as KinoDataLibrary.KinoDataSet.tbl_breed_hairtypeRow;
                tr = tdr.tbl_breedRow as KinoDataSet.tbl_breedRow;
                if (tdr != null && tr != null)
                {
                    DataRow dr = tdr as DataRow;

                    MessageBoxResult res = MessageBox.Show("For finding in the Internet press Yes, For download from file press No, or Cancel Operation.", "Load Image", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if ( res== MessageBoxResult.Yes)
                    {
                        string _url = GetKeyForSearch(tr, tdr);
                        WebBrowserLib.WebBrowserControl webctl = new WebBrowserLib.WebBrowserControl(_url, WebBrowserLib.SeacrTypeEnum.GoogleImage, tr.tbl_breed_nameRow.URL);

                        bool? dlgres = webctl.ShowDialog();
                        if (dlgres == true && webctl.SelectedFileUrl()!="")
                        {
                                var wClient = new WebClient();
                            string _newFilePAth = Path.GetTempPath() + @"\\" + Guid.NewGuid().ToString();
                                wClient.DownloadFile(webctl.SelectedFileUrl(), _newFilePAth);
                            Common.SavePhotoToDataRow(ref dr, "photo", _newFilePAth);
                        }

                    }
                    else if(res==MessageBoxResult.No)
                    {
                        Common.SavePhotoToDataRow(ref dr, "photo");                  
                    }
                    else
                    {
                        return;
                    }
                    MyCommonDB.SaveBreedHairType();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in btnAddHairTypePhoto_Click");
            }     
        }

        private void btnAddColorTypePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                KinoDataSet.tbl_breedRow tr = null;
                RadListBox grdv;
                grdv = MyCommonDB.FindVisualChild<RadListBox>(stpnl, "grdcolortypes");
                if (grdv.SelectedItem as DataRowView == null)
                {
                    MessageBox.Show("You must select row in the grid");
                    return;
                }
                

                    KinoDataLibrary.KinoDataSet.tbl_breed_colortypeRow tdr = ((DataRowView)grdv.SelectedItem).Row as KinoDataLibrary.KinoDataSet.tbl_breed_colortypeRow;
                    tr = tdr.tbl_breedRow as KinoDataSet.tbl_breedRow;
                    if (tdr != null && tr != null)
                    {
                        DataRow dr = tdr as DataRow;

                        MessageBoxResult res = MessageBox.Show("For finding in the Internet press Yes, For download from file press No, or Cancel Operation.", "Load Image", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                        if (res == MessageBoxResult.Yes)
                        {
                            string _url = GetKeyForSearch(tr, tdr);
                            WebBrowserLib.WebBrowserControl webctl = new WebBrowserLib.WebBrowserControl(_url, WebBrowserLib.SeacrTypeEnum.GoogleImage, tr.tbl_breed_nameRow.URL);

                            bool? dlgres = webctl.ShowDialog();
                            if (dlgres == true && webctl.SelectedFileUrl() != "")
                            {
                                var wClient = new WebClient();
                                string _newFilePAth = Path.GetTempPath() + @"\\" + Guid.NewGuid().ToString();
                                wClient.DownloadFile(webctl.SelectedFileUrl(), _newFilePAth);
                                Common.SavePhotoToDataRow(ref dr, "photo", _newFilePAth);
                            }

                        }
                        else if (res == MessageBoxResult.No)
                        {
                            Common.SavePhotoToDataRow(ref dr, "photo");
                        }
                        else
                        {
                            return;
                        }                               

                        MyCommonDB.SaveBreedColorType();
                 
                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in btnAddColorTypePhoto_Click");
            }
        }

        private void btnAddSizeTypePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                KinoDataSet.tbl_breedRow tr = null;
                RadListBox grdv;
                grdv = MyCommonDB.FindVisualChild<RadListBox>(stpnl, "grdsizetypes");
                if (grdv.SelectedItem as DataRowView == null)
                {
                    MessageBox.Show("You must select row in the grid");
                    return;
                }
                KinoDataLibrary.KinoDataSet.tbl_breed_sizetypeRow tdr = ((DataRowView)grdv.SelectedItem).Row as KinoDataLibrary.KinoDataSet.tbl_breed_sizetypeRow;
                tr = tdr.tbl_breedRow as KinoDataSet.tbl_breedRow;
                    if (tdr != null && tr != null)
                    {
                        DataRow dr = tdr as DataRow;

                        MessageBoxResult res = MessageBox.Show("For finding in the Internet press Yes, For download from file press No, or Cancel Operation.", "Load Image", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                        if (res == MessageBoxResult.Yes)
                        {
                            string _url = GetKeyForSearch(tr, tdr);
                            WebBrowserLib.WebBrowserControl webctl = new WebBrowserLib.WebBrowserControl(_url, WebBrowserLib.SeacrTypeEnum.GoogleImage, tr.tbl_breed_nameRow.URL);

                            bool? dlgres = webctl.ShowDialog();
                            if (dlgres == true && webctl.SelectedFileUrl() != "")
                            {
                                var wClient = new WebClient();
                                string _newFilePAth = Path.GetTempPath() + @"\\" + Guid.NewGuid().ToString();
                                wClient.DownloadFile(webctl.SelectedFileUrl(), _newFilePAth);
                                Common.SavePhotoToDataRow(ref dr, "photo", _newFilePAth);
                            }

                        }
                        else if (res == MessageBoxResult.No)
                        {
                            Common.SavePhotoToDataRow(ref dr, "photo");
                        }
                        else
                        {
                            return;
                        }

                        MyCommonDB.SaveBreedSizeType();
                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in btnAddSizeTypePhoto_Click");
            }
        }

        private void breedRadDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            try
            {
                KinoDataSet.tbl_breedRow tr = null;
                DataRowView drv = e.NewItem as DataRowView;
                     if (drv == null) return;
                     tr = drv.Row as KinoDataSet.tbl_breedRow;
                     if (tr == null) return;
                     tr.active = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in breedRadDataForm_AddedNewItem");
            }

        }

        private void breedRadDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {

        }

        private void breedRadDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            try
            {
                if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
                    SaveChanges();
                else
                {
                    kinoDataSet.tbl_breed_colortype.RejectChanges();
                    kinoDataSet.tbl_breed_hairtype.RejectChanges();
                    kinoDataSet.tbl_breed_sizetype.RejectChanges();
                    kinoDataSet.tbl_breed.RejectChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in breedRadDataForm_EditEnded");
            }

        }

        private void breedRadDataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void btnAddBreedPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                KinoDataSet.tbl_breedRow tr = null;

                DataRowView drv = (DataRowView) breedRadDataForm.CurrentItem;
                if (drv == null) return;
                tr = drv.Row as KinoDataSet.tbl_breedRow;

                if ( tr != null)
                {
                    DataRow dr = (DataRow) tr;
                    MessageBoxResult res = MessageBox.Show("For finding in the Internet press Yes, For download from file press No, or Cancel Operation.", "Load Image", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        string _url =  tr.tbl_breed_nameRow.breed_name; ;
                        WebBrowserLib.WebBrowserControl webctl = new WebBrowserLib.WebBrowserControl(_url, WebBrowserLib.SeacrTypeEnum.GoogleImage, tr.tbl_breed_nameRow.URL);

                        bool? dlgres = webctl.ShowDialog();
                        if (dlgres == true && webctl.SelectedFileUrl() != "")
                        {
                            var wClient = new WebClient();
                            string _newFilePAth = Path.GetTempPath() + @"\\" + Guid.NewGuid().ToString();
                            wClient.DownloadFile(webctl.SelectedFileUrl(), _newFilePAth);
                            
                            Common.SavePhotoToDataRow(ref dr, "breed_photo", _newFilePAth);
                        }

                    }
                    else if (res == MessageBoxResult.No)
                    {
                        Common.SavePhotoToDataRow(ref dr, "breed_photo");
                    }
                    else
                    {
                        return;
                    }

                    MyCommonDB.SaveBreed();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in btnAddBreedPhoto_Click");
            }
        }

        private void grdbreed_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            DataRowView drv = grdbreed.SelectedItem as DataRowView;
            if(drv!=null)
            {
                tbl_breedRow b = drv.Row as tbl_breedRow;
                List<ImageByte> img = b.AllImages;

                grdPhotos.ItemsSource= img;
            }
        }
    }
}

    //public partial class BreedsUC : UserControl
