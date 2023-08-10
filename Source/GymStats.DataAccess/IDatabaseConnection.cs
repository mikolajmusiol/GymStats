using System.Data.SqlClient;

namespace GymStats.DataAccess
{
    public interface IDatabaseConnection
    {
        SqlConnection GetConnection();
    }
}