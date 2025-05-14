using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace AcademicNetworkUI
{
    public partial class UserSettings : Form
    {
        private int currentUserID; 
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Public\Downloads\User.accdb;";

        private string currentUsername;
        MainMenu mainMenuForm;
        public UserSettings(int userId, MainMenu mainMenuForm)
        {
            InitializeComponent();
            currentUserID = userId;
            currentUsername = loggedInUser;
            LoadUserInfo();
            this.mainMenuForm = mainMenuForm;
        }


        private void LoadUserInfo()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                // For OleDb, use ? as a parameter placeholder instead of named parameters
                string query = "SELECT Username, Phone, Email FROM Users WHERE ID = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    // Add the parameter in the correct order
                    cmd.Parameters.AddWithValue("?", currentUserID);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())  // Check if the query returned any data
                        {
                            lblUsernameValue.Text = reader["Username"].ToString();
                            lblPhoneValue.Text = reader["Phone"].ToString();
                            lblEmailValue.Text = reader["Email"].ToString();

                            // Also set the text in edit textboxes
                            //txtUsernameEdit.Text = reader["Username"].ToString();
                            txtPhoneEdit.Text = reader["Phone"].ToString();
                            txtEmailEdit.Text = reader["Email"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("User information not found.");
                        }
                    }
                }
            }
        }








        private void btnLogOut_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (!(form is Login))
                {
                    form.Close();
                }
            }

            new Login().Show();
        }

        private void btnEditUsername_Click(object sender, EventArgs e)
        {
            lblUsernameValue.Visible = false;
            //txtUsernameEdit.Text = lblUsernameValue.Text;
            //txtUsernameEdit.Visible = true;
            btnSaveChanges.Visible = true;
            btnCancelChanges.Visible = true;
        }

        private void btnEditPhone_Click(object sender, EventArgs e)
        {
            lblPhoneValue.Visible = false;
            txtPhoneEdit.Text = lblPhoneValue.Text;
            txtPhoneEdit.Visible = true;

            btnSaveChanges.Visible = true;
            btnCancelChanges.Visible = true;
        }


        private void btnEditEmail_Click(object sender, EventArgs e)
        {
            lblEmailValue.Visible = false;
            txtEmailEdit.Text = lblEmailValue.Text;
            txtEmailEdit.Visible = true;

            btnSaveChanges.Visible = true;
            btnCancelChanges.Visible = true;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //string newUsername = txtUsernameEdit.Visible ? txtUsernameEdit.Text : null;
            string newPhone = txtPhoneEdit.Visible ? txtPhoneEdit.Text : null;
            string newEmail = txtEmailEdit.Visible ? txtEmailEdit.Text : null;

            // Validate visible fields
            if ((txtPhoneEdit.Visible && string.IsNullOrWhiteSpace(newPhone)) ||
                (txtEmailEdit.Visible && string.IsNullOrWhiteSpace(newEmail)))
            {
                MessageBox.Show("Please fill in all visible fields.");
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    List<string> fieldsToUpdate = new List<string>();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;

                    //if (txtUsernameEdit.Visible)
                    //{
                    //    fieldsToUpdate.Add("Username = ?");
                    //    cmd.Parameters.AddWithValue("?", newUsername);
                    //}

                    if (txtPhoneEdit.Visible)
                    {
                        fieldsToUpdate.Add("Phone = ?");
                        cmd.Parameters.AddWithValue("?", newPhone);
                    }

                    if (txtEmailEdit.Visible)
                    {
                        fieldsToUpdate.Add("Email = ?");
                        cmd.Parameters.AddWithValue("?", newEmail);
                    }

                    if (fieldsToUpdate.Count == 0)
                    {
                        MessageBox.Show("No fields were edited.");
                        return;
                    }

                    string query = "UPDATE Users SET " + string.Join(", ", fieldsToUpdate) + " WHERE ID = ?";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("?", currentUserID); // Use currentUserID (not creating new account)

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // ✅ Update Messages table if username changed
                        //if (txtUsernameEdit.Visible && !string.IsNullOrWhiteSpace(newUsername) && newUsername != lblUsernameValue.Text)
                        //{
                        //    string oldUsername = lblUsernameValue.Text;

                        //    using (OleDbCommand updateMessagesCmd = new OleDbCommand("UPDATE Messages SET Sender = ? WHERE Sender = ?", conn))
                        //    {
                        //        updateMessagesCmd.Parameters.AddWithValue("?", newUsername);
                        //        updateMessagesCmd.Parameters.AddWithValue("?", oldUsername);
                        //        updateMessagesCmd.ExecuteNonQuery();
                        //    }
                        //}

                        MessageBox.Show("User information updated successfully!");

                        // ✅ Update MainMenu label if username was changed
                        //if (txtUsernameEdit.Visible && !string.IsNullOrWhiteSpace(newUsername))
                        {
                            if (mainMenuForm != null && !mainMenuForm.IsDisposed)
                            {
                                //mainMenuForm.lblUsernameDisplay.Text = newUsername;
                            }
                            //loggedInUser = newUsername;
                        }

                        LoadUserInfo(); // Refresh display

                        // Hide edit boxes and show labels
                        //txtUsernameEdit.Visible = false;
                        txtPhoneEdit.Visible = false;
                        txtEmailEdit.Visible = false;

                        lblUsernameValue.Visible = true;
                        lblPhoneValue.Visible = true;
                        lblEmailValue.Visible = true;

                        btnSaveChanges.Visible = false;
                        btnCancelChanges.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


    
    public string loggedInUser;
        private void btnCancelChanges_Click(object sender, EventArgs e)
        {
            //if (txtUsernameEdit.Visible)
            //{
            //    txtUsernameEdit.Visible = false;
            //    lblUsernameValue.Visible = true;
            //    txtUsernameEdit.Text = lblUsernameValue.Text;
            //}

            // Hide and reset Phone
            if (txtPhoneEdit.Visible)
            {
                txtPhoneEdit.Visible = false;
                lblPhoneValue.Visible = true;
                txtPhoneEdit.Text = lblPhoneValue.Text;
            }

            // Hide and reset Email
            if (txtEmailEdit.Visible)
            {
                txtEmailEdit.Visible = false;
                lblEmailValue.Visible = true;
                txtEmailEdit.Text = lblEmailValue.Text;
            }

            // Hide Save and Cancel buttons
            btnSaveChanges.Visible = false;
            btnCancelChanges.Visible = false;
        }

        private void siticoneButton6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete your account? This action cannot be undone.", "Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteAccount();
            }
        }
        private void DeleteAccount()
        {
            var result = MessageBox.Show("Are you sure you want to delete your account?", "Delete Account", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                

                // Proceed with deleting the account
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Users WHERE ID = ?";
                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("?", currentUserID);  // Use the current user's ID

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Your account has been deleted.");

                                // Clear session data
                                currentUserID = 0;  // Clear the logged-in user ID
                                currentUsername = null;  // Clear the username
                                foreach (Form form in Application.OpenForms)
                                {
                                    if (form is MainMenu)
                                    {
                                        form.Close();  // Close MainMenu
                                        break;  // Exit after closing MainMenu
                                    }
                                }
                                // Close the current form (UserSettings)
                                this.Close();

                                // Navigate to the Login page
                                Login loginForm = new Login();
                                loginForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Account deletion failed.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting account: " + ex.Message);
                    }
                }
            }
        }

        public static class CurrentUser
        {
            public static int UserID { get; set; }
            public static string Username { get; set; }
        }

        private void LogoutUser()
        {
            CurrentUser.UserID = 0;
            CurrentUser.Username = null;

            // Navigate to login form
            Login loginForm = new Login(); // Assuming the login form is named Login
            loginForm.Show();
            this.Hide(); //
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {

        }
        
    }
}
