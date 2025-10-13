using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
namespace _301._3_Development.RepoHandling
{
    public static class DatabaseAccessLayer
    {
        private static readonly string _connectionString = "Data Source=C:/Users/willi/source/repos/PasswordManager/301.3-Development/External_database/databaseSQLite/HospitalData.db";
    
        public static SQLiteConnection ConnectToDatabase() // connects to hospital database and returns open connection
        {
            var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            return conn;

        }
    }
}
