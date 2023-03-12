using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for ShiftEditorWindow.xaml
    /// </summary>
    public partial class ShiftEditorWindow : Window
    {
        public ShiftEditorWindow()=>InitializeComponent();
        
        private void ButtonClose_Click(object sender, RoutedEventArgs e)=>Close();
    }
}
