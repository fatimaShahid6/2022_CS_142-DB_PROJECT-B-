using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form36 : Form
    {
        private Configuration configuration;

        public Form36()
        {
            InitializeComponent();
            configuration = Configuration.getInstance(); // Assuming Configuration has a static getInstance() method
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form38 f38 = new Form38();
            f38.Show();
            this.Hide();
        }

        private void LoadData()
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
                    string query = "SELECT * FROM Assessment";

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

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Form36_Load(object sender, EventArgs e)
        {
            // Load data when the form is loaded
            LoadData();
        }
    }
}
