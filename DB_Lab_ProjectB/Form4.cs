using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form4 : Form
    {
        private Configuration configuration; // Declare configuration object at class level

        public Form4()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Call the UpdateClo method
            bool result = UpdateClo();

            if (result)
            {
                MessageBox.Show("CLO updated successfully!");
            }
            else
            {
                MessageBox.Show("Failed to update CLO.");
            }
        }

        // Method to update a CLO in the database
        private bool UpdateClo()
        {
            try
            {
                // Get the ID from textBox1
                int id;
                if (!int.TryParse(textBox1.Text, out id))
                {
                    MessageBox.Show("Please enter a valid integer for ID.");
                    return false;
                }

                // Get the new name from textBox2
                string newName = textBox2.Text;

                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    // Check if the ID exists
                    string checkQuery = "SELECT COUNT(*) FROM Clo WHERE Id = @Id";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@Id", id);
                        int count = (int)checkCmd.ExecuteScalar();

                        // If the ID exists, update the CLO
                        if (count > 0)
                        {
                            // Update the Name and DateUpdated fields
                            string updateQuery = "UPDATE Clo SET Name = @Name, DateUpdated = @DateUpdated WHERE Id = @Id";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                            {
                                updateCmd.Parameters.AddWithValue("@Name", newName);
                                updateCmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                                updateCmd.Parameters.AddWithValue("@Id", id);

                                int rowsAffected = updateCmd.ExecuteNonQuery();

                                // Check if any rows were affected
                                return rowsAffected > 0;
                            }
                        }
                        else
                        {
                            // If the ID does not exist, display an error message
                            MessageBox.Show("CLO with ID " + id + " does not exist.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Failed to update the CLO
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
