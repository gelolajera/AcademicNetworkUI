using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicNetworkUI
{
    public class Message//Represents chat messages with formatting capabilities
    {
        public int MessageID { get; set; }
        public int GroupID { get; set; }
        public string Sender { get; set; }  
        public string MessageText { get; set; }
        public DateTime Timestamp { get; set; }

        public Message(int groupID, string sender, string text)
        {
            GroupID = groupID;
            Sender = sender;
            MessageText = text;
            Timestamp = DateTime.Now;
        }

        public string FormatMessage()
        {
            return $"[{Timestamp}] {Sender}: {MessageText}";
        }
    }
}
