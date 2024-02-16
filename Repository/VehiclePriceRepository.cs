using DbModels;
using Microsoft.EntityFrameworkCore;
using Provider;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class VehiclePriceRepository : IRepository<VehiclePrice>
    {
        private readonly RageDbContext _provider;

        public VehiclePriceRepository()
        {
            _provider = new RageDbContext();
        }

        public async Task<int> CreateAsync(VehiclePrice entity)
        {
            _provider.VehiclePrice.Add(entity);

            await _provider.SaveChangesAsync();

            return entity.Id;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<VehiclePrice> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<VehiclePrice>> GetValuesAsync()
        {
            var list = await _provider.VehiclePrice.AsNoTracking().ToListAsync();

            return list;
        }

        public Task<bool> UpdateAsync(VehiclePrice entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
