using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Shared.Models
{
    public class BudgetType : BudgetModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The field Name must be set")]
        [MaxLength(50, ErrorMessage = "The Name length must be less than 50 characters")]
        public string Name { get; set; }
    }
}
