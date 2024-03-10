using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form44 : Form
    {
        private string connectionString = "Data Source=DESKTOP-74I34NE;Initial Catalog=ProjectB;Integrated Security=True";
        private string csvFilePath = "StudentResult.txt";
        public Form44()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form44_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form45 f45 = new Form45();
            f45.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Get parameters from text boxes
                int maxRubricLevel = int.Parse(textBox4.Text);
                int assessmentComponentId = int.Parse(textBox2.Text);
                int measurementLevel = int.Parse(textBox3.Text);
                int studentId = int.Parse(textBox1.Text);

                // Validate StudentId
                if (!StudentExists(studentId))
                {
                    MessageBox.Show("Student ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Retrieve RegistrationNumber from Student table
                string registrationNumber = "";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT RegistrationNumber FROM Student WHERE Id = @StudentId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        registrationNumber = cmd.ExecuteScalar()?.ToString();
                    }
                }

                // Calculate Obtained Marks
                double obtainedMarks = (double)measurementLevel / maxRubricLevel;

                // Retrieve AssessmentComponent details
                string name = "";
                int totalMarks = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name, TotalMarks FROM AssessmentComponent WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", assessmentComponentId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name = reader["Name"].ToString();
                                totalMarks = Convert.ToInt32(reader["TotalMarks"]);
                            }
                            else
                            {
                                throw new Exception("AssessmentComponent not found.");
                            }
                        }
                    }
                }

                // Store data in StudentResult table
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string insertQuery = "INSERT INTO StudentResult (StudentId, AssessmentComponentId, RubricMeasurementId, EvaluationDate) VALUES (@StudentId, @AssessmentComponentId, @RubricMeasurementId, @EvaluationDate)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentComponentId);
                        cmd.Parameters.AddWithValue("@RubricMeasurementId", measurementLevel);
                        cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Write data to CSV file
                using (StreamWriter writer = new StreamWriter(csvFilePath, true))
                {
                    writer.WriteLine($"{registrationNumber},{studentId},{assessmentComponentId},{measurementLevel},{DateTime.Now},{obtainedMarks}");
                }

                MessageBox.Show("Student result processed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing student result: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool StudentExists(int studentId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Student WHERE Id = @StudentId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    }
}
