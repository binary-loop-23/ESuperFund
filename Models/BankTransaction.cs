using System;
using System.ComponentModel.DataAnnotations;

namespace esuperfund.Models
{
    public class BankTransaction
    {
        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Narration { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}

