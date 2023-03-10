using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace NewZapures_V2.Models
{
    public class LandDetailsBO
    {
        //public int iFk_AplcnId { get;set; }
        //public int iFK_ClgId { get;set; }
        //public string iFK_TypeConvert { get;set; }
        //public BinaryConverter bLndConvert { get;set; }
        //public string sOwnrName { get;set; }
        //public string LandArea { get;set; }
        //public string sOwnerbuildNo { get;set; }
        //public string dtOrderDt { get;set; }
        //public string sLandDetails { get;set; }
        //public string sLndNo { get;set; }
        //public BinaryConverter IsAnyOtherInfo { get;set; }

    }


    public class LandInfoBO
    {
        public int iPK_ID { get; set; }
        public int iDeptID { get; set; }
        public int iCorsID { get; set; }
        public int iClgID { get; set; }
        public string iFinyr { get; set; }
        public string Course { get; set; }
        public int iTrstID { get; set; }
        public string sApplGUID { get; set; }
        public string sLndArea { get; set; }
        public string sLndDocType { get; set; }
        public string sIsLndCnvrted { get; set; }
        public string sOrdrNo { get; set; }
        public string dtOrdrDate { get; set; }
        public string sLndTyp { get; set; }
        public string sKhasaraNo { get; set; }
        public string slndAccureTyp { get; set; }
        public string dLndArea { get; set; }
        public string dTotalArea { get; set; }
        public string sEntryType { get; set; }
        public string sOwnerName { get; set; }
        //image
        public string UploadConvertedDocument { get; set; }
        public string UploadConvertedDocumentExtension { get; set; }
        public string UploadConvertedDocumentContent { get; set; }

        //image
        public string UploadLandTitleDoc { get; set; }
        public string UploadLandTitleDocExtension { get; set; }
        public string UploadLandTitleDocContent { get; set; }

        //image
        public string UploadLandNotConvertDoc { get; set; }
        public string UploadLandNotConvertDocExtension { get; set; }
        public string UploadLandNotConvertDocContent { get; set; }


        //image
        public string UploadLandDoc { get; set; }
        public string UploadLandDocExtension { get; set; }
        public string UploadLandDocContent { get; set; }

        //image
        public string UploadLandAccureTypeDoc { get; set; }
        public string UploadLandAccureTypeDocExtension { get; set; }
        public string UploadLandAccureTypeDocContent { get; set; }

        public string UploadConvertedDocumentAffidavit { get; set; }
        public string UploadConvertedDocumentExtensionAffidavit { get; set; }
        public string UploadConvertedDocumentContentAffidavit { get; set; }

    }

    public class BuildingDetails
    {
        public int iPK_ID { get; set; }
        public string sAppGUID { get; set; }
        public int iFK_FinyrID { get; set; }
        public int iFK_TrstID { get; set; }
        public int iFK_ClgID { get; set; }
        public int iFK_DeptID { get; set; }
        public string sBldgntyp { get; set; }
        public string sOrdNo { get; set; }
        public string dtOrd { get; set; }
        public string OwnedBldgnDoc { get; set; }
        public string dtAgrExp { get; set; }
        public string rentedOrderNo { get; set; }
        public string AgrExpDoc { get; set; }
        public string rentedCertificateDoc { get; set; }
        public string dtFireFrom { get; set; }
        public string dtFireTo { get; set; }
        public string FireNOCDoc { get; set; }
        public string sPWDOrderNo { get; set; }
        public string dtPWDFrom { get; set; }
        public string dtPWDTo { get; set; }
        public string PWDNOCDoc { get; set; }
        public string BFrontImg { get; set; }
        public string BBackImg { get; set; }
        public string BLeftImg { get; set; }
        public string BRightImg { get; set; }
        public string BOtherImg { get; set; }
        public string iStatus { get; set; }
        public string dtCrtdOn { get; set; }
        public string dtCrtdBy { get; set; }

    }
}