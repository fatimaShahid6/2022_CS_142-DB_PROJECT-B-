using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form2 : Form
    {
        private Configuration configuration;

        public Form2()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the values from your textboxes or other controls
                string name = textBox2.Text;
                DateTime dateCreated = DateTime.Now;
                DateTime dateUpdated = DateTime.Now;

                // Call the AddClo method to add a new CLO
                bool result = AddClo(name, dateCreated, dateUpdated);

                if (result)
                {
                    MessageBox.Show("CLO added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add CLO.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Method to add a new CLO to the database
        // Method to add a new CLO to the database
        private bool AddClo(string name, DateTime dateCreated, DateTime dateUpdated)
        {
            try
            {
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    // Don't include the Id column in the INSERT statement
                    string insertQuery = "INSERT INTO Clo (Name, DateCreated, DateUpdated) VALUES (@Name, @DateCreated, @DateUpdated)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@DateCreated", dateCreated);
                        cmd.Parameters.AddWithValue("@DateUpdated", dateUpdated);

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


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
