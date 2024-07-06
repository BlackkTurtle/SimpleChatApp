using Microsoft.EntityFrameworkCore;
using SimpleChatApp.DAL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL.Infrastructure;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly SimpleChatDbContext DatabaseContext;
    protected readonly DbSet<TEntity> Table;

    protected GenericRepository(SimpleChatDbContext databaseContext)
    {
        DatabaseContext = databaseContext;
        Table = DatabaseContext.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetAsync() => await Table.ToListAsync();

    public virtual async Task<TEntity> GetByIdAsync(Guid id) => (await Table.FindAsync(id))!;

    public virtual async Task InsertAsync(TEntity entity) => await Table.AddAsync(entity);

    public virtual async Task UpdateAsync(TEntity entity) => await Task.Run(() => Table.Update(entity));

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        await Task.Run(() => Table.Remove(entity));
    }
}