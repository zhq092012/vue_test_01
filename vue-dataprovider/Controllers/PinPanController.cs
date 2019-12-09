//
//  PinPanController.cs
//
//  Author:
//       zhanghuqiang <1169071140@qq.com>
//
//  Copyright (c) 2019 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dataprovider.EF;
using Microsoft.AspNetCore.Mvc;

namespace dataprovider.Controllers
{

    [Route("api/[controller]")]
    public class PinPanController : ControllerBase
    {
        private MyDataDBContext _context;

        public PinPanController(MyDataDBContext context)
        {
            _context = context;
        }

        public IEnumerable<TbBsPplb> Get()
        {
            return _context.TbBsPplb.ToList();
        }
    }
}
