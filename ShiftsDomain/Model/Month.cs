using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ShiftsDomain.Model
{
    public class Month   : INotifyPropertyChanged
    {
        public DateTime Date { get; set; }

        #region SELECTABLECODE
        private Workday? selectedWorkday1;
        private Workday? selectedWorkday2;
        [XmlIgnore] public Workday? SelectedWorkday1
        {
            get => selectedWorkday1;
            set
            {
                if (selectedWorkday1 != null)
                    selectedWorkday1.IsSelected = false;

                if (SelectedWorkdays != null)
                    foreach (var item in SelectedWorkdays)
                    {

                        item.IsSelected = false;
                    }
                selectedWorkday1 = value;
                SelectRange();
                OnPropertyChanged(nameof(SelectedWorkday1));
                OnPropertyChanged(nameof(IsRange));
                OnPropertyChanged(nameof(HasValue1));
                OnPropertyChanged(nameof(IsSingle));
            }
        }
        [XmlIgnore] public Workday? SelectedWorkday2
        {
            get => selectedWorkday2;
            set
            {
                if (selectedWorkday2 != null)
                    selectedWorkday2.IsSelected = false;

                if (SelectedWorkdays != null)
                    foreach (var item in SelectedWorkdays)
                    {

                        item.IsSelected = false;
                    }
                selectedWorkday2 = value;
                SelectRange();
                OnPropertyChanged(nameof(SelectedWorkday2));
                OnPropertyChanged(nameof(IsRange));
                OnPropertyChanged(nameof(HasValue2));
                OnPropertyChanged(nameof(IsSingle));
            }
        }
        [XmlIgnore] public IEnumerable<Workday>? SelectedWorkdays
        {
            get;
            private set;
        }   
        [XmlIgnore] public bool HasValue1 => selectedWorkday1 != null;
        [XmlIgnore] public bool HasValue2 => selectedWorkday2 != null;
        [XmlIgnore] public bool IsSingle => HasValue1 && !HasValue2;
        [XmlIgnore] public bool IsRange
        {
            get
            {
                foreach (var emp in Employes)
                {
                    if (emp.Workdays.Contains(selectedWorkday1) && emp.Workdays.Contains(selectedWorkday2))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private void SelectRange()
        {
            if (IsRange)
            {
                SelectedWorkdays =
                    Employes.FirstOrDefault(w => w.Workdays.Contains(SelectedWorkday1) && w.Workdays.Contains(SelectedWorkday2)).
                                Workdays.Where(w => w.Date >= selectedWorkday1?.Date && w.Date <= selectedWorkday2?.Date);

                foreach (var item in SelectedWorkdays)
                {

                    item.IsSelected = true;
                }
            }
        }     
        #endregion


        private ObservableCollection<DateTime> _rangeDates;
        [XmlIgnore] public ObservableCollection<DateTime> RangeDates
        {
            get
            {
                if (_rangeDates == null)
                {
                    _rangeDates = new ObservableCollection<DateTime>();
                    for (DateTime dt = Date.FirstDateInMonth(); dt <= Date.LastDateInMonth(); dt = dt.AddDays(1))
                    {
                        _rangeDates.Add(dt);
                    }
                }
                return _rangeDates;
            }
        }      
        [XmlIgnore] public string MonthTitle => $"Табель выходов УАМ ТЭЦ-ПВС {Date.NameRus()}";
        [XmlArray("Employes")] [XmlArrayItem("EmployesItem")] public ObservableCollection<Employe> Employes { get; set; }
        [XmlIgnore]public string FileName => $"Saves\\{Date.NameEN()}.xml";
        public void CreateMonth(IEnumerable<Employe> employes)
        {
            Employes = new ObservableCollection<Employe>();
            foreach (var employe in employes)
            {
                Add(employe);
            }
        }
        
        private void Add(Employe employe)
        {
            if (employe.Show)
            {
                var emp = employe.Clone() as Employe;
                CreateRangeWorkDays(emp, Date.FirstDateInMonth(), Date.LastDateInMonth(), emp.DefaultShift);
                Employes.Add(emp);
            }
        }
        private void CreateRangeWorkDays(Employe emp, DateTime start, DateTime end, Shift shift)
        {
            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                var day = new Workday()
                {
                    Date = date,
                    Id = date.Day,
                    Shift = shift,
                    Litera = shift.GetLiteraFromDate(date),
                    NonDefaultValue = ""
                };

                var excistDay = emp.Workdays.Where(w => w.Date == day.Date).FirstOrDefault();
                if (excistDay == null)
                {
                    emp.Workdays.Add(day);
                    day.PropertyChanged += emp.Item_PropertyChanged;
                }
                else
                {
                    excistDay.Litera = day.Litera;
                    excistDay.Shift = day.Shift;

                }
            }
        }

        #region SAVELOADCODE
        [XmlIgnore]
        private static XmlSerializer _serializer = new XmlSerializer(typeof(Month));        
        public static Month Load(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    var month = _serializer.Deserialize(fs) as Month;
                    foreach (var item in month.Employes)
                    {
                        item.Subscribe();

                    }
                    return month;
                }
            }
            catch { return null; }
        }
        public static void Save(Month month)
        {
            using (FileStream fs = new FileStream($"{month.FileName}", FileMode.Create))
            {
                _serializer.Serialize(fs, month);
            }
        } 
        #endregion

        public Month()
        {
            Employes = new ObservableCollection<Employe>();
        }

        #region  implement inotifychanger
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
