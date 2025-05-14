using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicNetworkUI
{
    public class User//Manages user data and authentication
    {
        public int ID { get; set; }
        public string Username { get; set; }
        private string Password { get; set; }  
        public string Email { get; set; }
        public string Phone { get; set; }

        public User(string username, string email, string phone)
        {
            Username = username;
            Email = email;
            Phone = phone;
        }

        public bool VerifyPassword(string inputPassword)
        {
          
            return Password == inputPassword; 
        }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword; 
        }

        public List<Group> GetUserGroups()
        {
          
            return new List<Group>();
        }
    }
}

