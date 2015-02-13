using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace Todo
{
    public class User
    {
        public User()
        {
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public Boolean isCoach { get; set; }

        public string Email { get; set; }

        public string MicrosoftID { get; set; }

        public string LoginType { get; set; }

        public string LoginUserId { get; set; }

        public string LoginPassword { get; set; }

        public string GUILanguage { get; set; }

        public string Culture { get; set; }

        public string TrainingProgramLanguages { get; set; }

        //[JsonProperty(PropertyName = "text")]
        //public string Text { get; set; }

        //[JsonProperty(PropertyName = "complete")]
        //public bool Complete { get; set; }

        ////[JsonProperty(PropertyName = "notes")]
        ////public string Notes { get; set; }

        [Version]
        public string Version { get; set; }

        //public override string ToString()
        //{
        //    return "    Title: " + Text + "\n    Complete: " + Complete;
        //}
    }
}
