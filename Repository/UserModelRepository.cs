using DbModels;
using Microsoft.EntityFrameworkCore;
using Provider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class UserModelRepository : IRepository<UserModel>
    {
        public readonly RageDbContext _provider;

        public UserModelRepository(RageDbContext rageDbContext)
        {
            _provider = rageDbContext;
        }

        public async Task<int> CreateAsync(UserModel entity)
        {
            _provider.UserModels.Add(entity);

            await _provider.SaveChangesAsync();

            return entity.Id;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserModel>> GetValuesAsync()
        {
            return _provider.UserModels.AsNoTracking().ToListAsync();
        }

        public async Task<UserModel> GetUserModelByLoginId(int loginId)
        {
            return await _provider.UserModels
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LoginModelId == loginId);  
        }

        public async Task<bool> UpdateAsync(UserModel entity)
        {
            _provider.UserModels.Update(entity);

            await _provider.SaveChangesAsync();

            return true;
        }
    }
}
