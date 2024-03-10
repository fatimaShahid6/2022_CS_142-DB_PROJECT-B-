using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form29 : Form
    {
        // Define your connection string
        private string connectionString = "Data Source=YourServerAddress;Initial Catalog=YourDatabaseName;User ID=YourUsername;Password=YourPassword;";
        private Configuration configuration;

        public Form29()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the values from your textboxes or other controls
                int rubricId = Convert.ToInt32(textBox1.Text); // Assuming the rubric ID is entered in a textbox
                string details = textBox2.Text;
                int measurementLevel = Convert.ToInt32(textBox3.Text); // Assuming the measurement level is entered in a textbox

                // Call the AddRubricLevel method to add a new Rubric Level
                bool result = AddRubricLevel(rubricId, details, measurementLevel);

                if (result)
                {
                    MessageBox.Show("Rubric level added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add rubric level.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private bool AddRubricLevel(int rubricId, string details, int measurementLevel)
        {
            try
            {
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    // Don't include the Id column in the INSERT statement
                    string insertQuery = "INSERT INTO RubricLevel (RubricId, Details, MeasurementLevel) VALUES (@RubricId, @Details, @MeasurementLevel)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@RubricId", rubricId);
                        cmd.Parameters.AddWithValue("@Details", details);
                        cmd.Parameters.AddWithValue("@MeasurementLevel", measurementLevel);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form28 f28 = new Form28();
            f28.Show();
            this.Hide();

        }
    }
}
