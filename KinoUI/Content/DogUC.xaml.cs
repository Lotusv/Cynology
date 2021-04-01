using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Forms;
using System.Windows.Input;
using KinoDataLibrary;
using Telerik.Windows;
using Telerik.Windows.Controls;
using ReportsClassLib;

namespace KinoUI.Content
{
    /// <summary>
    /// Interaction logic for DogUC.xaml
    /// </summary>
    public partial class DogUC : UserControl
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

        #region timewatch
        private System.Windows.Forms.Timer _timer;

        // The last time the timer was started
        private DateTime _startTime = DateTime.MinValue;

        // Time between now and when the timer was started last
        private TimeSpan _currentElapsedTime = TimeSpan.Zero;

        // Time between now and the first time timer was started after a reset
        private TimeSpan _totalElapsedTime = TimeSpan.Zero;

        // Whether or not the timer is currently running
        private bool _timerRunning = false;

        #endregion


        //filter country codes
  

        public DogUC()
        {

            try
            {
#if DEBUG
                   _timer = new System.Windows.Forms.Timer();

                _startTime = DateTime.Now;
                _timer.Start();
                _timer_Tick("before InitializeComponent");                               //
#endif


                InitializeComponent();
#if DEBUG
      _timer_Tick("after InitializeComponent");                            //
#endif
                



                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
#if DEBUG
         _timer_Tick("before set datacontext");                         //
#endif
                    
                    MyCommonDB.PedigreeDT();
                    kinoDataSet = MyCommonDB.KinoDS();
                    this.DataContext = kinoDataSet;

                    pedigreeViewSource = ((CollectionViewSource) (this.FindResource("pedigreeViewSource")));
                    dogDocumentViewSource = ((CollectionViewSource) (this.FindResource("dogDocumentViewSource")));
                    colorSource = ((CollectionViewSource) (this.FindResource("colorSource")));

                    this.grdPedigree.AddHandler(RadComboBox.SelectionChangedEvent, new SelectionChangedEventHandler(ComboBox_SelectionChanged));
#if DEBUG
                    _timer_Tick("after set datacontext");              //
#endif
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in DogUC ctor");
            }
        }

        void View_CurrentChanging(object sender, System.ComponentModel.CurrentChangingEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dogRadDataForm.CurrentItem;
                if (drv != null && drv.IsNew)
                {
                    e.Cancel = true;
                    return;
                }
                if (drv != null && drv.IsEdit)
                    e.Cancel = true;

                //MessageBox.Show(cmbSir.Selected)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in DogUC View_CurrentChanging");
            }
        }

        void _timer_Tick(String _from)
        {
            // We do this to chop off any stray milliseconds resulting from 
            // the Timer's inherent inaccuracy, with the bonus that the 
            // TimeSpan.ToString() method will now show correct HH:MM:SS format
            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);

            // The current elapsed time is the time since the start button was
            // clicked, plus the total time elapsed since the last reset

            _currentElapsedTime = timeSinceStartTime - _currentElapsedTime;

            System.Diagnostics.Debug.WriteLine(_from + " _currentElapsedTime: " + _currentElapsedTime.ToString());
            System.Diagnostics.Debug.WriteLine(_from + "timeSinceStartTime: " + timeSinceStartTime.ToString());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ////if ((e.Source as RadComboBox).UniqueName == "cmbStatus")
            ////{
            ////    Console.WriteLine("ComboBox selection changed!");
            ////}

            //var cell = (e.OriginalSource as RadComboBox).ParentOfType<GridViewCell>();
            //if (cell != null && cell.Column.UniqueName == "cmbStatus")
            //{
            //    Console.WriteLine("ComboBox selection changed!");
            //}
            //////Second approach:
            ////RadComboBox comboBox = (RadComboBox)e.OriginalSource;
            ////if (comboBox.SelectedValue == null || comboBox.SelectedValuePath != "Code") // we take action only if the continent combo is changed
            ////{
            ////}
        }

        public void childgridview_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            try
            {
                System.Data.DataRow dr = e.Row.DataContext as System.Data.DataRow;
                if (dr != null && (dr["document_type_id"] == System.DBNull.Value || dr["document_type_id"] == ""))
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "document_type_id";
                    validationResult.ErrorMessage = "Document type is required, please fill unigue value or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in childgridview_RowValidating");
            }

        }

        public void gridview_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            try
            {
                System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
                if (drv != null && (drv.Row["full_name"] == System.DBNull.Value || drv.Row["full_name"] == ""))
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "full_name";
                    validationResult.ErrorMessage = "Full name is required, please fill unigue value or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }
                if (drv != null && (drv.Row["breed_id"] == System.DBNull.Value || drv.Row["breed_id"] == ""))
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "breed_id";
                    validationResult.ErrorMessage = "Breed is required, please fill unigue value or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in gridview_RowValidating");
            }

        }


        public void SaveChanges()
        {
            try
            {
                grdPedigree.CommitEdit();
                MyCommonDB.SavePedigree();
                MyCommonDB.SavePedigreeDocument();
                MyCommonDB.SaveRegistration_counter();
                MyCommonDB.SavePedigreeChampions();
                MyCommonDB.SavePedigreeDewormings();
                MyCommonDB.SavePedigreeEctoparasits();
                MyCommonDB.SavePedigreeHealthTests();
                MyCommonDB.SavePedigreeVaccines();
                MyCommonDB.SavePedigreeTrial();
                //FilterColorSource(0);
                FilterPropertiesCombosByBread(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in SaveChanges");
            }
        }

        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            SaveChanges();
        }


        private void FilterPropertiesCombosByBread(Int32 breedID)
        {
            FilterColorSource(breedID);
            FilterHairSource(breedID);
            FilterSizeSource(breedID);

        }

        private void FilterColorSource(Int32 breedID)
        {
            //colorSource.Filter = "";
            try
            {
                if(breedID==0)
                {
                    kinoDataSet.tbl_color.DefaultView.RowFilter = "";
                    return;
                }

                var query = from ctc in kinoDataSet.tbl_color_type_color
                            join ct in kinoDataSet.tbl_color_type on ctc.color_type_id equals ct.color_type_id
                            join bc in kinoDataSet.tbl_breed_colortype on ct.color_type_id equals bc.color_type_id
                            where bc.breed_id == breedID
                            select new { ctc};
                string filter = "";

                foreach (var q in query)
                {
                    if(filter=="")
                        filter+=q.ctc.color_id.ToString();
                    else
                        filter += "," +q.ctc.color_id.ToString();
                }
                if(filter!="") kinoDataSet.tbl_color.DefaultView.RowFilter = "color_id in (" + filter + ")";
                else kinoDataSet.tbl_color.DefaultView.RowFilter = "color_id in (-1)";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in FilterColorSource");
            }

        }

        private void FilterHairSource(Int32 breedID)
        {
            try
            {
                if (breedID == 0)
                {
                    kinoDataSet.tbl_hair_type.DefaultView.RowFilter = "";
                    return;
                }

                var query = from ctc in kinoDataSet.tbl_breed_hairtype
                            where ctc.breed_id == breedID
                            select new { ctc };
                string filter = "";

                foreach (var q in query)
                {
                    if (filter == "")
                        filter += q.ctc.hair_type_id.ToString();
                    else
                        filter += "," + q.ctc.hair_type_id.ToString();
                }
                if (filter != "") kinoDataSet.tbl_hair_type.DefaultView.RowFilter = "hair_type_id in (" + filter + ")";
                else kinoDataSet.tbl_hair_type.DefaultView.RowFilter = "hair_type_id in (-1)";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in FilterHairSource");
            }

        }

        private void FilterSizeSource(Int32 breedID)
        {
            try
            {
                if (breedID == 0)
                {
                    kinoDataSet.tbl_size_type.DefaultView.RowFilter = "";
                    return;
                }

                var query = from ctc in kinoDataSet.tbl_breed_sizetype
                            where ctc.breed_id == breedID
                            select new { ctc };
                string filter = "";

                foreach (var q in query)
                {
                    if (filter == "")
                        filter += q.ctc.size_type_id.ToString();
                    else
                        filter += "," + q.ctc.size_type_id.ToString();
                }
                if (filter != "") kinoDataSet.tbl_size_type.DefaultView.RowFilter = "size_type_id in (" + filter + ")";
                else kinoDataSet.tbl_size_type.DefaultView.RowFilter = "size_type_id in (-1)";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in FilterSizeSource");
            }

        }

        private void SelectBreed(object sender, RoutedEventArgs e)
        {
            try
            {
                MyCommonDB.BreedDT();
                KinoDataSet ds = MyCommonDB.KinoDS();
                var dlg = new SelectBreedDlg
                {
                    Title = "Breed select",
                    DataContext = ds
                };
                dlg.ShowDialog();

                if (dlg.DialogResult.HasValue && dlg.DialogResult.Value)
                {
                    DataRowView drv = ((Button)sender).DataContext as DataRowView;
                    if (drv != null)
                    {
                        KinoDataSet.tbl_pedigreeRow pdgdr = drv.Row as KinoDataSet.tbl_pedigreeRow;
                        if (dlg.SelectedBreedID() != 0 )
                        {
                            Int32 selbrid = dlg.SelectedBreedID();
                            if (pdgdr["breed_id"] == DBNull.Value || pdgdr.breed_id != selbrid)
                            {
                                pdgdr.breed_id = selbrid;
                                //FilterColorSource(pdgdr.breed_id);
                                FilterPropertiesCombosByBread(pdgdr.breed_id);
                                drv.EndEdit();


                               
                                RadComboBox cmbsir;
                                cmbsir = MyCommonDB.FindVisualChild<RadComboBox>(dogRadDataForm, "cmbSir");
                                if (cmbsir == null) return;
                                BindingOperations.GetBindingExpression(cmbsir, RadComboBox.ItemsSourceProperty).UpdateTarget();

                                RadComboBox cmbDame;
                                cmbDame = MyCommonDB.FindVisualChild<RadComboBox>(dogRadDataForm, "cmbDame");
                                if (cmbDame == null) return;
                                BindingOperations.GetBindingExpression(cmbDame, RadComboBox.ItemsSourceProperty).UpdateTarget();

                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in Button_Click");
            }

        }

        private void grdDocuments_Loaded(object sender, RoutedEventArgs e)
        {

            //KinoDataSet.tbl_pedigreeRow tr; //View_TicketBalanceViewSource
            //try
            //{
            //    RadGridView dataControl = (RadGridView)sender;
            //    //Telerik.Windows.Controls.GridView.GridViewRow gvr = dataControl.ParentRow;
            //    //DataRowView drv = (DataRowView)gvr.Item;
            //    DataRowView drv = (DataRowView)dogRadDataForm.CurrentItem;
            //    if (drv != null)
            //    {
            //        tr = drv.Row as KinoDataSet.tbl_pedigreeRow;
            //        if (tr != null)
            //        {
            //            LoadDocumentsForDetails(tr, dataControl);
            //        }
            //    }
#if DEBUG
             _timer_Tick("after grdDocuments_Loaded");                                 //
#endif

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + " in grdDocuments_Loaded");
            //}
        }

        void LoadDocumentsForDetails(KinoDataSet.tbl_pedigreeRow tr, RadGridView dataControl)
        {
            try
            {
                DataRow[] tsrvr = tr.GetChildRows("FK_tbl_PedigreeDocument_tbl_pedigree");
                dataControl.ItemsSource = tsrvr;
                dataControl.Tag = tr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in LoadDocumentsForDetails");
            }
        }

        private void btnAddDocument_Click(object sender, RoutedEventArgs e)
        {
            KinoDataSet.tbl_pedigree_documentRow seldr = (KinoDataSet.tbl_pedigree_documentRow)kinoDataSet.tbl_pedigree_document.NewRow();
            Button btn = (Button)sender;


            try
            {
                //StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                //RadGridView documentsgrdv;

                //KinoDataSet.tbl_pedigreeRow tr = null;

                //documentsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdDocuments");
                //if (documentsgrdv != null)
                //{

                //    KinoDataSet.tbl_document_typeRow dtdr = kinoDataSet.tbl_document_type.AsEnumerable().FirstOrDefault<KinoDataSet.tbl_document_typeRow>();
                //    tr = documentsgrdv.Tag as KinoDataSet.tbl_pedigreeRow;
                //    if (tr != null)
                //    {
                //        seldr.pedigree_id = tr.pedigree_id;
                //        seldr.document_type_id = dtdr.document_type_id;
                //        DataRow dr = (DataRow)seldr;
                //        kinoDataSet.tbl_pedigree_document.Rows.Add(seldr);
                //        Common.SavePhotoToDataRow(ref dr, "PedigreeDocument");
                //        LoadDocumentsForDetails(tr, documentsgrdv);
                //        //documentsgrdv.CommitEdit();
                //    }

                //}

                DataRowView drv = (DataRowView)dogRadDataForm.CurrentItem;
                KinoDataSet.tbl_pedigreeRow tr = null;
                if (drv == null) return;
                KinoDataSet.tbl_document_typeRow dtdr = kinoDataSet.tbl_document_type.AsEnumerable().FirstOrDefault<KinoDataSet.tbl_document_typeRow>();

                tr=drv.Row as KinoDataSet.tbl_pedigreeRow;
                if (tr == null) return;

                seldr.pedigree_id = tr.pedigree_id;
                seldr.document_type_id = dtdr.document_type_id;
                DataRow dr = (DataRow)seldr;
                kinoDataSet.tbl_pedigree_document.Rows.Add(seldr);
                Common.SavePhotoToDataRow(ref dr, "PedigreeDocument");
                //LoadDocumentsForDetails(tr, documentsgrdv);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in btnAddDocument_Click");
            }
        }

        private void btnDelDocument_Click(object sender, RoutedEventArgs e)
        {
            KinoDataSet.tbl_pedigree_documentRow seldr;// = (KinoDataSet.tbl_pedigree_documentRow)kinoDataSet.tbl_pedigree_document.NewRow();
            Button btn = (Button)sender;
            try
            {
                StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                RadGridView documentsgrdv;
                documentsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdDocuments");
                if (documentsgrdv != null)
                {
                    DataRowView drv = documentsgrdv.SelectedItem as DataRowView;
                    if (drv == null) return;

                    seldr = drv.Row as KinoDataSet.tbl_pedigree_documentRow;
                        seldr.Delete();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in btnDelDocument_Click");
            }
        }

        private void grdDocuments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        void SetCounterForBreed(KinoDataSet.tbl_pedigreeRow pedRow)
        {
            KinoDataSet.tbl_breedRow breedRow = pedRow.tbl_breedRow;
            KinoDataSet.tbl_registration_counterRow regcntRow = breedRow.tbl_registration_counterRow;
            if (regcntRow == null)
            {
                MessageBox.Show("You have not set registration counter for this breed");
                return;
            }
            regcntRow.registration_counter = regcntRow.registration_counter += 1;
            pedRow.reg_number = regcntRow.registration_counter.ToString();
        }

        private void grdPedigree_CellValidated(object sender, GridViewCellValidatedEventArgs e)
        {
            //if (e.Cell.Column.UniqueName == "cmbStatus" && ((KinoDataSet.tbl_pedigreeRow)((System.Data.DataRowView)((Telerik.Windows.Controls.GridView.GridViewRow)e.Cell.ParentRow).Item).Row).status == 2)
            //{
            //    MessageBox.Show("Registered");

            //    KinoDataSet.tbl_pedigreeRow pedRow = ((KinoDataSet.tbl_pedigreeRow)((System.Data.DataRowView)((Telerik.Windows.Controls.GridView.GridViewRow)e.Cell.ParentRow).Item).Row);
            //    if (((DataRow)pedRow)["reg_number"] == DBNull.Value)
            //    {
            //        SetCounterForBreed(pedRow);
            //    }
            //}
        }

        private void btnSir_Click(object sender, RoutedEventArgs e)
        {
            //
            try
            {
                string btnname = ((FirstFloor.ModernUI.Windows.Controls.ModernButton)sender).Name;
                DataRowView drv = ((Button)sender).DataContext as DataRowView;
                if (drv != null)
                {
                    KinoDataSet.tbl_pedigreeRow pdgdr = drv.Row as KinoDataSet.tbl_pedigreeRow;
                    SelectParentDlg dlg = null;
                    if (btnname == "btnDame")
                    {
                        dlg = new SelectParentDlg(2, pdgdr.tbl_breedRow.tbl_breed_nameRow.breed_nameID, pdgdr.pedigree_id)
                        {
                            Title = "Dame select",
                        };
                    }
                    else
                    {
                        dlg = new SelectParentDlg(1, pdgdr.tbl_breedRow.tbl_breed_nameRow.breed_nameID, pdgdr.pedigree_id)
                        {
                            Title = "Sir select",
                        };
                    }


                    dlg.ShowDialog();

                    if (dlg.DialogResult.HasValue && dlg.DialogResult.Value && dlg.ParentID() != 0)
                    {
                        if (btnname == "btnDame")
                        {
                            if (dlg.ParentID() != 0)
                                pdgdr.dam_name_id = dlg.ParentID();
                        }
                        else
                        {
                            if (dlg.ParentID() != 0)
                                pdgdr.sire_name_id = dlg.ParentID();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in btnSir_Click");
            }
        }

        private void btnOpenDocument_Click(object sender, RoutedEventArgs e)
        {
            KinoDataSet.tbl_pedigree_documentRow seldr;
            Button btn = (Button)sender;
            try
            {
                StackPanel stpnl = MyCommonDB.GetParentDependencyObjectFromVisualTree(btn, typeof(StackPanel)) as StackPanel;
                RadGridView documentsgrdv;
                documentsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdDocuments");
                if (documentsgrdv == null) documentsgrdv = MyCommonDB.FindVisualChild<RadGridView>(stpnl, "grdDocumentsRO");
                if (documentsgrdv == null) return;
                //KinoDataSet.tbl_pedigreeRow tr = null;
                //if (documentsgrdv != null)
                //{
                //    tr = documentsgrdv.Tag as KinoDataSet.tbl_pedigreeRow;
                //    if (tr != null)
                //    {
                DataRowView drv = documentsgrdv.SelectedItem as DataRowView;
                if (drv == null) return;

                seldr = drv.Row as KinoDataSet.tbl_pedigree_documentRow;
                        Common.OpenFileFromDataRow(seldr, "file_name", "PedigreeDocument");
                //    }
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " in btnOpenDocument_Click");
            }
        }

        private void DogUsCtl_Loaded(object sender, RoutedEventArgs e)
        {
            //
            try
            {
                
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    pedigreeViewSource.View.CurrentChanging += View_CurrentChanging;
                }
#if DEBUG
                    _timer_Tick("after DogUsCtl_Loaded");                              //
#endif

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in DogUsCtl_Loaded");
            }
        }

        private void dogRadDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {

        }

        private void dogRadDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            SaveChanges();
        }

        private void dogRadDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
                SaveChanges();
            else
            {
                kinoDataSet.tbl_pedigree.RejectChanges();
            }
        }

        private void DogUsCtl_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    pedigreeViewSource.View.CurrentChanging -= View_CurrentChanging;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in DogUsCtl_Unloaded");
            }
        }

        private void cmbSir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Telerik.Windows.Controls.RadComboBox cmb = sender as Telerik.Windows.Controls.RadComboBox;
            //if (cmb != null && cmb.SelectedValue!=null)
            //{
            //    MessageBox.Show(cmb.SelectedValue.ToString());
            //}
        }

        private void cmbSir_DropDownOpened(object sender, EventArgs e)
        {
            RadComboBox cmb = sender as RadComboBox;
            try
            {
                //DataRowView drv = (DataRowView)dogRadDataForm.CurrentItem;
                //if (drv != null)
                //{
                //    DataRow dr = drv.Row;
                //    Int32 pedigree_id = (Int32)dr["pedigree_id"];
                //    Int32 breed_id = (Int32)dr["breed_id"];
                //    string fltr = MyCommonDB.GetBreedsByBreed(breed_id);
                //    string custfilter= "sex_id=" + 1 + " and breed_id IN (" + fltr + ") and pedigree_id <> " + pedigree_id;
                //    ((System.Windows.Data.BindingListCollectionView)cmb.ItemsSource).CustomFilter = custfilter;


                //}

                //BindingOperations.GetBindingExpression(cmb, RadComboBox.ItemsSourceProperty).UpdateTarget();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in cmbSir_DropDownOpened");
            }

        }

        private void cmbSir_DropDownClosed(object sender, EventArgs e)
        {
            RadComboBox cmb = sender as RadComboBox;
            ((System.Windows.Data.BindingListCollectionView)cmb.ItemsSource).CustomFilter = "";
        }

        private void btnGenerateNumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string btnname = ((FirstFloor.ModernUI.Windows.Controls.ModernButton)sender).Name;
                DataRowView drv = ((Button)sender).DataContext as DataRowView;
                if (drv != null)
                {
                    System.Data.DataRow dr = drv.Row;

                    if (dr != null && (dr["reg_number"] == System.DBNull.Value || dr["reg_number"] == ""))
                    {

                        //var query = from dog in kinoDataSet.tbl_pedigree
                        //            where dog.pedigree_id == (Int32)dr["pedigree_id"]
                        //            from breed in kinoDataSet.tbl_breed
                        //            where breed.breed_id == dog.breed_id
                        //            join counter in kinoDataSet.tbl_registration_counter on breed.registration_counterID equals counter.registration_counterID
                        //            select new { counter };

                        var query = from breed in kinoDataSet.tbl_breed
                                    where breed.breed_id == (Int32)dr["breed_id"]
                                    join counter in kinoDataSet.tbl_registration_counter on breed.registration_counterID equals counter.registration_counterID
                                    select new { counter };

                        foreach (var item in query)
                        {

                            dr["reg_number"] =item.counter.registration_counter= item.counter.registration_counter + 1 ;
                            drv.EndEdit();
                        }

                    }
                    else
                    {
                        MessageBox.Show("This dog already has number");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in btnGenerateNumber_Click");
            }
        }

        private void grdPedigree_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {

        }

        private void dogRadDataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }

        }

        private void dogRadDataForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            try
            {
                DataRowView drv = (DataRowView)dogRadDataForm.CurrentItem;
                if (drv != null && drv.Row["breed_id"] != DBNull.Value)
                {
                    //FilterColorSource((Int32)drv.Row["breed_id"]);
                    FilterPropertiesCombosByBread((Int32)drv.Row["breed_id"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in dogRadDataForm_BeginningEdit");
            }
        }

        private void dogRadDataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dogRadDataForm.CurrentItem;
                KinoDataSet.tbl_pedigreeRow pr = drv.Row as KinoDataSet.tbl_pedigreeRow;
                if (drv != null)
                {
                    var qry = from p in kinoDataSet.tbl_pedigree
                              where ((p.breed_id == null ? 0 : p.breed_id) == (pr.breed_id == null ? 0 : pr.breed_id))  && ((p["country_codeid"] == DBNull.Value ? 0 : p.country_codeid) == (pr["country_codeid"] == DBNull.Value ? 0 : pr.country_codeid)) && ((p["additional_postfix"] == DBNull.Value ? "" :  p.additional_postfix.Trim()) == (pr["additional_postfix"] == DBNull.Value ? "" : pr.additional_postfix.Trim())) && ((p["reg_number"] == DBNull.Value ? "" : p.reg_number.Trim())  == (pr["reg_number"] == DBNull.Value ? "" : pr.reg_number.Trim())) && ((p["additional_prefix"] == DBNull.Value ? "" : p.additional_prefix.Trim()) == (pr["additional_prefix"] == DBNull.Value ? "" : pr.additional_prefix.Trim())) && p.pedigree_id!=pr.pedigree_id
                              select new { p };
                    foreach (var row in qry)
                    {
                        MessageBox.Show("You have dog with same breed, country and reg number");
                        e.Cancel = true;
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in dogRadDataForm_ValidatingItem");
            }
        }

        private void RadComboBox_DropDownOpened(object sender, EventArgs e)
        {
            RadComboBox cmb = sender as RadComboBox;
            try
            {
                DataRowView drv = (DataRowView)dogRadDataForm.CurrentItem;
                if (drv != null)
                {
                    DataRow dr = drv.Row;
                    Int32 pedigree_id = (Int32)dr["pedigree_id"];
                    Int32 breed_id = (Int32)dr["breed_id"];
                    string fltr = MyCommonDB.GetBreedsByBreed(breed_id);
                    string custfilter = "sex_id=" + 2 + " and breed_id IN (" + fltr + ") and pedigree_id <> " + pedigree_id;
                    ((System.Windows.Data.BindingListCollectionView)cmb.ItemsSource).CustomFilter = custfilter;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in RadComboBox_DropDownOpened");
            }
        }


        private void ShowPedigree_old()
        {
            try
            {
                DataRowView drv = (DataRowView)dogRadDataForm.CurrentItem;
                if (drv != null)
                {
                    DataRow dr = drv.Row;
                    Int32 pedigree_id = (Int32)dr["pedigree_id"];

                    CrystalDecisions.CrystalReports.Engine.ReportDocument Rprt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    string FilePath = null;

                    KinoReportsDataSetTableAdapters.Func_PedigreeInfoByDofIDTableAdapter dta = new KinoReportsDataSetTableAdapters.Func_PedigreeInfoByDofIDTableAdapter();
                    KinoReportsDataSet ds = new KinoReportsDataSet();
                    dta.Fill(ds.Func_PedigreeInfoByDofID, pedigree_id);

                    string reportname = "Pedigree.rpt";


                    FilePath = Common.CurrentPath() + "\\Reports\\" + reportname;
                    Rprt.Load(FilePath, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy);
                    Rprt.Database.Tables[0].SetDataSource((DataTable)ds.Func_PedigreeInfoByDofID);


                    Crystal_rprt_viewer crv = new Crystal_rprt_viewer(Rprt);
                    crv.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in Open pedigree report");
            }
        }
        private void ShowPedigree()
        {
            try
            {
                DataRowView drv = (DataRowView) dogRadDataForm.CurrentItem;
                if (drv != null)
                {
                    DataRow dr = drv.Row;
                    Int32 pedigree_id = (Int32) dr["pedigree_id"];
                    T_ReportViewer wnd = new T_ReportViewer();
                    PedigreeReport report = new PedigreeReport();
                    KinoReportsDataSetTableAdapters.Func_PedigreeInfoByDofIDTableAdapter dta = new KinoReportsDataSetTableAdapters.Func_PedigreeInfoByDofIDTableAdapter();
                    KinoReportsDataSet ds = new KinoReportsDataSet();
                    string selectCommand = "SELECT pedigree_id, breed_name, sex, full_name, name_alias, FullNumber, br_date, color, Owner, Breeder, tatoo, chip_id, death_date, status, details, father_Name, father_Number, father_Details, mother_Name, mother_Number, " +
                         " mother_Details, father_granpa_Name, father_granpa_Number, father_granpa_Details, father_granma_Name, father_granma_Number, father_granma_Details, mother_granpa_Name, mother_granpa_Number,  " +
                         " mother_granpa_Details, mother_granma_Name, mother_granma_Number, mother_granma_Details, father_granpagranpa_Name, father_granpagranpa_Number, father_granpagranpa_Details, father_granpagranma_Name,  " +
                         " father_granpagranma_Number, father_granpagranma_Details, father_granmagranpa_Name, father_granmagranpa_Number, father_granmagranpa_Details, father_granmagranma_Name, father_granmagranma_Number,  " +
                         " father_granmagranma_Details, mother_granpagranpa_Name, mother_granpagranpa_Number, mother_granpagranpa_Details, mother_granpagranma_Name, mother_granpagranma_Number, mother_granpagranma_Details,  " +
                         " mother_granmagranpa_Name, mother_granmagranpa_Number, mother_granmagranpa_Details, mother_granmagranma_Name, mother_granmagranma_Number, mother_granmagranma_Details, tbl_pedigree_champion_status,  " +
                         " Father_Champions, Mother_Champions, Father_granma_Champions, Father_granmagranma_Champions, Father_granmagranpa_Champions, Father_granpa_Champions, Father_granpa_granma_Champions,  " +
                         " Father_granpa_granpa_Champions, Mother_granpa_Champions, Mother_granpagranma_Champions, Mother_granpagranpa_Champions, Mother_granma_Champions, Mother_granmagranma_Champions,  " +
                         " Mother_granmagranpa_Champions, Father_HealtTests, Mother_HealtTests, Father_granma_HealtTests, Father_granmagranma_HealtTests, " +
                         " Father_granmagranpa_HealtTests, Father_granpa_HealtTests, Father_granpa_granma_HealtTests, Father_granpa_granpa_HealtTests, Mother_granpa_HealtTests, Mother_granpagranma_HealtTests, " +
                         " Mother_granpagranpa_HealtTests, Mother_granma_HealtTests, Mother_granmagranma_HealtTests, Mother_granmagranpa_HealtTests, HealtTests, size_type_id, size_type, hair_type_id, hair_type, Breeder_address FROM  dbo.Func_PedigreeInfoByDofID(" + pedigree_id.ToString() + ") AS Func_PedigreeInfoByDofID_1";
                    string connectionString = MyCommonDB.ConnString();// "Data Source=(local)\\SQLEXPRESS;Initial Catalog=AdventureWorks;Integrated Security=True";
                    Telerik.Reporting.SqlDataSource sqlDataSource = new Telerik.Reporting.SqlDataSource(connectionString, selectCommand);
                    report.DataSource = sqlDataSource;

                    //var typeReportSource = new Telerik.Reporting.TypeReportSource();
                    //typeReportSource.TypeName = "ReportsClassLib.PedigreeReport, ReportsClassLib";
                    //wnd.reportViewer1.ReportSource = typeReportSource;

                    //((Telerik.Reporting.Report)wnd.reportViewer1.Report).ReportParameters["ManagerId"].Value = 5;

                    var instanceReportSource = new Telerik.Reporting.InstanceReportSource();

                    // Assigning the Report object to the InstanceReportSource
                    instanceReportSource.ReportDocument = report;
                    wnd.reportViewer1.ReportSource = instanceReportSource;
                    //// Adding the initial parameter values
                    //instanceReportSource.Parameters.Add(new Telerik.Reporting.Parameter("OrderNumber", "SO43659"));

                    //wnd.reportViewer1.Report = report;
                    wnd.reportViewer1.RefreshReport();
                    wnd.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in Open pedigree report");
            }
        }

        private void mnuPedigree_Click(object sender, RadRoutedEventArgs e)
        {
            ShowPedigree();
        }
    }
}
