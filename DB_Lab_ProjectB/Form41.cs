using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form41 : Form
    {
        private Configuration configuration;

        public Form41()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form39 f39 = new Form39();
            f39.Show();
            Hide();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    string query = "SELECT * FROM AssessmentComponent";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Form41_Load(object sender, EventArgs e)
        {
            LoadData(); // Load data when the form loads
        }
    }
}
