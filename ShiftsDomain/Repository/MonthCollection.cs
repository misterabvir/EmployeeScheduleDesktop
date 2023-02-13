using ShiftsDomain.Features;
using ShiftsDomain.Model;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace ShiftsDomain.Repository
{
    public class MonthCollection : BaseTCollection<MonthFile>
    {
        public static void CreateNew(DateTime date, IEnumerable<Employe> employes)
        {
            var month = new Month() { Date = date, Employes = employes as ObservableCollection<Employe> };
            if (Collection.Select(f => f.FileName).Contains(month.FileName))
                return;
            month.CreateMonth(employes);
            Month.Save(month);

            Collection.Add(new MonthFile() { Name = month.Date.NameRus(), FileName = month.FileName});
        }

        protected static void CheckNullable()
        {          
            Collection = Collection.Where(w => w.IsExist) as ObservableCollection<MonthFile>;
            if (Collection == null)
            {
                Collection = new ObservableCollection<MonthFile>();
                Save();
            }
        }



    }
 
    
}
