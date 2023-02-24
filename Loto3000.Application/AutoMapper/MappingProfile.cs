using AutoMapper;
using Loto3000.Domain.Models;
using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.DrawDto;
using Loto3000Application.Dto.GameDto;
using Loto3000Application.Dto.NewFolder;
using Loto3000Application.Dto.TicketDto;
using Loto3000Application.Dto.UserDto;

namespace Loto3000.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Admin
            CreateMap<AdminDto, Admin>().ReverseMap();
            CreateMap<AdminLoginDto, Admin>().ReverseMap();
            CreateMap<ChangePassAdmin, Admin>().ReverseMap();
            CreateMap<CreateAdminDto, Admin>().ReverseMap();
            
            //DrowDto
            CreateMap<DrowDto, Draw>().ReverseMap();
            //Game
            CreateMap<CreateGameDto, Game>().ReverseMap();
            //Ticket
            CreateMap<CreateCombinationModel, Ticket>().ReverseMap();
            CreateMap<TicketDto, Ticket>().ReverseMap();
            CreateMap<WinningTicketDto, Ticket>().ReverseMap();
            //User
            CreateMap<ChangePasswordDto, User>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap(); 
            CreateMap<LoginUserDto, User>().ReverseMap();
            CreateMap<TokenDto, User>().ReverseMap(); 
            CreateMap<UpdateUserDto, User>().ReverseMap(); 
            CreateMap<UserBuyCreditsDto, User>().ReverseMap(); 
            CreateMap<UserDto, User>().ReverseMap();
          



        }
    }
}
