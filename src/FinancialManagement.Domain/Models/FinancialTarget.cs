using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Domain.Enums;

namespace FinancialManagement.Domain.Models;

[Table("financial_target")]
public class FinancialTarget
{
    [Column("id_financial_target")]
    public Guid IdFinancialTarget { get; set; }

    [Column("title_target")]
    public string Title { get; set; } = "";

    [Column("value_needed")]
    public decimal ValueNeeded { get; set; }

    [Column("date_limit")]
    public DateTime DateLimit { get; set; }

    [Column("status_target")]
    public StatusFinancialTarget Status { get; set; }

    [Column("description_target")]
    public string Description { get; set; } = "";

    public FinancialTarget() => IdFinancialTarget = Guid.NewGuid();

}
