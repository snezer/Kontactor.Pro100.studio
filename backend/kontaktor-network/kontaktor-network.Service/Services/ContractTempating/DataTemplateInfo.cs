using System.Collections.Generic;
using KONTAKTOR.Service.Services.DocxTemplating;
using KONTAKTOR.Services.DocxTemplating;

namespace KONTRAKTOR.Services.Contract.Letter
{
    public class AccountOperationLetterInfo
    {
        public long DocumentId { get; set; }

        public string AccountNumber { get; set; }

        public DocxData DocumentModel { get; set; }
    }

    

    public class OtherQueryLetterModel : ITemplateDescription<CommonOtherQueryLetterTemplateData>
    {
        public CommonOtherQueryLetterTemplateData CommonTemplateData { get; set; }
        public IParagraph[] DocumentParts { get; set; }
        public IParagraph PartsDivider { get; set; }
    }

    public class CommonOtherQueryLetterTemplateData : ICommonTemplateData
    {

        /// <summary>
        /// Номер входящего документа от СДО
        /// </summary>
        public string SdoIncomingNumber { get; set; }
        /// <summary>
        /// Дата входящего документа от СДО
        /// </summary>
        public string SdoIncomingDate { get; set; }
        /// <summary>
        /// Номер исходящего документа в СДО
        /// </summary>
        public string SdoOutgoingNumber { get; set; }
        /// <summary>
        /// Дата исходящего документа в СДО
        /// </summary>
        public string SdoOutgoingDate { get; set; }
        /// <summary>
        /// Название организации в отношении которой производится ответ
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// Адрес организации  в отношении котрой производится ответ
        /// </summary>
        public string OrgAddress { get; set; }
        /// <summary>
        /// Номер дела (НЕ тоже что запрос)
        /// </summary>
        public string DeloNum { get; set; }
        /// <summary>
        /// Дата дела
        /// </summary>
        public string DeloDate { get; set; }
        public string RequestNum { get; set; }
        public string RequestDate { get; set; }
        public string SignerFIO { get; set; }
        public string SignerPosition { get; set; }
        public string BankingSecrecy { get; set; }
    }



    
}