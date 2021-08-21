using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KONTAKTOR.Service.Services.DocxTemplating
{
    public interface ITemplateDescription<T> where T : ICommonTemplateData
    {
        T CommonTemplateData { get; set; }
        IParagraph[] DocumentParts { get; set; }
        IParagraph PartsDivider { get; set; }
    }

    public interface ICommonTemplateData { }

    public interface IParagraph
    {
    }

    public class CompositeParagraph : IParagraph
    {
        public IParagraph[] InnerContent { get; set; }
    }

    public class Divider : IParagraph
    {
        public int EmptyLinesNumber { get; set; }
    }

    public class FreeTextModel : IParagraph
    {
        public string Text { get; set; }
    }

    public class ColumnDescriptionModel
    {
        public string Text { get; set; }
        public decimal? Width { get; set; }

        public static implicit operator ColumnDescriptionModel(string str)
        {
            return new ColumnDescriptionModel() { Text = str };
        }

        public static implicit operator ColumnDescriptionModel((string s, decimal d) pTuple)
        {
            return new ColumnDescriptionModel() { Text = pTuple.s, Width = pTuple.d };
        }
    }

    public class TableDescriptionModel : IParagraph
    {
        public ColumnDescriptionModel[] Columns { get; set; }

        public IEnumerable<string[]> Rows { get; set; }

        public VerticalMergeDescriptionModel[] VerticalMerges { get; set; }
    }

    public class VerticalMergeDescriptionModel
    {
        public int Column { get; set; }

        public int StartRow { get; set; }

        public int EndRow { get; set; }
    }
}
