using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicNetworkUI
{
    public class GroupMember//Represents the many-to-many relationship between users and groups
    {
        public int MemberID { get; set; }
        public int GroupID { get; set; }
        public string Username { get; set; }

        public GroupMember(int groupID, string username)
        {
            GroupID = groupID;
            Username = username;
        }
    }
}
