using ShiftsDomain;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShiftsUI.Editors.DateMonthEditor
{

    

    public partial class DateMonthEditorControl : UserControl
    {
        private List<DateTime> _dates;
        public DateTime CurrentDate => _dates[cmbListDate.SelectedIndex];
        private DateTime _date = DateTime.Now;
        public DateMonthEditorControl()
        {
            InitializeComponent();
            CreateList();
            cmbListDate.ItemsSource = _dates.Select(s => s.NameRus());
            cmbListDate.SelectedItem = DateTime.Now.NameRus();
        }

        private void CreateList()
        {
            _dates = new List<DateTime>();
            var start = _date.AddMonths(-6);
            var end = _date.AddMonths(12);
            for (DateTime d = start; d < end; d=d.AddMonths(1))
            {
                _dates.Add(d);
            }
        }
    }
}
