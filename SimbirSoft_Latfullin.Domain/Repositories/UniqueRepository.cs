using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimbirSoft_Latfullin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbirSoft_Latfullin.Domain.Repositories
{
    public class UniqueRepository : IRepository<UniqueResult>
    {
        private readonly ApplicationContext _db;

        public UniqueRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<UniqueResult> Add(UniqueResult entity)
        {
            await _db.UniqueResults.AddAsync(entity);
            await _db.SaveChangesAsync();
            return await GetByUri(entity.Uri);
        }

        public async Task<UniqueResult> Delete(UniqueResult entity)
        {
            _db.UniqueResults.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<UniqueResult> Delete(string id)
        {
            var result = await Get(id);
            _db.UniqueResults.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }

        public async Task<UniqueResult> Get(string id)
        {
            var result = await _db.UniqueResults.FirstAsync(x => x.Id == id);
            return result;
        }

        public async Task<UniqueResult> GetByUri(string uri)
        {
            var result = await _db.UniqueResults.FirstAsync(x => x.Uri == uri);
            return result;
        }

        public async Task<UniqueResult> Update(UniqueResult entity)
        {
            _db.UniqueResults.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AnyUri(string uri)
        {
            return await _db.UniqueResults.AnyAsync(x => x.Uri == uri);
        }
    }
}
