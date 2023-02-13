using Microsoft.Win32;
using ShiftsDomain.Model;
using ShiftsDomain.Repository;
using ShiftsUI.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ShiftsUI
{
    public partial class MainWindow : Window
    {
        public Month? CurrentMonth { get; private set; }

        public MainWindow()
        {
            InitializeComponent();          
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.DefaultExt = "*.xml";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\Saves";
            if (openFileDialog.ShowDialog() == true)
            {
                table.CurrentMonth = Month.Load(openFileDialog.FileName);
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow cWind = new CreateWindow();
            cWind.ShowDialog();
        }

        private void ButtonRule_Click(object sender, RoutedEventArgs e)
        {
            RuleEditorWindow rWind = new RuleEditorWindow();
            rWind.ShowDialog();
        }

        private void ButtonEmploye_Click(object sender, RoutedEventArgs e)
        {
            EmployeEditorWindow eWind = new EmployeEditorWindow();
            eWind.ShowDialog();
        } 

        private void ButtonShift_Click(object sender, RoutedEventArgs e)
        {
            ShiftEditorWindow sWind = new ShiftEditorWindow();
            sWind.ShowDialog();
            
        }

        private void ButtonConvert_Click(object sender, RoutedEventArgs e)
        {
            if (table.CurrentMonth == null) return;
            
            ConvertToExcelWIndow cew = new ConvertToExcelWIndow() { CurrentMonth = table.CurrentMonth };
            cew.ShowDialog();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Month.Save(table.CurrentMonth);
        }
    }
}
