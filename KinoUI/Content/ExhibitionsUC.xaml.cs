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
using CrystalDecisions.CrystalReports.Engine;
using FirstFloor.ModernUI.Windows.Controls;
using KinoDataLibrary;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace KinoUI.Content
{
    /// <summary>
    /// Interaction logic for ExhibitionsUC.xaml
    /// </summary>
    public partial class ExhibitionsUC : UserControl
    {
        KinoDataSet kinoDataSet;
        CollectionViewSource exhibitionViewSource;
        CollectionViewSource countryViewSource;
        CollectionViewSource cityViewSource;

        CollectionViewSource exhcategoryViewSource;
        CollectionViewSource ExhibitionStatusViewSource;

        public ExhibitionsUC()
        {
            InitializeComponent();
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    //kinoDataSet = ((KinoDataSet)(this.FindResource("kinoDataSet")));
                    exhibitionViewSource = ((CollectionViewSource)(this.FindResource("exhibitionViewSource")));
                    countryViewSource = ((CollectionViewSource)(this.FindResource("countryViewSource")));
                    cityViewSource = ((CollectionViewSource)(this.FindResource("cityViewSource")));

                    exhcategoryViewSource = ((CollectionViewSource)(this.FindResource("exhcategoryViewSource")));
                    ExhibitionStatusViewSource = ((CollectionViewSource)(this.FindResource("ExhibitionStatusViewSource")));

                    MyCommonDB.ExhibitionsDT();
                    kinoDataSet = MyCommonDB.KinoDS();

                    //countryViewSource.Source = MyCommonDB.CountryDT();
                    //cityViewSource.Source = MyCommonDB.CityDT();
                    this.DataContext = kinoDataSet;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in ExhibitionsUC ctor");
            }
        }
        public void gridview_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            try
            {
                System.Data.DataRowView drv = e.Row.DataContext as System.Data.DataRowView;
                if (drv != null && (drv.Row["exhibition_name"] == System.DBNull.Value || drv.Row["exhibition_name"] == ""))
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "exhibition_name";
                    validationResult.ErrorMessage = "Name is required, please fill unigue value or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }

                if (drv != null && (drv.Row["country_id"] == System.DBNull.Value || drv.Row["country_id"] == ""))
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "country_id";
                    validationResult.ErrorMessage = "Country is required, select country from list or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }


                if (drv != null && (drv.Row["exhib_categoryID"] == System.DBNull.Value || drv.Row["exhib_categoryID"] == ""))
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "exhib_categoryID";
                    validationResult.ErrorMessage = "Category is required, select category from list or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }


                if (drv != null && (drv.Row["exhib_status"] == System.DBNull.Value || drv.Row["exhib_status"] == ""))
                {
                    Telerik.Windows.Controls.GridViewCellValidationResult validationResult = new Telerik.Windows.Controls.GridViewCellValidationResult();
                    validationResult.PropertyName = "exhib_status";
                    validationResult.ErrorMessage = "Status is required, select status from list or press Escape for reject.";
                    e.ValidationResults.Add(validationResult);
                    e.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message + " in gridview_RowValidating");
            }

        }

        public void mnuSave_Click(object sender, RadRoutedEventArgs e)
        {
            grdExhibitions.CommitEdit();
            MyCommonDB.SaveExhibitions();

        }

        private void mnuDogs_Click(object sender, RadRoutedEventArgs e)
        {
            try
            {
                BBCodeBlock bs = new BBCodeBlock();
                DataRowView drv = grdExhibitions.SelectedItem as DataRowView;
               
                if (drv != null)
                {
                    DataRow dr = drv.Row;
                    //NavigationCommands.GoToPage.Execute("/Content/ExhibitionDogRegistrationUC.xaml", this);
                    //bs.LinkNavigator.Navigate(new Uri("/Content/ExhibitionDogRegistrationUC.xaml", UriKind.Relative), this);//dr["exhibition_id"].ToString()
                    NavigationCommands.GoToPage.Execute(new Uri("/Content/ExhibitionDogRegistrationUC.xaml#exhibition_id=" + dr["exhibition_id"].ToString(), UriKind.Relative), this);
                    //NavigationCommands.

                    //NavigationWindow nav = this.Parent as NavigationWindow;
                    //ExhibitionDogRegistrationUC ctl = new ExhibitionDogRegistrationUC((Int32)dr["exhibition_id"]);
                    //nav.Navigate(ctl);
                    ModernFrame frm = this.Parent as ModernFrame;
                    if (frm!=null)
                        frm.FragmentNavigation += frm_FragmentNavigation;
                    
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in mnuDogs_Click");
            }
        }

        void frm_FragmentNavigation(object sender, FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            //throw new NotImplementedException();
            string param = e.Fragment;
            param = param.Replace("exhibition_id=", "");
            ExhibitionDogRegistrationUC ctl = ((ModernFrame)sender).Content as ExhibitionDogRegistrationUC;
            if (ctl != null)
            {
                ctl.LoadDataContext(Int32.Parse(param));
            }
        }

        private void mnuCatalog_Click(object sender, RadRoutedEventArgs e)
        {
            Int32 exhibition_id=0;
            try
            {

                DataRowView drv = grdExhibitions.SelectedItem as DataRowView;

                if (drv != null)
                {
                    DataRow dr = drv.Row;

                    exhibition_id= (Int32)dr["exhibition_id"];
   

                }


                CrystalDecisions.CrystalReports.Engine.ReportDocument Rprt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string FilePath = null;

                KinoDataLibrary.KinoDataSetTableAdapters.View_CatalogDetailsTableAdapter dta = new KinoDataLibrary.KinoDataSetTableAdapters.View_CatalogDetailsTableAdapter();
                dta.Fill(kinoDataSet.View_CatalogDetails, exhibition_id);
                
                string reportname = "Catalog.rpt";


                FilePath = Common.CurrentPath() + "\\Reports\\" + reportname;
                Rprt.Load(FilePath, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy);
                Rprt.Database.Tables[0].SetDataSource((DataTable)kinoDataSet.View_CatalogDetails);
                //barcode = tr.TicketCode;
                //string BrCdFilePath;
                //if (((DataRow)tr)["Codabar"] == System.DBNull.Value || !tr.Codabar) BrCdFilePath = LibMyCommon.DrawedEANBarCodePathFun(barcode, 200, 50);
                //else BrCdFilePath = LibMyCommon.DrawCodabarToFilePath("A"+ barcode + "A", 200, 50);

                //ReportDocument BarCodeSubRep = default(ReportDocument);
                //BarCodeSubRep = MyCommon.GetSubreportDocument("BarCodeRprt", Rprt);
                //if (BarCodeSubRep != null)
                //{
                //    DataTable ImgDT = MyCommon.ImageTable(BrCdFilePath);
                //    BarCodeSubRep.Database.Tables[0].SetDataSource(ImgDT);
                //}
                //else
                //{
                //    byte[] FileBytes = System.IO.File.ReadAllBytes(BrCdFilePath);
                //    dt.Rows[0]["BarcodePhoto"] = FileBytes;
                //    Rprt.Database.Tables[0].SetDataSource(dt);
                //}

                Crystal_rprt_viewer crv = new Crystal_rprt_viewer(Rprt);
                crv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in mnuCatalog_Click");
            }
        }

        public ReportDocument GetSubreportDocument(string reportObjectName, ReportDocument Report)
        {
            string subreportName = null;
            SubreportObject subreportObject;// = default(SubreportObject);
            ReportDocument subreport = new ReportDocument();

            try
            {
                if ((Report.ReportDefinition.ReportObjects[reportObjectName]) is SubreportObject)
                {
                    subreportObject = Report.ReportDefinition.ReportObjects[reportObjectName] as SubreportObject;
                    subreportName = subreportObject.SubreportName;
                    subreport = Report.OpenSubreport(subreportName);
                    return subreport;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in GetSubreportDocument");
                return null;
            }

        }

        private void mnuReview_Click(object sender, RadRoutedEventArgs e)
        {
            Int32 exhibition_id = 0;
            FormulaFieldDefinitions myFormulas;
            FormulaFieldDefinition orgNameformulafield;
            FormulaFieldDefinition orgLocalNameformulafield;
            ReportDocument subRep;
            try
            {

                DataRowView drv = grdExhibitions.SelectedItem as DataRowView;
                if (drv == null) return;

                DataRow dr = drv.Row;
                exhibition_id = (Int32)dr["exhibition_id"];

                KinoDataSet.tbl_organizationDataTable odt = MyCommonDB.OrganisationDT();// kinoDataSet.tbl_organization;
                KinoDataSet.tbl_organizationRow odr = odt.Rows[0] as KinoDataSet.tbl_organizationRow;
               



                CrystalDecisions.CrystalReports.Engine.ReportDocument Rprt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string FilePath = null;

                KinoDataLibrary.KinoDataSetTableAdapters.View_CatalogDetailsTableAdapter dta = new KinoDataLibrary.KinoDataSetTableAdapters.View_CatalogDetailsTableAdapter();
                dta.Fill(kinoDataSet.View_CatalogDetails, exhibition_id);

                string reportname = "ReviewList.rpt";


                FilePath = Common.CurrentPath() + "\\Reports\\" + reportname;
                Rprt.Load(FilePath, CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy);
                if (odr != null)
                {
                    myFormulas = Rprt.DataDefinition.FormulaFields;
                    orgNameformulafield = myFormulas["OrgName"];
                    orgLocalNameformulafield = myFormulas["OrgLocalName"];



                    orgNameformulafield.Text = "ToText ('" + odr.OrgName + "')";
                    orgLocalNameformulafield.Text = "ToText ('" + odr.LocalOrgName + "')";
                }

                DataTable dt = kinoDataSet.View_CatalogDetails.Clone() as DataTable;
                foreach (DataRow idr in kinoDataSet.View_CatalogDetails.Rows)
                {
                    dt.ImportRow(idr);
                    dt.ImportRow(idr);
                }
                Rprt.Database.Tables[0].SetDataSource(dt);
                //Rprt.SetDataSource(dt);
                KinoDataSet.View_ExhStatusesForReviewListDataTable View_ExhStatusesForReviewListDT = MyCommonDB.View_ExhStatusesForReviewListDT();
                subRep = GetSubreportDocument("ReviewListSub", Rprt);
                if (subRep != null)
                    subRep.Database.Tables[0].SetDataSource((DataTable)View_ExhStatusesForReviewListDT);
                Crystal_rprt_viewer crv = new Crystal_rprt_viewer(Rprt);
                crv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in mnuCatalog_Click");
            }
        }

        private void grdExhibitions_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure to delete operation?", "Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
