using System;
using System.Collections.Generic;
using System.Text;

namespace kontaktor_network.DA.Models
{
    public class AccountingInformation
    {
        public string LegalAddress { get; set; }
        public string MailAdress { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string BIK { get; set; }
        public string BankName { get; set; }
        public string Account { get; set; }
        public string CorrAccount { get; set; }
        public string OGRN;
    }
}
