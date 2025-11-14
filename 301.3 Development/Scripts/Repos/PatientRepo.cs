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
        
        public bool AddPatient(Patient patient)
        {
            
            bool patientAdded = false;

            SetUser(patient);

            SQLiteCommand cmd = AddUser(); // this will start a transaction and pass the command to this variable
            
            SQLiteConnection conn = cmd.Connection;

            DebugPrint(patient);

            try
            {
                cmd.CommandText = @"INSERT INTO Patients (PatientID, Birth_Place, Birth_Date, Sex, DoctorID, Medical_History)
                            VALUES (@PatientID, @Birth_Place, @Birth_Date, @Sex, @DoctorID, @Medical_History)";
                cmd.Parameters.AddWithValue("@PatientID", userid); // put here
                cmd.Parameters.AddWithValue("@Birth_Place", patient.Birth_Place);
                cmd.Parameters.AddWithValue("@Birth_Date", patient.Birth_Date);
                cmd.Parameters.AddWithValue("@Sex", patient.Sex);
                cmd.Parameters.AddWithValue("@DoctorID", patient.DoctorID);
                cmd.Parameters.AddWithValue("@Medical_History", patient.Medical_History);

                //Debug.WriteLine(user.Role);

                cmd.ExecuteNonQuery();
                transaction.Commit();
                Debug.WriteLine("Patient added");
                patientAdded = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                patientAdded = false ;
                try
                {
                    Console.WriteLine("Rolling back...");
                    transaction.Rollback();
                    
                }
                catch(Exception ex2)
                {
                    Console.WriteLine("Rollback failed");
                    Console.WriteLine(ex2);
                }


            }




            // at this point i think routing based on user.Role will be the way to go
            Debug.WriteLine(userid);
            //Debug.WriteLine($"USER ID for {user.Role}: ", userid);
            return patientAdded;
        }
        private void SetUser(Patient patient)
        {
            user = new User();
            user.Name_First = patient.Name_First;
            user.Name_Last = patient.Name_Last;
            user.Email = patient.Email;
            user.Phone = patient.Phone;
            user.PasswordHash = patient.PasswordHash;
            user.Role = patient.Role;

        }
        private void DebugPrint(Patient patient)
        {
            Debug.Write("User");

            Debug.WriteLine(user.Name_First); Debug.WriteLine(user.Name_Last); Debug.WriteLine(user.Email); Debug.WriteLine(user.Phone); Debug.WriteLine(user.PasswordHash); Debug.WriteLine(user.Role);
            Debug.WriteLine("Patient");
            Debug.Write(userid);
            Debug.WriteLine(patient.Birth_Place); Debug.WriteLine(patient.Birth_Date); Debug.WriteLine(patient.Sex); Debug.WriteLine(patient.DoctorID); Debug.WriteLine(patient.Medical_History);
        }
    }
}
