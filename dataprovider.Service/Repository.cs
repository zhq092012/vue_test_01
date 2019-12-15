//
//  Repository.cs
//
//  Author:
//       zhanghuqiang <1169071140@qq.com>
//
//  Copyright (c) 2019 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dataprovider.Models;
using Microsoft.EntityFrameworkCore;

namespace dataprovider.Service
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private ProvContext context;
    private readonly DbSet<T> dbSet;
    public Repository(ProvContext _context)
    {
      context = _context;
      dbSet = context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
      return dbSet;
    }

    public T GetById(int id)
    {
      return dbSet.Find(id);
    }

    public bool Add(T entity, bool isSave = false)
    {
      dbSet.Add(entity);
      if (isSave)
      {
        return context.SaveChanges() > 0;
      }
      return false;

    }
  }

}
