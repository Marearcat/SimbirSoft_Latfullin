using SimbirSoft_Latfullin.Domain.Entities.Abstact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbirSoft_Latfullin.Domain.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        Task<TEntity> Get(string id);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Delete(TEntity entity);

        Task<TEntity> Delete(string id);
    }
}
