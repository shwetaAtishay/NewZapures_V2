using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class CollegeAmendmentList
    {
        public int iPk_ClgID { get; set; }
        public string PartyId { get; set; }
        public string NameOfClg { get; set; }
    }
    public class CollegeAmendmentListEdit
    {
        public int iPk_ClgID { get; set; }
        public int iFk_DistId { get; set; }
        public int iFk_ThslId { get; set; }
        public int iFk_TrstInfoId { get; set; }
        public string Addressoneold { get; set; }
        public string Addresstwoold { get; set; }

    }
    public class CollegeAmendmentSave
    {
        public int iTrustId { get; set; }
        public int iCollegeId { get; set; }
        public string stypeId { get; set; }
        public int iNewSocity_Id { get; set; }
        public int iNewSocityOld { get; set; }
        public string sNewNameEnglish { get; set; }
        public string sNewNameHindi { get; set; }
        public string sOldNameEnglish { get; set; }
        public DateTime CreateOn { get; set; }
        public string sCreateBy { get; set; }
        public int iNocStatus { get; set; }
        public int iDistrictold { get; set; }
        public int itehshilold { get; set; }
        public int iDistrictNew { get; set; }
        public int itehshilNew { get; set; }
        public string sAddreslineoneold { get; set; }
        public string sAddresslinetwoold { get; set; }
        public string sAddreslineoneNew { get; set; }
        public string sAddresslinetwoNew { get; set; }
    }
}