using TicketApp.Exceptions;
using TicketApp.Models;
using TicketApp.Repositories;

namespace TicketApp.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> GetTickets(int pageNumber, int pageSize)
        {
            try
            {
                return await _ticketRepository.GetAllTickets(pageNumber,pageSize);
            }
            catch (AppException ex)
            {
                throw new AppException("Service error: " + ex.Message);
            }
        }

        public async Task<Ticket> GetTicket(int id)
        {
            try
            {
                return await _ticketRepository.GetTicketById(id);
            }
            catch (AppException ex)
            {
                throw new AppException("Service error: " + ex.Message);
            }
        }

        public async Task CreateTicket(Ticket ticket)
        {
            try
            {
                await _ticketRepository.AddTicket(ticket);
            }
            catch (AppException ex)
            {
                throw new AppException("Service error: " + ex.Message);
            }
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            try
            {
                await _ticketRepository.UpdateTicket(ticket);
            }
            catch (AppException ex)
            {
                throw new AppException("Service error: " + ex.Message);
            }
        }

        public async Task DeleteTicket(int id)
        {
            try
            {
                await _ticketRepository.DeleteTicket(id);
            }
            catch (AppException ex)
            {
                throw new AppException("Service error: " + ex.Message);
            }
        }
    }
}
