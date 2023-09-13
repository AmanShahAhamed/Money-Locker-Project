using Microsoft.AspNetCore.Mvc;
using Model;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace Money_Locker_Project.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(UserLoginModel loginModel)
        {
            PaymentResponse paymentResponse = new();
            ErrorInfo errorResponse = new();
            bool isAuthenticated = AuthenticateUser(loginModel);

            if (isAuthenticated)
            {
                // User authenticated successfully

                paymentResponse.StatusCode = (int)HttpStatusCode.OK;
                paymentResponse.PaymentSuccessMsg = "Login successful";
                return Ok(paymentResponse);
            }
            else
            {
                // Invalid credentials

                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.ErrorMsg = "Invalid username or password";

                return Unauthorized(errorResponse);
            }
        }

        private bool AuthenticateUser(UserLoginModel loginModel)
        {
            User userLoginInfo = new();
            // Authenticate User
            string query = "SELECT CustomerName, Password FROM UserLogin WHERE CustomerName = @UserName ";

            using (SqlConnection conn = new("server = LP009311; database = MoneyLocker; Integrated Security = true"))
            {
                conn.Open();

                using (SqlCommand command = new(query, conn))
                {
                    //command.Parameters.AddWithValue("@id", loginModel.CustomerId);
                    command.Parameters.AddWithValue("@UserName", loginModel.UserName);

                    SqlDataAdapter dataAdapter = new(command);
                    DataTable dataTable = new();
                    dataAdapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Access the PaymentTransactionId from each row
                        userLoginInfo.Password = (string)row["Password"];
                        userLoginInfo.User_Name = (string)row["CustomerName"];
                    }
                }
            }
            return (loginModel.UserName == userLoginInfo.User_Name && loginModel.Password == userLoginInfo.Password);
        }
    }
}
