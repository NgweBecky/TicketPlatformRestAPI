using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.ValidationAttributes
{
    public class Ticket_EnsureReportDatePresentAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;
            if (!ticket.ValidateReportDatePresent())
            {
                return new ValidationResult("Report date is required");
            }
            return ValidationResult.Success;
        }
    }
}
