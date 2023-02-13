using Microsoft.Win32;
using ShiftsDomain.Model;
using ShiftsDomain.Repository;
using ShiftsUI.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShiftsUI.Editors.MonthCreator
{
    public partial class MonthCreatorControl : UserControl
    {

        private ObservableCollection<Employe> _employes = EmployesCollection.Collection;
        public DateTime Date => mcDate.CurrentDate;
        public MonthCreatorControl()
        {
            InitializeComponent();
            lbEmployes.ItemsSource = _employes;
        }

     

    }
}
