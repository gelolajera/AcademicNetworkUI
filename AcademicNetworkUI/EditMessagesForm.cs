using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AcademicNetworkUI.MainMenu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AcademicNetworkUI
{
    public partial class EditMessagesForm : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Public\Downloads\User.accdb;";
        private int currentGroupID;
        private string currentUsername;
        private int selectedMessageID = -1;  
        private int groupID;
        private string sender;
        private int messageID;
        public EditMessagesForm(int groupID, string username)
        {
            InitializeComponent();
            this.groupID = groupID;
            this.sender = sender;
            this.messageID = messageID;

            LoadMessageForEditing();
        }
        private void LoadMessageForEditing()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT MessageText FROM Messages WHERE MessageID = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", messageID);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtEditMessage.Text = reader["MessageText"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Message not found.");
                        }
                    }
                }
            }
        }
    
    private void LoadUserMessages()
        {
            lstUserMessages.Items.Clear();  

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT MessageID, MessageText FROM Messages WHERE GroupID = ? AND Sender = ? ORDER BY Timestamp ASC";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", currentGroupID); 
                    cmd.Parameters.AddWithValue("?", currentUsername);  

                    try
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int messageID = Convert.ToInt32(reader["MessageID"]);
                                string messageText = reader["MessageText"].ToString();

                                lstUserMessages.Items.Add(new MessageItem(messageID, messageText));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }



        private void lstUserMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUserMessages.SelectedItem != null)
            {
                var selectedItem = (MessageItem)lstUserMessages.SelectedItem;
                selectedMessageID = selectedItem.MessageID;  
                txtEditMessage.Text = selectedItem.MessageText;  
            }
        }
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string newMessageText = txtEditMessage.Text.Trim();

            if (string.IsNullOrEmpty(newMessageText))
            {
                MessageBox.Show("Message cannot be empty.");
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Messages SET MessageText = ? WHERE MessageID = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", newMessageText);
                    cmd.Parameters.AddWithValue("?", messageID);

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Message updated successfully!");
                            this.Close(); 
                        }
                        else
                        {
                            MessageBox.Show("Failed to update message.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            if (selectedMessageID != -1)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this message?", "Delete Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Messages WHERE MessageID = ?";

                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("?", selectedMessageID);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Message deleted successfully!");
                                LoadUserMessages();  
                            }
                            else
                            {
                                MessageBox.Show("Error deleting the message.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a message to delete.");
            }
        }
        public class MessageItem
        {
            public int MessageID { get; set; }
            public string MessageText { get; set; }

            public MessageItem(int messageID, string messageText)
            {
                MessageID = messageID;
                MessageText = messageText;
            }

            public override string ToString()
            {
                return MessageText;  
            }
        }
    }
    }

