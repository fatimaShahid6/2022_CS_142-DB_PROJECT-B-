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
    public partial class Form20 : Form
    {
        private Configuration configuration;

        public Form20()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form18 f18 = new Form18();
            f18.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)

        {
           
        }

        private void LoadClassAttendanceData()
        {
            try
            {
                // Create a new DataTable to hold the data
                DataTable dataTable = new DataTable();

                // Establish connection to the database
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    // Define the SQL query
                    string query = "SELECT Id, AttendanceDate FROM ClassAttendance";

                    // Create a SqlCommand object
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Create a SqlDataAdapter to execute the query and fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form20_Load(object sender, EventArgs e)
        {
            LoadClassAttendanceData();
        }
    }
}
