using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form19 : Form
    {
        private Configuration configuration;

        public Form19()
        {
            InitializeComponent();
            configuration = Configuration.getInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = DateTime.Now;

                InsertClassAttendanceRecord(currentDate);

                MessageBox.Show("Attendance has been recorded successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void InsertClassAttendanceRecord(DateTime currentDate)
        {
            using (SqlConnection con = configuration.getConnection())
            {
                con.Open();
                string query = "INSERT INTO ClassAttendance (AttendanceDate) VALUES (@AttendanceDate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AttendanceDate", currentDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form18 f18 = new Form18();
            f18.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form20 f20 = new Form20();
            f20.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form21 f21 = new Form21();
            f21.Show();
            this.Hide();
        }
    }
}