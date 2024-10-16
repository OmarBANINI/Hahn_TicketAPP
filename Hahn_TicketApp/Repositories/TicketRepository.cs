using TicketApp.Data;
using TicketApp.Models;
using Microsoft.EntityFrameworkCore;
using TicketApp.Exceptions;

namespace TicketApp.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllTickets(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Tickets.ToListAsync();
            }
            catch (AppException ex)
            {
                throw new AppException("An error occurred while retrieving the ticket: " + ex.Message);
            }
            
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            try
            {
                return await _context.Tickets.FindAsync(id);
            }
            catch (AppException ex)
            {
                throw new AppException("An error occurred while retrieving the ticket: " + ex.Message);
            }
            
        }

        public async Task AddTicket(Ticket ticket)
        {
            try
            {
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
            }
            catch (AppException ex)
            {
                throw new AppException("An error occurred while retrieving the ticket: " + ex.Message);
            }
            
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            try
            {
                _context.Entry(ticket).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new AppException("The ticket does not exist.");
            }
            catch (Exception ex)
            {
                throw new AppException("An error occurred while updating the ticket: " + ex.Message);
            }

        }

        public async Task DeleteTicket(int id)
        {
            try
            {
                var ticket = await GetTicketById(id);
                if (ticket == null) throw new AppException("Ticket not found.");

                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AppException("An error occurred while deleting the ticket: " + ex.Message);
            }
        }

    }
}
