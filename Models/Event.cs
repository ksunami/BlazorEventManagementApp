using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorEventManagementApp.Models
{
     public class Event
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string EventName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required, StringLength(100)]
        public string Location { get; set; } = string.Empty;
    }
}