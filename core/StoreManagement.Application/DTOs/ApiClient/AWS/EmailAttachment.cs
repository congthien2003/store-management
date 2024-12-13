using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.DTOs.ApiClient.AWS
{
    public class EmailAttachment
    {
        public string RecipientEmail { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
        public byte[] Attachment { set; get; }
        public string AttachmentName { set; get; }
        public string ContentType { set; get; }
    }
}
