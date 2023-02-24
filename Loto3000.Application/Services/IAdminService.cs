using Loto3000Application.Dto.AdminDto;
using Loto3000Application.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000Application.Services
{
    public interface IAdminService
    {
        public Task<AdminDto> CreateAdmin(CreateAdminDto dto);
        public Task ChangeAdminPassword(ChangePassAdmin model, int id);
        public Task DeleteAdmin(int id);
        public TokenDto AuthenticateAdmin(AdminLoginDto adminDto);


    }
}
