using System.Xml.Serialization;

namespace ShiftsDomain.Model
{
    public class MonthFile
    {
        public string Name { get; set; }
        public string FileName { get; set; }

        [XmlIgnore]
        public bool IsExist => File.Exists(FileName);
    }
}
