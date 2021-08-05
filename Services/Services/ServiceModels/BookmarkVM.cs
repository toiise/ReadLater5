using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Services.ServiceModels
{
   public class BookmarkVM
    {
        
        public int ID { get; set; }
        [Required]
        [Url]
        public string URL { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        
        public int? CategoryId { get; set; }

        public string UserID { get; set; }
        
        public virtual Category Category { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
