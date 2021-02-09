using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using ToDoListWebAPI.Models;
using ToDoListWebAPI.DAL;
using Microsoft.Extensions.Configuration;


namespace ToDoListWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
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
        [HttpGet("Users")]
        public JsonResult CheckLogging()
        {
            DataTable loggingUsers = new UserDAL(_configuration).GetLoggingUsers();
            return new JsonResult(loggingUsers);
        }

        [HttpPost]
         public JsonResult Post(Lista list)
         {
            string added = new ToDoListDAL(_configuration).Added(list);
            return new JsonResult(added);
        }
        [HttpPost("Users")]
        public JsonResult CreateLogging(Logging log)
        {
            string createLogging = new UserDAL(_configuration).CreateLogging(log);
            return new JsonResult(createLogging);
        }

        [HttpPut]
        public JsonResult Put(Lista list)
        {
            string updated = new ToDoListDAL(_configuration).Updated(list);
            return new JsonResult(updated);
        }

        [HttpPut("USers")]
        public JsonResult EditUSer(Logging log)
        {
            string editUser = new UserDAL(_configuration).EditUser(log);
            return new JsonResult(editUser);
        }

        [HttpDelete ("{id}")]
        public JsonResult Delete(int id)
        {
            string deleted = new ToDoListDAL(_configuration).Delete(id);
            return new JsonResult(deleted);
        }

        [HttpDelete("Users/{id}")]
        public JsonResult DeleteUser(int id)
        {
            string userDelete = new UserDAL(_configuration).DeleteUser(id);
            return new JsonResult(userDelete);
        }
    }
}   
