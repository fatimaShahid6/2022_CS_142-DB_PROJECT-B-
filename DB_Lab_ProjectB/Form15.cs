using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Data.SqlClient;

namespace DB_Lab_ProjectB
{
    public partial class Form15 : Form
    {
        // Store the student ID received from the previous form
        private int studentId;

        // Constructor with studentId parameter
        public Form15(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form14 f14 = new Form14();
            f14.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();

                // Fetch existing values from the database without revalidating ID
                SqlCommand cmdGetStudent = new SqlCommand("SELECT FirstName, LastName, Contact, Email, RegistrationNumber, Status FROM Student WHERE Id = @Id", con);
                cmdGetStudent.Parameters.AddWithValue("@Id", studentId);

                con.Open();
                SqlDataReader reader = cmdGetStudent.ExecuteReader();

                if (reader.Read())
                {
                    // Get new values from textboxes
                    string newFirstName = textBox2.Text;
                    string newLastName = textBox3.Text;
                    string newContact = textBox5.Text;
                    string newEmail = textBox6.Text;
                    string newRegistrationNumber = textBox4.Text;
                    int newStatus;

                    // Parse status value
                    if (!int.TryParse(textBox7.Text, out newStatus))
                    {
                        MessageBox.Show("Invalid status value. Please enter 5 for active or 6 for inactive.");
                        reader.Close();
                        con.Close();
                        return;
                    }

                    // Update only non-null and non-empty values provided by the user
                    if (string.IsNullOrEmpty(newFirstName)) newFirstName = reader["FirstName"].ToString();
                    if (string.IsNullOrEmpty(newLastName)) newLastName = reader["LastName"].ToString();
                    if (string.IsNullOrEmpty(newContact)) newContact = reader["Contact"].ToString();
                    if (string.IsNullOrEmpty(newEmail)) newEmail = reader["Email"].ToString();
                    if (string.IsNullOrEmpty(newRegistrationNumber)) newRegistrationNumber = reader["RegistrationNumber"].ToString();
                    if (string.IsNullOrEmpty(textBox7.Text)) newStatus = (int)reader["Status"];

                    reader.Close();

                    // Update the student record in the database
                    SqlCommand cmdUpdateStudent = new SqlCommand(
                        @"UPDATE Student
                          SET FirstName = @FirstName,
                              LastName = @LastName,
                              Contact = @Contact,
                              Email = @Email,
                              RegistrationNumber = @RegistrationNumber,
                              Status = @Status
                          WHERE Id = @Id", con);

                    cmdUpdateStudent.Parameters.AddWithValue("@FirstName", newFirstName);
                    cmdUpdateStudent.Parameters.AddWithValue("@LastName", newLastName);
                    cmdUpdateStudent.Parameters.AddWithValue("@Contact", newContact);
                    cmdUpdateStudent.Parameters.AddWithValue("@Email", newEmail);
                    cmdUpdateStudent.Parameters.AddWithValue("@RegistrationNumber", newRegistrationNumber);
                    cmdUpdateStudent.Parameters.AddWithValue("@Status", newStatus);
                    cmdUpdateStudent.Parameters.AddWithValue("@Id", studentId);

                    cmdUpdateStudent.ExecuteNonQuery();
                    MessageBox.Show("Student information updated successfully.");
                }
                else
                {
                    MessageBox.Show("Student with the given ID does not exist.");
                }

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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form15_Load(object sender, EventArgs e)
        {

        }
        // You can add event handlers and Form15_Load method here if needed
    }
}
