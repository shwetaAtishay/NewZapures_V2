using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class BasicDetailsBO
    {
        public int? Id { get; set; }
        public string TrustInfoId { get; set; }
        [Required]
        public string CollegeName { get; set; }
        [Required]
        public int UniverSity { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string LocationType { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public int TehsilId { get; set; }
        public int CityId { get; set; }
        public int BlockId { get; set; }
        public string WardNo { get; set; }
        public int AssembleArea { get; set; }
        public int CityTownVillage { get; set; }
        public int UrbanRular { get; set; }
        public int PanchayatSimiti { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public int PinCode { get; set; }
        public string Latitudedd { get; set; }
        public string Longitudedd { get; set; }
        [Required]
        public string AddiMobileNumber { get; set; }
        public string Websiteurl { get; set; }
        [Required]
        public string LandlineNumber { get; set; }
        [Required]
        public string CollageLogo { get; set; }
        public string CollageLogoExtension { get; set; }
        public string CollageLogoContenttype { get; set; }

        [Required]
        public string collegeLevel { get; set; }
        [Required]
        public string collegeMedium { get; set; }
        [Required]
        public string CollageType { get; set; }

        public List<ContactDetails> ContactDetails { get; set; }

        public string CollageCategory { get; set; }
        public string AISHECodeStatus { get; set; }
        public string AIESHCode { get; set; }
        public int Paliamentarea { get; set; }
        public int LegislativeId { get; set; }
        [Required]
        public string CollageNamehindi { get; set; }

        public int DivisionId { get; set; }
        public int SubDivisionId { get; set; }
        public string CollegeTypeId { get; set; }
        public string TagDegrees { get; set; }

    }

    public class ContactDetails
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Email { get; set; }
    }


    public class NewCollegeDetails
    {
        public string sNameOfClg { get; set; }
        public string clgAddress { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string iClgTyp { get; set; }
        public string collegeType { get; set; }
        public int DistrictID { get; set; }
        public string District { get; set; }
        public int TehsilID { get; set; }
        public string Tehsil { get; set; }
        public string backwardArea { get; set; }
        public string totalFee { get; set; }
        public List<CollegeCourseSubject> courseSubjects { get; set; }

    }
    public class CollegeCourseSubject
    {
        public string CourseName { get; set; }
        public string subjectName { get; set; }
    }
}