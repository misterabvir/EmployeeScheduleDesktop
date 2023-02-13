using GemBox.Spreadsheet;
using System;

namespace EE
{
    public static class ExDic
    {
        public static CellStyle MainStyle => GetStyle();
        public static CellStyle PerIDTitleStyle => GetStyle(colorBackName: ColorName.Orange);
        public static CellStyle NameTitleStyle => GetStyle(colorBackName: ColorName.Orange);
        public static CellStyle NumberTitleDayStyle => GetStyle(line1: LineStyle.Medium);
        public static CellStyle NumberTitleDayWeakStyle => GetStyle(colorFontName: ColorName.DarkRed, colorBackName: ColorName.Background1Darker15Pct, bold: true);
        public static CellStyle ResTitleStyle => GetStyle(colorBackName: ColorName.Orange);

        public static CellStyle SeparateStyle => GetStyle(line2: LineStyle.None, line4: LineStyle.None, colorBackName: ColorName.White);

        public static CellStyle PerIDStyle => GetStyle(bold: true);
        public static CellStyle NameStyle => GetStyle(bold: true, hor: HorizontalAlignmentStyle.Left);
        public static CellStyle NameEngStyle => GetStyle(colorFontName: ColorName.Red, bold: true, hor: HorizontalAlignmentStyle.Left);
        public static CellStyle NumberDayStyle => GetStyle();
        public static CellStyle NumberDayWeakStyle => GetStyle( colorFontName: ColorName.DarkRed, colorBackName: ColorName.Background1Darker15Pct, bold: true);
        public static CellStyle NumberDayTomorrowStyle => GetStyle( colorFontName: ColorName.Text2Lighter60Pct);
        public static CellStyle ResStyle => GetStyle(format: "0.0");
        public static CellStyle ResDayStyle => GetStyle(format: "0");

        

        private static CellStyle GetStyle(VerticalAlignmentStyle ver = VerticalAlignmentStyle.Center, 
            HorizontalAlignmentStyle hor = HorizontalAlignmentStyle.Center, 
            LineStyle line1= LineStyle.Thin,
            LineStyle line2= LineStyle.Thin,
            LineStyle line3= LineStyle.Thin,
            LineStyle line4= LineStyle.Thin, 
            string format = "00",
            bool bold = false, ColorName colorFontName = ColorName.Black, ColorName colorBackName = ColorName.White)
        {
            CellStyle style = new CellStyle();
            style.VerticalAlignment = ver;
            style.HorizontalAlignment = hor;

            if(bold)
                style.Font.Weight = ExcelFont.BoldWeight;

 
            style.Font.Color = SpreadsheetColor.FromName(colorFontName);
            style.FillPattern.SetSolid(SpreadsheetColor.FromName(colorBackName));          
            

            style.NumberFormat = format;
            style.Borders.SetBorders(MultipleBorders.Top, SpreadsheetColor.FromArgb(0, 0, 0),   line1);
            style.Borders.SetBorders(MultipleBorders.Right, SpreadsheetColor.FromArgb(0, 0, 0),   line2);
            style.Borders.SetBorders(MultipleBorders.Bottom, SpreadsheetColor.FromArgb(0, 0, 0),   line3);
            style.Borders.SetBorders(MultipleBorders.Left, SpreadsheetColor.FromArgb(0, 0, 0), line4);
            return style;
        }
    }


}
