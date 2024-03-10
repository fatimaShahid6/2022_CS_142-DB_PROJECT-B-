using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form9 : Form
    {
        private string connectionString = "Data Source=DESKTOP-74I34NE;Initial Catalog=ProjectB;Integrated Security=True";

        public Form9()
        {
            InitializeComponent();
        }

        private void LoadRubrics()
        {
            
                try
                {
                    DataTable dataTable = new DataTable();

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        string query = @"
                SELECT 
                    AC.Id AS AssessmentComponentId,
                    AC.Name,
                    AC.TotalMarks,
                    AC.DateCreated AS AssessmentComponentDateCreated,
                    AC.DateUpdated AS AssessmentComponentDateUpdated,
                    AC.AssessmentId,
                    RL.Details AS RubricDetails,
                    RL.MeasurementLevel AS RubricMeasurementLevel
                FROM 
                    AssessmentComponent AS AC
                INNER JOIN 
                    Rubric AS R ON AC.RubricId = R.Id
                INNER JOIN 
                    RubricLevel AS RL ON R.Id = RL.RubricId";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(dataTable);
                            }
                        }
                    }

                    dataGridView.DataSource = dataTable;
                    dataGridView.Refresh(); // Refresh the DataGridView to reflect changes
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



            private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form6 f6 = new Form6();
            f6.Show();
        }

        // Define an empty CellContentClick event handler to resolve the CS1061 error
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // This event handler can be left empty as it is not currently needed
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            LoadRubrics();
        }
    }
}
