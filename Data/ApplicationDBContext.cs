﻿using System;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using esuperfund.Models;
using Microsoft.EntityFrameworkCore;

namespace esuperfund.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
        {

        }

        public DbSet<RawBankTransaction> RawBankTransactions { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }

    }
}

