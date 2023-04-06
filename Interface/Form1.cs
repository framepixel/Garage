using Garage;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;//.MySqlException

namespace Interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*
         * This function sets all the floor elements of the comboBox2
         */
        public void SetComboBox2Items(List<Floor> items)
        {
            comboBox2.ValueMember = "Id";
            comboBox2.DisplayMember = "Name";
            comboBox2.DataSource = items;
        }

        /*
         * This function sets all the parking elements of the comboBox1
         */
        public void SetComboBox1Items(List<Parking> items)
        {
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";
            comboBox1.DataSource = items;
        }

        /*
         * This function sets all the parking elements of the comboBox4
         */
        public void SetComboBox4Items(List<Parking> items)
        {
            comboBox4.ValueMember = "Id";
            comboBox4.DisplayMember = "Name";
            comboBox4.DataSource = items;
        }

        /*
         * This function sets all the vehicle types of the comboBox3
         */
        public void SetComboBox3Items(string[] items)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(items);
        }

        /*
         * This function sets all the vehicle elements of the comboBox7
         */
        public void SetComboBox7Items(List<Vehicle> items)
        {
            comboBox7.ValueMember = "Id";
            comboBox7.DisplayMember = "Id";
            comboBox7.DataSource = items;
        }

        /*
         * This function sets all the vehicle elements of the comboBox6
         */
        public void SetComboBox6Items(List<Vehicle> items)
        {
            comboBox6.ValueMember = "Id";
            comboBox6.DisplayMember = "Id";
            comboBox6.DataSource = items;
        }

        /*
         * This function sets all the vehicle elements of the comboBox5
         */
        public void SetComboBox5Items(List<Parking> items)
        {
            comboBox5.ValueMember = "Id";
            comboBox5.DisplayMember = "Name";
            comboBox5.DataSource = items;
        }

        /*
         * This function adds a vehicle to the database after a click on button1
         */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // check if all fields are full or not
                if (string.IsNullOrEmpty(textBox1.Text) || comboBox3.SelectedItem == null)
                {
                    throw new ArgumentNullException("Empty fields!");
                }
                // add the vehicle
                Vehicle.AddVehicle(new Vehicle(textBox1.Text, comboBox3.SelectedItem.ToString()));
                // update the fields
                this.update_All();
                // success message
                MessageBox.Show("Vehicle added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Duplicate License Plate Number! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /*
         * This function adds a parking to the database after a click on button2
         */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // check if all fields are full or not
                if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
                {
                    throw new ArgumentNullException("Empty fields!");
                }
                // add the parking
                Parking.AddParking(new Parking(textBox4.Text, textBox3.Text));
                // update the fields
                this.update_All();
                // success message
                MessageBox.Show("Parking added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /*
         * This function adds a floor to the database after a click on button3
         */
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // check if all fields are full or not
                if (string.IsNullOrEmpty(textBox6.Text) || comboBox1.SelectedItem == null)
                {
                    throw new ArgumentNullException("Empty fields!");
                }
                // add the floor
                Floor.AddFloor(new Floor(int.Parse(comboBox1.SelectedValue.ToString()), textBox6.Text));
                // update the fields
                this.update_All();
                // success message
                MessageBox.Show("Floor Added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /*
         * This function sets the elements of comboBox2 after comboBox4 has been changed
         */
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetComboBox2Items(Floor.GetFloorsByParkingId(int.Parse(comboBox4.SelectedValue.ToString())));
        }

        /*
         * This function adds a parking spot to the database after a click on button4
         */
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // check if all fields are full or not
                if (comboBox4.SelectedItem == null || comboBox2.SelectedItem == null)
                {
                    throw new ArgumentNullException("Empty fields!");
                }
                // add the parking spot
                Parking_Spot.AddParkingSpot(new Parking_Spot(int.Parse(comboBox2.SelectedValue.ToString())), (int)numericUpDown1.Value);
                // update the fields
                this.update_All();
                // success message
                MessageBox.Show("Parking Spot(s) added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /*
         * This function updates the fields after any change to the database has been made
         */
        private void update_All()
        {
            try
            {
                this.SetComboBox1Items(Parking.GetAllParkings());
                this.SetComboBox4Items(Parking.GetAllParkings());
                if(comboBox4.SelectedValue != null)
                    this.SetComboBox2Items(Floor.GetFloorsByParkingId(int.Parse(comboBox4.SelectedValue.ToString())));
                this.SetComboBox5Items(Parking.GetAllParkings());
                if(comboBox5.SelectedValue != null)
                    textBox2.Text = "" + Parking.GetAllAvailableParkingSpots(int.Parse(comboBox5.SelectedValue.ToString()));
                this.SetComboBox7Items(Vehicle.GetAllNonParkedVehicles());
                this.SetComboBox6Items(Vehicle.GetAllParkedVehicles());
            }
            catch
            {
                Console.WriteLine("Empty tables.");
            }
            
        }

        /*
         * This function sets the text of textBox2 to the available parking spots
         */
        public void setTextBox2()
        {
            try
            {
                if(comboBox5.SelectedValue != null)
                    textBox2.Text = "" + Parking.GetAllAvailableParkingSpots(int.Parse(comboBox5.SelectedValue.ToString()));
            }
            catch
            {
                Console.WriteLine("Empty table.");
            }
            
        }

        /*
         * This function sets the text of textBox2 when comboBox5 is changed
         */
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setTextBox2();
        }

        /*
         * This function makes a vehicle enter the parking after a click on button6
         */
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // check if  is full or not
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    throw new ArgumentNullException("Empty fields");
                }
                // check if the content of the field textBox2 is equal to zero not,
                // meaning if the parking has available parking lots or not  
                if (int.Parse(textBox2.Text) == 0)
                {
                    MessageBox.Show("Parking Full! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        // check if all fields are full or not
                        if (comboBox7.SelectedItem == null || comboBox5.SelectedItem == null)
                        {
                            throw new ArgumentNullException("Empty fields!");
                        }
                        // make the vehicle enter the parking
                        Vehicle.EnterParking(comboBox7.SelectedValue.ToString(), int.Parse(comboBox5.SelectedValue.ToString()));
                        // update the fields
                        this.update_All();
                        // success message
                        MessageBox.Show("Car entered parking!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (ArgumentNullException ex)
                    {
                        MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception:" + ex);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("All fields are required! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        /*
         * This function modifies textBox5 and textBox7 after the value of comboBox6a click on button1
         * has been changed.
         */
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox6.SelectedValue != null)
                {
                    Parking_Spot ps = Parking_Spot.GetParkingSpotByVehicleId(comboBox6.SelectedValue.ToString());
                    Floor fl = Floor.GetFloorById(ps.Floor_id);
                    Parking pk = Parking.GetParkingById(fl.Parking_id);
                    textBox5.Text = pk.ToString();
                    textBox7.Text = fl.ToString();
                }
            }
            catch
            {
                Console.WriteLine("Empty table.");
            }
            

        }

        /*
         * This function makes a vehicle leave the parking after a click on button7
         */
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(comboBox6.SelectedValue != null)
                {
                    Parking_Spot.EmptyParkingSpotByVehicleId(comboBox6.SelectedValue.ToString());
                    this.update_All();
                    
                    MessageBox.Show("Car left parking.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("All fields must be full! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex);
            }
            

        }
        // this variable is used to check if Form2 is open or not.
        private bool isFormOpen = false;

        /*
         * This function checks if Form2 is open or not and then decides
         * to either open it or not after a click on button5
         */
        private void button5_Click(object sender, EventArgs e)
        {
            if (!isFormOpen)
            {
                isFormOpen = true;
                Form2 form = new Form2();
                form.FormClosed += (s, args) => { isFormOpen = false; };
                form.Show();
            }
        }
    }
}
