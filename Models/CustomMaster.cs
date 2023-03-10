using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class CustomMaster
    {
        public int? Id { get; set; }
        public string text { get; set; }
    }
    public class ListCustom : ResponseData
    {
        public List<CustomMaster> ListRequest { get; set; }
    }
    public class ExistingNOCRequest
    {
        public string Type { get; set; }
        public string TrustId { get; set; }
        public string CollageId { get; set; }
        public string DepartmentId { get; set; }
        public string CourseId { get; set; }
    }
}