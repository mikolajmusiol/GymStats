using System.Configuration;
using System.Data.SqlClient;

namespace GymStats.DataAccess
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["GymProjDBTest"].ConnectionString);
        }
    }
}