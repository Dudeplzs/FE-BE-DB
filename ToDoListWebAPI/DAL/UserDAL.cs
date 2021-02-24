using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using ToDoListWebAPI.Models;

namespace ToDoListWebAPI.DAL
{
    public class UserDAL
    {
        private readonly IConfiguration _configuration;
        public UserDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Check User -> Exists or Not
        public bool UserExist(string email, string username, string password)
        {
            bool isuserExist = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string checkUserQuery = @"select count(*) from dbo.Users
                                     where Email = @Email
                                     and Username = @Username
                                     and Password = @Password";

                SqlCommand cmd = new SqlCommand(checkUserQuery, myConn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                myConn.Open();

                int UserExist = (int)cmd.ExecuteScalar();

                if (UserExist > 0)
                {
                    isuserExist = true;
                    return isuserExist;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConn.Close();
            }
            return isuserExist;
        }
        #endregion

        #region Check User Role
        public string UserRole(string Email)
        {
            string Role = "";
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string checkUserQuery = @"select Roles from dbo.Users
                                     where Email = @Email";

                SqlCommand cmd = new SqlCommand(checkUserQuery, myConn);
                cmd.Parameters.AddWithValue("@Email", Email);
                myConn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Role = (string)reader.GetValue(0);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConn.Close();
            }
            return Role;
        }
        #endregion

        #region Get User for Login
        public AppUser Get(AppUser _user)
            {
            var users = new List<AppUser>();
            string role = UserRole(_user.Email);

            if (!UserExist(_user.Email, _user.Username, _user.Password))
            {
                return null;
            }
            users.Add(new AppUser { Email = _user.Email, Username = _user.Username, Password = _user.Password, Roles = role });
            return users.Where(x => x.Username == x.Username && x.Password == x.Password).FirstOrDefault();

        }
        #endregion

        #region Create User - Post
        public string CreateUser(AppUser log)
        {
            string result = "";
            bool isCreated = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string createQuery = @"insert into dbo.Users (Email, Username, Password, Roles) values 
                                (@Email, @Username, @Password, @Roles)";

                SqlCommand cmd = new SqlCommand(createQuery, myConn);
                cmd.Parameters.AddWithValue("@Email", log.Email);
                cmd.Parameters.AddWithValue("@Username", log.Username);
                cmd.Parameters.AddWithValue("@Password", log.Password);
                cmd.Parameters.AddWithValue("@Roles", "Standard");

                string checkEmail = @"select count(*) from dbo.Users
                                    where Email = @Email";

                SqlCommand email_check_cmd = new SqlCommand(checkEmail, myConn);
                email_check_cmd.Parameters.AddWithValue("@Email", log.Email);

                myConn.Open();

                int UserExist = (int)email_check_cmd.ExecuteScalar();

                int rows = cmd.ExecuteNonQuery();

                isCreated = rows > 0;

                // Se o UserExit for 0 significa que não existe nenhum Email, logo pode ser criado um novo!!
                result = (UserExist == 0 && log.Email != null) ? "User was created successfully" : "Can't create User";

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

        #region Get All Users
        public DataTable GetAllUsers()
        {
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            DataTable table = new DataTable();
            try
            {
                string getUsersQuery = @"select * from dbo.Users";
                SqlCommand cmd = new SqlCommand(getUsersQuery, myConn);
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

        #region Edit User - Put
        public string EditUser(AppUser log)
        {
            string result = "Can't Update User";
            bool isEdited = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string editUserQuery = @" 
                                        update dbo.Users
                                        set User_Type = @User_Type
                                        where Email = @Email";
                SqlCommand cmd = new SqlCommand(editUserQuery, myConn);
                cmd.Parameters.AddWithValue("@Email", log.Email);
                cmd.Parameters.AddWithValue("@User_Type", log.Roles);

                string checkEmail = @"select count(*) from dbo.Users
                                    where Email = @Email";
                SqlCommand email_check_cmd = new SqlCommand(checkEmail, myConn);
                email_check_cmd.Parameters.AddWithValue("@Email", log.Email);

                myConn.Open();

                int UserExist = (int)email_check_cmd.ExecuteScalar();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0) 
                {
                    isEdited = true;
                }
                isEdited = rows > 0;

                if (UserExist == 0)
                {
                    result = "Your Email doens't exist!! Check it!";
                }
                else
                {
                    result = "Updated Successfully";
                }
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

        #region Delete Users - Delete
        public string DeleteUser(int id)
        {
            string result = "";
            bool isDeleted = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string deleteUSerquery = @"
                    delete from dbo.Users 
                    where id= @id";
                SqlCommand cmd = new SqlCommand(deleteUSerquery, myConn);
                cmd.Parameters.AddWithValue("@id", id);
                myConn.Open();

                //ExecuteNonQuery valida quantas rows foram afectadas
                int rows = cmd.ExecuteNonQuery();

                isDeleted = rows > 0;
                result = (isDeleted == true) ? "Deleted Successfully" : "Can't Delete Users";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
