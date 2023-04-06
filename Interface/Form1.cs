using Garage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;//.MySqlException
using System.Data;
using System.Linq.Expressions;

namespace Interface
{
    public partial class Form1 : Form
    {
        private ErrorProvider errorProvider1;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void SetComboBox2Items(List<Floor> items)
        {
            //comboBox2.Items.Clear();
            comboBox2.ValueMember = "Id";

            comboBox2.DisplayMember = "Name";

            comboBox2.DataSource = items;
            //comboBox2.Items.AddRange(items.ToArray());
        }

        public void SetComboBox1Items(List<Parking> items)
        {
            //comboBox1.Items.Clear();
            comboBox1.ValueMember = "Id";

            comboBox1.DisplayMember = "Name";

            comboBox1.DataSource = items;

            //comboBox1.Items.AddRange(items.ToArray());
        }

        public void SetComboBox4Items(List<Parking> items)
        {
            //comboBox4.Items.Clear();
            comboBox4.ValueMember = "Id";

            comboBox4.DisplayMember = "Name";

            comboBox4.DataSource = items;

            //comboBox1.Items.AddRange(items.ToArray());
        }

        public void SetComboBox3Items(string[] items)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(items);
        }

        public void SetComboBox7Items(List<Vehicle> items)
        {
            comboBox7.ValueMember = "Id";
            comboBox7.DisplayMember = "Id";
            comboBox7.DataSource = items;
        }

        public void SetComboBox6Items(List<Vehicle> items)
        {
            comboBox6.ValueMember = "Id";
            comboBox6.DisplayMember = "Id";
            comboBox6.DataSource = items;
        }

        public void SetComboBox5Items(List<Parking> items)
        {
            comboBox5.ValueMember = "Id";
            comboBox5.DisplayMember = "Name";
            comboBox5.DataSource = items;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || comboBox3.SelectedItem == null)
                {
                    throw new ArgumentNullException("Empty fields!");
                }
                Vehicle.AddVehicle(new Vehicle(textBox1.Text, comboBox3.SelectedItem.ToString()));
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Wrong Input!");
                MessageBox.Show("Duplicate License Plate Number! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }catch (ArgumentNullException ex) 
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { 
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
                {
                    throw new ArgumentNullException("Empty fields!");
                }
                Parking.AddParking(new Parking(textBox4.Text, textBox3.Text));
                this.update_All();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox6.Text) || comboBox1.SelectedItem == null)
                {
                    throw new ArgumentNullException("Empty fields!");
                }
                Floor.AddFloor(new Floor(int.Parse(comboBox1.SelectedValue.ToString()), textBox6.Text));
                this.update_All();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetComboBox2Items(Floor.GetFloorsByParkingId(int.Parse(comboBox4.SelectedValue.ToString())));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox4.SelectedItem == null || comboBox2.SelectedItem == null)
                {
                    throw new ArgumentNullException("Empty fields!");
                }
                Parking_Spot.AddParkingSpot(new Parking_Spot(int.Parse(comboBox2.SelectedValue.ToString())), (int)numericUpDown1.Value);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void update_All()
        {
            try
            {
                this.SetComboBox1Items(Parking.GetAllParkings());
                this.SetComboBox4Items(Parking.GetAllParkings());
                this.SetComboBox2Items(Floor.GetFloorsByParkingId(int.Parse(comboBox4.SelectedValue.ToString())));
                this.SetComboBox5Items(Parking.GetAllParkings());
                textBox2.Text = "" + Parking.GetAllAvailableParkingSpots(int.Parse(comboBox5.SelectedValue.ToString()));
                this.SetComboBox7Items(Vehicle.GetAllNonParkedVehicles());
                this.SetComboBox6Items(Vehicle.GetAllParkedVehicles());
            }
            catch
            {
                Console.WriteLine("Empty tables.");
            }
            
        }

        public void setTextBox2()
        {
            try
            {
                textBox2.Text = "" + Parking.GetAllAvailableParkingSpots(int.Parse(comboBox5.SelectedValue.ToString()));
            }
            catch
            {
                Console.WriteLine("Empty table.");
            }
            
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setTextBox2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox2.Text) == 0)
            {
                MessageBox.Show("Parking Full! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Vehicle.EnterParking(comboBox7.SelectedValue.ToString(), int.Parse(comboBox5.SelectedValue.ToString()));
                    this.update_All();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception:" + ex);
                }
            }
        }
    }
}
