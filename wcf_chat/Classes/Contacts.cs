using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace wcf_chat
{
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        public string LoginUser { get; set; }
        [ForeignKey("LoginUser")]
        public Users User { get; set; }
        public string LoginUserContact { get; set; }
        [ForeignKey("LoginUserContact")]
        public Users User1 { get; set; }
    }
}
