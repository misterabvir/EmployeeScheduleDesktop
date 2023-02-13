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
using System.Windows.Shapes;

namespace ShiftsUI.Dialogs
{
   
    public partial class CreateWindow : Window
    {



        public CreateWindow()
        {
            InitializeComponent();
        }
        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {

            var ee = EmployesCollection.Collection.Where(r => r.Show);
            MonthCollection.CreateNew(mcDate.Date, ee);
            MonthCollection.Save();
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
