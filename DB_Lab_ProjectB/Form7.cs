using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form7 : Form
    {
        private Configuration configuration; // Add this line

        public Form7()
        {
            InitializeComponent();
            configuration = Configuration.getInstance(); // Initialize the Configuration object
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the values from your textboxes or other controls
                string details = textBox1.Text;
                int cloId = Convert.ToInt32(textBox2.Text);

                // Call the AddRubric method to add a new rubric
                bool result = AddRubric(details, cloId);

                if (result)
                {
                    MessageBox.Show("Rubric added successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to add rubric. Invalid CLO ID.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid integer for CLO ID.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool AddRubric(string details, int cloId)
        {
            using (SqlConnection con = configuration.getConnection())
            {
                try
                {
                    con.Open();

                    // Check if the provided CLO ID exists in the CLO table
                    string checkQuery = "SELECT COUNT(*) FROM Clo WHERE Id = @CloId";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, con))
                    {
                        checkCommand.Parameters.AddWithValue("@CloId", cloId);
                        int cloCount = (int)checkCommand.ExecuteScalar();

                        if (cloCount == 0)
                        {
                            return false; // Invalid CLO ID
                        }
                    }

                    // If the CLO ID is valid, proceed to insert the rubric
                    string query = "INSERT INTO Rubric (Id, Details, CloId) VALUES (@Id, @Details, @CloId)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int newRubricId = GetNextRubricId(con); // Get the next available RubricId
                        cmd.Parameters.AddWithValue("@Id", newRubricId);
                        cmd.Parameters.AddWithValue("@Details", details);
                        cmd.Parameters.AddWithValue("@CloId", cloId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return false;
                }
            }
        }

        // Method to get the next available RubricId
        private int GetNextRubricId(SqlConnection con)
        {
            string query = "SELECT ISNULL(MAX(Id), 0) + 1 FROM Rubric"; // Get the maximum existing RubricId and increment it by 1
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                object result = cmd.ExecuteScalar();
                return (int)result;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form6 f6 = new Form6();
            f6.Show();
        }
    }
}
