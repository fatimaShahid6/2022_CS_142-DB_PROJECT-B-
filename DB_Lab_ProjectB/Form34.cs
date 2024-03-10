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
    public partial class Form34 : Form
    {
        private Configuration configuration; // Declare the configuration object

        public Form34()
        {
            InitializeComponent();
            configuration = Configuration.getInstance(); // Initialize the configuration object
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
                string title = textBox1.Text;
                int totalMarks = int.Parse(textBox2.Text);
                int totalWeightage = int.Parse(textBox3.Text);

                AddAssessment(title, totalMarks, totalWeightage);

                MessageBox.Show("Assessment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Total Marks and Total Weightage.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AddAssessment(string title, int totalMarks, int totalWeightage)
        {
            try
            {
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    // Define the SQL query to insert a new Assessment
                    string query = "INSERT INTO Assessment (Title, DateCreated, TotalMarks, TotalWeightage) VALUES (@Title, @DateCreated, @TotalMarks, @TotalWeightage)";

                    // Create SqlCommand object
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
                        cmd.Parameters.AddWithValue("@TotalWeightage", totalWeightage);

                        // Execute the insert query
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Assessment: " + ex.Message);
            }
        }

    }
}
