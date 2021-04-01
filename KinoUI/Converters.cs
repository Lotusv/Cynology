using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using KinoDataLibrary;
using Telerik.Windows.Controls;
using System.Data;
using System.Windows.Media;
using Telerik.Windows.Controls.Data.DataForm;

namespace KinoUI
{
    public class AgeCategoriesByDogSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                DataRowView drv = value as DataRowView;
                if (drv != null)
                {
                    DataRow dr = drv.Row;
                    KinoDataSet.tbl_pedigree_exhibitionDataTable dt = dr.Table as KinoDataSet.tbl_pedigree_exhibitionDataTable;
                    KinoDataSet ds = dt.DataSet as KinoDataSet;

                    if (dr["pedigree_id"] == DBNull.Value)
                    {
                        return null;
                    }
                    Int32 pedigree_id = (Int32)dr["pedigree_id"];
                    return MyCommonDB.Func_AgeGroupsbyPedigreeDT(pedigree_id);
                }
                return null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value) return 1;
            else return 0;
            //return Binding.DoNothing;
        }
    }


    public class RequiredTextboxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == System.DBNull.Value || value == "") return System.Windows.Media.Brushes.Red;
            else
            {
                return System.Windows.Media.Brushes.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RequiredComboboxConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == System.DBNull.Value || value == null || value == "")
            {
                return System.Windows.Media.Brushes.Red;
            }
            else
                return System.Windows.Media.Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DataFormModeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //return true;
            StackPanel stpnl = ((TextBox)value).Parent as StackPanel;
            //var template = stpnl.Template;
            //FrameworkElement parent = VisualTreeHelper.GetParent(stpnl) as FrameworkElement;
            var parent = VisualTreeHelper.GetParent(stpnl);
            RadDataForm rdfrm = null;
            while (!(parent is RadDataForm))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent != null)
                rdfrm = (RadDataForm)parent;
            if (rdfrm.Mode == Telerik.Windows.Controls.Data.DataForm.RadDataFormMode.ReadOnly) return true;
            else
                return false;

            //Telerik.Windows.Controls.DataFormDataField fdatfld = ((TextBox)value).Parent as Telerik.Windows.Controls.DataFormDataField;
            //if (fdatfld.Mode == Telerik.Windows.Controls.Data.DataForm.RadDataFormMode.ReadOnly) return true;
            //else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //
    public class CountryVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == null || values[1] == null || values[2] == null || values[3] == null || values[4] == null) return Visibility.Collapsed;

            Int32 _orgCountry= (Int32)values[0];
            DataRowView drv=    values[1] as DataRowView;
            String _number = values[2].ToString();
            Int32 _status = (Int32)values[4];
            if (_number != "") 
                return Visibility.Collapsed;
            if (_status.ToString() != "3") 
                return Visibility.Collapsed;

            KinoDataSet.tbl_organizationDataTable organisationDT;//
            organisationDT = MyCommonDB.OrganisationDT();//= MyCommonDB.OrganisationDT();
            KinoDataSet.tbl_organizationRow dr = organisationDT.Rows[0] as KinoDataSet.tbl_organizationRow;
            if (dr == null) return Visibility.Collapsed;
            if (dr.country_id == _orgCountry)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;

        }

        public object[] ConvertBack(object value, Type[] targetTypes,
       object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }


    //public class CountryVisibilityConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        Int32 _orgCountry;
    //        if (value == DBNull.Value)
    //        {
    //            return Visibility.Collapsed;
    //        }
    //        else
    //        {
    //            KinoDataSet.tbl_organizationDataTable OrganisationDT = MyCommonDB.OrganisationDT();
    //            KinoDataSet.tbl_organizationRow dr = OrganisationDT.Rows[0] as KinoDataSet.tbl_organizationRow;
    //            if (dr.country_id == (Int32)value)
    //                return Visibility.Visible;
    //            else
    //                return Visibility.Collapsed;
    //        }
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }

    //}

    public class IsNotEnabledDataFormConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var mode = (RadDataFormMode)value;
            return (mode == RadDataFormMode.ReadOnly);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class IsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var mode = (RadDataFormMode)value;
            return !(mode == RadDataFormMode.ReadOnly);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class IsReadOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var mode = (RadDataFormMode)value;
            return (mode == RadDataFormMode.ReadOnly);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public struct ParentStructure
    {
        public string Descr { get; set; }

        public Int32 pedigree_id { get; set; }
    }


    public class ParentsSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                //View_DogsWithExtendedDescription
                int _gender = int.Parse(parameter.ToString());
                DataRowView drv = value as DataRowView;
                if (drv != null)
                {
                    DataRow dr = drv.Row;
                    KinoDataSet.tbl_pedigreeDataTable dt = dr.Table as KinoDataSet.tbl_pedigreeDataTable;
                    KinoDataSet ds = dt.DataSet as KinoDataSet;

                    if (dr["pedigree_id"] == DBNull.Value)
                    {
                        return ds.tbl_pedigree;
                    }
                    Int32 pedigree_id = (Int32)dr["pedigree_id"];
                    Int32 breed_id = (Int32)dr["breed_id"];


                        string fltr = MyCommonDB.GetBreedsByBreed(breed_id);
                    string fltr2 = MyCommonDB.GetChildsByDog(pedigree_id);
                    
                        
                        DataRow[] drs = ds.View_DogsWithExtendedDescription.Select("sex_id=" + _gender + " and breed_id IN (" + fltr + ") and pedigree_id <> " + pedigree_id + " and pedigree_id not in (" + fltr2 + ")");
                        return drs.ToList<DataRow>();

                        //System.Collections.ObjectModel.ObservableCollection<ParentStructure> _col = new System.Collections.ObjectModel.ObservableCollection<ParentStructure>();
                        //foreach (DataRow item in drs)
                        //{
                        //    ParentStructure str = new ParentStructure();
                        //    str.Descr = item["Descr"].ToString();
                        //    str.pedigree_id = (Int32)item["pedigree_id"];
                        //    _col.Add(str);
                        //}
                        //return _col;


                    //var query = ds.tbl_pedigree.AsEnumerable().Where(r => r.Field<Int32>("pedigree_id") == pedigree_id);
                    //foreach (var item in query)
                    //{
                    //    DataRow idr = item as DataRow;
                    //    Int32 persid = 0;
                    //    Int32 persid2 = 0;
                    //    if (idr["owner_id"] != DBNull.Value)
                    //        persid = (Int32)idr["owner_id"];
                    //    if (idr["coowner_id"] != DBNull.Value)
                    //        persid2 = (Int32)idr["coowner_id"];

                    //    DataRow[] drs = ds.tbl_person.Select("person_id=" + persid + " or person_id=" + persid2);
                    //    return drs.ToList<DataRow>();
                    //}
                }
                return null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value) return 1;
            else return 0;
            //return Binding.DoNothing;
        }
    }

    public class OwnersByDogConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                DataRowView drv = value as DataRowView;
                if (drv != null)
                {
                    DataRow dr = drv.Row;
                    KinoDataSet.tbl_pedigree_exhibitionDataTable dt = dr.Table as KinoDataSet.tbl_pedigree_exhibitionDataTable;
                    KinoDataSet ds = dt.DataSet as KinoDataSet;
                    
                    if (dr["pedigree_id"] == DBNull.Value)
                    {
                        return ds.tbl_person;
                    }
                    Int32 pedigree_id = (Int32)dr["pedigree_id"];

                    var query = ds.tbl_pedigree.AsEnumerable().Where(r => r.Field<Int32>("pedigree_id") == pedigree_id);
                    foreach (var item in query)
                    {
                        DataRow idr = item as DataRow;
                        Int32 persid=0;
                        Int32 persid2 = 0;
                        if(idr["owner_id"]!=DBNull.Value)
                            persid = (Int32)idr["owner_id"];
                        if (idr["coowner_id"] != DBNull.Value)
                            persid2 = (Int32)idr["coowner_id"];

                        DataRow[] drs = ds.tbl_person.Select("person_id=" + persid + " or person_id=" + persid2);
                        return drs.ToList<DataRow>();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value) return 1;
            else return 0;
            //return Binding.DoNothing;
        }
    }

    public class IntToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                int val = int.Parse(value.ToString());
                if (val != 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value) return 1;
            else return 0;
            //return Binding.DoNothing;
        }
    }

    public class ParentNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value.ToString() != "")
                {
                    int val = int.Parse(value.ToString());
                    if (val != 0)
                    {
                        MyCommonDB.PedigreeDT();
                        KinoDataSet kinoDataSet = MyCommonDB.KinoDS();
                        var query = kinoDataSet.tbl_pedigree.AsEnumerable().Where(r => r.Field<Int32>("pedigree_id") == val);
                        foreach (var item in query)
                        {
                            DataRow dr = item as DataRow;
                            return dr["full_name"].ToString();
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class HalfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Double wdt = (Double)value/2;
            //return wdt;


            //FontFamily fontFamily = new FontFamily("Arial");
            //double fontDpiSize = 14;

            //double fontHeight = Math.Ceiling(fontDpiSize * fontFamily.LineSpacing);

            Double wdt=50;
            //RectangleViewModel rectvm = ((System.Windows.Controls.Border)value).DataContext as RectangleViewModel;
            //if (rectvm == null)
            //{
            //    System.Windows.Controls.Primitives.Thumb thumb = ((System.Windows.Controls.Border)value).DataContext as System.Windows.Controls.Primitives.Thumb;
            //    rectvm = thumb.DataContext as RectangleViewModel;
            //}
            //string txt;

            //if (rectvm.TableDataRow.Table.Columns["OrderNumber"] != null && rectvm.TableDataRow["OrderNumber"] != System.DBNull.Value && rectvm.TableDataRow["OrderNumber"].ToString() != "")
            //    txt = rectvm.TableCode + " - " + rectvm.TableDataRow["OrderNumber"].ToString();
            //else
            //    txt = rectvm.TableCode;
            //FormattedText ft = new FormattedText(txt,
            //                        CultureInfo.CurrentCulture,
            //                        CultureInfo.CurrentCulture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight,
            //                        new Typeface("Arial"),
            //                        14,
            //                        new SolidColorBrush(Colors.White));
            //int txtlengt = ft.Text.Length;
            //Double fontHeight = ft.Height;
            //Double fontWidth = ft.Width;
            //if (parameter.ToString() == "w")
            //{
            //    wdt = (rectvm.Width / 2) - (fontWidth / 2);
            //}
            //else
            //{
            //    wdt = (rectvm.Height / 2) - (fontHeight / 2);
            //}
            return wdt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BreedColorImageConverter : IMultiValueConverter
    {

        public object Convert(object[] value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            KinoUI.Content.SelectBreedDlg wnd=null;
            wnd = value[1] as KinoUI.Content.SelectBreedDlg;

            //if (wnd != null && value[0] != null)
            //{
            //    int breedname = (int)wnd.cmbBreedName.SelectedValue;
            //    int colortype = (int)value[0];

            //        BitmapImage image = new BitmapImage();
            //        image.BeginInit();
            //        //if (value != null && value is byte[])
            //        //{
            //        //    byte[] bytes = value as byte[];
            //        //    MemoryStream stream = new MemoryStream(bytes);
            //        //    image.StreamSource = stream;
            //        //    // image.EndInit();
            //        //}
            //        //else
            //        //{
            //        image.UriSource = new Uri("images/Users_icon.png", UriKind.RelativeOrAbsolute);
            //        //}
            //        image.EndInit();
            //        return image;


            //}
            
            return null;

        }
        public object[] ConvertBack(
                object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
