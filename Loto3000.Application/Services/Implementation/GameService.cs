using AutoMapper;
using Loto3000.Domain.Enums;
using Loto3000.Domain.Helprers;
using Loto3000.Domain.Models;

using Loto3000Application.Dto.GameDto;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Exeption;
using Loto3000Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loto3000Application.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<Draw> drawRepository;
        private readonly IRepository<Game> gameRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IMapper mapper;


        public GameService(IRepository<Admin> adminRepository, IRepository<Draw> drawRepository,
                                            IRepository<Game> gameRepository, IRepository<Ticket> ticketRepository, IMapper mapper)
        {
            this.adminRepository = adminRepository;
            this.drawRepository = drawRepository;
            this.gameRepository = gameRepository;
            this.ticketRepository = ticketRepository;
            this.mapper = mapper;
        }

        public async Task<CreateGameDto> CreateGameFromAdmin(int adminId)
        {
            var admin = await adminRepository.GetByID(adminId);
            if (admin is null)
            {
                throw new NotFoundException("Administrator doesn't Exist");
            };

            var drawActive = await drawRepository.GetAll().FirstOrDefaultAsync(x => x.IsActive == true);
           
            if (drawActive == null)
            {
                throw new ArgumentLotoExeption("There is no already an active draw!");
            }

            if (DateTime.Now <= drawActive.EndGame)
            {
                throw new ArgumentLotoExeption("The drow is not active please try letter");
            }

            var gameActive = gameRepository.GetAll().FirstOrDefault(x => x.IsActive == true);
           
            if (gameActive != null)
            {
                throw new ArgumentLotoExeption("There is already an active Game!");
            }

            var game = new Game(drawActive, admin);
            admin.Game.Add(game);  
            await gameRepository.Create(game);
            await adminRepository.Update(admin);
            return mapper.Map<CreateGameDto>(game);

        }

        public async Task<IEnumerable<WinningTicketDto>> WinningPrizesFromUsers(int adminId)
        {
            var admin = await adminRepository.GetByID(adminId);
            if (admin is null)
            {
                throw new NotFoundException("Administrator doesn't Exist");
            };
            var gameActive = await gameRepository.GetAll().FirstOrDefaultAsync(x => x.IsActive == true);
            var drawActive = await drawRepository.GetAll().FirstOrDefaultAsync(x => x.IsActive == true);

            if (gameActive != null)
            {
                throw new ArgumentLotoExeption("There is already an active Game!");
            }

            var tickets = await ticketRepository.GetAll().ToListAsync();

            var ticketCombinations = tickets.Select(x => x.CombinationNumbers.StringToIntList()).ToList();

            var gameNumbers = gameRepository.GetAll().FirstOrDefault(x => x.IsActive == true)!.GameNumbers;

            var gameCombinations = gameNumbers.StringToIntList().ToList();

            foreach (var ticket in tickets)
            {
                
                int iterator = 0;
                var combination = ticket.CombinationNumbers.StringToIntList();
                foreach(var gcom in gameCombinations)
                {
                    foreach(var tcom in combination)
                    {
                        if(tcom == gcom)
                        {
                            ++iterator;
                        }
                    }

                }
                switch (iterator)
                {
                    case 3:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.GiftCard_50;
                        
                        break;
                    case 4:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.GiftCard_100;
                        
                        break;
                    case 5:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.TV;
                        
                        break;
                    case 6:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.Vacation;
                        
                        break;
                    case 7:
                        ticket.HasPrice = true;
                        ticket.Prize = TicketPrize.Car;
                        
                        break;

                }
                
                await ticketRepository.Update(ticket);
            }
            gameActive!.IsActive = false;
            await gameRepository.Update(gameActive);
            drawActive!.IsActive = false;
            await drawRepository.Update(drawActive);

            var wintickets = await ticketRepository.GetAll().Where(x => x.IsActive == true)
                                    .Where(y => y.HasPrice == true).ToListAsync();
            var winnigTickets = wintickets.Select(z => mapper.Map<WinningTicketDto>(z)).ToList();
            foreach (var ticket in tickets)
            {
                ticket.IsActive = false;
                await ticketRepository.Update(ticket);
            }
            return winnigTickets;
        }
    }
}
