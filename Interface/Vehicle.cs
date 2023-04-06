using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage
{
    public class Vehicle
    {
        private string _id;
        public string Id 
        { 
            get { return this._id; }
            set { this._id = value; } 
        }

        private string _type;
        public string Type 
        {
            get { return this._type; }
            set { this._type = value; } 
        }

        public Vehicle() { }
        public Vehicle(string Id, string Type) {
                this._id = Id;
                this._type = Type;
        }

        private static bool CheckIfExists(string Id)
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";
            using (var connection = new MySqlConnection(connectionString))
            {
                string query = $"SELECT COUNT(*) FROM vehicles WHERE id = \"@Value\"";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Value", Id);
                    connection.Open();
                    long count = (long)command.ExecuteScalar();
                    connection.Close();
                    if (count > 0)
                        throw new Exception("Element Available Exception");
                    return false;
                }
            }
        }

        public static void AddVehicle(Vehicle v)
        {   
            //CheckIfExists(v.Id);
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO vehicles VALUES(@id, @type)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", v.Id);
                    command.Parameters.AddWithValue("@type", v.Type);
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static List<Vehicle> GetAllVehicles()
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM vehicles", connection);
                MySqlDataReader reader = command.ExecuteReader();
                List<Vehicle> ls = new List<Vehicle>();
                while (reader.Read())
                {
                    ls.Add(new Vehicle(reader["id"].ToString(),
                                            reader["type"].ToString()));
                }
                reader.Close();
                connection.Close();
                return ls;
            }
        }

        public static List<Vehicle> GetAllNonParkedVehicles()
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM parking_spot WHERE vehicle_id IS NOT NULL", connection);
                MySqlDataReader reader = command.ExecuteReader();
                List<Parking_Spot> ls = new List<Parking_Spot>();
                while (reader.Read())
                {
                    ls.Add(new Parking_Spot(int.Parse(reader["id"].ToString()),
                                            reader["vehicle_id"].ToString(),
                                            reader["name"].ToString(),
                                            int.Parse(reader["floor_id"].ToString())));
                }
                reader.Close();
                connection.Close();

                List<Vehicle> lst = Vehicle.GetAllVehicles();
                List<Vehicle> lst_mod = Vehicle.GetAllVehicles();

                for (int i = lst.Count - 1; i >= 0; i--)
                {
                    Vehicle v = lst[i];
                    foreach (Parking_Spot ps in ls)
                    {
                        if (v.Id == ps.Vehicle_id)
                        {
                            lst.RemoveAt(i);
                        }
                    }
                }

                return lst;
            }
        }

        public static List<Vehicle> GetAllParkedVehicles()
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM parking_spot WHERE vehicle_id IS NOT NULL", connection);
                MySqlDataReader reader = command.ExecuteReader();
                List<Parking_Spot> ls = new List<Parking_Spot>();
                while (reader.Read())
                {
                    ls.Add(new Parking_Spot(int.Parse(reader["id"].ToString()),
                                            reader["vehicle_id"].ToString(),
                                            reader["name"].ToString(),
                                            int.Parse(reader["floor_id"].ToString())));
                }
                reader.Close();
                connection.Close();

                List<Vehicle> lst = Vehicle.GetAllVehicles();
                List<Vehicle> lst_mod = new List<Vehicle>();

                for (int i = lst.Count - 1; i >= 0; i--)
                {
                    Vehicle v = lst[i];
                    foreach (Parking_Spot ps in ls)
                    {
                        if (v.Id == ps.Vehicle_id)
                        {
                            lst_mod.Add(v);
                        }
                    }
                }

                return lst_mod;
            }
        }

        public static void EnterParking(string id_vehicle, int id_parking)
        {

            List<Floor> floorList = Floor.GetFloorsByParkingId(id_parking);
            Parking_Spot parking_Spot = null;
            foreach (Floor floor in floorList)
            {
                parking_Spot = Parking_Spot.GetFirstEmptyParkingSpotByFloorId(floor.Id);
                Console.WriteLine("id of ps:", parking_Spot.Id);
                if (parking_Spot != null) break;
            }

            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("UPDATE parking_spot SET vehicle_id = @vehicle_id WHERE id = @id", connection);
                command.Parameters.AddWithValue("@vehicle_id", id_vehicle); 
                command.Parameters.AddWithValue("@id", parking_Spot.Id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public override string ToString()
        {
            return this.Id;
        }
    }
}