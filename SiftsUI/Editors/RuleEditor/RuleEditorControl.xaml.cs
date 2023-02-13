
using ShiftsDomain.Model;
using ShiftsDomain.Repository;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace ShiftsUI.Editors.RuleEditor
{
    /// <summary>
    /// Interaction logic for RuleEditorControl.xaml
    /// </summary>
    public partial class RuleEditorControl : UserControl
    {

        public static ObservableCollection<Rule> _rules = RulesCollection.Collection;
        public RuleEditorControl()
        {
            InitializeComponent();

            lbRules.ItemsSource = _rules;
        }

        private void BtnAddRule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Rule rule = new Rule() { Litera = "SOME", DefaultValue = 0.0F };
            _rules.Add(rule);
        }

        private void BtnRemoveRule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _rules.Remove((lbRules.SelectedItem as Rule));
        }

        private void BtnSaveRuleClick(object sender, System.Windows.RoutedEventArgs e)
        {
            RulesCollection.Save();
        }
    }
}
