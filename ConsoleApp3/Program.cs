using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    class Program
    {
        //initialise variables:
        static OleDbConnection conn;
        static OleDbCommand cmd;
        static OleDbDataReader reader;

        //method to get a list of vehicles:
        public static void GetVehicle()
        {
            int counter = 0;
            //establish connection with database:
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\nicol\Documents\Vehicles.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            //query database for all entries:
            cmd.CommandText = "SELECT * FROM Vehicles";
            conn.Open();
            reader = cmd.ExecuteReader();
            //display all entries in console:
            while (reader.Read())
            {
                counter++;
                Console.WriteLine(reader[0] + "-" + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4]);
            }
            conn.Close();
            //print number of entries:
            Console.WriteLine("====================================");
            Console.WriteLine("Number of Vehicles :" + counter);
            Console.WriteLine("====================================");
        }
        //method to insert a new vehicle entry:
        public static void InsertVehicle()
        {
            //ask user for entry details:
            Console.Write("Vehicle Model : ");
            string model = Console.ReadLine();
            Console.Write("Registration : ");
            string reg = Console.ReadLine();
            Console.Write("Owner Name : ");
            string name = Console.ReadLine();
            Console.Write("Apartment Number : ");
            int appnum = Convert.ToInt32(Console.ReadLine());
            Console.Write("ID Number : ");
            int id = Convert.ToInt32(Console.ReadLine());
            //establish connection:
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\nicol\Documents\Vehicles.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            //enter command string to enter data into table:
            cmd.CommandText = "INSERT INTO Vehicles (Vehicle_Model, Registration, Owner, Apartment, IDNumber) VALUES ('" + model + "','" + reg + "','" + name + "','" + appnum + "','" + id + "')";
            conn.Open();
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            if (x > 0)
            {
                Console.WriteLine("Inserted");
            }
            else
            {
                Console.WriteLine("Error. Record not inserted.");
            }
        }
        //method for updating vehicle data:
        public static void UpdateVehicle()
        {
            //ask user for entry details:
            Console.Write("ID : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Vehicle Model : ");
            string model = Console.ReadLine();
            Console.Write("Registration : ");
            string registration = Console.ReadLine();
            Console.Write("Owner name : ");
            string owner = Console.ReadLine();
            Console.Write("Apartment number : ");
            int apartment = Convert.ToInt32(Console.ReadLine());
            //establish connection:
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\nicol\Documents\Vehicles.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            //enter command string to update data
            //the entry is identified by ID number
            cmd.CommandText = "UPDATE Vehicles SET Vehicle_Model='" + model + "',Registration='" + registration + "',Owner='" + owner + "',Apartment='" + apartment + "' WHERE IDNumber=" + id;

            conn.Open();
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            if (x > 0)
            {
                Console.WriteLine("Updated");
            }
            else
            {
                Console.WriteLine("Error. Record not updated");
            }
        }
        //method to delete record of vehicle:
        public static void DeleteVehicle()
        {
            //ask user for ID:
            Console.Write("ID : ");
            int id = Convert.ToInt32(Console.ReadLine());
            //establish a connection:
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\nicol\Documents\Vehicles.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            //enter command string to delete data entry:
            cmd.CommandText = "DELETE FROM Vehicles WHERE IDNumber=" + id + "";
            
            conn.Open();
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            if (x > 0)
            {
                Console.WriteLine("Deleted.");
            }
            else
            {
                Console.WriteLine("Error. Record not deleted.");
            }
        }
        public static int Average()
        {
            int counter = 0;
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\nicol\Documents\Vehicles.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Vehicles";
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                counter++;
            }

            conn.Close();
            Console.WriteLine("====================================");
            Console.WriteLine("Number of Permits issued :" + counter);
            Console.WriteLine("====================================");
            return counter;
        }

        [TestFixture]
        public class TestClass
        {
            [TestCase]
            public void AddTest()
            {
                int result = Average();
                Console.Write("Testing: Number of permits issued");
                Assert.AreEqual(4, result);
            }
        }

        public static void Unique()
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\nicol\Documents\Vehicles.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT IDNumber, Vehicle_Model, Vehicle_Model2, Vehicle_Model3 FROM Vehicles";
            conn.Open();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int counter = 0;
                for (int i = 1; i <= 3; i++)
                {
                    if (reader[i] != DBNull.Value)
                    {
                        counter++;
                        if (String.IsNullOrEmpty(Convert.ToString(reader[i])))
                        {
                            counter--;
                        }
                    }
                }
                Console.WriteLine(reader[0] + " has " + counter + " unique vehicles assigned to permit.");
            }
            conn.Close();
        }
        
        /*public static void Fees()
        {

        }*/


        static void Main(string[] args)
        {
            while (true)
            {
                //Display options to user:
                Console.WriteLine("1.List of Vehicles");
                Console.WriteLine("2.Insert");
                Console.WriteLine("3.Update");
                Console.WriteLine("4.Delete");
                Console.WriteLine("5.Average");
                Console.WriteLine("6.Unique cars");
                Console.WriteLine("====================================");
                Console.Write("Select : ");
                //ask user for selection:
                string sec = Console.ReadLine();
                Console.WriteLine("====================================");
                //list vehicles:
                if (sec == "1")
                {
                    GetVehicle();
                }
                //insert data:
                else if (sec == "2")
                {
                    InsertVehicle();
                    Console.WriteLine("====================================");
                    GetVehicle();
                }
                //update entry:
                else if (sec == "3")
                {
                    UpdateVehicle();
                    Console.WriteLine("====================================");
                    GetVehicle();
                }
                //delete entry:
                else if (sec == "4")
                {
                    DeleteVehicle();
                    Console.WriteLine("====================================");
                    //GetVehicle();
                }
                else if (sec == "5")
                {
                    Average();
                    Console.WriteLine("====================================");
                    //GetVehicle();
                }
                else if (sec == "6")
                {
                    Unique();
                    Console.WriteLine("====================================");
                    //GetVehicle();
                }
                //ask user to press y if they wish to continue, otherwise break;
                Console.Write("Continue (y/n) : ");
                string y = Console.ReadLine();
                if (y != "y")
                {
                    break;
                }
            }
        }
    }   
}
