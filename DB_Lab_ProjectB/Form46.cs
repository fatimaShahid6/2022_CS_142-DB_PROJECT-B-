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
    public partial class Form46 : Form
    {
        private string connectionString = "Data Source=DESKTOP-74I34NE;Initial Catalog=ProjectB;Integrated Security=True";
        private string csvFilePath = "StudentResult.txt";
        public Form46()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form45 f45 = new Form45();
            f45.Show();
            this.Hide();
        }
        private void OverallResultForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"SELECT S.FirstName, S.LastName, S.RegistrationNumber,
                                           AC.Name AS AssessmentComponentName, AC.TotalMarks AS AssessmentComponentTotalMarks,
                                           RL.Details AS RubricDetails,
                                           SR.EvaluationDate
                                    FROM StudentResult SR
                                    INNER JOIN Student S ON SR.StudentId = S.Id
                                    INNER JOIN AssessmentComponent AC ON SR.AssessmentComponentId = AC.Id
                                    LEFT JOIN RubricLevel RL ON SR.RubricMeasurementId = RL.MeasurementLevel
                                    ORDER BY S.FirstName, S.LastName, SR.EvaluationDate";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Add ObtainedMarks column to the DataTable
                        dataTable.Columns.Add("ObtainedMarks", typeof(string));

                        // Read the ObtainedMarks from the CSV file and add it to the DataTable
                        using (StreamReader reader = new StreamReader(csvFilePath))
                        {
                            int rowIndex = 0;
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] parts = line.Split(',');
                                string obtainedMarks = parts[4]; // Assuming ObtainedMarks is the fifth column
                                dataTable.Rows[rowIndex]["ObtainedMarks"] = obtainedMarks;
                                rowIndex++;
                            }
                        }

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving overall student results: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form46_Load(object sender, EventArgs e)
        {
            OverallResultForm_Load(sender, e);
        }
    }
}
