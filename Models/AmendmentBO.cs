using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class AmendmentBO
    {
        public string PartyId { get; set; }
        public string sName { get; set; }
        public string iPk_AplcnAttch { get; set; }
        public string trusID { get; set; }
        public int? CollegeId { get; set; }
        public string iCollegeId { get; set; }
        public string sSSOID { get; set; }
        public string ConsentofManagement { get; set; }
        public string ConsentofManagementExtension { get; set; }
        public string ConsentofManagementContenttype { get; set; }
        //2nd
        public string DocumentConsent { get; set; }
        public string DocumentConsentExtension { get; set; }
        public string DocumentConsentContenttype { get; set; }

        //Chnage College name
        public string Englishname { get; set; }
        public string HindiName { get; set; }
        public string CollegeAttch { get; set; }
        public string CollegeAttchExtensionExtension { get; set; }
        public string CollegeAttchContenttype { get; set; }

        //managmentCommity
        public string NewSosityName { get; set; }
        public string ResolutionMagtCommitte { get; set; }
        public string ResolutionMagtCommitteExtension { get; set; }
        public string ResolutionMagtCommittetype { get; set; }

        public string ChangeManagement { get; set; }
        public string ChangeManagementExtension { get; set; }
        public string ChangeManagementtetype { get; set; }

        //NOC ISSUED merger

        public string ProposalSocietyMerger { get; set; }
        public string ProposalSocietyMergerExtension { get; set; }
        public string ProposalSocietyMergertetype { get; set; }

        public string NOCsissued { get; set; }
        public string NOCsissuedExtension { get; set; }
        public string NOCsissuedtetype { get; set; }

        public string LstUnivsityAffiliationCollege { get; set; }
        public string LstUnivsityAffiliationCollegeExtension { get; set; }
        public string LstUnivsityAffiliationCollegetype { get; set; }

        public string Affidavitconsent { get; set; }
        public string AffidavitconsentExtension { get; set; }
        public string Affidavitconsenttype { get; set; }

        public string AffidavitParents { get; set; }
        public string AffidavitParentsExtension { get; set; }
        public string AffidavitParentstype { get; set; }

        //other Noc issue
        public string AllNOCsIssued { get; set; }
        public string AllNOCsIssuedExtension { get; set; }
        public string AllNOCsIssuedstype { get; set; }

        //Latest NOC
        public string LatestUniversityAffilliation { get; set; }
        public string LatestUniversityAffilliationExtension { get; set; }
        public string LatestUniversityAffilliationtype { get; set; }

        //NOCAffiliatting
        public string NOCAffiliatting { get; set; }
        public string NOCAffiliattingExtension { get; set; }
        public string NOCAffiliattingtype { get; set; }

        //AffidavitConsentParents
        public string AffidavitConsentParents { get; set; }
        public string AffidavitConsentParentsExtension { get; set; }
        public string AffidavitConsentParentstype { get; set; }

        //land details
        public string LandCertificate { get; set; }
        public string LandCertificateExtension { get; set; }
        public string LandCertificatetype { get; set; }

        //BluePrintBuilding
        public string BluePrintBuilding { get; set; }
        public string BluePrintBuildingExtension { get; set; }
        public string BluePrintBuildingtype { get; set; }

        //Staff information
        public string StaffInformation { get; set; }
        public string StaffInformationExten { get; set; }
        public string StaffInformationContent { get; set; }

        // College Place Change Start

        public int District { get; set; }
        public int Tehsil { get; set; }
        public string oldPlaceAddressone { get; set; }
        public string oldPlaceAddresstwo { get; set; }
        public string NewPlaceAddressone { get; set; }
        public string NewPlaceAddresstwo { get; set; }
        public string PlaceAttch { get; set; }
        public string PlaceAttchExtension { get; set; }
        public string PlaceAttchContenttype { get; set; }

        public string ChangePlace { get; set; }
        public string ChangeManagementPlace { get; set; }
        public string ChangeManagementtePlace { get; set; }

        // College Place End
        public string stypeId { get; set; }
        public string iPk_ClgAmdId { get; set; }
        public string sNameOfClg { get; set; }
        public string docFile { get; set; }
        public string DocumentContent { get; set; }
        public string DocumentExtension { get; set; }
        /// <summary>
        // chnage college place
        /// </summary>
        public string iDistrictNew { get; set; }
        public string DistName { get; set; }
        public string tehsilName { get; set; }
        public string sAddreslineoneold { get; set; }
        public string sAddreslineoneNew { get; set; }
        public string sAddresslinetwoold { get; set; }
        public string sAddresslinetwoNew { get; set; }

        public string ImageTypeName { get; set; }

        public string SocietyMergertype { get; set; }
        public string NOCsissuedtype { get; set; }
        public string LstUnivsityAffiliationColtype { get; set; }
        public string AAAffidavitconsenttype { get; set; }
        public string PAffidavitParentstype { get; set; }
        public string AllNOCsIssuedtype { get; set; }
        public string UniversityAffilliationType { get; set; }
        public string NOCAffiliattingTyp { get; set; }
        public string AffidavitConsentParentsTyp { get; set; }
        public string LandCertificateTyp { get; set; }
        public string BluePrintBuildingTyp { get; set; }
        public string StaffInformationTyp { get; set; }

    }
}