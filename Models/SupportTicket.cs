using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class SupportTicket
    {
        public int ModuleId { get; set; }
        public int FunctionalityId { get; set; }

        public int TicketId { get; set; }

        public string Description { get; set; }

        public string Attachment { get; set; }
    }
    public class SupportIssue
    {
        public int? Id { get; set; }

        public int Tickets { get; set; }
        public string TicketsName { get; set; }
        public string Subject { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public string dateCreate { get; set; }
        public string TicketFile { get; set; }
        public string TicketFileExtension { get; set; }
        public string TicketFileContentType { get; set; }

        public string TicketStatus { get; set; }
    }

}