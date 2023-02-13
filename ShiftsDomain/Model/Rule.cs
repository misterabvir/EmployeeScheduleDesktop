using System.ComponentModel;
using System.Xml.Serialization;

namespace ShiftsDomain.Model
{
    public class Rule : INotifyPropertyChanged
    {
        #region private fields
        private int _id;
        private string _litera;
        private float _defaultValue; 
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
        public string Litera
        {
            get => _litera;
            set
            {
                _litera = value;
                OnPropertyChanged(nameof(Litera));
            }
        }
        public float DefaultValue
        {
            get => _defaultValue;
            set
            {
                _defaultValue = value;
                OnPropertyChanged(nameof(DefaultValue));
            }
        } 
        #endregion

        #region features
        [XmlIgnore]
        public float Value => (float.TryParse(Litera, out float result)) ? result : DefaultValue; 
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
