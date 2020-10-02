using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GreenProjectMobile.Models
{
    public class Tip: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private int _id;
        private string _name;
        private string _desc;
        private bool _tipStatus;

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }

        }
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
				OnPropertyChanged();
            }
        }
        public string desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
				OnPropertyChanged();
            }
        }
        public bool tipStatus {
            get { return _tipStatus; }
            set
            {
                _tipStatus = value;
                OnPropertyChanged();
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class TipsResult
    {
        public int status { get; set; }
        public List<Tip> tips { get; set; }
        public string msg { get; set; }
    }
}
