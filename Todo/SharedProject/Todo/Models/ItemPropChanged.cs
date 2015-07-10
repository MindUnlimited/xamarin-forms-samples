using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;
using System.ComponentModel;

namespace Todo
{
    public class Item : INotifyPropertyChanged
    {
        public string ID { get; set; }

        [JsonIgnore]
        String _ownedby;

        public String OwnedBy
        {
            get { return _ownedby; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (_ownedby == value)
                    return;
                _ownedby = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string _parent;

        public string Parent
        {
            get { return _parent; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (_parent == value)
                    return;
                _parent = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        int _type;

        public int Type
        {
            get { return _type; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (_type == value)
                    return;
                _type = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string name;

        public string Name
        {
            get { return name; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        int status;

        public int Status
        {
            get { return status; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (status == value)
                    return;
                status = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        int importance;

        public int Importance
        {
            get { return importance; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (importance == value)
                    return;
                importance = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        int urgency;

        public int Urgency
        {
            get { return urgency; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (urgency == value)
                    return;
                urgency = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        int order;

        public int Order
        {
            get { return order; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (order == value)
                    return;
                order = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string notes;

        public string Notes
        {
            get { return notes; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (notes == value)
                    return;
                notes = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string createdby;

        public string CreatedBy
        {
            get { return createdby; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (createdby == value)
                    return;
                createdby = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string updatedby;

        public string UpdatedBy
        {
            get { return updatedby; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (updatedby == value)
                    return;
                updatedby = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string deletedby;

        public string DeletedBy
        {
            get { return deletedby; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (deletedby == value)
                    return;
                deletedby = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string assignedto;

        public string AssignedTo
        {
            get { return assignedto; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (assignedto == value)
                    return;
                assignedto = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        string startdate;

        public string StartDate
        {
            get { return startdate; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (startdate == value)
                    return;
                startdate = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string enddate;

        public string EndDate
        {
            get { return enddate; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (enddate == value)
                    return;
                enddate = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string effortestimate;

        public string EffortEstimate
        {
            get { return effortestimate; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (effortestimate == value)
                    return;
                effortestimate = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        int recurrent;

        public int Recurrent
        {
            get { return recurrent; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (recurrent == value)
                    return;
                recurrent = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string rewardtype;

        public string RewardType
        {
            get { return rewardtype; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (rewardtype == value)
                    return;
                rewardtype = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        float rewardamount;

        public float RewardAmount
        {
            get { return rewardamount; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (rewardamount == value)
                    return;
                rewardamount = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string punishmenttype;

        public string PunishmentType
        {
            get { return punishmenttype; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (punishmenttype == value)
                    return;
                punishmenttype = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        float punishmentamount;

        public float PunishmentAmount
        {
            get { return punishmentamount; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (punishmentamount == value)
                    return;
                punishmentamount = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        string dependenton;

        public string DependentOn
        {
            get { return dependenton; }
            set
            {
                // OnPropertyChanged should not be called if the property value
                // does not change.
                if (dependenton == value)
                    return;
                dependenton = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        [Version]
        public string Version { get; set; }

        //public override string ToString()
        //{
        //    return "    Text: " + Name + "\n    Status: " + Status + "\n    Owned by: " + OwnedBy;
        //}

        public override string ToString()
        {
            return Name;

        }
    }
}
