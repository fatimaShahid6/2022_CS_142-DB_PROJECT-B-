using DB_Lab_ProjectB;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_Lab_ProjectB
{
    public partial class Form13 : Form
    {
        private SqlConnection con;

        public Form13()
        {
            InitializeComponent();
            con = Configuration.getInstance().getConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form11 f11 = new Form11();
            f11.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();

                // Ensure the connection is open before executing SQL commands
                con.Open();

                int statusValue;
                if (!int.TryParse(textBox6.Text, out statusValue))
                {
                    MessageBox.Show("Invalid status value. Please enter 5 for active or 6 for inactive student.");
                    return;
                }

                // Ensure the status value is either 5 (active) or 6 (inactive)
                if (statusValue != 5 && statusValue != 6)
                {
                    MessageBox.Show("Invalid status value. 5 for active and 6 for inactive.");
                    return;
                }

                SqlCommand cmdStudent = new SqlCommand("INSERT INTO Student (FirstName, LastName, RegistrationNumber, Contact, Email, Status) " +
                                                       "VALUES (@FirstName, @LastName, @RegistrationNumber, @Contact, @Email, @Status)", con);
                cmdStudent.Parameters.AddWithValue("@FirstName", textBox2.Text);
                cmdStudent.Parameters.AddWithValue("@LastName", textBox3.Text);
                cmdStudent.Parameters.AddWithValue("@RegistrationNumber", textBox1.Text);
                cmdStudent.Parameters.AddWithValue("@Contact", textBox4.Text);
                cmdStudent.Parameters.AddWithValue("@Email", textBox5.Text);
                cmdStudent.Parameters.AddWithValue("@Status", statusValue);

                cmdStudent.ExecuteNonQuery();

                MessageBox.Show("Successfully saved");

                // Close the connection after executing SQL commands
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
