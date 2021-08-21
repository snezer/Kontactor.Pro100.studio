using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KONTAKTOR.Service.Services.DocxTemplating;
using KONTAKTOR.Services.DocxTemplating;

namespace KONTAKTOR.Service.Services
{
    public class DocXGenerationService 
    {
        public async Task<byte[]> CreateDocumentBodyAsync<T>(string rootTemplatePath, ITemplateDescription<T> templateDescription)
            where T : ICommonTemplateData
        {
            var filler = new DocxFileGenerator(rootTemplatePath);
            var result = await filler.Process(templateDescription);
            return result;
        }
    }
}
