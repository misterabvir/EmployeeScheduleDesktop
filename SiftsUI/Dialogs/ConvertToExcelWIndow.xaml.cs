using EE;
using ShiftsDomain.Model;
using System.Threading.Tasks;
using System.Windows;

namespace ShiftsUI.Dialogs
{

    public partial class ConvertToExcelWIndow : Window
    {
        
        public Month CurrentMonth { get; set; }
     

        public ConvertToExcelWIndow()
        {
            InitializeComponent();
           
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            ExcelExporter ex = new ExcelExporter(CurrentMonth, ratioFull.IsChecked == true);
            ex.CreatingOver += Ex_CreatingOver;
            var t = ex.Start();       
        }

        private void Ex_CreatingOver()
        {
            MessageBox.Show("Done");
        }
    }
}
