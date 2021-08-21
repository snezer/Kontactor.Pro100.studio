namespace KONTAKTOR.Services.DocxTemplating
{
  public class DocxTemplate
  {
    /// <summary>
    /// Метка, которую искать в документе (туда будет вставлен шаблон)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Полный путь к шаблону с именем файла
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// Поля и таблицы
    /// </summary>
    private DocxData _data = new DocxData();
    public DocxData Data { get { return _data; } set { _data = value; } }
  }
}