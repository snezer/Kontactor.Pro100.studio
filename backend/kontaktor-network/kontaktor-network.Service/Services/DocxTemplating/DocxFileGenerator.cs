using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using KONTAKTOR.Service.Services.DocxTemplating;
using Newtonsoft.Json.Linq;

namespace KONTAKTOR.Services.DocxTemplating
{
    /// <summary>
    /// Класс для генерации Word документов по шаблону
    /// Вставляет значения в метки  {}
    /// Заполняет размеченные метками таблицы
    /// Вставляет внутрь другой Word документ
    /// </summary>
    public class DocxFileGenerator
    {


        private string _templatePath;

        public DocxFileGenerator(string rootPathForTempate)
        {
            _templatePath = rootPathForTempate;

        }

        private async Task<MemoryStream> GetTemplateStream()
        {
            MemoryStream underlyingStream = new MemoryStream();
            using (FileStream fs = File.Open(_templatePath, FileMode.Open, FileAccess.Read))
            {
                await fs.CopyToAsync(underlyingStream);
            }

            underlyingStream.Seek(0, SeekOrigin.Begin);
            return underlyingStream;
        }

        public async Task<byte[]> Process<T>(ITemplateDescription<T> model) where T : ICommonTemplateData
        {
            using (var stream = await GetTemplateStream())
            {
                using (WordprocessingDocument document = WordprocessingDocument.Open(stream, true))
                {
                    Fill(document, model);
                    document.Save();
                    return stream.ToArray();
                }
            }
        }

        private void Fill<T>(WordprocessingDocument wordDoc, ITemplateDescription<T> model) where T : ICommonTemplateData
        {
            ProcessRoot(wordDoc, model);
            OpenXmlElement contentPart = null;
            if (model.DocumentParts != null && model.DocumentParts.Any())
            {
                contentPart = _lastElement = FindContentParagraph(wordDoc);
                if (_lastElement != null)
                {
                    foreach (var part in model.DocumentParts)
                    {
                        Insert(wordDoc, part);
                        if (part != null && model.PartsDivider != null)
                            Insert(wordDoc, model.PartsDivider);
                    }
                }

                contentPart?.Remove();
            }
        }

        private OpenXmlElement FindContentParagraph(WordprocessingDocument wordDoc)
        {
            var result =  wordDoc.MainDocumentPart.Document.Descendants<Paragraph>()
                .FirstOrDefault(p => p.Descendants<Text>().Any(t => t.Text.Contains("<<CONTENT>>")));
            return result;
        }


        private OpenXmlElement _lastElement = null;

        private void Insert(WordprocessingDocument wordDoc, IParagraph part)
        {
            if (part == null)
            {
                return;
            }
            switch (part)
            {
                case FreeTextModel freeText:
                    ProcessText(wordDoc, freeText);
                    break;
                case TableDescriptionModel tableModel:
                   ProcessTable(wordDoc, tableModel);
                    break;
                case Divider dividerModel:
                    ProcessDivider(wordDoc, dividerModel);
                    break;
                default:
                    throw new NotSupportedException($"Параграф такого типа не поддерживается <{part.GetType()}>");
            }
        }

        private void ProcessText(WordprocessingDocument wordDoc, FreeTextModel freeText)
        {
            var paragraph = AddFreeText(wordDoc, freeText);
            _lastElement.InsertAfterSelf(paragraph);
            _lastElement = paragraph;
        }

        private void ProcessTable(WordprocessingDocument wordDoc, TableDescriptionModel tableModel)
        {
            var table = GetTable(wordDoc, tableModel);
            _lastElement.InsertAfterSelf(table);
            _lastElement = table;
        }

        private void ProcessDivider(WordprocessingDocument wordDoc, Divider dividerModel)
        {
            foreach (var i in Enumerable.Range(0, dividerModel.EmptyLinesNumber))
            {
                ProcessText(wordDoc, new FreeTextModel(){Text = string.Empty});
            }
        }

        private Paragraph AddFreeText(WordprocessingDocument wordDoc, FreeTextModel freeText) 
        {
            Paragraph paragraph1 = new Paragraph() { RsidParagraphMarkRevision = "002A5193", RsidParagraphAddition = "00674C4F", RsidParagraphProperties = "005F6103", RsidRunAdditionDefault = "002A5193", ParagraphId = "5EA2031C", TextId = "1A845A23" };

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();

            ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
            Languages languages1 = new Languages() { Val = "en-US" };

            paragraphMarkRunProperties1.Append(languages1);

            paragraphProperties1.Append(paragraphMarkRunProperties1);

            Run run1 = new Run();

            RunProperties runProperties1 = new RunProperties();
            Languages languages2 = new Languages() { Val = "en-US" };

            runProperties1.Append(languages2);
            Text text1 = new Text();
            text1.Text = freeText.Text;

            run1.Append(runProperties1);
            run1.Append(text1);
            BookmarkStart bookmarkStart1 = new BookmarkStart() { Name = "_GoBack", Id = "0" };
            BookmarkEnd bookmarkEnd1 = new BookmarkEnd() { Id = "0" };

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);
            paragraph1.Append(bookmarkStart1);
            paragraph1.Append(bookmarkEnd1);
            return paragraph1;
        }

        private Table GetTable(WordprocessingDocument wordDoc, TableDescriptionModel tableDesc)
        {
            Table resultTable = new Table();

            TableProperties tableProperties = new TableProperties();
            TableWidth tableWidth1 = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
            TableIndentation tableIndentation1 = new TableIndentation() { Width = 108, Type = TableWidthUnitValues.Dxa };

            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableBorders1.Append(topBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(bottomBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);
            TableLayout tableLayout1 = new TableLayout() { Type = TableLayoutValues.Fixed };
            TableLook tableLook1 = new TableLook() { Val = "04A0", FirstRow = true, LastRow = false, FirstColumn = true, LastColumn = false, NoHorizontalBand = false, NoVerticalBand = true };

            tableProperties.Append(tableWidth1);
            tableProperties.Append(tableIndentation1);
            tableProperties.Append(tableBorders1);
            tableProperties.Append(tableLayout1);
            tableProperties.Append(tableLook1);

            TableGrid tableGrid = new TableGrid();
            int notDefinedWidthCount = tableDesc.Columns.Count(c => !c.Width.HasValue);
            decimal freeColumnWidthPerOne = notDefinedWidthCount>0 
                    ? (5000 - tableDesc.Columns.Sum(c => c.Width.GetValueOrDefault() * 50)) / notDefinedWidthCount
                    : 0;
            TableRow tableRowHeader = new TableRow() { RsidTableRowAddition = "00A32340", RsidTableRowProperties = "00751E12" };

            TableRowProperties tableRowHeaderProperties = new TableRowProperties();
            TableRowHeight tableRowHeaderHeight = new TableRowHeight() { Val = (UInt32Value)20U };

            tableRowHeaderProperties.Append(tableRowHeaderHeight);
            tableRowHeader.Append(tableRowHeaderProperties);

            List<(string Pcts,string Dxas)> columnsRealWidthsPcts = new List<(string, string)>(tableDesc.Columns.Length);
            foreach (var column in tableDesc.Columns)
            {
                string realWidthInPcts =
                    $"{(column.Width.HasValue ? (int) column.Width * 50 : (int) freeColumnWidthPerOne)}";
                string metricsWidth =$"{((int)(((column.Width.HasValue ? (int)column.Width * 50 : (int)freeColumnWidthPerOne) / (decimal)5000) * 8872))}";
                GridColumn gridColumn = new GridColumn() { Width =  metricsWidth };
                columnsRealWidthsPcts.Add((Pcts: realWidthInPcts, Dxas: metricsWidth));
                tableGrid.Append(gridColumn);

                TableCell headerCell = new TableCell();

                TableCellProperties tableCellProperties2 = new TableCellProperties();
                TableCellWidth tableCellWidth2 = new TableCellWidth() { Width = realWidthInPcts, Type = TableWidthUnitValues.Pct };

                TableCellBorders tableCellBorders2 = new TableCellBorders();
                TopBorder topBorder3 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                LeftBorder leftBorder3 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                BottomBorder bottomBorder3 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                RightBorder rightBorder3 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

                tableCellBorders2.Append(topBorder3);
                tableCellBorders2.Append(leftBorder3);
                tableCellBorders2.Append(bottomBorder3);
                tableCellBorders2.Append(rightBorder3);
                TableCellVerticalAlignment tableCellVerticalAlignment2 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
                HideMark hideMark2 = new HideMark();

                tableCellProperties2.Append(tableCellWidth2);
                tableCellProperties2.Append(tableCellBorders2);
                tableCellProperties2.Append(tableCellVerticalAlignment2);
                tableCellProperties2.Append(hideMark2);

                Paragraph paragraph2 = new Paragraph() { RsidParagraphAddition = "00A32340", RsidRunAdditionDefault = "00A32340" };

                ParagraphProperties paragraphProperties2 = new ParagraphProperties();
                Justification justification2 = new Justification() { Val = JustificationValues.Center };

                ParagraphMarkRunProperties paragraphMarkRunProperties2 = new ParagraphMarkRunProperties();
                RunFonts runFonts4 = new RunFonts() { ComplexScript = "Arial" };
                FontSize fontSize4 = new FontSize() { Val = "18" };
                FontSizeComplexScript fontSizeComplexScript4 = new FontSizeComplexScript() { Val = "18" };

                paragraphMarkRunProperties2.Append(runFonts4);
                paragraphMarkRunProperties2.Append(fontSize4);
                paragraphMarkRunProperties2.Append(fontSizeComplexScript4);

                paragraphProperties2.Append(justification2);
                paragraphProperties2.Append(paragraphMarkRunProperties2);

                Run run3 = new Run();

                RunProperties runProperties3 = new RunProperties();
                RunFonts runFonts5 = new RunFonts() { ComplexScript = "Arial" };
                FontSize fontSize5 = new FontSize() { Val = "18" };
                FontSizeComplexScript fontSizeComplexScript5 = new FontSizeComplexScript() { Val = "18" };

                runProperties3.Append(runFonts5);
                runProperties3.Append(fontSize5);
                runProperties3.Append(fontSizeComplexScript5);
                Text text3 = new Text();
                text3.Text = column.Text;

                run3.Append(runProperties3);
                run3.Append(text3);

                paragraph2.Append(paragraphProperties2);
                paragraph2.Append(run3);

                headerCell.Append(tableCellProperties2);
                headerCell.Append(paragraph2);
                tableRowHeader.Append(headerCell);
            }

            resultTable.Append(tableProperties);
            resultTable.Append(tableGrid);
            resultTable.Append(tableRowHeader);

            IList<TableRow> tableRows = new List<TableRow>();

            foreach (var rowValues in tableDesc.Rows)
            {
                TableRow valueRow = new TableRow() { RsidTableRowAddition = "00A32340", RsidTableRowProperties = "00751E12" };

                TableRowProperties valueRowProperties = new TableRowProperties();
                TableRowHeight height = new TableRowHeight() { Val = (UInt32Value)20U };

                valueRowProperties.Append(height);

                int i = 0;
                foreach (var rowValue in rowValues)
                {
                    TableCell valueCell = new TableCell();

                    TableCellProperties tableCellProperties4 = new TableCellProperties();
                    TableCellWidth tableCellWidth4 = new TableCellWidth() { Width = columnsRealWidthsPcts[i++].Pcts, Type = TableWidthUnitValues.Pct }; //TODO: копировать ширину из значений колонки

                    TableCellBorders tableCellBorders4 = new TableCellBorders();
                    TopBorder topBorder5 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                    LeftBorder leftBorder5 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                    BottomBorder bottomBorder5 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                    RightBorder rightBorder5 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

                    tableCellBorders4.Append(topBorder5);
                    tableCellBorders4.Append(leftBorder5);
                    tableCellBorders4.Append(bottomBorder5);
                    tableCellBorders4.Append(rightBorder5);
                    NoWrap noWrap2 = new NoWrap();
                    TableCellVerticalAlignment tableCellVerticalAlignment4 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
                    HideMark hideMark4 = new HideMark();

                    tableCellProperties4.Append(tableCellWidth4);
                    tableCellProperties4.Append(tableCellBorders4);
                    tableCellProperties4.Append(noWrap2);
                    tableCellProperties4.Append(tableCellVerticalAlignment4);
                    tableCellProperties4.Append(hideMark4);

                    Paragraph paragraph4 = new Paragraph() { RsidParagraphMarkRevision = "00AE4495", RsidParagraphAddition = "00A32340", RsidRunAdditionDefault = "00AE4495" };

                    ParagraphProperties paragraphProperties4 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties4 = new ParagraphMarkRunProperties();
                    RunFonts runFonts8 = new RunFonts() { ComplexScript = "Arial" };
                    FontSize fontSize8 = new FontSize() { Val = "18" };
                    FontSizeComplexScript fontSizeComplexScript8 = new FontSizeComplexScript() { Val = "18" };
                    Languages languages1 = new Languages() { Val = "en-US" };

                    paragraphMarkRunProperties4.Append(runFonts8);
                    paragraphMarkRunProperties4.Append(fontSize8);
                    paragraphMarkRunProperties4.Append(fontSizeComplexScript8);
                    paragraphMarkRunProperties4.Append(languages1);

                    paragraphProperties4.Append(paragraphMarkRunProperties4);

                    Run run5 = new Run();

                    RunProperties runProperties5 = new RunProperties();
                    RunFonts runFonts9 = new RunFonts() { ComplexScript = "Arial" };
                    FontSize fontSize9 = new FontSize() { Val = "18" };
                    FontSizeComplexScript fontSizeComplexScript9 = new FontSizeComplexScript() { Val = "18" };
                    Languages languages2 = new Languages() { Val = "en-US" };

                    runProperties5.Append(runFonts9);
                    runProperties5.Append(fontSize9);
                    runProperties5.Append(fontSizeComplexScript9);
                    runProperties5.Append(languages2);
                    Text text5 = new Text();
                    text5.Text = rowValue;

                    run5.Append(runProperties5);
                    run5.Append(text5);
                    

                    
                    paragraph4.Append(paragraphProperties4);
                    paragraph4.Append(run5);

                    valueCell.Append(tableCellProperties4);
                    valueCell.Append(paragraph4);

                   
                    valueRow.Append(valueCell);
                }

                valueRow.Append(valueRowProperties);
                resultTable.Append(valueRow);
                tableRows.Add(valueRow);
            }

            MergeCells(tableRows, tableDesc);

            return resultTable;
        }

        private static void MergeCells(IList<TableRow> tableRows, TableDescriptionModel tableDescriptionModel)
        {
            var verticalMerges = tableDescriptionModel.VerticalMerges;

            if (verticalMerges == null)
                return;

            foreach (var verticalMerge in verticalMerges)
            {
                GetCell(tableRows, verticalMerge.StartRow, verticalMerge.Column)
                    .TableCellProperties
                    .Append(new VerticalMerge
                    {
                        Val = MergedCellValues.Restart
                    });

                for (var row = verticalMerge.StartRow + 1; row <= verticalMerge.EndRow; row++)
                    GetCell(tableRows, row, verticalMerge.Column)
                        .TableCellProperties
                        .Append(new VerticalMerge
                        {
                            Val = MergedCellValues.Continue
                        });
            }
        }

        private static TableCell GetCell(IList<TableRow> tableRows, int row, int column)
        {
            return tableRows[row].Elements<TableCell>().ElementAt(column);
        }

        private void ProcessRoot<T>(WordprocessingDocument wordDoc, ITemplateDescription<T> model) where T : ICommonTemplateData
        {
            var commonData = model.CommonTemplateData;
            var dict = JObject.FromObject(commonData).ToObject<Dictionary<string, object>>();


            foreach (var pair in dict)
            {
                string replacement = pair.Value?.ToString() ?? string.Empty;
                wordDoc.ReplaceStringInWordDocument($"{{{pair.Key}}}", replacement);
            }

        }
    }
}