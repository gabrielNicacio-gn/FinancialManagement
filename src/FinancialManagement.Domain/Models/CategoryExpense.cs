using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Domain.Models
{
    [Table("category_expense")]
    public class CategoryExpense
    {
        [Column("id_category_expense")]
        public Guid IdCategory { get; set; }

        [Column("name_category")]
        public string Name { get; set; } = "";

        [Column("icon_category")]
        public string IconUrl { get; set; } = "";
        [Column("user_id")]
        public Guid UserId { get; set; }

        public Expense? Expense { get; set; }

        public CategoryExpense() => IdCategory = Guid.NewGuid();
    }
}