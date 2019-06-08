using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wcf_chat
{
    public class Conversation
    {
        [Key]
        public int ConvId { get; set; }
        public string UserOne { get; set; }
        [ForeignKey("UserOne")]
        public Users User1 { get; set; }
        public string UserTwo { get; set; }
        [ForeignKey("UserTwo")]
        public Users User2 { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

