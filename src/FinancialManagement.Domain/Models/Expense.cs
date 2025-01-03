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
    public Guid IdExpense { get; set; }

    [Column("value_expense")]
    public decimal Value { get; set; }

    [Column("date_expense")]
    public DateTime DateExpenses { get; set; }

    [Column("description_expense")]
    public string Description { get; set; } = "";

    [Column("category_expense")]
    public CategoryExpense IdCategory { get; set; }
    public CategoryExpense CategoryeExpense { get; set; }

    public Expense() => IdExpense = Guid.NewGuid();
}
