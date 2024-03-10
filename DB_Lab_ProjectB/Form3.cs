using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form3 : Form
    {
        private Configuration configuration;

        public Form3()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
            LoadCloData();
        }

        private void LoadCloData()
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
                    string query = "SELECT * FROM Clo";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // This event handler is left empty as it is not needed for this functionality
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadCloData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
