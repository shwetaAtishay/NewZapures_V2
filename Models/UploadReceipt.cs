using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewZapures_V2.Models
{
    public class UploadReceipt
    {
        public string AppGUID { get; set; }
        public string UploadReceiptDocument { get; set; }
        public string UploadReceiptDocumentExtension { get; set; }
        public string UploadReceiptDocumentContent { get; set; }
    }
    public class UploadMasterReceipt : UploadReceipt
    {
        public string fullBase64Data { get; set; }
        public decimal paidAMT { get; set; }
        public decimal dueAMT { get; set; }
        public string paidBy { get; set; }
    }
}
