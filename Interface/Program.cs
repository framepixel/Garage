using Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Interface
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            form.SetComboBox3Items(new string[] { "Car", "Motorcycle" });
            form.SetComboBox1Items(Parking.GetAllParkings());
            form.SetComboBox7Items(Vehicle.GetAllNonParkedVehicles());
            form.SetComboBox4Items(Parking.GetAllParkings());
            form.SetComboBox5Items(Parking.GetAllParkings());
            form.setTextBox2();
            form.SetComboBox6Items(Vehicle.GetAllParkedVehicles());
            form.Show();
            Application.Run(form);

            
        }
    }
}
