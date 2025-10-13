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
        private string conString = "Data Source=C:/Users/willi/source/repos/PasswordManager/301.3-Development/External_database/databaseSQLite/HospitalData.db";
        
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
            GetPatientsFromSQL(queery);

        }
        public List<Patient> GetPatients()
        {
            string queery = """
                SELECT * 
                FROM Patient
                """;

            List<Patient> patients = GetPatientsFromSQL(queery);

            Debug.WriteLine("Printing results...");

            foreach (Patient pat in patients)
            {
                Debug.WriteLine($"Here is the patient: {pat}");
            }

            return patients;

        }
        private bool ExecuteSqlQueery(string queery)
        {
            bool result = true;

            using (var connection = new SQLiteConnection(conString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = queery;
                    command.ExecuteScalar();
                    connection.Close();
                }

            }
            
            return result;
        }
        public List<Patient> GetPatientsFromSQL(string queery)
        {
            List<Patient> list = new List<Patient>();
            
            using var connection = new SQLiteConnection(conString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = queery;

            var reader = command.ExecuteReader();


            while (reader.Read())
            {
                Patient patient = new Patient();

                int columnCount = reader.FieldCount;
                Debug.WriteLine(columnCount);
                for (int i = 0; i < columnCount; i++)
                {
                    if (i == 0)
                    {
                        Debug.Write($"Patient ID: {reader.GetInt32(i)} ");
                        patient.Id = reader.GetInt32(i);
                    }
                    else
                    {
                        string valid = reader.GetString(i).ToString();

                        Debug.Write($"[ {valid} ]");

                        patient = ParceToPatient(patient, valid, i);

                    }

                }
                list.Add( patient );
            }

            return list;
        }
        public Patient ParceToPatient(Patient p, string value, int it)
        {
            switch (it)
            {
                case 1:
                    p.Name_First = value; break;
                case 2:
                    p.Name_Last = value; break; 
                case 3:
                    p.Birth_Place = value; break;
                case 4:
                    p.Birth_Date = value; break;
                case 5:
                    p.Sex = value[0]; break;
                case 6:
                    p.Phone = value; break;
                case 7:
                    p.Appointment_Date = value; break;
                case 8:
                    p.Passport_Number = value; break;
            }
            return p;
        }

        public bool NewPatient(Patient patient)
        {
            bool result = true;

            string queery = $"""
                 INSERT INTO Patient(
                Name_First, Name_Last, Birth_Place, Birth_Date, Sex, Phone, Appointment_Date, Passport_Number)
                VALUES(
                '{patient.Name_First}', 
                '{patient.Name_Last}', 
                '{patient.Birth_Place}', 
                '{patient.Birth_Date}', 
                '{patient.Sex}', 
                '{patient.Phone}', 
                '{patient.Appointment_Date}', 
                '{patient.Passport_Number}'); 
                """;

            Debug.WriteLine( queery );
            try
            {
                bool sqlResult = ExecuteSqlQueery(queery);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public void RemoveUser(string firstname, string phonenumber) // currently only for patients
        {
            string queery = $"""
                DELETE FROM Patient WHERE Name_First='{firstname}' AND Phone='{phonenumber}';
                """;

            if (!ExecuteSqlQueery(queery))
            {
                Debug.WriteLine("Failed to remove user");
            }
        }


        public void RemoveAllUsers() // currently only for patients
        {
            string queery = $"""
                DELETE FROM Patient;
                """;

            if (!ExecuteSqlQueery(queery))
            {
                Debug.WriteLine("Failed to remove users");
            }
        }

    }
}
