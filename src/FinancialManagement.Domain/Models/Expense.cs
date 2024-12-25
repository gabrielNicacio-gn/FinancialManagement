using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Domain.Models;

[Table("expense")]
public class Expense
{
    [Column("id_expense")]
    public Guid IdExpenses { get; set; }

    [Column("value_expense")]
    public decimal Value { get; set; }

    [Column("date_expense")]
    public DateTime DateExpenses { get; set; }

    [Column("description_expense")]
    public string Description { get; set; } = "";

    public Expense() => IdExpenses = Guid.NewGuid();
}
