using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    /// <summary>
    /// This will be what the contact table looks like
    /// </summary>
    public class Contact
    {
        public int id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(50)]
        public string email { get; set; }
        [MaxLength(50)]
        public string number { get; set; }  
        /// <summary>
        /// Because this thing is very dis-joined (as in there are nothing solid to bite on too initially), foreign keys can NOT be enforced.  It'll beak stuff.
        /// The forengn key field must therefore be manually set EVERY TIME!
        /// </summary>
        public int customer_id { get; set; }
    }
}
