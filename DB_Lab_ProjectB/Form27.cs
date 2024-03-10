using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form27 : Form
    {
        private Configuration configuration;

        public Form27()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form22 f22 = new Form22();
            f22.Show();
            this.Hide();
        }

        private void SearchStudent(string registrationNumber)
        {
            try
            {
                string query = @"SELECT SA.AttendanceId, CA.AttendanceDate, S.FirstName, S.LastName, S.Contact, S.Email, 
                         S.RegistrationNumber, L1.Name AS Status, L2.Name AS AttendanceStatus
                     FROM StudentAttendance SA
                     JOIN ClassAttendance CA ON SA.AttendanceId = CA.Id
                     JOIN Student S ON SA.StudentId = S.Id
                     JOIN Lookup L1 ON S.Status = L1.LookupId
                     JOIN Lookup L2 ON SA.AttendanceStatus = L2.LookupId
                     WHERE S.RegistrationNumber = @RegistrationNumber";

                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("Registration number does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string registrationNumber = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(registrationNumber))
            {
                SearchStudent(registrationNumber);
            }
            else
            {
                MessageBox.Show("Please enter a registration number to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form27_Load(object sender, EventArgs e)
        {

        }
    }
}
