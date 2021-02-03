using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ToDoListWebAPI.Models;

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

            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            DataTable table = new DataTable();
            try
            {
                string query = @"select * from dbo.Lista";
                SqlCommand cmd = new SqlCommand(query, myConn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                myConn.Open();
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConn.Close();
            }
            return table;
        }
        #endregion

        #region Insert Data - Post
        public string Added(Lista list)
        {
            string result = "";
            bool isAdded = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string query = @"insert into dbo.Lista (Nome, Estado) values 
                                (@Nome,@Estado)";
                SqlCommand cmd = new SqlCommand(query,myConn);
                cmd.Parameters.AddWithValue("@Nome", list.Nome);
                cmd.Parameters.AddWithValue("@Estado", list.Estado);
                myConn.Open();

                // ExecuteNonQuery valida quantas rows foram afectadas
                int rows = cmd.ExecuteNonQuery();

                isAdded = rows > 0;

                result = (isAdded == true) ? "Added Successfully" : "Can't Add Element";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                myConn.Close();
            }
            return result;            
        }
        #endregion

        #region Update Data - Put
        public string Updated(Lista list)
        {
            string result = "";
            bool isUpdated = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string query = @"
                    update dbo.Lista 
                    set Nome = @Nome, Estado = @Estado
                    where id = @id";
                SqlCommand cmd = new SqlCommand(query, myConn);
                cmd.Parameters.AddWithValue("@Nome", list.Nome);
                cmd.Parameters.AddWithValue("@Estado", list.Estado);
                cmd.Parameters.AddWithValue("@id", list.id);
                myConn.Open();

                //ExecuteNonQuery valida quantas rows foram afectadas
                int rows = cmd.ExecuteNonQuery();

                isUpdated = rows > 0;

                result = (isUpdated == true) ? "Updated Successfully" : "Can't Update Element";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                myConn.Close();
            }
            return result;
        }
        #endregion

        #region Delete Data - Delete
        public string Delete(int id)
        {
            string result= "";
            bool isDeleted = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string query = @"
                    delete from dbo.Lista 
                    where id= @id";
                SqlCommand cmd = new SqlCommand(query, myConn);
                cmd.Parameters.AddWithValue("@id", id);
                myConn.Open();

                //ExecuteNonQuery valida quantas rows foram afectadas
                int rows = cmd.ExecuteNonQuery();

                isDeleted = rows > 0;
                result = (isDeleted == true) ? "Deleted Successfully" : "Can't Delete Element";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                myConn.Close();
            }
            return result;
        }
        #endregion
    }
}
