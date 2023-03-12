using System.Windows;

namespace ShiftsUI.Dialogs
{
    /// <summary>
    /// Interaction logic for RuleEditorWindow.xaml
    /// </summary>
    public partial class RuleEditorWindow : Window
    {
        public RuleEditorWindow()=>InitializeComponent();       

        private void ButtonClose_Click(object sender, RoutedEventArgs e) => Close();        
    }
}
