using DbModels;
using Microsoft.EntityFrameworkCore;
using Provider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class LoginModelRepository : IRepository<LoginModel>
    {
        public readonly RageDbContext _provider;
        
        public LoginModelRepository(RageDbContext rageDbContext)
        {
            _provider = rageDbContext;
        }

        public async Task<int> CreateAsync(LoginModel entity)
        {
            _provider.LoginModels.Add(entity);

            await _provider.SaveChangesAsync();

            return entity.Id;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LoginModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginModel> GetByUserNameAsync(string username)
        {
            var user = await _provider.LoginModels
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == username);

            return user;
        }

        public Task<List<LoginModel>> GetValuesAsync()
        {
            throw new NotImplementedException();
        }



        public Task<bool> UpdateAsync(LoginModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
