using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practika.ViewModel
{
    public class DelAndDobView
    {
        private void Dobav()
        {
            string connectionString = "server=ngknn.ru;Trusted_Connection=No;DataBase=43p_praktika_Smolin;User=33П;PWD=12357";
            string sql = "INSERT INTO Service (service, price) VALUES ('New Service', 10.0)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
            }
        }


        

    }
}
