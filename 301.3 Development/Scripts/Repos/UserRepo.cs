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
        protected User? user;
        protected int userid;
        protected SQLiteTransaction transaction;
        public SQLiteCommand AddUser() //perhaps having the derived clases using this function as a base will be more aerodynamic
        {
            SQLiteConnection conn = DatabaseAccessLayer.ConnectToDatabase();
            SQLiteCommand cmd = new SQLiteCommand(conn);

            transaction = conn.BeginTransaction();

            cmd.Transaction = transaction;
            cmd.Connection = conn;
            
            cmd.CommandText = @"INSERT INTO Users (Email, PasswordHash, Role, Name_First, Name_Last, Phone)
                            VALUES (@Email, @PasswordHash, @Role, @First, @Last, @Phone)";
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
            cmd.Parameters.AddWithValue("@First", user.Name_First);
            cmd.Parameters.AddWithValue("@Last", user.Name_First);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);

            cmd.ExecuteNonQuery();



            Debug.WriteLine(user.Role);

            
            userid = (int)conn.LastInsertRowId;


            // at this point i think routing based on user.Role will be the way to go
            Debug.WriteLine(userid);
            Debug.WriteLine($"USER ID for {user.Role}: ", userid);
            return cmd;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            return users;
        }

        
    }
}
