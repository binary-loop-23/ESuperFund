using System;
using System.ComponentModel.DataAnnotations;

namespace esuperfund.Models
{
    public class RawBankTransaction
    {
        public int? AccountNumber { get; set; }

        public DateOnly? Date { get; set; }

        public string? Narration { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Balance { get; set; }
    }
}

