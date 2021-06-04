using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TPAPIproject.Models
{
    [Table("TP_Documentation")]
    public partial class TpDocumentation
    {
        public int Id { get; set; }
        public int IdentificationTypeId { get; set; }
        public long IdentificationNumber { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public bool CheckMessagesCell { get; set; }
        public bool CheckMessagesEmail { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual TpIdentificationType IdentificationType { get; set; }
    }
}
