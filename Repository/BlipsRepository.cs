using DbModels;
using Microsoft.EntityFrameworkCore;
using Provider;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class BlipsRepository : IRepository<Blips>
    {
        private readonly RageDbContext _context;

        public BlipsRepository(RageDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Blips entity)
        {
            await _context.Blips.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Blips> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Blips>> GetValuesAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Blips>> GetValuesByTypeAsync(int typeId)
        {
            return await _context.Blips.Where(x => x.TypeId == typeId).ToListAsync();
        }

        public Task<bool> UpdateAsync(Blips entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
