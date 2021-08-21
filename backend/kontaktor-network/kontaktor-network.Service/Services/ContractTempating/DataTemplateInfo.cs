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

    

    public class ContractLetterModel : ITemplateDescription<CommonContractTemplateData>
    {
        public CommonContractTemplateData CommonTemplateData { get; set; }
        public IParagraph[] DocumentParts { get; set; }
        public IParagraph PartsDivider { get; set; }
    }

    public class CommonContractTemplateData : ICommonTemplateData
    {
        public string SignPlace { get; set; }
        public string SignDate { get; set; }
        public string OwnerStart { get; set; }
        public string TenantStart { get; set; }
        public string CommonAddress { get; set; }
        public string RoomFloor { get; set; }
        public string MaxFloors { get; set; }
        public string Area { get; set; }
        public string RentFrom { get; set; }
        public string RentTo { get; set; }
        public string RentRegistration { get; set; }
        public string RentDataConditions { get; set; }
        public string RentPaymentConditions { get; set; }
        public string RentWay { get; set; }
        public string OwnerReq { get; set; }
        public string TenantReq { get; set; }
    }



    
}