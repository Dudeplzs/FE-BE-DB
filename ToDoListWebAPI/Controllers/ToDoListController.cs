using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using ToDoListWebAPI.Models;
using ToDoListWebAPI.DAL;
using Microsoft.Extensions.Configuration;


namespace ToDoListWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public ToDoListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {

            DataTable dt = new ToDoListDAL(_configuration).GetData();
            return new JsonResult(dt);
        }



        /* [HttpPost]
         public JsonResult Post(Lista list)
         {
             //string query = @"
             //        insert into dbo.Lista (Nome, Estado) values 
             //        ('"+ list.Nome+@"', '"+list.Estado+@"')
             //        ";
             DataTable table = new DataTable();
             // Defenir uma variável para guardar a informãção que vamos receber da BD
             string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
             using (SqlConnection myCon = new SqlConnection(sqlDataSource))
             try
             {
                 string query = @"
                    insert into dbo.Lista (Nome, Estado) values
                    (@Nome,@Estado)";
                 SqlDataReader myReader;
                     myCon.Open();
                     using (SqlCommand myCommand = new SqlCommand(query, myCon))
                     {
                         myCommand.Parameters.AddWithValue("@Nome", list.Nome);
                         myCommand.Parameters.AddWithValue("@Estado", list.Estado);
                         myReader = myCommand.ExecuteReader();
                         table.Load(myReader);
                         myReader.Close();
                     }
             }
             catch(Exception ex)
             {
                 Console.WriteLine(ex);
             }
             finally {
                 myCon.Close();
             }
             return new JsonResult("Added Succefully");
         }

         [HttpPut]
         public JsonResult Put(Lista list)
         {

             DataTable table = new DataTable();
             // Defenir uma variável para guardar a informãção que vamos receber da BD
             string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
             SqlDataReader myReader;
             using (SqlConnection myCon = new SqlConnection(sqlDataSource))
             try
             {
                 string query = @"
                    update dbo.Lista 
                    set Nome= @Nome, Estado=@Estado
                    where id= @Id";
                 myCon.Open();
                 using (SqlCommand myCommand = new SqlCommand(query, myCon))
                 {
                     myCommand.Parameters.AddWithValue("@Nome", list.Nome);
                     myCommand.Parameters.AddWithValue("@Estado", list.Estado);
                     myCommand.Parameters.AddWithValue("@id", list.id);
                     myReader = myCommand.ExecuteReader();
                     table.Load(myReader);
                     myReader.Close();
                 }
             }
             catch(Exception ex)
             {
                     Console.WriteLine(ex);
             }
             finally
             {
                 myCon.Close();
             }
             return new JsonResult("Updated Succefully");
         }

         [HttpDelete ("{id}")]
         public JsonResult Delete(int id)
         {

             DataTable table = new DataTable();
             // Defenir uma variável para guardar a informãção que vamos receber da BD
             string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
             SqlDataReader myReader;
             using (SqlConnection myCon = new SqlConnection(sqlDataSource))
             try{
                 string query = @"
                    delete from dbo.Lista 
                    where id= @id";
                 myCon.Open();
                 using (SqlCommand myCommand = new SqlCommand(query, myCon))
                 {
                     myCommand.Parameters.AddWithValue("@id", id);
                     myReader = myCommand.ExecuteReader();
                     table.Load(myReader);
                     myReader.Close();
                 }
                 }
                 catch (Exception ex)
                 { 
                     Console.WriteLine(ex);
                 }
                 finally
                 {
                     myCon.Close();
                 }
             return new JsonResult("Delete Succefully");
         }*/
    }
}   
