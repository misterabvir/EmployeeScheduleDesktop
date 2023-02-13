using System.ComponentModel;

namespace ShiftsDomain.Model
{
    public class Shift : INotifyPropertyChanged
    {
        #region private fields
        private int _id;
        private string _name;
        private string _description;
        private bool _weekModifier;
        private string _scheme;
        private int _offset;
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
        public string Name 
        { 
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description 
        { 
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public bool WeekModifier 
        { 
            get => _weekModifier;
            set
            {
                _weekModifier = value;
                OnPropertyChanged(nameof(WeekModifier));
            }
        }
        public string Scheme 
        { 
            get => _scheme; 
            set
            {
                _scheme = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Offset 
        { 
            get => _offset; 
            set
            {
                _offset = value;
                OnPropertyChanged(nameof(Offset));
            }
        }
        #endregion

        #region features
        private DateTime _START => new(2022, 01, 01);
        public string GetLiteraFromDate(DateTime date)
        {
            var schemes = Scheme.Split("_");
            if (WeekModifier)
            {
                return schemes[(int)date.DayOfWeek];
            }
            int days = (int)(date - _START).TotalDays;
            if (days < 0) days *= -1;
            days %= schemes.Length;
            return schemes[days];
        }
        #endregion

        #region  implement inotifychanger
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
