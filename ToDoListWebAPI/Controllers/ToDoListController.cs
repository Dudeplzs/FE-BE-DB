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

        [HttpPost]
         public JsonResult Post(Lista list)
         {
            string added = new ToDoListDAL(_configuration).Added(list);
            return new JsonResult(added);
        }

        [HttpPut]
        public JsonResult Put(Lista list)
        {
            string updated = new ToDoListDAL(_configuration).Updated(list);
            return new JsonResult(updated);
        }

        [HttpDelete ("{id}")]
        public JsonResult Delete(int id)
        {
            string deleted = new ToDoListDAL(_configuration).Delete(id);
            return new JsonResult(deleted);
        }
    }
}   
