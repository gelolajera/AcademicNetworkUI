using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.OleDb;
using System.Windows.Forms;
using BCrypt.Net;

namespace AcademicNetworkUI
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Public\Downloads\User.accdb;";

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }
        private bool IsUsernameTaken(string username)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", username);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; 
                }
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string verifyPassword = txtVerifyPassword.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            if (IsUsernameTaken(username))
            {
                MessageBox.Show("Username is already taken. Please choose a different username.");
                return; 
            }
            if (password != verifyPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return; 
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, [Password], Email, Phone) VALUES (?, ?, ?, ?)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", username);
                    cmd.Parameters.AddWithValue("?", hashedPassword); 
                    cmd.Parameters.AddWithValue("?", email);
                    cmd.Parameters.AddWithValue("?", phone);
                    cmd.ExecuteNonQuery();
                }
              
                txtUsername.Clear();
                txtPassword.Clear();
                txtVerifyPassword.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
            }

            MessageBox.Show("Registration successful!");
            btnBack_Click(sender ,e);
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Connection Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Connection Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
