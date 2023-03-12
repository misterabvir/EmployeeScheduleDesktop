using ShiftsDomain.Repository;
using System.Linq;
using System.Windows;


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
