using System;
using System.Data.OleDb;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using BCrypt.Net;

namespace AcademicNetworkUI
{
    public partial class ForgotPassword : Form
    {
        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Public\Downloads\User.accdb;";

        public ForgotPassword()
        {
            InitializeComponent();  
        }
        private bool isVerified = false;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        private string generatedCode;
        private string userEmail;
        public static OleDbDataReader ExecuteQuery(string query, OleDbParameter[] parameters)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            conn.Open();
            using (OleDbCommand cmd = new OleDbCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
        }
        private void SendCode_Click(object sender, EventArgs e)
        {
            userEmail = tbxEmail.Text.Trim();

            if (string.IsNullOrEmpty(userEmail))
            {
                MessageBox.Show("Please enter your email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
            OleDbParameter[] parameters = { new OleDbParameter("@Email", userEmail) };

            using (OleDbDataReader reader = ExecuteQuery(query, parameters))
            {
                reader.Read();
                int count = Convert.ToInt32(reader[0]);

                if (count == 0)
                {
                    MessageBox.Show("Email not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Random random = new Random();
            generatedCode = random.Next(100000, 999999).ToString();

            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("gelo.lajera@gmail.com", "shuy ygby jkrz fghr"); 
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("gelo.lajera@gmail.com");
                mail.To.Add(userEmail);
                mail.Subject = "Password Reset Verification Code";
                mail.Body = $"Your verification code is: {generatedCode}";

                smtp.Send(mail);

                MessageBox.Show("Verification code sent! Check your email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            if (tbxVerificationCode.Text.Trim() == generatedCode)
            {
                isVerified = true;
                MessageBox.Show("Verification successful! You can now reset your password.", "Verified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Incorrect verification code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            string newPassword = tbxPassword.Text.Trim();
            string confirmPassword = tbxConfirmPassword.Text.Trim();

            if (!isVerified)
            {
                MessageBox.Show("You must verify the code first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all password fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            string updateQuery = "UPDATE Users SET [Password] = @Password WHERE [Email] = @Email";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            using (OleDbCommand cmd = new OleDbCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@Email", userEmail);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password successfully reset!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password reset failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Close();
        }
    }
}
