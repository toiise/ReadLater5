using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class Clicks
    {
        [Key]
        public int ID { get; set; }
        [StringLength(maximumLength: 50)]
        public string Url { get; set; }
        public string UserID { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
