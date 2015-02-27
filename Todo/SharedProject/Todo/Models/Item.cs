using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace Todo
{
    public class Item
    {
        public string ID { get; set; }

        public string OwnedBy { get; set; }

        public string Parent { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public int Importance { get; set; }

        public int Urgency { get; set; }

        public int Order { get; set; }

        public string Notes { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string DeletedBy { get; set; }

        public string AssignedTo { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string EffortEstimate { get; set; }

        public int Recurrent { get; set; }

        public string RewardType { get; set; }

        public float RewardAmount { get; set; }

        public string PunishmentType { get; set; }

        public float PunishmentAmount { get; set; }

        public string DependentOn { get; set; }

        [Version]
        public string Version { get; set; }

        public override string ToString()
        {
            return "    Text: " + Name + "\n    Status: " + Status;
        }
    }
}
