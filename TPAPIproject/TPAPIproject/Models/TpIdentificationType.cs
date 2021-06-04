using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TPAPIproject.Models
{
    [Table("TP_IdentificationType")]
    public partial class TpIdentificationType
    {
        public TpIdentificationType()
        {
            TpDocumentations = new HashSet<TpDocumentation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<TpDocumentation> TpDocumentations { get; set; }
    }
}
