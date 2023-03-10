using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class SMSRequest
    {
        public int TemplateId { get; set; }
        public string Language { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public List<string> MobileNo { get; set; }
        public string SSOID { get; set; }
        public string SMSBody { get; set; }
        public string RegNo { get; set; }

    }

    public class SmsResponseModel
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseID { get; set; }
        public string TrustType { get; set; }
    }
}