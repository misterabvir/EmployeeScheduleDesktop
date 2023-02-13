using ShiftsDomain.Model;
using ShiftsDomain.Repository;
using GemBox.Spreadsheet;
using ShiftsDomain;

namespace EE
{
    public class ExcelExporter
    {
        private Month _month;
        private bool _isFull;
        private List<string> _shNames;

        private string _filename => $"{_month.MonthTitle}{(_isFull?" (полный) ":"")}.xls";
        private DateTime _date => _isFull ? _month.Date.LastDateInMonth() : DateTime.Now;
        private string _path => Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";

        public ExcelExporter(Month month, bool isFull)
        {
            _month = month;
            _isFull = isFull;
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public void Create()
        {
            if (_month == null) throw new NullReferenceException(nameof(_month));

            ExcelFile file = BuildTable();
            SaveFile(_path + _filename, file);
        }

        private void SaveFile(string fullpath, ExcelFile file)
        {
            file.Save(fullpath);
        }

        private ExcelFile BuildTable()
        {
            ExcelFile file = new ExcelFile();            
            file.Worksheets.Add(_month.Date.NameRus());
            var sheet = file.Worksheets[0];

            _shNames = GetNames(ShiftsCollection.Collection.Select(s => s.Name));


            CreateTitle(sheet);

            SetWidthCollumns(sheet);

            CreateHeaders(sheet);

            CreateLines(sheet);

            return file;
        }

        private List<string> GetNames(IEnumerable<string> names)
        {
            var list = new List<string>();

            foreach (var item in names)
            {
                var name = item.Split(',');
                if (!list.Contains(name[0])) 
                    list.Add(name[0]);    
            }


            return list;
        }

        private void CreateLines(ExcelWorksheet sheet)
        {
            int row = 2;
            foreach (var emp in _month.Employes)
            {
                CreateSeparate(sheet, row++);
                CreateLine(emp, sheet, row++);
            }
        }

        private void CreateSeparate(ExcelWorksheet sheet, int row)
        {
            var cell = sheet.Rows[row];
            cell.Height = 100;
            int cols  = 4 + DateTime.DaysInMonth(_month.Date.Year, _month.Date.Month) + _shNames.Count;
            
            var range = sheet.Cells.GetSubrangeAbsolute(row, 0, row, cols) ;

            range.Style = ExDic.SeparateStyle;
        }

        private void CreateLine(Employe employe, ExcelWorksheet sheet, int row)
        {
            int cols = 3 + DateTime.DaysInMonth(_month.Date.Year, _month.Date.Month) + 6;
            var cell = sheet.Cells.GetSubrangeAbsolute(row, 0, row, cols);
            int index = 0;

            SetCell(cell[index++], employe.PerId, ExDic.PerIDStyle);
            SetCell(cell[index++], employe.Name, employe.Engineer ? ExDic.NameEngStyle : ExDic.NameStyle);
            SetCell(cell[index++], employe.DefaultShift.Name, employe.Engineer ? ExDic.NameEngStyle : ExDic.NameStyle);
           
            for (int i = 0; i < DateTime.DaysInMonth(_month.Date.Year, _month.Date.Month); i++)
            {
                var style = (i >= _date.Day) ? ExDic.NumberDayTomorrowStyle : employe.Workdays[i].IsWorkable ? ExDic.NumberDayStyle : ExDic.NumberDayWeakStyle;
                if (!_isFull && employe.Workdays[i].Date > DateTime.Now)
                    SetCell(cell[index++], "", style);
                else 
                    SetCell(cell[index++], employe.Workdays[i].Litera, style);
            }
            if (!_isFull)
            {

                    SetCell(cell[index++], ZeroResultValue(employe.Workdays.Where(w => w.IsWorkable && w.Date <= DateTime.Now).Count()), ExDic.ResDayStyle);
                    SetCell(cell[index++], ZeroResultValue(employe.Workdays.Where(w => w.IsWorkable && w.Date <= DateTime.Now).Select(s => s.Value).Sum()), ExDic.ResStyle);
            }
            else
            {
                SetCell(cell[index++], ZeroResultValue(employe.Workdays.Where(w => w.IsWorkable).Count()), ExDic.ResDayStyle);
                SetCell(cell[index++], ZeroResultValue(employe.Workdays.Where(w => w.IsWorkable).Select(s => s.Value).Sum()), ExDic.ResStyle);
            }


            foreach (var item in _shNames)
            {
                
                if(!_isFull)
                    SetCell(cell[index++], ZeroResultValue(employe.Workdays.Where(w => w.IsWorkable && w.Date <= DateTime.Now && w.Shift.Name.StartsWith(item)).Select(s => s.Value).Sum()), ExDic.ResStyle);
             
               else
                    SetCell(cell[index++], ZeroResultValue(employe.Workdays.Where(w => w.IsWorkable && w.Shift.Name.StartsWith(item)).Select(s => s.Value).Sum()), ExDic.ResStyle);
            }
        }

        private object ZeroResultValue(float exp)
        {
            if (exp == 0) return "";
            return exp;
        }


        private void SetWidthCollumns(ExcelWorksheet sheet)
        {
            var cols = sheet.Columns;
            int index = 0;
            cols[index++].Width = 2000; 
            cols[index++].Width = 5000; 
            cols[index++].Width = 3000;
            for (int i = 0; i < DateTime.DaysInMonth(_month.Date.Year, _month.Date.Month); i++)
            {
                cols[index++].Width = 1000;
            }
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
            cols[index++].Width = 2500;
        }

        private void CreateHeaders(ExcelWorksheet sheet)
        {
            int cols = 4 + DateTime.DaysInMonth(_month.Date.Year, _month.Date.Month) + _shNames.Count;
            var cell = sheet.Cells.GetSubrangeAbsolute(1, 0, 1, cols);
            int index = 0;
            SetCell(cell[index++], TxtDic.Number, ExDic.PerIDTitleStyle);
            SetCell(cell[index++], TxtDic.Name, ExDic.NameTitleStyle);
            SetCell(cell[index++], TxtDic.Shift, ExDic.NameTitleStyle);
            for (int i = 0; i < DateTime.DaysInMonth(_month.Date.Year, _month.Date.Month); i++)
            {
                var d = new DateTime(year: _month.Date.Year, month: _month.Date.Month, day: i + 1);
                
                if((d.DayOfWeek) == DayOfWeek.Sunday || (d.DayOfWeek) == DayOfWeek.Saturday)
                    SetCell(cell[index++], (i + 1), ExDic.NumberTitleDayWeakStyle);
                else 
                    SetCell(cell[index++], (i + 1), ExDic.NumberTitleDayStyle);
            }
            SetCell(cell[index++], TxtDic.AmountEx, ExDic.ResTitleStyle);
            SetCell(cell[index++], TxtDic.AmountHours, ExDic.ResTitleStyle);
            foreach (var item in _shNames)
                SetCell(cell[index++], item, ExDic.ResTitleStyle);
        }

        private void SetCell(ExcelCell range, object val, CellStyle style)
        { 
            range.Value = val;
            range.Style = style;
        }



        private void CreateTitle(ExcelWorksheet sheet)
        {
            int cols  = 4 + DateTime.DaysInMonth(_month.Date.Year, _month.Date.Month) + _shNames.Count;
            var cell= sheet.Cells.GetSubrangeAbsolute(0, 0, 0, cols) ;
            cell.Merged = true;            
            cell.Style.Font.Weight = ExcelFont.BoldWeight;
            cell.Value = _month.MonthTitle;

        }

        public async Task Start()
        {
            AsyncFactory fac = new AsyncFactory() { TaskLink = Create };
            await fac.StartTask();
            OnCreatingOver();
        }

        public delegate void  CreatingOverEventHandler();
        public event CreatingOverEventHandler CreatingOver;
        private void OnCreatingOver()
        {
            CreatingOver?.Invoke();
        }        

    }

    public class AsyncFactory
    {
        public Action TaskLink { get; set; }

        async public Task StartTask()
        {
            await Task.Run(() => TaskLink());
        }
    }
}