using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RegistrationForm.Pages.Users
{
    public class EditModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String UserId = Request.Query["UserId"];

            try
            {

                String connectionString = "Data Source=RAJA_BON\\MSSQLSERVER1;UID=parth;Initial Catalog=RegistrationFormDb;pwd=parth1;Connection Timeout=36000;encrypt=true;trustServerCertificate=true";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String sql = "SELECT * FROM Users WHERE UserId=@UserId";
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@UserId", UserId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                userInfo.UserId = "" + reader.GetInt32(0);
                                userInfo.FirstName = reader.GetString(1);
                                userInfo.LastName = reader.GetString(2);
                                userInfo.Email = reader.GetString(3);
                                userInfo.PasswordHash = reader.GetString(4);
                                userInfo.DateOfBirth = reader.GetDateTime(5).ToString();
                                userInfo.Gender = reader.GetString(6);
                                userInfo.StreetAddress = reader.GetString(7);
                                userInfo.City = reader.GetString(8);
                                userInfo.State = reader.GetString(9);
                                userInfo.ZipCode = reader.GetString(10);
                                userInfo.PhoneNumber = reader.GetString(11);
                                

                               


                            }
                        }
                    }
                }

            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            userInfo.UserId = Request.Form["UserId"];
            userInfo.FirstName = Request.Form["FirstName"];
            userInfo.LastName = Request.Form["LastName"];
            userInfo.Email = Request.Form["Email"];
            userInfo.PasswordHash = Request.Form["PasswordHash"];
            userInfo.DateOfBirth = Request.Form["DateOfBirth"];
            userInfo.Gender = Request.Form["Gender"];
            userInfo.StreetAddress = Request.Form["StreetAddress"];
            userInfo.City = Request.Form["City"];
            userInfo.State = Request.Form["State"];
            userInfo.ZipCode = Request.Form["ZipCode"];
            userInfo.PhoneNumber = Request.Form["PhoneNumber"];

            if (userInfo.UserId.Length == 0 || userInfo.FirstName.Length == 0 || userInfo.LastName.Length == 0 ||
               userInfo.Email.Length == 0 || userInfo.PasswordHash.Length == 0)
            {
                errorMessage = "All these fields are required ";
                return;
            }

            try
            {
                String connectionString = "Data Source=RAJA_BON\\MSSQLSERVER1;UID=parth;Initial Catalog=RegistrationFormDb;pwd=parth1;Connection Timeout=36000;encrypt=true;trustServerCertificate=true";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String sql = "UPDATE Users " +
                                 "SET FirstName=@FirstName,LastName=@LastName,Email=@Email,PasswordHash=@PasswordHash,DateOfBirth=@DateOfBirth,Gender=@Gender,StreetAddress=@StreetAddress,City=@City,State=@State,ZipCode=@ZipCode,PhoneNumber=@PhoneNumber  "+
                                 "WHERE UserId=@UserId";

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                       
                        command.Parameters.AddWithValue("@FirstName", userInfo.FirstName);
                        command.Parameters.AddWithValue("@LastName", userInfo.LastName);
                        command.Parameters.AddWithValue("@Email", userInfo.Email);
                        command.Parameters.AddWithValue("@PasswordHash", userInfo.PasswordHash);
                        command.Parameters.AddWithValue("@DateOfBirth", userInfo.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", userInfo.Gender);
                        command.Parameters.AddWithValue("@StreetAddress", userInfo.StreetAddress);
                        command.Parameters.AddWithValue("@City", userInfo.City);
                        command.Parameters.AddWithValue("@State", userInfo.State);
                        command.Parameters.AddWithValue("@ZipCode", userInfo.ZipCode);
                        command.Parameters.AddWithValue("@PhoneNumber", userInfo.PhoneNumber);
                        command.Parameters.AddWithValue("@UserId", userInfo.UserId);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Users/Index");
        }
    }
}
