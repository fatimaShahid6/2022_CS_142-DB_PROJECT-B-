using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form42 : Form
    {
        
        private string connectionString = "Data Source=DESKTOP-74I34NE;Initial Catalog=ProjectB;Integrated Security=True";

        public Form42()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form39 f39 = new Form39();
            f39.Show();
            Hide();
        }

        private void UpdateAssessmentComponent(int assessmentComponentId, string name, int totalMarks, int rubricId)
        {
            try
            {
                if (!AssessmentComponentExists(assessmentComponentId))
                {
                    MessageBox.Show("Assessment Component ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "UPDATE AssessmentComponent SET Name = @Name, TotalMarks = @TotalMarks, RubricId = @RubricId, DateUpdated = @DateUpdated WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@TotalMarks", totalMarks);
                        cmd.Parameters.AddWithValue("@RubricId", rubricId);
                        cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Id", assessmentComponentId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Assessment Component updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update Assessment Component.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AssessmentComponentExists(int assessmentComponentId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM AssessmentComponent WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", assessmentComponentId);

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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form42_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int assessmentComponentId = int.Parse(textBox1.Text);
                string name = textBox2.Text;
                int totalMarks = int.Parse(textBox3.Text);
                int rubricId = int.Parse(textBox4.Text);

                UpdateAssessmentComponent(assessmentComponentId, name, totalMarks, rubricId);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Assessment Component ID, Total Marks, and Rubric ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
