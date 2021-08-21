using System.Collections.Generic;

namespace KONTAKTOR.Services.DocxTemplating
{
  public class DocxTableCell
  {
    /// <summary>
    /// Ячейки. Каждая ячейка содержит название метки в документе, текст для метки
    /// </summary>
    private List<DocxField> _cells = new List<DocxField>();
    public List<DocxField> Cells { get { return _cells; } set { _cells = value; } }
  }
}