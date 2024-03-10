using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1= new Form1();
            f1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Delete selected record from DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Prompt user for confirmation before deleting
                    DialogResult result = MessageBox.Show("Are you sure you want to delete the selected record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // If user confirms deletion, proceed
                    if (result == DialogResult.Yes)
                    {
                        // Get the RubricId of the selected row
                        int rubricId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                        // Delete the rubric record from the Rubric table
                        DeleteRubric(rubricId);

                        // Remove row from DataGridView
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                        MessageBox.Show("Record deleted successfully from the Rubric table.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a record to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteRubric(int rubricId)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Rubric WHERE Id = @RubricId", con))
                {
                    cmd.Parameters.AddWithValue("@RubricId", rubricId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting rubric: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();

                SqlCommand cmdLoadData = new SqlCommand("SELECT * FROM Rubric", con); // Select records from Rubric table
                SqlDataAdapter adapter = new SqlDataAdapter(cmdLoadData);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
