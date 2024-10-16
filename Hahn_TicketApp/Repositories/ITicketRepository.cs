using TicketApp.Models;

namespace TicketApp.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllTickets(int pageNumber, int pageSize);
        Task<Ticket> GetTicketById(int id);
        Task AddTicket(Ticket ticket);
        Task UpdateTicket(Ticket ticket);
        Task DeleteTicket(int id);
    }
}
