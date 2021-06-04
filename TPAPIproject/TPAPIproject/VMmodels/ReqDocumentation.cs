using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPAPIproject.VMmodels
{
    public class ReqDocumentation
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
    }
}
