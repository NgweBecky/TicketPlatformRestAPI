using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.ValidationAttributes
{
    public class Ticket_EnsureDueDateAfterReportDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if (!ticket.ValidateDueDateAfterReportDate())
            {
                return new ValidationResult("Due date has to be after the report date.");
            }
            return ValidationResult.Success;
        }
    }
}
