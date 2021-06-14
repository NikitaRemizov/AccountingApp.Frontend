using System;
using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Shared.Models
{
    public class BudgetChange : BudgetModel
    {
        [Required(ErrorMessage = "The field Date must be set")]
        [DataType(DataType.Date, ErrorMessage = "The provided date format is invalid")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "The field Amount must be set")]
        public long Amount { get; set; }
        [Required(ErrorMessage = "The field BudgetTypeId must be set")]
        public Guid BudgetTypeId { get; set; }
        public string BudgetTypeName { get; set; }
    }
}
