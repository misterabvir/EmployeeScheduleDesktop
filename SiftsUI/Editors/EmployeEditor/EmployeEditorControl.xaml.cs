using ShiftsDomain.Model;
using ShiftsDomain.Repository;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;

namespace ShiftsUI.Editors.EmployeEditor
{

    public partial class EmployeEditorControl : UserControl
    {
        public static ObservableCollection<Employe> _employes = EmployesCollection.Collection; 

        public EmployeEditorControl()
        {
            InitializeComponent();
            lbEmploye.ItemsSource = _employes;
            cmbShift.Items.Clear();
            cmbShift.ItemsSource = ShiftsCollection.Collection;
        }

        private void BtnAddRule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Employe employe = new Employe() { Name = "Смит Д.", Engineer = true, PerId = "123456", Description="Some guy", OrderAnchor=500, Show=true, DefaultShift = ShiftsCollection.Collection[0]};
            _employes.Add(employe);
        }

        private void BtnRemoveRule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _employes.Remove((lbEmploye.SelectedItem as Employe));
        }

        private void BtnSaveRuleClick(object sender, System.Windows.RoutedEventArgs e)
        {
            EmployesCollection.Save();
        }

        private void lbEmploye_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbEmploye.SelectedItem == null)
            {
                cmbShift.SelectedIndex = -1;
                return;
            }
            Employe employe = lbEmploye.SelectedItem as Employe;
            cmbShift.SelectedItem = ShiftsCollection.Collection.Where(s => s.Name == employe.DefaultShift.Name).FirstOrDefault();


        }
    }
}
