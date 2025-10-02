using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;






namespace _301._3_Development.Scripts
{
    class DataHandler
    {
        private string conString = "Data Source=C:/Users/270331790/OneDrive - UP Education/Desktop/databaseSQLite/HospitalData.db";
        
        public void ConnectToDatabase()
        {
            using var connection = new SQLiteConnection(conString);
            connection.Open();

            connection.Close();
        }
        public void PrintPatients()
        {
            string queery = """
                SELECT * 
                FROM Patient
                """;
            SQLCommand(queery);

        }
        public void SQLCommand(string queery)
        {
            using var connection = new SQLiteConnection(conString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = queery;

            using var reader = command.ExecuteReader();

            
            while (reader.Read())
            {
                int columnCount = reader.FieldCount;
                Debug.WriteLine(columnCount);
                for(int i = 0; i < columnCount; i++)
                {
                    if (i == 0)
                    {
                        Debug.Write( $"Patient ID: {reader.GetInt32(i)} ");
                    }
                    else
                    {
                        string valid = reader.GetString(i).ToString();

                        Debug.Write($"[ {valid} ]");

                    }
                    
                }
            }

            connection.Close();
        }
        
    }
}
