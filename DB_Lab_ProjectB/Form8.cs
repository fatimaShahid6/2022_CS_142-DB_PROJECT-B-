using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form8 : Form
    {
        private Configuration configuration;

        public Form8()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private bool UpdateRubric(string newDetails, int newCloId)
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
                        checkCommand.Parameters.AddWithValue("@CloId", newCloId);
                        int cloCount = (int)checkCommand.ExecuteScalar();

                        if (cloCount == 0)
                        {
                            MessageBox.Show("Invalid CLO ID.");
                            return false;
                        }
                    }

                    // If the CLO ID is valid, proceed to update the rubric
                    string updateQuery = "UPDATE Rubric SET Details = @NewDetails WHERE CloId = @CloId";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@NewDetails", newDetails);
                        cmd.Parameters.AddWithValue("@CloId", newCloId);

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string newDetails = textBox1.Text;
                int newCloId = Convert.ToInt32(textBox2.Text);

                bool result = UpdateRubric(newDetails, newCloId);

                if (result)
                {
                    MessageBox.Show("Rubric updated successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to update rubric.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid values for CLO ID.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Define functionality for textBox2 text changed event
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // Define functionality for Form8 load event
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form6 f6 = new Form6();
            f6.Show();
        }
    }
}
