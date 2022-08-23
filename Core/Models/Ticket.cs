using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Models.ValidationAttributes;
namespace Core.Models
{
    public class Ticket
    {
        public int? TicketId { get; set; }
        [Required]
        public int? ProjectId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string Owner { get; set; }
        [Ticket_EnsureReportDatePresent]
        public DateTime? ReportDate { get; set; }
        [Ticket_EnsureDueDatePresent]
        public DateTime? DueDate { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// when creating a ticket, if due date is entered, it has to be in the future.
        /// </summary>
        public bool ValidateFutureDueDate()
        {
            if (TicketId.HasValue) return true;
            if (!DueDate.HasValue) return true;

            return (DueDate.Value > DateTime.Now);
        }
        /// <summary>
        /// when owner is assign to the ticket, the due date has to be present
        /// </summary>
        public bool ValidateReportDatePresent()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return ReportDate.HasValue;
        }

        /// <summary>
        /// when due date and report date are present, due date should be equal to or greater than report date
        /// </summary>
        public bool ValidateDueDatePresent()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return DueDate.HasValue;
        }
        public bool ValidateDueDateAfterReportDate()
        {
            if (!DueDate.HasValue || !ReportDate.HasValue) return true;

            return DueDate.Value.Date >= ReportDate.Value.Date;
        }
    }
}
