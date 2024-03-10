using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form24 : Form
    {
        private Configuration configuration;

        public Form24()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void Form24_Load(object sender, EventArgs e)
        {
            LoadStudentAttendanceData();
        }

        private void LoadStudentAttendanceData()
        {
            try
            {
                string query = @"SELECT SA.AttendanceId, CA.AttendanceDate, S.FirstName, S.LastName, S.Contact, S.Email, 
                             S.RegistrationNumber, L1.Name AS AttendanceStatus, L2.Name AS Status
                         FROM StudentAttendance SA
                         JOIN ClassAttendance CA ON SA.AttendanceId = CA.Id
                         JOIN Student S ON SA.StudentId = S.Id
                         JOIN Lookup L1 ON S.Status = L1.LookupId
                         JOIN Lookup L2 ON SA.AttendanceStatus = L2.LookupId";

                DataTable dataTable = new DataTable();

                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataTable);
                    }
                }

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form22 f22 = new Form22();
            f22.Show();
            this.Hide();
        }
    }
}
