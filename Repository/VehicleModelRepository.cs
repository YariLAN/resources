using DbModels;
using Microsoft.EntityFrameworkCore;
using Provider;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class VehicleModelRepository : IRepository<VehicleModel>
    {
        private readonly RageDbContext _provider;

        public VehicleModelRepository()
        {
            _provider = new RageDbContext();
        }

        public async Task<int> CreateAsync(VehicleModel entity)
        {
            _provider.VehicleModels.Add(entity);

            await _provider.SaveChangesAsync();

            return entity.Id;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<VehicleModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<VehicleModel>> GetValuesAsync()
        {
            return  await _provider.VehicleModels
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<VehicleModel>> GetVehiclesByOwnerIdAsync(int ownerId)
        {
            return await _provider.VehicleModels
                .AsNoTracking()
                .Where(x => x.OwnerID == ownerId)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(VehicleModel entity)
        {
            _provider.VehicleModels.Update(entity);

            await _provider.SaveChangesAsync();

            return true;
        }
    }
}
