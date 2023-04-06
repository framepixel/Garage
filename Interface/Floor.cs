using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Garage
{
    public class Floor
    {
        private int _id;
        public int Id
        { 
            get { return _id; }
            set { _id = value; }
        }

        private int _parking_id;
        public int Parking_id
        { 
            get { return this._parking_id; } 
            set { this._parking_id = value; }
        }

        private string _name;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public Floor(int id, int parking_id, string name)
        {
            this._id = id;
            this._parking_id = parking_id;
            this._name = name;
        }
        public Floor(int parking_id, string name)
        {
            this._parking_id = parking_id;
            this._name = name;
        }

        public static List<Floor> GetAllFloors()
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM floor", connection);
                MySqlDataReader reader = command.ExecuteReader();
                List<Floor> ls = new List<Floor>();
                while (reader.Read())
                {
                    ls.Add(new Floor(int.Parse(reader["id"].ToString()),
                                            int.Parse(reader["parking_id"].ToString()),
                                            reader["name"].ToString()));
                }
                reader.Close();
                connection.Close();
                return ls;
                
            }
        }

        public static List<Floor> GetFloorsByParkingId(int id)
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM floor WHERE parking_id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                List<Floor> ls = new List<Floor>();
                while (reader.Read())
                {
                    ls.Add(new Floor(int.Parse(reader["id"].ToString()),
                                            int.Parse(reader["parking_id"].ToString()),
                                            reader["name"].ToString()));
                }
                reader.Close();
                connection.Close();
                return ls;

            }
        }

        public static void AddFloor(Floor v)
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO floor (name, parking_id) VALUES(@name, @parking_id)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", v.Name);
                    command.Parameters.AddWithValue("@parking_id", v.Parking_id);
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
