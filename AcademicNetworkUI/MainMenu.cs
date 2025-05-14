using MaterialSkin.Controls;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static AcademicNetworkUI.UserSettings;

namespace AcademicNetworkUI
{
    public partial class MainMenu : Form
    {

        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Public\Downloads\User.accdb;";
        private string loggedInUser;
        private int loggedInUserID;

        public MainMenu(int userId, string username)
        {
            InitializeComponent();
            loggedInUser = username;
            loggedInUserID = userId;
            lblUsernameDisplay.Text = loggedInUser;
            LoadGroups();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMenu_Click_1(object sender, EventArgs e)
        {

        }

        private void btnMenu_Click_1(object sender, MouseEventArgs e)
        {


        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            panelMembers.Visible = !panelMembers.Visible;
        }

        private void rightBtn_MouseClick(object sender, MouseEventArgs e)
        {
            panelMembers.Visible = !panelMembers.Visible;
        }

        private void siticoneCloseButton1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private UserSettings userSettingsForm;

        private void siticoneImageButton2_Click(object sender, EventArgs e)
        {
            if (userSettingsForm == null || userSettingsForm.IsDisposed)
            {
                Console.WriteLine("Opening UserSettings form...");
                userSettingsForm = new UserSettings(loggedInUserID, this);
                userSettingsForm.Show();
            }
            else
            {
                Console.WriteLine("UserSettings form is already open.");
            }
        }

        public void HandleLogin(int userID, string username)
        {
            loggedInUserID = userID;
            loggedInUser = username;
            lblUsernameDisplay.Text = username;
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            panelCreateGroup.Visible = true;
        }
        private void LoadGroups()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT G.GroupID, G.GroupName 
                         FROM Groups G
                         INNER JOIN GroupMembers GM ON G.GroupID = GM.GroupID
                         WHERE GM.Username = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", loggedInUser);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvGroups.DataSource = dt;
                    }
                }
            }

            if (dgvGroups.Columns["GroupID"] != null)
            {
                dgvGroups.Columns["GroupID"].Visible = false;
            }

            dgvGroups.Columns["GroupName"].HeaderText = "Group Name";
            dgvGroups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGroups.AllowUserToAddRows = false;
        }



        private void btnConfirmCreateGroup_Click(object sender, EventArgs e)
        {


            string groupName = txtGroupName.Text.Trim();

            if (string.IsNullOrEmpty(groupName))
            {
                MessageBox.Show("Please enter a group name.");
                return;
            }

            CreateGroup(groupName, loggedInUser);

            MessageBox.Show("Group created successfully!");
            txtGroupName.Clear();
            panelCreateGroup.Visible = false;
            LoadGroups();
        }

        private void CreateGroup(string groupName, string currentUsername)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string insertGroupQuery = "INSERT INTO Groups (GroupName, CreatedBy) VALUES (?, ?)";
                using (OleDbCommand cmd = new OleDbCommand(insertGroupQuery, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupName);
                    cmd.Parameters.AddWithValue("?", currentUsername);
                    cmd.ExecuteNonQuery();
                }

                string getGroupIDQuery = "SELECT MAX(GroupID) FROM Groups";
                int groupID;
                using (OleDbCommand cmd = new OleDbCommand(getGroupIDQuery, conn))
                {
                    groupID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string insertMemberQuery = "INSERT INTO GroupMembers (GroupID, Username) VALUES (?, ?)";
                using (OleDbCommand cmd = new OleDbCommand(insertMemberQuery, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);
                    cmd.Parameters.AddWithValue("?", currentUsername);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void JoinGroup(int groupID, string currentUsername)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO GroupMembers (GroupID, Username) VALUES (?, ?)";
                using (OleDbCommand cmd = new OleDbCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);
                    cmd.Parameters.AddWithValue("?", currentUsername);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void btnCancelCreateGroup_Click(object sender, EventArgs e)
        {
            panelCreateGroup.Visible = false;
            txtGroupName.Clear();
        }
        private void JoinGroupAction(int groupID, string currentUsername)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM GroupMembers WHERE GroupID = ? AND Username = ?";
                using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("?", groupID);
                    checkCmd.Parameters.AddWithValue("?", currentUsername);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("You are already a member of this group.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO GroupMembers (GroupID, Username) VALUES (?, ?)";
                using (OleDbCommand cmd = new OleDbCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);
                    cmd.Parameters.AddWithValue("?", currentUsername);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Successfully joined the group!");
            LoadGroups();
        }
        string query = "SELECT GroupName FROM Groups";

        private void LoadAvailableGroups()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT G.GroupID, G.GroupName 
                         FROM Groups G
                         WHERE G.GroupID NOT IN 
                         (SELECT GM.GroupID FROM GroupMembers GM WHERE GM.Username = ?)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", loggedInUser);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvAvailableGroups.DataSource = dt;
                    }
                }
            }

            if (dgvAvailableGroups.Columns.Contains("GroupID"))
            {
                dgvAvailableGroups.Columns["GroupID"].Visible = false;
            }

            dgvAvailableGroups.Columns["GroupName"].HeaderText = "Available Groups";
        }

        private void siticoneButton2_Click_1(object sender, EventArgs e)
        {
            panelJoinGroup.Visible = true;
            LoadAvailableGroups();

        }

        private void btnConfirmJoin_Click(object sender, EventArgs e)
        {
            if (selectedGroupID == -1)
            {
                MessageBox.Show("Please select a group to join.");
                return;
            }

            string username = loggedInUser;

            string query = "INSERT INTO GroupMembers (GroupID, Username) VALUES (?, ?)";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", selectedGroupID);
                        cmd.Parameters.AddWithValue("?", username);
                        cmd.ExecuteNonQuery();
                    }


                    LoadAvailableGroups();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

            MessageBox.Show("You have successfully joined the group!");

            panelJoinGroup.Visible = false;
            LoadGroups();
        }

        private void btnCancelJoin_Click(object sender, EventArgs e)
        {
            panelJoinGroup.Visible = false;
        }
        private string currentUsername;
        private int currentGroupID;
        private TextBox txtEditMessage;
        private Button btnSaveChanges;
        private void InitializeUserDetails(string username, int groupID)
        {
            currentUsername = username;
            currentGroupID = groupID;
        }
        private void EditMessage()
        {
            if (lstMessages.SelectedItem != null)
            {
                Message selectedMessage = lstMessages.SelectedItem as Message;

                if (selectedMessage != null)
                {
                    txtEditMessage.Text = selectedMessage.MessageText;

                    btnSaveEdit.Click += (sender, e) =>
                    {
                        string newMessageText = txtEditMessage.Text;

                        using (OleDbConnection conn = new OleDbConnection(connectionString))
                        {
                            conn.Open();
                            string query = "UPDATE Messages SET MessageText = ? WHERE MessageID = ?";
                            using (OleDbCommand cmd = new OleDbCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("?", newMessageText);
                                cmd.Parameters.AddWithValue("?", selectedMessage.MessageID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        LoadMessages(selectedMessage.GroupID);
                    };
                }
            }
        }
        private void DeleteMessage()
        {
            if (lstMessages.SelectedItem != null)
            {
                Message selectedMessage = lstMessages.SelectedItem as Message;

                if (selectedMessage != null)
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Messages WHERE MessageID = ?";
                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("?", selectedMessage.MessageID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadMessages(selectedMessage.GroupID);
                }
            }
        }

        private void LoadMessages(int groupID)
        {
            lstMessages.Items.Clear();
            messageCache.Clear();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MessageID, GroupID, Sender, MessageText, Timestamp FROM Messages WHERE GroupID = ? ORDER BY Timestamp ASC";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        int index = 0;
                        while (reader.Read())
                        {
                            Message msg = new Message
                            {
                                MessageID = Convert.ToInt32(reader["MessageID"]),
                                GroupID = Convert.ToInt32(reader["GroupID"]),
                                Sender = reader["Sender"].ToString(),
                                MessageText = reader["MessageText"].ToString(),
                                Timestamp = Convert.ToDateTime(reader["Timestamp"])
                            };

                            string displayText = $"[{msg.Timestamp}] {msg.Sender}: {msg.MessageText}";
                            lstMessages.Items.Add(displayText);

                            messageCache[index] = msg;
                            index++;
                        }
                    }
                }
            }
        }
        private void lstMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMessages.SelectedIndex != -1)
            {
                Message selectedMsg = messageCache[lstMessages.SelectedIndex];

                bool isCurrentUserMessage = selectedMsg.Sender == loggedInUser;

                btnEditMessage.Enabled = isCurrentUserMessage;
                btnDeleteMessage.Enabled = isCurrentUserMessage;
            }
            else
            {
                btnEditMessage.Enabled = false;
                btnDeleteMessage.Enabled = false;
            }

            btnSaveEdit.Enabled = false;
        }

        private void LoadMessages(int groupID, string currentUsername)
        {
            lstMessages.Items.Clear();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT Sender, MessageText, Timestamp FROM Messages WHERE GroupID = ? ORDER BY Timestamp ASC";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sender = reader["Sender"].ToString();
                            string message = reader["MessageText"].ToString();
                            string timestamp = reader["Timestamp"].ToString();

                            if (sender == currentUsername)
                            {
                                lstMessages.Items.Add($"[{timestamp}] {sender}: {message}");
                            }
                        }
                    }
                }
            }
        }


        public class Message
        {
            public int MessageID { get; set; }
            public int GroupID { get; set; }
            public string Sender { get; set; }
            public string MessageText { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private int currentMessageID;


        private void btnEditMessage_Click(object sender, EventArgs e)
        {
            if (lstMessages.SelectedIndex != -1 && messageCache.ContainsKey(lstMessages.SelectedIndex))
            {
                Message selectedMsg = messageCache[lstMessages.SelectedIndex];
                if (selectedMsg != null)
                {
                    string newText = Microsoft.VisualBasic.Interaction.InputBox(
                        "Edit your message:",
                        "Edit Message",
                        selectedMsg.MessageText);

                    if (!string.IsNullOrWhiteSpace(newText) && newText != selectedMsg.MessageText)
                    {
                        string filteredText = FilterBadWords(newText.Trim());
                        UpdateMessage(selectedMsg, filteredText);
                    }
                }
            }
        }

        private void UpdateMessage(Message msg, string newText)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Messages SET MessageText = ? WHERE MessageID = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", newText);
                        cmd.Parameters.AddWithValue("?", msg.MessageID);
                        cmd.ExecuteNonQuery();
                    }
                }

                msg.MessageText = newText;

                int selectedIndex = lstMessages.SelectedIndex;
                lstMessages.Items[selectedIndex] = $"[{msg.Timestamp}] {msg.Sender}: {newText}";

                MessageBox.Show("Message updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetMessageIDFromMessage(string message)
        {
            return int.Parse(message.Split(']')[0].Trim('['));
        }

        private string GetMessageContentFromMessage(string message)
        {
            return message.Substring(message.IndexOf(']') + 2);
        }
        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            if (lstMessages.SelectedIndex != -1 && txtEditMessage.Visible)
            {
                Message selectedMsg = messageCache[lstMessages.SelectedIndex];
                string updatedText = txtEditMessage.Text;


                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Messages SET MessageText = ? WHERE MessageID = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", updatedText);
                        cmd.Parameters.AddWithValue("?", selectedMsg.MessageID);
                        cmd.ExecuteNonQuery();
                    }
                }

                selectedMsg.MessageText = updatedText;


                int selectedIndex = lstMessages.SelectedIndex;
                lstMessages.Items[selectedIndex] = $"[{selectedMsg.Timestamp}] {selectedMsg.Sender}: {updatedText}";

                txtEditMessage.Visible = false;
                btnSaveEdit.Enabled = false;
                btnEditMessage.Enabled = true;
            }
        }
        private Dictionary<int, Message> messageCache = new Dictionary<int, Message>();

        private void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            if (lstMessages.SelectedIndex != -1)
            {
                Message selectedMsg = messageCache[lstMessages.SelectedIndex];

                DialogResult result = MessageBox.Show("Are you sure you want to delete this message?",
                                                      "Confirm Delete",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Messages WHERE MessageID = ?";
                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("?", selectedMsg.MessageID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadMessages(selectedMsg.GroupID);
                }
            }
        }


        private int GetSelectedGroupID()
        {
            if (dgvGroups.SelectedRows.Count > 0)
            {
                int groupID = Convert.ToInt32(dgvGroups.SelectedRows[0].Cells["GroupID"].Value);
                return groupID;
            }
            else
            {
                MessageBox.Show("Please select a group.");
                return -1;
            }
        }
        private List<string> badWords = new List<string>
        {
             "badword",
             "badword2",
             "badword3",
             "badword4",
        };
        private string FilterBadWords(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string filteredText = text;
            foreach (string badWord in badWords)
            {

                string replacement = new string('*', badWord.Length);

                filteredText = Regex.Replace(filteredText, Regex.Escape(badWord), replacement, RegexOptions.IgnoreCase);
            }
            return filteredText;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                return;
            }

            int groupID = GetSelectedGroupID();
            if (groupID == -1)
            {
                MessageBox.Show("Please select a group to send a message.");
                return;
            }

            string messageText = FilterBadWords(txtMessage.Text.Trim());
            string senderUsername = loggedInUser;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Messages ([GroupID], [Sender], [MessageText], [Timestamp]) VALUES (?, ?, ?, ?)";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);
                    cmd.Parameters.AddWithValue("?", senderUsername);
                    cmd.Parameters.AddWithValue("?", messageText);
                    cmd.Parameters.AddWithValue("?", timestamp);
                    cmd.ExecuteNonQuery();
                }
            }

            txtMessage.Clear();
            LoadMessages(groupID);
        }
        private void dgvAvailableGroups_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvAvailableGroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvGroups_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string groupName = dgvGroups.Rows[e.RowIndex].Cells["GroupName"].Value?.ToString();

                if (string.IsNullOrEmpty(groupName))
                {
                    MessageBox.Show("Group name is unavailable.");
                    return;
                }

                lblGroupNameDisplay.Text = groupName;

                int groupID = Convert.ToInt32(dgvGroups.Rows[e.RowIndex].Cells["GroupID"].Value);

                LoadGroupMembers(groupID);

                LoadMessages(groupID);
            }
        }

        private int selectedGroupID = -1;
        private void dgvAvailableGroups_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                selectedGroupID = Convert.ToInt32(dgvAvailableGroups.Rows[e.RowIndex].Cells["GroupID"].Value);


                lblSelectedGroupID.Text = selectedGroupID.ToString();
            }
        }
        //private void LoadGroupMembers(int groupID)
        //{
        //    dgvMembers.Rows.Clear();

        //    using (OleDbConnection conn = new OleDbConnection(connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();

        //            Console.WriteLine("Connection Opened");


        //            string query = @"SELECT GM.Username 
        //                     FROM GroupMembers GM
        //                     WHERE GM.GroupID = ?"; 

        //            using (OleDbCommand cmd = new OleDbCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("?", groupID);

        //                using (OleDbDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            string username = reader["Username"].ToString();
        //                            Console.WriteLine("Fetched Username: " + username); 

        //                            dgvMembers.Rows.Add(username); 
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("No rows found for GroupID: " + groupID); 
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Error: " + ex.Message);  
        //        }
        //    }
        //}




        private void panelJoinGroup_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEditMessage_Click_1(object sender, EventArgs e)
        {

        }
        private bool IsGroupCreator(int groupID, string username)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT CreatedBy FROM Groups WHERE GroupID = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString() == username;
                    }
                }
            }
            return false;
        }
        private void LoadGroupMembers(int groupID)
        {
            lstGroupMembers.Items.Clear();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Username FROM GroupMembers WHERE GroupID = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstGroupMembers.Items.Add(reader["Username"].ToString());
                        }
                    }
                }
            }

            bool isMember = IsUserMemberOfGroup(groupID, loggedInUser);
            btnLeaveGroup.Visible = isMember;

            bool isCreator = IsGroupCreator(groupID, loggedInUser);
            btnKickMember.Enabled = isCreator && lstGroupMembers.SelectedIndex != -1;
            btnKickMember.Visible = isCreator;
        }

        private bool IsUserMemberOfGroup(int groupID, string username)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM GroupMembers WHERE GroupID = ? AND Username = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", groupID);
                    cmd.Parameters.AddWithValue("?", username);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private void btnLeaveGroup_Click(object sender, EventArgs e)
        {
            int groupID = GetSelectedGroupID();
            if (groupID == -1)
            {
                MessageBox.Show("Please select a group first.", "No Group Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to leave this group?",
                                                  "Confirm Leave",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LeaveGroup(groupID);
            }
        }

        private void LeaveGroup(int groupID)
        {
            try
            {

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM GroupMembers WHERE GroupID = ? AND Username = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", groupID);
                        cmd.Parameters.AddWithValue("?", loggedInUser);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("You have left the group.", "Left Group", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadGroupMembers(groupID);
                            btnLeaveGroup.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("You are not a member of this group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                LoadGroups();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error leaving group: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KickMember(int groupID, string memberUsername)
        {
            try
            {

                if (!IsGroupCreator(groupID, loggedInUser))
                {
                    MessageBox.Show("Only the group creator can kick members.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (memberUsername == loggedInUser)
                {
                    MessageBox.Show("You cannot kick yourself from the group.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM GroupMembers WHERE GroupID = ? AND Username = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", groupID);
                        cmd.Parameters.AddWithValue("?", memberUsername);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"{memberUsername} has been kicked from the group.", "Member Kicked", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadGroupMembers(groupID);
                        }
                        else
                        {
                            MessageBox.Show("Failed to kick member. They may have already left the group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error kicking member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnKickMember_Click(object sender, EventArgs e)
        {
            int groupID = GetSelectedGroupID();
            if (groupID == -1)
            {
                MessageBox.Show("Please select a group first.", "No Group Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstGroupMembers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a member to kick.", "No Member Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string memberToKick = lstGroupMembers.SelectedItem.ToString();

            DialogResult result = MessageBox.Show($"Are you sure you want to kick {memberToKick} from the group?",
                                                "Confirm Kick",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                KickMember(groupID, memberToKick);
            }
        }

        private void lstGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            int groupID = GetSelectedGroupID();
            if (groupID != -1)
            {
                LoadMessages(groupID);
                LoadGroupMembers(groupID);

                bool isCreator = IsGroupCreator(groupID, loggedInUser);
                btnKickMember.Visible = isCreator;

            }
        }
        private void lstGroupMembers_SelectedIndexChanged(object sender, EventArgs e)
        {

            int groupID = GetSelectedGroupID();
            bool isCreator = IsGroupCreator(groupID, loggedInUser);

            btnKickMember.Enabled = isCreator && lstGroupMembers.SelectedIndex != -1;
        }

        private void dgvGroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
