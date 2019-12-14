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
    public Repository(ProvContext _context)
    {
      context = _context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
      return await context.Set<T>().ToListAsync();
    }
  }

}
