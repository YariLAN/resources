using DbModels;
using Microsoft.EntityFrameworkCore;
using Provider;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfRepositoryBlips
    {
        private readonly BlipsRepository _blipsRepository;

        private readonly TypesBlipsRepository _typesBlipsRepository;

        public UnitOfRepositoryBlips(RageDbContext context) 
        {
            _blipsRepository = new BlipsRepository(context);
            _typesBlipsRepository = new TypesBlipsRepository(context);  
        }

        public async Task<int> CreateAsync(TypesBlips type, Blips blip)
        {
            var id = await _typesBlipsRepository.CreateAsync(type);

            blip.TypeId = id;

            await _blipsRepository.CreateAsync(blip);

            return blip.Id;
        }

        public async Task<List<double[]>> GetBlipsAsync(int typeId)
        {
            return (await _blipsRepository.GetValuesByTypeAsync(typeId))
                .Select(x => x.Position)
                .ToList();
        }
    }

    public class TypesBlipsRepository : IRepository<TypesBlips>
    {
        private readonly RageDbContext _context;

        public TypesBlipsRepository(RageDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(TypesBlips entity)
        {
            var type = await _context.TypesBlips.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (type != null)
            {
                return entity.Id;
            }

            await _context.TypesBlips.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<TypesBlips> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TypesBlips>> GetValuesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(TypesBlips entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
