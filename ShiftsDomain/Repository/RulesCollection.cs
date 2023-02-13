using ShiftsDomain.Model;

namespace ShiftsDomain.Repository
{
    public class RulesCollection: BaseTCollection<Rule>
    {
        
        public static float GetRuleValue(string litera)
        {
            var rule = Collection.Where(r => r.Litera == litera).FirstOrDefault();
            if (rule == null)
                return 0F;
            return rule.Value;
        }

    }
}
