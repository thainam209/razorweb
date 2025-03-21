using System;
using System.Data;
using System.Data.SqlClient;

namespace webdemo.Data
{
    public class Dbconnect
    {
        string strCon = @"Data Source=DESKTOP-B3GUKF8\THAINAM;Initial Catalog=News;Integrated Security=True"; //chuỗi kết nối
        SqlConnection sqlCon = null; //đối tượng kết nối

        //Hàm kết nối cơ sở dữ liệu

        public Dbconnect()
        {
            this.sqlCon = new SqlConnection(strCon);
        }
        public void OpenCon()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

        }

        //Hàm đóng kết nối
        public void CloseCon()
        {
            if (sqlCon.State == ConnectionState.Open && sqlCon != null) sqlCon.Close();
        }

        public SqlConnection getConnect() { return sqlCon; }
    }
}
