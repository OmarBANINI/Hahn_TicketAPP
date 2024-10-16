using System.ComponentModel.DataAnnotations;

namespace TicketApp.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
