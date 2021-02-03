using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ToDoListWebAPI.DAL
{
    public class ToDoListDAL
    {
        private readonly IConfiguration _configuration;
        public ToDoListDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Select Data - GetData()
        public DataTable GetData()
        {

            var myConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            //GetConnectionString("ConnectionStrings:ToDoListAppCon");
            SqlConnection myCon = new SqlConnection(myConn);
            DataTable table = new DataTable();
            try
            {
                string query = @"select * from dbo.Lista";
                SqlCommand cmd = new SqlCommand(query, myCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                myCon.Open();
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myCon.Close();
            }
            return table;
        }
        #endregion
    }
}
