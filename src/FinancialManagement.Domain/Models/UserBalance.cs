using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Domain.Models
{
    [Table("user_balance")]
    public class UserBalance
    {
        [Column("id_user_balance")]
        public Guid IdUserBalance { get; set; }

        [Column("value_balance")]
        public decimal ValueBalance { get; set; }
    }
}