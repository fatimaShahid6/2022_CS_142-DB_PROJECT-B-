using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form30 : Form
    {
        private Configuration configuration;

        public Form30()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form28 f28 = new Form28();
            f28.Show();
            this.Hide();
        }

        private void Form30_Load(object sender, EventArgs e)
        {
            LoadRubricLevelData();
        }

        private void LoadRubricLevelData()
        {
            try
            {
                // Create a new DataTable to hold the data
                DataTable dataTable = new DataTable();

                // Establish connection to the database
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    // Define the SQL query to select data from the RubricLevel table
                    string query = "SELECT * FROM RubricLevel";

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
    }
}
