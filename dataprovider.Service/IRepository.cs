//
//  IRepository.cs
//
//  Author:
//       zhanghuqiang <1169071140@qq.com>
//
//  Copyright (c) 2019 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dataprovider.Service
{
  public interface IRepository<T> where T : class
  {
    Task<IEnumerable<T>> GetAll();
  }
}
