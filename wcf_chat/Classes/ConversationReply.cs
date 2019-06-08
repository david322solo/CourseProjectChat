using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace wcf_chat
{
    public class ConversationReply
    {
        [Key]
        public int ConvReplyId { get; set; }
        public string Message { get; set; }
        public string UserLoginFk { get; set; }
        [ForeignKey("UserLoginFk")]
        public Users Users { get; set; }
        public DateTime date { get; set; }
        public int ConvId { get; set; }
        [ForeignKey("ConvId")]
        public Conversation conversation { get; set; }
    }
}
