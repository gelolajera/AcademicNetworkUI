using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicNetworkUI
{
    public class Group//Handles group management and membership
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string CreatedBy { get; set; }  

        public Group(string name, string creator)
        {
            GroupName = name;
            CreatedBy = creator;
        }

        public List<string> GetMembers()
        {
           
            return new List<string>();
        }

        public void AddMember(string username)
        {
           
        }

        public List<Message> GetMessages()
        {
          
            return new List<Message>();
        }
    }
}
