using Garage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        /*
         * This function calls refresh() when the form is loaded
         */
        private void Form2_Load(object sender, EventArgs e)
        {
            this.refresh();
        }

        /*
         * This function refreshes the dataGridView items
         */
        private void refresh()
        {
            try
            {
                List<Vehicle> list_vehicle = Vehicle.GetAllVehicles();
                dataGridView1.DataSource = list_vehicle;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.ReadOnly = true;

                List<Floor> list_floors = Floor.GetAllFloors();
                dataGridView2.DataSource = list_floors;
                dataGridView2.AutoGenerateColumns = true;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.AllowUserToDeleteRows = false;
                dataGridView2.ReadOnly = true;

                List<Parking> list_parkings = Parking.GetAllParkings();
                dataGridView3.DataSource = list_parkings;
                dataGridView3.AutoGenerateColumns = true;
                dataGridView3.AllowUserToAddRows = false;
                dataGridView3.AllowUserToDeleteRows = false;
                dataGridView3.ReadOnly = true;

                List<Parking_Spot> list_parking_Spots = Parking_Spot.GetAllParkingSpots();
                dataGridView4.DataSource = list_parking_Spots;
                dataGridView4.AutoGenerateColumns = true;
                dataGridView4.AllowUserToAddRows = false;
                dataGridView4.AllowUserToDeleteRows = false;
                dataGridView4.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        /*
         * This function calls refresh() when the button1 is clicked
         */
        private void button1_Click(object sender, EventArgs e)
        {
            this.refresh();
        }
    }
}
