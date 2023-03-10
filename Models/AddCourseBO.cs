using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class AddCourseBO
    {

        public int? iPk_AplcnId { get; set; }
        public int? iPk_AddCourseId { get; set; }
        public int? iPK_SubId { get; set; }
        public int? iFK_DeptId { get; set; }
        public string TrustName { get; set; }
        public string TrustInfoId { get; set; }
        public string CollegeName { get; set; }
        public string iCollegeId { get; set; }
        public string TagDegrees { get; set; }
        public int? iFK_CourseId { get; set; }
        public string TagCourse { get; set; }

        public string Facility { get; set; }

        public string SubjectName { get; set; }
        public string applicationNumber { get; set; }
        public int? iIsActive { get; set; }
        public string SubjectId { get; set; }

        public string File { get; set; }
        public string DocumentContent { get; set; }
        public string DocumentExtension { get; set; }
        public int? PNOC { get; set; }
        public int? TNOC { get; set; }
        public int? ClgId { get; set; }
        public string Sublist { get; set; }
        //public int? iPk_AddCourseId { get; set; }
        //public string iPk_AplcnId { get; set; }
        //public int? iPK_SubId { get; set; }
        //public int? iFK_DeptId { get; set; }
        //public string TrustName { get; set; }
        //public string TrustInfoId { get; set; }
        //public string CollegeName { get; set; }
        //public string iCollegeId { get; set; }
        //public string TagDegrees { get; set; }
        //public string TagCourse { get; set; }
        //public string SubjectName { get; set; }
        //public int? iIsActive { get; set; }
    }
    public class ListAddCourseBO : ResponseData
    {
        public List<AddCourseBO> ListRequest { get; set; }
    }

}