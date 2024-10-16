using TicketApp.Models;

namespace TicketApp.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetTickets(int pageNumber, int pageSize);
        Task<Ticket> GetTicket(int id);
        Task CreateTicket(Ticket ticket);
        Task UpdateTicket(Ticket ticket);
        Task DeleteTicket(int id);
    }
}
