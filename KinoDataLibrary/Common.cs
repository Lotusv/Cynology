using System;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;

using System.Windows.Forms;
using System.Xml;
using System.Globalization;
using System.Runtime.InteropServices;

using System.Collections;

using System.Data.SqlClient;
using System.Text;

namespace KinoDataLibrary
{
    public static class Common
    {
        public static string _connstr = "Data Source=62.212.231.217\\RISK_GEEE_server,9913;Initial Catalog=Kino;Persist Sec" +
            "urity Info=True;User ID=kino;Password=kino;Connect Timeout=300";

       
        public static string SQLConnectionString()
        {
            return _connstr;
        }

        //public static byte[] BreedColorPhoto(int breednameid, int colortypeid)
        //{
        //    string sql = "";
        //}

        public static void SetSQLConnectionString(string arg)
        {
            _connstr = arg;
        }

        public static string CurrentPath()
        {
            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string str = appPath.Replace(@"file:\", "");
            return str;
        }

        public static decimal StringToInvariantDecimal(string Param)
        {
            try
            {
                string str = Param.Replace(",", ".");
                return Decimal.Parse(str, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable XmlToDataTableConverter(string xmlString)
        {
            var mySourceDoc = new XmlDocument();
            try
            {
                if (!String.IsNullOrEmpty(xmlString))
                {
                    //load the file from the stream
                    mySourceDoc.LoadXml(xmlString);
                    var xmlt = XmlReader.Create(new StringReader(mySourceDoc.OuterXml));
                    var ds = new DataSet();
                    var dt = new DataTable();
                    ds.ReadXml(xmlt);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables["document"];
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static DataTable GetDataTableBySql(string strSQL)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(SQLConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL, sqlcon))
                    {
                        sqlcon.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            dt = new System.Data.DataTable();
                            dt.Load(rdr);
                            sqlcon.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in GetDataTableBySQL");
            }
            return dt;
        }

        
    }
}


