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
    public partial class Form31 : Form
    {
       

        private void Form31_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private Configuration configuration;

        public Form31()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdateRubricLevel(int rubricId, string details, int measurementLevel)
        {
            try
            {
                if (!RubricLevelExists(rubricId))
                {
                    MessageBox.Show("RubricLevel ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    string query = "UPDATE RubricLevel SET Details = @Details, MeasurementLevel = @MeasurementLevel WHERE Id = @RubricId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Details", details);
                        cmd.Parameters.AddWithValue("@MeasurementLevel", measurementLevel);
                        cmd.Parameters.AddWithValue("@RubricId", rubricId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("RubricLevel updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update RubricLevel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool RubricLevelExists(int rubricId)
        {
            try
            {
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM RubricLevel WHERE Id = @RubricId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RubricId", rubricId);

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
        private void button1_Click(object sender, EventArgs e)
        {
            Form28 f28 = new Form28();
            f28.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                int rubricId;
                if (!int.TryParse(textBox1.Text, out rubricId))
                {
                    MessageBox.Show("Invalid Rubric ID. Please enter a valid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string details = textBox2.Text;
                int measurementLevel;
                if (!int.TryParse(textBox3.Text, out measurementLevel))
                {
                    MessageBox.Show("Invalid Measurement Level. Please enter a valid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UpdateRubricLevel(rubricId, details, measurementLevel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
