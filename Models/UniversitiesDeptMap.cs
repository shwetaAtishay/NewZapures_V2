using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class UniversitiesDeptMap
    {
        public int Dept { get; set; }
        public int University { get; set; }
        public string DeptName { get; set; }

        public string UniversitiesName { get; set; }
    }
    public class UniversitiesDeptMapTableData
    {
        public int pk_MapId { get; set; }
        public string DeptName { get; set; }

        public string UniversitiesName { get; set; }
    }
}