using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Request.CategoryExpense
{
    public class UpdateCategoryExpenseDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "The field is required.")]
        public string NamePropertyToBeUpdate { get; set; } = "";
    }
}