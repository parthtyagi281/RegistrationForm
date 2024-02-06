using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Linq.Expressions; 

namespace RegistrationForm.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> listsUsers = new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=RAJA_BON\\MSSQLSERVER1;UID=parth;Initial Catalog=RegistrationFormDb;pwd=parth1;Connection Timeout=36000;encrypt=true;trustServerCertificate=true";
                using (SqlConnection  sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    String sql = "SELECT * FROM Users";
                    using (SqlCommand command = new SqlCommand(sql, sqlConnection)) 
                    {
                      using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                          while (reader.Read()) 
                            {
                              UserInfo userInfo = new UserInfo();
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
                                userInfo.RegistrationDate = reader.GetDateTime(12).ToString();

                                listsUsers.Add(userInfo);


                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
    }


    public class UserInfo
    {
   
    
        public String UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String PasswordHash { get; set; }
        public String DateOfBirth { get; set; }
        public String Gender { get; set; }
        public String StreetAddress { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String PhoneNumber { get; set; }
        public String RegistrationDate { get; set;}
        
    }


}

