using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Garage
{
    internal class Parking_Spot
    {
        public int _id;
        public int Id 
        { 
            get { return this._id; }
            set { this._id = value; }
        }

        private string _vehicle_id;
        public string Vehicle_id
        { 
            get { return _vehicle_id; } 
            set { this.Vehicle_id = value; }
        }

        private string _name;
        public string Name
        { 
            get { return _name; }
            set { this._name = value; }
        }

        private int _floor_id;
        public int Floor_id
        {
            get { return this._floor_id; }
            set { this._floor_id = value; }
        }

        public Parking_Spot(int id, string vehicle_id, string name, int floor_id)
        {
            this._id = id;
            this._vehicle_id = vehicle_id;
            this._name = name;
            this._floor_id = floor_id;
        }

        public Parking_Spot(int floor_id)
        {
            this._floor_id = floor_id;
        }

        public Parking_Spot() { }

        public static List<Parking_Spot> GetAllParkingSpots()
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM parking_spot", connection);
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
                return ls;
            }
        }

        public static void AddParkingSpot(Parking_Spot v, int amount)
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO parking_spot (floor_id)  VALUES(@floor_id)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@floor_id", v.Floor_id);
                    for(int i = 0; i < amount; i++)
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
        }

        public static List<Parking_Spot> GetEmptyParkingSpotsByFloorId(int id)
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM parking_spot WHERE floor_id = @floor_id AND vehicle_id IS NULL", connection);
                command.Parameters.AddWithValue("@floor_id", id);
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
                return ls;
            }
        }

        public static Parking_Spot GetFirstEmptyParkingSpotByFloorId(int id)
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM parking_spot WHERE floor_id = @floor_id AND vehicle_id IS NULL", connection);
                command.Parameters.AddWithValue("@floor_id", id);
                MySqlDataReader reader = command.ExecuteReader();
                Parking_Spot ps = new Parking_Spot();
                if(reader.Read())
                {
                    ps = new Parking_Spot(int.Parse(reader["id"].ToString()),
                                            reader["vehicle_id"].ToString(),
                                            reader["name"].ToString(),
                                            int.Parse(reader["floor_id"].ToString()));
                }

                connection.Close();
                return ps;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
