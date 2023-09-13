using Microsoft.AspNetCore.Mvc;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace Money_Locker_Project.Controllers
{
    [Route("api/userdetails")]
    [ApiController]
    public class GetUserDetailsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUserDetails(int customerId)
        {
            // Replace this with your logic to fetch user details from a database or any other source
            //User user = GetUser(customerId);
            User userInfo = GetUser(customerId);

            if (userInfo == null)
            {
                return NotFound(); // User not found
            }

            return Ok(userInfo); // Return user details
        }

        private User GetUser(int customerId)
        {
            User userDetails = new();
            string query = "SELECT CustomerName, MoneyLockerTransactionId FROM MoneyLockerDetails WHERE CustomerId = @id";

            using (SqlConnection conn = new("server = LP009311; database = MoneyLocker; Integrated Security = true"))
            {
                conn.Open();

                using (SqlCommand command = new(query, conn))
                {
                    // Set the parameter value for the condition
                    command.Parameters.AddWithValue("@id", customerId); // Replace 'id' with the actual value you want to use

                    SqlDataAdapter dataAdapter = new(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Process the retrieved data as needed
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Access the PaymentTransactionId from each row
                        userDetails.User_Name = (string)row["CustomerName"];
                        userDetails.Money_Locker_Transaction_Id = (string)row["MoneyLockerTransactionId"];
                    }
                }
            }
            return userDetails;
        }

    }
}
