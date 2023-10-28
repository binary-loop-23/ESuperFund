using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esuperfund.Models
{
    public class RawBankTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        public int? AccountNumber { get; set; }

        public DateTime? Date { get; set; }

        public string? Narration { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Balance { get; set; }
    }
}

