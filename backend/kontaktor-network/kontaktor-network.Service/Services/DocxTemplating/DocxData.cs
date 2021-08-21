using System.Collections.Generic;
using System.Collections;

namespace KONTAKTOR.Services.DocxTemplating
{
  public class DocxData
  {
    /// <summary>
    /// Названия меток и тексты для замены
    /// </summary>
    private List<DocxField> _properties = new List<DocxField>();
    public List<DocxField> Properties { get { return _properties; } set { _properties = value; } }

    /// <summary>
    /// Таблицы
    /// </summary>
    private List<DocxTable> _tables = new List<DocxTable>();
    public List<DocxTable> Tables { get { return _tables; } set { _tables = value; } }

    /// <summary>
    /// Другие документы word, которые надо скомпоновать в один
    /// </summary>
    private List<DocxTemplate> _templates = new List<DocxTemplate>();
    public List<DocxTemplate> Templates { get { return _templates; } set { _templates = value; } }
  }
}