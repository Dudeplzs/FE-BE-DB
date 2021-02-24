using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using ToDoListWebAPI.Models;
using ToDoListWebAPI.DAL;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections;
using System.Linq;
using ToDoListWebAPI.Repository;
using ToDoListWebAPI.Services;
using System.Threading.Tasks;

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

        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet("General")]
        [Authorize(Roles = "Standard, Admin")]
        public string Employee() => "Funcionário";


        [HttpGet("Admin")]    
        [Authorize(Roles = "Admin")]
        public JsonResult GetUsers()
        {
            DataTable loginUsers = new UserDAL(_configuration).GetAllUsers();
            return new JsonResult(loginUsers);
        }

        [HttpGet]
        public JsonResult Get()
        {

            DataTable dt = new ToDoListDAL(_configuration).GetData();
            return new JsonResult(dt);
        }


        //[HttpGet("Users")]
        //public JsonResult CheckLogging()
        //{
        //    DataTable loggingUsers = new UserDAL(_configuration).GetLoggingUsers();
        //    return new JsonResult(loggingUsers);
        //}

        [HttpPost]
        public JsonResult Post(Lista list)
        {
            string added = new ToDoListDAL(_configuration).Added(list);
            return new JsonResult(added);
        }

        [Authorize]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(AppUser model)
        {
            AppUser user = new UserDAL(_configuration).Get(model);

            if (user == null)
                return NotFound(new { message = "Email, Username ou Password incorrectos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }


        public JsonResult CreateLogging(AppUser log)
        {
            string createLogging = new UserDAL(_configuration).CreateUser(log);
            return new JsonResult(createLogging);
        }

        [HttpPut]
        public JsonResult Put(Lista list)
        {
            string updated = new ToDoListDAL(_configuration).Updated(list);
            return new JsonResult(updated);
        }

        [HttpPut("Users")]
        public JsonResult EditUSer(AppUser log)
        {
            string editUser = new UserDAL(_configuration).EditUser(log);
            return new JsonResult(editUser);
        }

        [HttpDelete("{id}")]
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
