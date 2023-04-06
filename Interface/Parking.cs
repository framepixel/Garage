using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Garage
{
    public class Parking
    {   
        private int _id;
        public int Id 
        { 
            get { return this._id; }
            set{ this._id = value; }
        }
        
        private string _name;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        private string _address;
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }

        public Parking(int id, string name, string address)
        {
            this._id = id;
            this._name = name;
            this._address = address;
        }

        public Parking(string name, string address)
        {
            this._name = name;
            this._address = address;
        }

        /*
         * This function returns all the available parkings in the database
         */
        public static List<Parking> GetAllParkings()
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM parking", connection);
                MySqlDataReader reader = command.ExecuteReader();
                List<Parking> ls = new List<Parking>();
                while (reader.Read())
                {
                    ls.Add(new Parking(int.Parse(reader["id"].ToString()), reader["name"].ToString(), reader["address"].ToString()));
                }
                reader.Close();
                connection.Close();
                return ls;
            }
        }

        /*
         * This function returns a parking using and id as a parameter in the database
         */
        public static Parking GetParkingById(int id)
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM parking WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                Parking ls = null;
                if (reader.Read())
                {
                    ls = new Parking(int.Parse(reader["id"].ToString()), reader["name"].ToString(), reader["address"].ToString());
                }
                reader.Close();
                connection.Close();
                return ls;
            }
        }

        /*
         * This function adds a parking to the database
         */

        public static void AddParking(Parking v)
        {
            string connectionString = "server=localhost;database=garage_db;user=root;password=sqlPASS2001.";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO parking (address, name) VALUES(@address, @name)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@address", v.Address);
                    command.Parameters.AddWithValue("@name", v.Name);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        /*
         * This function returns all the available parking spots with a parking id from the database
         */
        public static int GetAllAvailableParkingSpots(int id)
        {   
            List<Floor> floorList = Floor.GetFloorsByParkingId(id);
            int cpt = 0;
            foreach (Floor floor in floorList)
            { 
                List<Parking_Spot> parking_Spots = Parking_Spot.GetEmptyParkingSpotsByFloorId(floor.Id);
                cpt += parking_Spots.Count();
            }

            return cpt;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
