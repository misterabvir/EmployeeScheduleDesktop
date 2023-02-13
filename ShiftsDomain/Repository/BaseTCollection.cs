using ShiftsDomain.Features;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ShiftsDomain.Repository
{
    public class BaseTCollection<T> : IEnumerable<T>, ICollection<T> where T : class
    {
      
        
        
        protected ObservableCollection<T> _collection;

        protected BaseTCollection()
        {
             _collection = new ObservableCollection<T>();
        }

        #region  SINGLETONE                
        protected static BaseTCollection<T>? _baseCollection;
        protected static XmlSerializer _serializer = new XmlSerializer(typeof(BaseTCollection<T>));
        public static ObservableCollection<T> Collection
        {
            get { return Base._collection; }
            protected set { Base._collection = value; }
        }
       
        protected static BaseTCollection<T> Base
        {
            get
            {
                if (_baseCollection == null)
                {
                    _baseCollection = Load();
                    if (_baseCollection == null)
                        _baseCollection = new BaseTCollection<T>();
                }

                return _baseCollection;
            }
        }
        public static void Save()
        {
            using (FileStream fs = new FileStream(StringPathConstant.GetPath(typeof(T)), FileMode.Create))
            {
                _serializer.Serialize(fs, _baseCollection);
            }
        }
        private static BaseTCollection<T>? Load()
        {

                using (FileStream fs = new FileStream(StringPathConstant.GetPath(typeof(T)), FileMode.OpenOrCreate))
                {
                    return _serializer.Deserialize(fs) as BaseTCollection<T>;
                }
        }
        #endregion

        #region Implements interfaces
        public int Count => _collection.Count;

        public bool IsReadOnly => false;

        public void Add(T item) => _collection.Add(item);

        public void Clear() => _collection.Clear();
        public bool Contains(T item) => _collection.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _collection.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();
        public bool Remove(T item) => _collection.Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    } 
    #endregion
}
