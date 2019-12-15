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
using dataprovider.Models;

namespace dataprovider.Service
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> GetAll();

    T GetById(int id);

    bool Add(T student, bool isSave);
  }
}
