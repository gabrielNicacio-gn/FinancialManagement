using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Request
{
    public class UpdateExpenseDto
    {
        [Required(ErrorMessage = "The field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "The field is required.")]
        [DataType(DataType.DateTime)]
        public DateTime DateExpenses { get; set; }
        [Required(ErrorMessage = "The field is required.")]
        [StringLength(200, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 3)]
        public string Description { get; set; } = "";
    }
}