using Loto3000Application.Dto.NewFolder;
using Loto3000Application.Dto.TicketDto;

namespace Loto3000Application.Services
{
    public interface ITicketService
    {
         
        public Task<TicketDto> CreateTicketFromUser(CreateCombinationModel combination, int userId);
        public Task<IEnumerable<TicketDto>> GetAllTicketFromAdmin(int adminId);
        public Task<IEnumerable<TicketDto>> GetAllActiveTicketsFromAdmin(int adminId);

    }
}
