using Loto3000Application.Dto.GameDto;
using Loto3000Application.Dto.TicketDto;

namespace Loto3000Application.Services
{
    public interface IGameService
    {
        public Task<CreateGameDto> CreateGameFromAdmin(int adminId);
        public Task<IEnumerable<WinningTicketDto>> WinningPrizesFromUsers(int adminId);
    }
}
