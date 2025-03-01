﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL.Infrastructure.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}