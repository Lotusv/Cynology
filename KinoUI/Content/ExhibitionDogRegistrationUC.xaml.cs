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
    /// Interaction logic for ExhibitionDogRegistrationUC.xaml
    /// </summary>
    public partial class ExhibitionDogRegistrationUC : UserControl
    {
        KinoDataSet kinoDataSet;
        Int32 _ExhibitionID;
        CollectionViewSource exhibitionDogViewSource;
        

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

        #endregion                                   //

        public ExhibitionDogRegistrationUC()
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
        }

        void _timer_Tick(String _from)
        {

            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);

            _currentElapsedTime = timeSinceStartTime - _currentElapsedTime;

            System.Diagnostics.Debug.WriteLine(_from + " _currentElapsedTime: " + _currentElapsedTime.ToString());
            System.Diagnostics.Debug.WriteLine(_from + "timeSinceStartTime: " + timeSinceStartTime.ToString());
        }

        public void LoadDataContext(Int32 ExhibitionID)
        {
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    _ExhibitionID = ExhibitionID;
                    //exhibitionDogViewSource = ((CollectionViewSource)(this.FindResource("exhibitionDogViewSource")));
                    //exhibitionDogViewSource.View.CurrentChanged += View_CurrentChanged;
                    MyCommonDB.Pedigree_exhibitionDT(ExhibitionID);
                    kinoDataSet = MyCommonDB.KinoDS();

                    this.DataContext = kinoDataSet;
                    kinoDataSet.tbl_pedigree_exhibition.TableNewRow += tbl_pedigree_exhibition_TableNewRow;
                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in ExhibitionDogRegistrationUC ctor");
            }

        }

        void tbl_pedigree_exhibition_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            //throw new NotImplementedException();
            KinoDataSet.tbl_pedigree_exhibitionRow dr = e.Row as KinoDataSet.tbl_pedigree_exhibitionRow;
            if (dr != null)
            {
                dr.exhibition_id = _ExhibitionID;
                dr.registration_date = DateTime.Now;
                dr.ring_ordernumber = 0;
                dr.registrationNumber = 0;
                dr.registration_status = 0;

            }
        }

        void View_CurrentChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        public ExhibitionDogRegistrationUC(Int32 ExhibitionID)
        {
#if DEBUG
            _timer_Tick("before InitializeComponent");                            //
#endif
            InitializeComponent();
#if DEBUG
            _timer_Tick("after InitializeComponent");                            //
#endif
            _ExhibitionID = ExhibitionID;
#if DEBUG
            _timer_Tick("before LoadDataContext");                            //
#endif
            LoadDataContext(ExhibitionID);
#if DEBUG
            _timer_Tick("after LoadDataContext");                            //
#endif
        }



        public void gridview_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            try
            {
                System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
                if (drv != null )
                {

                    if (drv.Row["pedigree_id"] == System.DBNull.Value || drv.Row["pedigree_id"] == "")
                    {
                        Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                        validationResult.PropertyName = "pedigree_id";
                        validationResult.ErrorMessage = "Dog is required, please fill unigue value or press Escape for reject.";
                        e.ValidationResults.Add(validationResult);
                        e.IsValid = false;
                    }
                    else
                    {

                        Int32 pedigree_id = (Int32)drv.Row["pedigree_id"];
                        Int32 exh_dogID = (Int32)drv.Row["exh_dogID"];
                        var query = kinoDataSet.tbl_pedigree_exhibition.AsEnumerable().Where(r => r.Field<Int32>("pedigree_id") == pedigree_id & r.Field<Int32>("exh_dogID") != exh_dogID);
                        foreach (var item in query)
                        {

                                Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                                validationResult.PropertyName = "pedigree_id";
                                validationResult.ErrorMessage = "Dog is unique for exhibition, please fill unigue value or press Escape for reject.";
                                e.ValidationResults.Add(validationResult);
                                e.IsValid = false;
                                break;


                        }

                    }

                    //if (drv != null && (drv.Row["registrationNumber"] == System.DBNull.Value || drv.Row["registrationNumber"] == ""))
                    //{
                    //    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    //    validationResult.PropertyName = "registrationNumber";
                    //    validationResult.ErrorMessage = "Registration number is required, enter value or press Escape for reject.";
                    //    e.ValidationResults.Add(validationResult);
                    //    e.IsValid = false;
                    //}


                    //if (drv != null && (drv.Row["agegroup_id"] == System.DBNull.Value || drv.Row["agegroup_id"] == ""))
                    //{
                    //    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    //    validationResult.PropertyName = "agegroup_id";
                    //    validationResult.ErrorMessage = "agegroup is required, select  from list or press Escape for reject.";
                    //    e.ValidationResults.Add(validationResult);
                    //    e.IsValid = false;
                    //}

                    //if (drv != null && (drv.Row["person_id"] == System.DBNull.Value || drv.Row["person_id"] == ""))
                    //{
                    //    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    //    validationResult.PropertyName = "person_id";
                    //    validationResult.ErrorMessage = "Owner is required, select  from list or press Escape for reject.";
                    //    e.ValidationResults.Add(validationResult);
                    //    e.IsValid = false;
                    //}

                    //if (drv != null && (drv.Row["registration_status"] == System.DBNull.Value || drv.Row["registration_status"] == ""))
                    //{
                    //    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    //    validationResult.PropertyName = "registration_status";
                    //    validationResult.ErrorMessage = "Status is required, select  from list or press Escape for reject.";
                    //    e.ValidationResults.Add(validationResult);
                    //    e.IsValid = false;
                    //}


                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in gridview_RowValidating");
            }

        }

        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            grdDogExhibitions.CommitEdit();
            MyCommonDB.SaveDogExhibitions();
        }

        private void dogExhibitionsUC_Loaded(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show(_ExhibitionID.ToString());            
        }

        private void grdDogExhibitions_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {

        }

        private void grdDogExhibitions_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            //if (e.Cell.Column.UniqueName == "registration_status")
            //{
            //    Telerik.Windows.Controls.GridView.GridViewRowItem rowitem =e.Cell.ParentRow;
            //    DataRowView drv = rowitem.DataContext as DataRowView;
            //    if (drv != null)
            //    {
            //        if (drv.Row["registrationNumber"] == System.DBNull.Value || (int)drv.Row["registrationNumber"] == 0)
            //        {
            //            drv.Row["registrationNumber"] = MyCommonDB.GetExhibitionRegistrationCount(_ExhibitionID);
            //        }
            //    }
            //}
        }

        private void grdDogExhibitions_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {

            KinoDataLibrary.KinoDataSet.tbl_pedigree_exhibitionRow pdr = ((KinoDataLibrary.KinoDataSet.tbl_pedigree_exhibitionRow)((System.Data.DataRowView)((Telerik.Windows.Controls.GridView.GridViewRowItem)e.Row).Item).Row);

                Telerik.Windows.Controls.GridView.GridViewRowItem rowitem = e.Cell.ParentRow;
                DataRowView drv = rowitem.DataContext as DataRowView;
                if (drv != null)
                {
                    if (e.Cell.Column.UniqueName == "pedigree_id")
                    {
                        //if (pdr.pedigree_id == null)  //e.NewValue==null)
                        //{
                        //    e.IsValid = false;
                        //    e.ErrorMessage = "Dog is mandatory.";
                        //}

                            if (drv.Row["pedigree_id"] == System.DBNull.Value || (int)drv.Row["pedigree_id"] == 0)
                            {
                                e.IsValid = false;
                                e.ErrorMessage = "Dog is mandatory.";
                            }

                    }

                    if (e.Cell.Column.UniqueName == "agegroup_id")
                    {
                        //if (pdr.agegroup_id == null)//e.NewValue == null)
                        //{
                        //    e.IsValid = false;
                        //    e.ErrorMessage = "Age group is mandatory.";
                        //}
                        if (drv.Row["agegroup_id"] == System.DBNull.Value || (int)drv.Row["agegroup_id"] == 0)
                        {
                            e.IsValid = false;
                            e.ErrorMessage = "Age group  is mandatory.";
                        }
                    }
                }

        }

        private void grdDogExhibitions_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
