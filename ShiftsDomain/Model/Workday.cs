using ShiftsDomain.Repository;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ShiftsDomain.Model
{
    public class Workday : INotifyPropertyChanged
    {
        #region private fields
        private int _id;
        private DateTime date;
        private string litera;
        private Shift shift;
        private string nonDefaultValue;
        private bool isSelected;
        #endregion

        #region public properties
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public string Litera
        {
            get => litera;
            set
            {
                litera = value;
                OnPropertyChanged(nameof(Litera));
                OnPropertyChanged(nameof(IsWorkable));
                OnPropertyChanged(nameof(IsToday));
                OnPropertyChanged(nameof(IsTomorrow));
                OnPropertyChanged(nameof(IsYesterday));
                OnPropertyChanged(nameof(Value));

            }
        }
        public Shift Shift
        {
            get => shift;
            set
            {
                shift = value;
                OnPropertyChanged(nameof(Shift));
            }
        }
        public string NonDefaultValue
        {
            get => nonDefaultValue;
            set
            {
                nonDefaultValue = value;
                OnPropertyChanged(nameof(NonDefaultValue));
                OnPropertyChanged(nameof(IsWorkable));
                OnPropertyChanged(nameof(Value));
            }
        } 
        #endregion

        #region public properties extension
        [XmlIgnore]
        public float DefaultValue => RulesCollection.GetRuleValue(Litera);
        [XmlIgnore]
        public float Value
        {
            get
            {
                if (float.TryParse(Litera, out float res1))
                    return res1;
                if (float.TryParse(NonDefaultValue, out float res2))
                    return res2;
                return DefaultValue;
            }
        }
        [XmlIgnore]
        public bool IsWorkable => Value > 0;
        [XmlIgnore]
        public bool IsToday
        {
            get
            {
                DateTime dt = DateTime.Now;
                return dt.Day == Date.Day && dt.Month == Date.Month && dt.Year == Date.Year;
            }
        }
        [XmlIgnore]
        public bool IsTomorrow
        {
            get
            {
                DateTime dt = DateTime.Now;
                return dt.Day < Date.Day && dt.Month == Date.Month && dt.Year == Date.Year;
            }
        }
        [XmlIgnore]
        public bool IsYesterday
        {
            get
            {
                DateTime dt = DateTime.Now;
                return dt.Day > Date.Day && dt.Month == Date.Month && dt.Year == Date.Year;
            }
        }


        [XmlIgnore]
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        #endregion

        #region implement inotifychanger
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        } 
        #endregion
    }
}
