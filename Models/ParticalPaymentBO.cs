using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class ParticalPaymentBO
    {
        public string ChanalNo { get; set; }
        public string BankName { get; set; }
        public string ApplicationFees { get; set; }
        public string BranchName { get; set; }
        public string DepositeOn { get; set; }
        public string PaymentDocument { get; set; }
        public string PaymentDocumentExtension { get; set; }
        public string PaymentDocumentContenttype { get; set; }
        public string Remark { get; set; }

        //GridList
        public int iPk_PymtID { get; set; }
        public string sChanalNo { get; set; }
        public string sBankName { get; set; }
        public string sApplicationFees { get; set; }
        public string sBranchName { get; set; }
        public string sDepositeOn { get; set; }
        public string sRemark { get; set; }
        public string dtCreateDate { get; set; }
        public string sAppID { get; set; }
    }
}