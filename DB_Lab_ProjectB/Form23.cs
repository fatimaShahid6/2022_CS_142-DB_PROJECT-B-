using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form23 : Form
    {
        private Configuration configuration;

        public Form23()
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Replace the values with actual input from your form
            int attendanceId = 1; // Example value
            int studentId = 1; // Example value
            int attendanceStatus = 1; // Example value

            MarkAttendance(attendanceId, studentId, attendanceStatus);
        }

        private void MarkAttendance(int attendanceId, int studentId, int attendanceStatus)
        {
            try
            {
                // Validate attendance status
                if (attendanceStatus < 1 || attendanceStatus > 4)
                {
                    MessageBox.Show("Invalid attendance status. Please enter 1 for Present, 2 for Absent, 3 for Leave, or 4 for Late.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert attendance record into the database
                using (SqlConnection con = configuration.getConnection())
                {
                    con.Open();
                    string query = "INSERT INTO StudentAttendance (AttendanceId, StudentId, AttendanceStatus) VALUES (@AttendanceId, @StudentId, @AttendanceStatus)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@AttendanceStatus", attendanceStatus);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Attendance marked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to mark attendance.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
