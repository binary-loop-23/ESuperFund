using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esuperfund.Models
{
    public class BankTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Narration { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}

