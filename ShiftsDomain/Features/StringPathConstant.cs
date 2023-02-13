using ShiftsDomain.Model;

namespace ShiftsDomain.Features
{
    public static class StringPathConstant
    {
       public static string PathRule => @"Default\rules.xml";
       public static string PathEmploye => @"Default\employes.xml";
       public static string PathShift=> @"Default\shifts.xml";
       public static string PathMonth=> @"Default\monthes.xml";


        public static string GetPath(Type type)
        {
           
            if (typeof(Employe) == type)
            {
                return PathEmploye;
            }
            else  if(typeof(Rule) == type)
            {
                return PathRule;
            }
            else  if(typeof(Shift) == type)
            {
                return PathShift;
            }
            else  if(typeof(MonthFile) == type)
            {
                return PathMonth;
            }
            throw new ArgumentException();

        }
    }
}
