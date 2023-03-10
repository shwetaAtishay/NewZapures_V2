using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class ApplicationTrack
    {
        public int TrkID { get; set; }
        public string sApplNo { get; set; }
        public string sSndrSSO { get; set; }
        public string sSndrLvlID { get; set; }
        public string sRcvrSSO { get; set; }
        public string sRcvrRolID { get; set; } 
        public string sRcvrLvlID  { get; set; } 
        public string dtPrcDate { get; set; }
        public string sPrcName  { get; set; }
        public string sNrtn { get; set; }
        public string Action { get; set; }

    }
}