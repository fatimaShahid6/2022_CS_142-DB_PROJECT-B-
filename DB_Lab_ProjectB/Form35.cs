using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form35 : Form
    {
        private Configuration configuration; // Add this line to declare the configuration object

        public Form35()
        {
            InitializeComponent();
            configuration = Configuration.getInstance(); // Initialize the configuration object
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form35_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form38 f38 = new Form38();
            f38.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int assessmentId = int.Parse(textBox1.Text);
                string title = textBox2.Text;
                int totalMarks = int.Parse(textBox3.Text);
                int totalWeightage = int.Parse(textBox4.Text);

                UpdateAssessment(assessmentId, title, totalMarks, totalWeightage);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Assessment ID, Total Marks, and Total Weightage.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void UpdateAssessment(int assessmentId, string title, int totalMarks, int totalWeightage)
        {
            try
            {
                // Check if the Assessment ID exists
                if (!AssessmentExists(assessmentId))
                {
                    MessageBox.Show("Assessment ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update Assessment details
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    // Define the SQL query to update Assessment
                    string query = "UPDATE Assessment SET Title = @Title, DateCreated = @DateCreated, TotalMarks = @TotalMarks, TotalWeightage = @TotalWeightage WHERE Id = @Id";

                    // Create SqlCommand object
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
                        cmd.Parameters.AddWithValue("@TotalWeightage", totalWeightage);
                        cmd.Parameters.AddWithValue("@Id", assessmentId); // Use Id as the parameter

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Assessment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update Assessment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool AssessmentExists(int assessmentId)
        {
            try
            {
                // Check if Assessment with given ID exists
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM Assessment WHERE Id = @Id"; // Adjust the query
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", assessmentId); // Use Id as the parameter

                        int count = (int)cmd.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
