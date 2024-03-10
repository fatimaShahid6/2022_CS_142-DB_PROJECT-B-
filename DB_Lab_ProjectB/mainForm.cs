using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DB_Lab_ProjectB
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form6 f6 = new Form6();
            f6.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form11 f11 = new Form11();
            f11.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form18 f18 = new Form18();
            f18.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form28 f28 = new Form28();
            f28.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form33 f33 = new Form33();
            f33.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form45 f45 = new Form45();
            f45.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream("CloWiseClassResult.pdf", FileMode.Create));
                document.Open();

                Paragraph title = new Paragraph("CLO-wise Class Result");
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                document.Add(new Chunk("\n\n"));
                PdfPTable table = new PdfPTable(5); // Assuming 5 columns
                table.AddCell("CLO Name");
                table.AddCell("Student Name");
                table.AddCell("Registration Number");
                table.AddCell("Assessment Title");
                table.AddCell("Obtained Marks");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
            SELECT CLO.Name AS CLOName,
                   S.FirstName,
                   S.LastName,
                   S.RegistrationNumber,
                   A.Title AS AssessmentTitle
            FROM StudentResult SR
            INNER JOIN Student S ON SR.StudentId = S.Id
            INNER JOIN AssessmentComponent AC ON SR.AssessmentComponentId = AC.Id
            INNER JOIN Assessment A ON AC.AssessmentId = A.Id
            INNER JOIN Rubric R ON AC.RubricId = R.Id
            INNER JOIN CLO ON R.CloId = CLO.Id
            ORDER BY CLO.Name, S.FirstName, S.LastName";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string registrationNumber = reader["RegistrationNumber"].ToString();
                            string obtainedMarks = GetSumOfObtainedMarksFromCsv(csvFilePath, registrationNumber);

                            table.AddCell(reader["CLOName"].ToString());
                            table.AddCell(reader["FirstName"].ToString() + " " + reader["LastName"].ToString());
                            table.AddCell(registrationNumber);
                            table.AddCell(reader["AssessmentTitle"].ToString());
                            table.AddCell(obtainedMarks);
                        }
                    }
                }

                document.Add(table);
                document.Close();

                MessageBox.Show("PDF report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private string GetSumOfObtainedMarksFromCsv(string csvFilePath, string registrationNumber)
            {
                try
                {
                    // Read all lines from the CSV file
                    string[] lines = File.ReadAllLines(csvFilePath);

                    // Initialize sum of obtained marks
                    double sumObtainedMarks = 0;

                    // Iterate through each line
                    foreach (string line in lines)
                    {
                        // Split the line into columns
                        string[] columns = line.Split(',');

                        // Extract the registration number (which is assumed to be the first value)
                        string csvRegistrationNumber = columns[0].Trim();

                        // Check if the current line corresponds to the provided registration number
                        if (csvRegistrationNumber == registrationNumber)
                        {
                            // Extract and parse the obtained marks (which is assumed to be the last value in the line)
                            string obtainedMarksStr = columns[columns.Length - 1].Trim();
                            double obtainedMarks = 0;
                            if (double.TryParse(obtainedMarksStr, out obtainedMarks))
                            {
                                // Add obtained marks to the sum
                                sumObtainedMarks += obtainedMarks;
                            }
                        }
                    }

                    // Return the sum of obtained marks
                    return sumObtainedMarks.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading CSV file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Return "N/A" if there's an error or if no obtained marks found for the registration number
                return "N/A";

        }
        private string connectionString = "Data Source=DESKTOP-74I34NE;Initial Catalog=ProjectB;Integrated Security=True";
        private string csvFilePath = "StudentResult.txt";

        private string GetObtainedMarksFromCsv(string csvFilePath, string registrationNumber)
        {
            try
            {
                // Read all lines from the CSV file
                string[] lines = File.ReadAllLines(csvFilePath);

                // Iterate through each line (assuming CSV structure is consistent)
                foreach (string line in lines)
                {
                    // Split the line into columns
                    string[] columns = line.Split(',');

                    // Extract the registration number (which is assumed to be the first value)
                    string csvRegistrationNumber = columns[0].Trim();

                    // Check if the current line corresponds to the provided registration number
                    if (csvRegistrationNumber == registrationNumber)
                    {
                        // Return the obtained marks (which is assumed to be the last value in the line)
                        return columns[columns.Length - 1].Trim(); // Last value in the line
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading CSV file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Return a default value if the registration number is not found in the CSV file
            return "N/A";
        }

        private void GeneratePDF(string connectionString, string csvFilePath)
        {
            string pdfFilePath = "AssessmentWiseClassResult.pdf"; // Output PDF file path

            try
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
                document.Open();

                Paragraph title = new Paragraph("Assessment-wise Class Result");
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                document.Add(new Chunk("\n\n"));
                PdfPTable table = new PdfPTable(6); // Assuming 6 columns
                table.AddCell("Assessment Title");
                table.AddCell("CLO Name");
                table.AddCell("Student Name");
                table.AddCell("Registration Number");
                table.AddCell("Total Marks");
                table.AddCell("Obtained Marks");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
    SELECT A.Title AS AssessmentTitle,
           CLO.Name AS CLOName,
           S.FirstName,
           S.LastName,
           S.RegistrationNumber,
           AC.TotalMarks AS TotalMarks,
           SR.AssessmentComponentId AS AssessmentComponentId
    FROM StudentResult SR
    INNER JOIN Student S ON SR.StudentId = S.Id
    INNER JOIN AssessmentComponent AC ON SR.AssessmentComponentId = AC.Id
    INNER JOIN Assessment A ON AC.AssessmentId = A.Id
    INNER JOIN Rubric R ON AC.RubricId = R.Id
    INNER JOIN CLO ON R.CloId = CLO.Id
    ORDER BY A.Title, CLO.Name, S.FirstName, S.LastName";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string registrationNumber = reader["RegistrationNumber"].ToString();
                            string obtainedMarks = GetObtainedMarksFromCsv(csvFilePath, registrationNumber);
                            table.AddCell(reader["AssessmentTitle"].ToString());
                            table.AddCell(reader["CLOName"].ToString());
                            table.AddCell(reader["FirstName"].ToString() + " " + reader["LastName"].ToString());
                            table.AddCell(registrationNumber);
                            table.AddCell(reader["TotalMarks"].ToString());
                            table.AddCell(obtainedMarks);
                        }
                    }
                }

                document.Add(table);
                document.Close();

                MessageBox.Show("PDF report generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            GeneratePDF(connectionString, csvFilePath);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
