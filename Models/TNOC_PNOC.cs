using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class TNOC_PNOC
    {
        public int iPK_Id { get; set; }
        public int iFK_CORS_ID { get; set; }
        public string sCorsName { get; set; }
        public int iFK_Subj_ID { get; set; }
        public string sSubjName { get; set; }
        public int Fk_StatusId { get; set; }
        public string sNOCNo { get; set; }
        public string dtNOCRcvdOn { get; set; }
        public string sAttachFile { get; set; }
        public string RANK { get; set; }

    }
}