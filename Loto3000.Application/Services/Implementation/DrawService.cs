using AutoMapper;
using Loto3000.Domain.Models;
using Loto3000Application.Dto.DrawDto;
using Loto3000Application.Exeption;
using Loto3000Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loto3000Application.Services.Implementation
{
    public class DrawService : IDrawService
    {
        private readonly IRepository<Draw> drowRepository;
        private readonly IRepository<Admin> adminRepository;
        private readonly IPasswordHasher passordHasher;
        private readonly IMapper mapper;
        public DrawService(IRepository<Draw> drowRepository, IRepository<Admin> adminRepository, IPasswordHasher passordHasher, IMapper mapper)
        {
            this.drowRepository = drowRepository;
            this.adminRepository = adminRepository;
            this.passordHasher = passordHasher;
            this.mapper = mapper;
        }

        public async Task<DrowDto> CreateDrowFromAdmin(int id)
        {
            var admin = await adminRepository.GetByID(id) ?? throw new NotFoundException("Admin Doesn't exist");

            var drawActive = await drowRepository.GetAll().FirstOrDefaultAsync(x => x.IsActive == true);
            // Proverka dali ima aktiven draw
            var activeDraw = GetActiveDraw();

            // Ako ima  -  Exception
            if (drawActive != null)
                 throw new NotFoundException("There is already an active draw!");

            // Ako nema aktiven draw
            var draw = new Draw(admin);
            
            await drowRepository.Create(draw);
            admin.Draw.Add(draw);
            await adminRepository.Update(admin);
            return mapper.Map<DrowDto>(draw);         }

        public async Task<IEnumerable<DrowDto>> GetAllDrow(int id)
        {
            var admin = await adminRepository.GetByID(id) ?? throw new NotFoundException("Admin Doesn't exist");
            return await drowRepository.GetAll().Select(x => mapper.Map<DrowDto>(x)).ToListAsync();
        }

        public async Task<DrowDto> DeleteActiveDrow(int id)
        {
            var admin = await adminRepository.GetByID(id) ?? throw new NotFoundException("Admin Doesn't exist");

            var drawActive = await drowRepository.GetAll().FirstOrDefaultAsync(x => x.IsActive == true);

            if (drawActive == null)
                throw new NotFoundException("There is no already an active draw!");
            await drowRepository.Delete(drawActive);
            return mapper.Map<DrowDto>(drawActive);
        }

       
        private async Task<Draw?> GetActiveDraw()
        {
            return await drowRepository.GetAll().Where(x => x.StartGame <= DateTime.Now && x.EndGame >= DateTime.Now).FirstOrDefaultAsync();
        }
    }
}
