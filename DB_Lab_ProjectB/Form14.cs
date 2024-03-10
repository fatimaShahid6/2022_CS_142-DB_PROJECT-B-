using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form14_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form11 f11 = new Form11();
            f11.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();

                int studentId;
                if (!int.TryParse(textBox1.Text, out studentId))
                {
                    MessageBox.Show("Invalid student ID. Please enter a valid numeric ID.");
                    return;
                }

                // Check if the student ID exists in the database
                SqlCommand cmdCheckStudent = new SqlCommand("SELECT COUNT(*) FROM Student WHERE Id = @Id", con);
                cmdCheckStudent.Parameters.AddWithValue("@Id", studentId);

                con.Open();
                int studentCount = (int)cmdCheckStudent.ExecuteScalar();
                con.Close();

                if (studentCount > 0)
                {
                    // Open Form15 with the student ID
                    Form15 f15 = new Form15(studentId);
                    f15.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Student with the given ID does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
