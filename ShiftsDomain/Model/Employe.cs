using ShiftsDomain.Repository;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ShiftsDomain.Model
{
    public class Employe : ICloneable, INotifyPropertyChanged
    {
        #region private fields
        private ObservableCollection<Shift> _shifts = ShiftsCollection.Collection;
        private int _id;
        private string _name;
        private string _personalId;
        private int _orderAnchor;
        private bool _engineer;
        private bool _show;
        private string _description;
        private Shift _defaultShift;
        private ObservableCollection<Workday> workdays;
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
        public string PerId
        {
            get => _personalId;
            set
            {
                _personalId = value;
                OnPropertyChanged(nameof(PerId));
            }
        }
        public int OrderAnchor
        {
            get => _orderAnchor;
            set
            {
                _orderAnchor = value;
                OnPropertyChanged(nameof(OrderAnchor));
            }
        }
        public bool Engineer
        {
            get => _engineer;
            set
            {
                _engineer = value;
                OnPropertyChanged(nameof(Engineer));
            }
        }
        public bool Show
        {
            get => _show;
            set
            {
                _show = value;
                OnPropertyChanged(nameof(Show));
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
        public Shift DefaultShift
        {
            get => _defaultShift;
            set
            {
                _defaultShift = value;
                OnPropertyChanged(nameof(DefaultShift));
            }
        }
        [XmlArray("Workdays")]
        [XmlArrayItem("WorkdaysItem")]
        public ObservableCollection<Workday> Workdays
        {
            get 
            {
                return workdays;
            }
            set 
            { 
                workdays = value;
              
            }           
        }

        public void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TotalOfDayOnWork));
            OnPropertyChanged(nameof(TotalOfHourOnWork));
        }    
        #endregion

        #region public properties extension
        [XmlIgnore]
        public string TotalOfDayOnWork
        {
            get
            {
                int all = Workdays.Where(w => w.IsWorkable).Count();
                int today = Workdays.Where(w => w.IsWorkable && w.Date <= DateTime.Now).Count();

                return $"{today} / {all}";
            }
        }
        [XmlIgnore]
        public string TotalOfHourOnWork
        {
            get
            {
                float all = Workdays.Where(w => w.IsWorkable).Select(s => s.Value).Sum();
                float today = Workdays.Where(w => w.IsWorkable && w.Date <= DateTime.Now).Select(s => s.Value).Sum();
                return $"{today:0.0} / {all:0.0}";
            }
        }
        #endregion

        #region construct
        public Employe()
        {
            Workdays = new ObservableCollection<Workday>();
        }
        #endregion

        #region implement iclonable
        public object Clone()
        {
            return new Employe()
            {
                Id = this.Id,
                Name = this.Name,
                Engineer = this.Engineer,
                PerId = this.PerId,
                OrderAnchor = this.OrderAnchor,
                Show = this.Show,
                Description = this.Description,
                DefaultShift = this.DefaultShift,
                Workdays = new ObservableCollection<Workday>()
            };
        }

        public void Subscribe()
        {
            foreach (var workday in Workdays)
            {
                workday.PropertyChanged += Workday_PropertyChanged;
            }
        }

        private void Workday_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TotalOfDayOnWork));
            OnPropertyChanged(nameof(TotalOfHourOnWork));
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
