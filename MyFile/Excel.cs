using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelFile = Microsoft.Office.Interop.Excel;

namespace MyFile
{
    public class Excel
    {
        private ExcelFile.Application _excel;
        private ExcelFile.Workbook _book;
        public Excel()
        {

        }

        public void Create()
        {
            _excel = new ExcelFile.Application();
            _excel.Visible = true;
            _book = _excel.Workbooks.Add();
        }

        public void Set(int row, int col, string value)
        {
            if (_excel == null || row < 0 || col < 0)
                return;

            _excel.Cells[row, col] = value;
        }

        public void SetHorizontalA1ignmentInRange(string range, ExcelFile.XlHAlign align)
        {
            _excel.get_Range(range).HorizontalAlignment = align;
        }

        public void SetBordersLineStyleInRange(string range, ExcelFile.XlLineStyle lineStyle)
        {
            _excel.get_Range(range).Borders.LineStyle = lineStyle;
        }

        public void SetBorderAroundInRange(string range, ExcelFile.XlLineStyle lineStyle, ExcelFile.XlBorderWeight weight, ExcelFile.XlColorIndex colorIndex)
        {
            _excel.get_Range(range).BorderAround(lineStyle, weight, colorIndex);
        }

        public void SetInteriorColorInRange(string range, int red, int green, int blue)
        {
            _excel.get_Range(range).Cells.Interior.Color = Color.FromArgb(red, green, blue).ToArgb();
        }

        public void SetEntireColumnAutoFit(string range)
        {
            _excel.get_Range(range).EntireColumn.AutoFit();
        }

        public void SaveAs(string path)
        {
            _book.SaveAs(path);
        }

        public void Close()
        {
            _book.Close();
            _book = null;
            _excel.Quit();
            _excel = null;
        }
    }
}
