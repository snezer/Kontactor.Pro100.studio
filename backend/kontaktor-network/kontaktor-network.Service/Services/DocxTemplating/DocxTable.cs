using System.Collections.Generic;

namespace KONTAKTOR.Services.DocxTemplating
{
  public class DocxTable
  {
    /// <summary>
    /// Строки таблицы (массив ячеек)
    /// </summary>
    private List<DocxTableCell> _rows = new List<DocxTableCell>();
    public List<DocxTableCell> Rows { get { return _rows; } set { _rows = value; } }
  }
}