using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _301._3_Development.RepoHandling;

namespace _301._3_Development.Scripts.Repos
{
    internal class UserRepo
    {
        public void AddUser(User user)
        {
            using var conn = DatabaseAccessLayer.ConnectToDatabase();
            using var cmd = new SQLiteCommand(conn);

            cmd.CommandText = @"INSERT INTO Users (Email, PasswordHash, Role, Name_First, Name_Last, Phone)
                            VALUES (@Email, @PasswordHash, @Role, @First, @Last, @Phone)";
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
            cmd.Parameters.AddWithValue("@First", user.FirstName);
            cmd.Parameters.AddWithValue("@Last", user.LastName);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);

            Debug.WriteLine(user.Role);

            cmd.ExecuteNonQuery();
            long userid = conn.LastInsertRowId;

            Debug.WriteLine(userid);
            Debug.WriteLine($"USER ID for {user.Role}: ", userid);
        }
    }
}
