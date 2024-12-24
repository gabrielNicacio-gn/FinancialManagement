using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Domain.Models
{
    [Table("objective")]
    public class Objective
    {
        [Column("id_objective")]
        public Guid IdObjective { get; set; }

        [Column("title_objective")]
        public string Title { get; set; } = "";

        [Column("value_needed")]
        public decimal ValueNeeded { get; set; }

        [Column("date_limit")]
        public DateTime DateLimit { get; set; }

        [Column("status_objective")]
        public string Status { get; set; } = "";

        [Column("description_objective")]
        public string Description { get; set; } = "";

        public Objective() => IdObjective = Guid.NewGuid();

    }
}