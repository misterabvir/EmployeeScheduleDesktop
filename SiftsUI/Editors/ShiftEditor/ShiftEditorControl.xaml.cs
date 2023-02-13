using ShiftsDomain.Model;
using ShiftsDomain.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShiftsUI.Editors.ShiftEditor
{
    /// <summary>
    /// Interaction logic for ShiftEditorControl.xaml
    /// </summary>
    public partial class ShiftEditorControl : UserControl
    {

        public static ObservableCollection<Shift> _shifts = ShiftsCollection.Collection;
        public ShiftEditorControl()
        {
            InitializeComponent();
            lbShift.ItemsSource = _shifts;
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Shift shift = new Shift() { 
                Name = "Гр.0, Бр.0", 
                Description = "Новый график",
                Offset = 0,
                WeekModifier = true,
                Scheme = "1_2_3_4_5_6_7",
            };
            _shifts.Add(shift);
        }

        private void BtnRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _shifts.Remove((lbShift.SelectedItem as Shift));
        }

        private void BtnSaveClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ShiftsCollection.Save();
        }
    }
}
