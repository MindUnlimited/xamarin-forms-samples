using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace Todo
{
    public class UserGroupMembership
    {
        public UserGroupMembership()
        {
        }

        public string ID { get; set; }

        public string MembershipID { get; set; }

        //public Boolean __deleted { get; set; }


        //public override string ToString()
        //{
        //    return "    Title: " + Text + "\n    Complete: " + Complete;
        //}
    }
}
