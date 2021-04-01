using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


//KinoDataSetTableAdapters


namespace KinoDataLibrary.KinoDataSetTableAdapters 
{
    public partial class tbl_cityTableAdapter
    {
        public SqlDataAdapter SQLAdapter
        {
            get
            {
                return Adapter;
            }
        }

        public SqlConnection DBConnection
        {
            get
            {
                return Connection;
            }
            set
            {
                this._connection = value;
            }
        }
    }
}

namespace KinoDataLibrary
{


    public class ImageByte
    {
        public Byte[] Photo { get; set; }
    }

    public partial class KinoDataSet
    {
        public partial class tbl_breedRow
        {
            public List<ImageByte> AllImages {
                get
                {
                    List<ImageByte> imgs = new List<ImageByte>();
                    if (((DataRow)this)["breed_photo"] != System.DBNull.Value) imgs.Add( new ImageByte { Photo=this.breed_photo });
                    var p = from a in this.Gettbl_breed_sizetypeRows().AsEnumerable()
                            where !a.IsphotoNull()
                                       select a;
                    foreach (var i in p)
                    {
                        imgs.Add(new ImageByte { Photo = i.photo });
                    }

                    var p1 = from a in this.Gettbl_breed_colortypeRows().AsEnumerable()
                            where !a.IsphotoNull()
                            select a;
                    foreach (var i in p1)
                    {
                        imgs.Add(new ImageByte { Photo = i.photo });
                    }

                    var p2 = from a in this.Gettbl_breed_hairtypeRows().AsEnumerable()
                            where !a.IsphotoNull()
                            select a;
                    foreach (var i in p2)
                    {
                        imgs.Add(new ImageByte { Photo = i.photo });
                    }
                    return imgs;
                }
            }
        }


        //public partial class tbl_breedRow : INotifyPropertyChanged
        //{
        //    public event PropertyChangedEventHandler PropertyChanged;

        //    protected void OnPropertyChanged(string name)
        //    {
        //        PropertyChangedEventHandler handler = PropertyChanged;
        //        if (handler != null)
        //        {
        //            handler(this, new PropertyChangedEventArgs(name));
        //        }
        //    }

        //    public string HairTypes
        //    {
        //        get
        //        {
        //            string res = "Hair Types: ";
        //            DataRow[] childs = this.GetChildRows("FK_tbl_breed_hairtype_tbl_breed");
        //            if (childs.Length > 0)
        //            {
        //                foreach (tbl_breed_hairtypeRow dr in childs)
        //                {
        //                    res += dr.tbl_hair_typeRow.hair_type.ToString();
        //                }
        //            }
        //            return res;
        //        }
        //        //set { }
        //    }
        //}

        //public partial class tbl_breed_hairtypeRow : INotifyPropertyChanged
        //{
        //    public event PropertyChangedEventHandler PropertyChanged;

        //    protected void OnPropertyChanged(string name)
        //    {
        //        PropertyChangedEventHandler handler = PropertyChanged;
        //        if (handler != null)
        //        {
        //            handler(this, new PropertyChangedEventArgs(name));
        //        }
        //    }


        //}


        //public partial class tbl_breed_hairtypeDataTable
        //{
        //    public event PropertyChangedEventHandler PropertyChanged;

        //    protected void OnPropertyChanged(string name)
        //    {
        //        PropertyChangedEventHandler handler = PropertyChanged;
        //        if (handler != null)
        //        {
        //            handler(this, new PropertyChangedEventArgs(name));
        //        }
        //    }


        //}

    }
}
