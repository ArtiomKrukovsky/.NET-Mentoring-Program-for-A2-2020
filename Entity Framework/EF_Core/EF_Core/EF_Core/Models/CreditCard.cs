using System;

namespace EF_Core.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public int EmployeeId { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardHolder { get; set; }

        public Employee Employee { get; set; }
    }
}