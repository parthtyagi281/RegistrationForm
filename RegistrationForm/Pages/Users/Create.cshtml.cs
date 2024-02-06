using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RegistrationForm.Pages.Users
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
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

            if(userInfo.FirstName.Length==0 || userInfo.LastName.Length==0 || 
               userInfo.Email.Length==0 || userInfo.PasswordHash.Length==0 )
            {
                errorMessage = "All these fields are required ";
                return;
            }


            //save the user into the database
            try
            {
                String connectionString = "Data Source=RAJA_BON\\MSSQLSERVER1;UID=parth;Initial Catalog=RegistrationFormDb;pwd=parth1;Connection Timeout=36000;encrypt=true;trustServerCertificate=true";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                   sqlConnection.Open();
                    String sql = "INSERT INTO Users" +
                                 "(FirstName,LastName,Email,PasswordHash,DateOfBirth,Gender,StreetAddress,City,State,ZipCode,PhoneNumber) VALUES " +
                                 "(@FirstName,@LastName,@Email,@PasswordHash,@DateOfBirth,@Gender,@StreetAddress,@City,@State,@ZipCode,@PhoneNumber);";
                                 
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@FirstName",userInfo.FirstName);
                        command.Parameters.AddWithValue("@LastName", userInfo.LastName);
                        command.Parameters.AddWithValue("@Email", userInfo.Email);
                        command.Parameters.AddWithValue("@PasswordHash",userInfo.PasswordHash);
                        command.Parameters.AddWithValue("@DateOfBirth",userInfo.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender",userInfo.Gender);
                        command.Parameters.AddWithValue("@StreetAddress",userInfo.StreetAddress);
                        command.Parameters.AddWithValue("@City",userInfo.City);
                        command.Parameters.AddWithValue("@State",userInfo.State);
                        command.Parameters.AddWithValue("@ZipCode",userInfo.ZipCode);
                        command.Parameters.AddWithValue("@PhoneNumber",userInfo.PhoneNumber);

                        command.ExecuteNonQuery();
                    }

                }
            }

            catch(Exception ex) 
            { 
              errorMessage = ex.Message;
              return;
            }
            userInfo.FirstName = ""; userInfo.LastName = ""; userInfo.Email = ""; userInfo.PasswordHash = "";userInfo.DateOfBirth = "";userInfo.Gender = "";userInfo.StreetAddress = "";userInfo.City = "";userInfo.State = "";userInfo.ZipCode = "";userInfo.PhoneNumber = "";
            successMessage = "New User Added Correctly";

            Response.Redirect("/Users/Index");
        }
    }
}
