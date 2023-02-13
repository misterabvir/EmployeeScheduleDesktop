using ShiftsDomain;
using ShiftsDomain.Model;
using ShiftsDomain.Repository;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ShiftsUI.Editors.TableViewer
{

    public partial class TableViewerControl : UserControl
    {
        private Month currentMonth;

        public Month CurrentMonth 
        { 
            get => currentMonth;
            set 
            { 
                currentMonth = value;
                StartInit();
            }
        }

        public TableViewerControl()
        {
            InitializeComponent();
            CurrentMonth = Month.Load("Saves\\" + DateTime.Now.NameEN() + ".xml");
            StartInit();
        }

        private void StartInit()
        {
            if (CurrentMonth == null) return;
            items.ItemsSource = CurrentMonth.Employes;
            itemsDates.ItemsSource = CurrentMonth.RangeDates;
            cmbShift.ItemsSource = ShiftsCollection.Collection;
            cmbShiftRange.ItemsSource = ShiftsCollection.Collection;
            gridSelect.DataContext = CurrentMonth;
            gridSelectRange.DataContext = CurrentMonth;
        }

        private void CellDay_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var element = sender as Label;
            if (element == null) return;
            Workday workday = (Workday)element.Tag;
        }

        private void ItemEmpoye_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var element = sender as StackPanel;
            if (element == null) return;
            var employe = (Employe)element.Tag;

        }

        private void SelectDay_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (!Keyboard.IsKeyDown(Key.LeftShift))
            {
                CurrentMonth.SelectedWorkday2 = null;
                SelectDay1(sender, e);
            }
            else if (Keyboard.IsKeyDown(Key.LeftShift) && CurrentMonth.SelectedWorkday1 == null)
            {
                SelectDay1(sender, e);
            }
            else if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                SelectDay2(sender, e);
            }
        }

        private void SelectDay1(object sender, MouseButtonEventArgs e)
        {
            var element = sender as Label;
            if (element == null) return;
            Workday workday = (Workday)element.Tag;
            CurrentMonth.SelectedWorkday1 = workday;
            CurrentMonth.SelectedWorkday1.IsSelected = true;
            cmbShift.SelectedItem = ShiftsCollection.Collection.Where(s => s.Name == workday.Shift.Name).FirstOrDefault();
        }

        private void SelectDay2(object sender, MouseButtonEventArgs e)
        {
            var element = sender as Label;
            if (element == null) return;
            Workday workday = (Workday)element.Tag;
            CurrentMonth.SelectedWorkday2 = workday;
            CurrentMonth.SelectedWorkday2.IsSelected = true;

            cmbShift.SelectedItem = ShiftsCollection.Collection.Where(s => s.Name == workday.Shift.Name).FirstOrDefault();
        }

        private void ButtonSetNewLitera_Click(object sender, RoutedEventArgs e)
        {

            var txt = txtLitera.Text;
            if (CurrentMonth.IsRange)
            { 
                foreach(var item in CurrentMonth.SelectedWorkdays)
                {
                    item.Litera = txt;
                }
            }
        }

        private void ButtonSetNewShift_Click(object sender, RoutedEventArgs e)
        {

            var shift = cmbShiftRange.SelectedItem as Shift;
            if (shift == null) return;
            if (CurrentMonth.IsRange)
            {
                foreach (var item in CurrentMonth.SelectedWorkdays)
                {
                    item.Shift = shift;
                    item.Litera = shift.GetLiteraFromDate(item.Date);
                }
            }

        }

        private void control_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
             CurrentMonth.SelectedWorkday1 = null;
             CurrentMonth.SelectedWorkday2 = null;
        }
    }
}
