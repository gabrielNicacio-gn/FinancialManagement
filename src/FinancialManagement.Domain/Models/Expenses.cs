using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Domain.Models
{
    [Table("expenses")]
    public class Expenses
    {
        [Column("id_expenses")]
        public Guid IdExpenses { get; set; }

        [Column("value_expenses")]
        public decimal Value { get; set; }

        [Column("date_expenses")]
        public DateTime DateExpenses { get; set; }

        [Column("description_expenses")]
        public string Description { get; set; } = "";

        public Expenses() => IdExpenses = Guid.NewGuid();
    }
}