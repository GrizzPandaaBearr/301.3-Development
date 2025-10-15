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
        public bool AddPatient(Patient patient, User user)
        {
            bool patientAdded = false;

            SQLiteCommand cmd = AddUser(user);
            SQLiteConnection conn = cmd.Connection;
            
            long userid = conn.LastInsertRowId; // get this

            try
            {
                cmd.CommandText = @"INSERT INTO Patients (PatientID, Birth_Place, Birth_Date, Sex, DoctorID, Medical_History)
                            VALUES (@PatientID, @Birth_Place, @Birth_Date, @Sex, @DoctorID, @Medical_History)";
                cmd.Parameters.AddWithValue("@Birth_Place", userid); // put here
                cmd.Parameters.AddWithValue("@Birth_Place", patient.Birth_Place);
                cmd.Parameters.AddWithValue("@Birth_Date", patient.Birth_Date);
                cmd.Parameters.AddWithValue("@Sex", patient.Sex);
                cmd.Parameters.AddWithValue("@DoctorID", patient.DoctorID);
                cmd.Parameters.AddWithValue("@Medical_History", patient.DoctorID);

                //Debug.WriteLine(user.Role);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
            
            


            // at this point i think routing based on user.Role will be the way to go
            Debug.WriteLine(userid);
            //Debug.WriteLine($"USER ID for {user.Role}: ", userid);
            return true;
        }
    }
}
