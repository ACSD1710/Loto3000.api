using Loto3000Application.Dto.UserDto;


namespace Loto3000Application.Services
{
    public interface IUserService
    {
        public Task<UserDto> CreateUser(CreateUserDto user);
        public Task ChangeUserPassword(ChangePasswordDto model, int id);
        public Task DeleteUser(int id);
        public Task UpdateUser(UpdateUserDto model, int id);
        public Task<UserDto> GetUserInfo(int id);
        public Task<string> BuyCreditsFromUser(UserBuyCreditsDto model, int id);
        public string UserLogin(LoginUserDto model);
        public TokenDto AuthenticateUser(LoginUserDto model);
    }
}
