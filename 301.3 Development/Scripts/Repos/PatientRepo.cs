using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _301._3_Development.RepoHandling;
using Microsoft.VisualBasic.ApplicationServices;

namespace _301._3_Development.Scripts.Repos
{
    class PatientRepo : UserRepo // NOT YET TESTEEEEEEEEEEEED
    {
        public void AddPatient(Patient patient)
        {
            using var conn = DatabaseAccessLayer.ConnectToDatabase();
            using var cmd = new SQLiteCommand(conn);

            cmd.CommandText = @"INSERT INTO Patients (Birth_Place, Birth_Date, Sex, DoctorID, Medical_History)
                            VALUES (@Email, @PasswordHash, @Role, @First, @Last, @Phone)";
            cmd.Parameters.AddWithValue("@Birth_Place", patient.Birth_Place);
            cmd.Parameters.AddWithValue("@Birth_Date", patient.Birth_Date);
            cmd.Parameters.AddWithValue("@Sex", patient.Sex);
            cmd.Parameters.AddWithValue("@DoctorID", patient.DoctorID);
            cmd.Parameters.AddWithValue("@Medical_History", patient.DoctorID);

            //Debug.WriteLine(user.Role);

            cmd.ExecuteNonQuery();
            long userid = conn.LastInsertRowId;


            // at this point i think routing based on user.Role will be the way to go
            Debug.WriteLine(userid);
            //Debug.WriteLine($"USER ID for {user.Role}: ", userid);
        }
    }
}
