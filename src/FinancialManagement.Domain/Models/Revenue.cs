using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Domain.Models;

[Table("revenue")]
public class Revenue
{
    [Column("id_revenue")]
    public Guid IdRevenue { get; set; }

    [Column("value_revenue")]
    public decimal Value { get; set; }

    [Column("date_revenue")]
    public DateTime DateRevenue { get; set; }

    [Column("description_revenue")]
    public string Description { get; set; } = "";

    [Column("category_revenue")]
    public string Category { get; set; } = "";

    public Revenue() => IdRevenue = Guid.NewGuid();
}
