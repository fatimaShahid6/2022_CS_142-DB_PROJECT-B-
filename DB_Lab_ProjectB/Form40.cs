using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form40 : Form
    {
        private Configuration configuration;
            private string connectionString;

            public Form40()
            {
                InitializeComponent();
                configuration = Configuration.getInstance(); // Assuming Configuration has a static getInstance() method
                connectionString = configuration.GetConnectionString(); // Assuming GetConnectionString() is a method in the Configuration class that returns the connection string
            }

            private void button1_Click(object sender, EventArgs e)
        {
            Form39 f39 = new Form39();
            f39.Show();
            Hide();
        }

        private void AddAssessmentComponent(string name, int totalMarks, int rubricId, int assessmentId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check if the provided RubricId exists in the Rubric table
                    if (!RubricExists(rubricId, con))
                    {
                        MessageBox.Show("Rubric ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if the provided AssessmentId exists in the Assessment table
                    if (!AssessmentExists(assessmentId, con))
                    {
                        MessageBox.Show("Assessment ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Define the SQL query to insert Assessment Component
                    string query = "INSERT INTO AssessmentComponent (Name, RubricId, TotalMarks, DateCreated, DateUpdated, AssessmentId) " +
                                   "VALUES (@Name, @RubricId, @TotalMarks, @DateCreated, @DateUpdated, @AssessmentId)";

                    // Create SqlCommand object
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@RubricId", rubricId);
                        cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@AssessmentId", assessmentId);

                        // Execute the insert query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Assessment Component added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add Assessment Component.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool RubricExists(int rubricId, SqlConnection con)
        {
            string query = "SELECT COUNT(*) FROM Rubric WHERE Id = @RubricId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RubricId", rubricId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private bool AssessmentExists(int assessmentId, SqlConnection con)
        {
            string query = "SELECT COUNT(*) FROM Assessment WHERE Id = @AssessmentId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AssessmentId", assessmentId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                int rubricId, totalMarks, assessmentId;
                if (int.TryParse(textBox2.Text, out rubricId) && int.TryParse(textBox3.Text, out totalMarks) && int.TryParse(textBox4.Text, out assessmentId))
                {
                    AddAssessmentComponent(name, totalMarks, rubricId, assessmentId);
                }
                else
                {
                    MessageBox.Show("Please enter valid numeric values for Rubric ID, Total Marks, and Assessment ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Rubric ID, Total Marks, and Assessment ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
   

private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form40_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
