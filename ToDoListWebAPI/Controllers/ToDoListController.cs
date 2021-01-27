using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using ToDoListWebAPI.Models;

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
            string query = @"
                    select Id, Nome, Estado from dbo.Lista";
            DataTable table = new DataTable();
            // Defenir uma variável para guardar a informãção que vamos receber da BD
            string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Lista list)
        {
            //string query = @"
            //        insert into dbo.Lista (Nome, Estado) values 
            //        ('"+ list.Nome+@"', '"+list.Estado+@"')
            //        ";
            string query = @"
                   insert into dbo.Lista (Nome, Estado) values
                   (@Nome,@Estado)";
            DataTable table = new DataTable();
            // Defenir uma variável para guardar a informãção que vamos receber da BD
            string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Nome", list.Nome);
                    myCommand.Parameters.AddWithValue("@Estado", list.Estado);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succefully");
        }

        [HttpPut]
        public JsonResult Put(Lista list)
        {
            string query = @"
                   update dbo.Lista 
                   set Nome= @Nome
                   where id= @Id";
            DataTable table = new DataTable();
            // Defenir uma variável para guardar a informãção que vamos receber da BD
            string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Nome", list.Nome);
                    myCommand.Parameters.AddWithValue("@Id", list.Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Succefully");
        }

        [HttpDelete ("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                   delete from dbo.Lista 
                   where id= @Id";
            DataTable table = new DataTable();
            // Defenir uma variável para guardar a informãção que vamos receber da BD
            string sqlDataSource = _configuration.GetConnectionString("ToDoListAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Delete Succefully");
        }
    }
}   
