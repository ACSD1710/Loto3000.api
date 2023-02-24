using AutoMapper;
using Loto3000.Domain.Helprers;
using Loto3000.Domain.Models;
using Loto3000Application.Dto.NewFolder;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Exeption;
using Loto3000Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loto3000Application.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<Draw> drawRepository;
        private readonly IMapper mapper;

        public TicketService(IRepository<User> userRepository, IRepository<Ticket> ticketRepository,
                                                                   IRepository<Draw> drawRepository,
                                                                IRepository<Admin> adminRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.ticketRepository = ticketRepository;
            this.drawRepository = drawRepository;
            this.adminRepository = adminRepository;
            this.mapper = mapper;
        }

        public async Task<TicketDto> CreateTicketFromUser(CreateCombinationModel combination, int userId)
        {
            var user = await userRepository.GetByID(userId);
            if (user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }
            var draw = await drawRepository.GetAll().FirstOrDefaultAsync(x => x.IsActive == true);
            
            if(draw == null)
            {
                throw new NotFoundException("There is not active Draw");
            }

            var combinationArray = combination.Combination!.ValidateCombination();
            var inputCombination = combinationArray.IntListToString();

            Ticket ticket = new Ticket(user, draw, inputCombination);
         
            await ticketRepository.Create(ticket);
            if(user.Credits < 5)
            {
                throw new ArgumentLotoExeption("You don't have enought credit");
            }
            user.Credits -= 5;
            draw.TotalCredits += 5;
            user.Tickets.Add(ticket);
            await drawRepository.Update(draw);
            await userRepository.Update(user);                    
            return mapper.Map<TicketDto>(ticket);

        }    

      public async Task<IEnumerable<TicketDto>> GetAllTicketFromAdmin(int adminId)
        {
            var admin = await adminRepository.GetByID(adminId);            
            if (admin == null)
            {
                throw new NotFoundException("Administrator doesn't exist");
            }
            return await ticketRepository.GetAll().Select(x => mapper.Map<TicketDto>(x)).ToListAsync();
        }

      public async Task<IEnumerable<TicketDto>> GetAllActiveTicketsFromAdmin(int adminId)
        {
            var admin = await userRepository.GetByID(adminId);
            if (admin == null)
            {
                throw new NotFoundException("Administrator doesn't exist");
            }
            return await ticketRepository.GetAll().Where(y => y.IsActive == true).Select(x => mapper.Map<TicketDto>(x)).ToListAsync();
        }

          
    }
}
