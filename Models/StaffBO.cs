using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class StaffBO
    {
        public class Staff : ErrorBO
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string Qualification { get; set; }
            public string Type { get; set; }
            public string Aadhaar { get; set; }
            public string AadhaarExtension { get; set; }
            public string AadhaarContentType { get; set; }
            public string Pan { get; set; }
            public string PanExtension { get; set; }
            public string PanContentType { get; set; }
            public string Profile { get; set; }
            public string ProfileExtension { get; set; }
            public string ProfileContentType { get; set; }
            public string Experience { get; set; }
            public string ExperienceExtension { get; set; }
            public string ExperienceContentType { get; set; }
            public string Guid { get; set; }
            public List<QualificationDetails> QualificationDetails { get; set; }
            public string DOB { get; set; }
            public string DOJ { get; set; }
            public string DOA { get; set; }
            public string ResearchGuide { get; set; }
            public string PFNumber { get; set; }
            public string PFDeduction { get; set; }
            public string Salary { get; set; }
            public string StaffStatus { get; set; }
            public string Specialization { get; set; }
            public string Course { get; set; }
            public string Subject { get; set; }
            public string ExpNo { get; set; }

        }
        public class QualificationDetails
        {
            public string Qualification { get; set; }
            public string Subject { get; set; }
            public string Institute { get; set; }
            public string PassingYear { get; set; }
            public string MarksPercentage { get; set; }
        }
    }
}