using System;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using BCrypt.Net;

namespace AcademicNetworkUI
{
    public partial class Login : Form
    {
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Public\Downloads\User.accdb;";

        public Login()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT Password, ID FROM Users WHERE Username = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                       
                        cmd.Parameters.AddWithValue("?", username); 

                        Console.WriteLine($"Executing query: {query} with parameter: {username}");

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hashedPassword = reader["Password"].ToString(); 
                                int userId = Convert.ToInt32(reader["ID"]);

                                if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                                {
                                    MainMenu mainMenu = new MainMenu(userId, username);
                                    mainMenu.Show();
                                    this.Hide();
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect password.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username not found.");
                            }
                        }
                    }

                    MessageBox.Show("Invalid username or password.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login error: " + ex.Message);
                }
            }
        }


        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword forgotPasswordForm = new ForgotPassword();
            forgotPasswordForm.ShowDialog();
        }
    }
}
