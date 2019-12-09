//
//  PinPanController.cs
//
//  Author:
//       zhanghuqiang <1169071140@qq.com>
//
//  Copyright (c) 2019 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using dataprovider.EF;
using dataprovider.Models;
using Newtonsoft.Json;

namespace dataprovider.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PinPanController : ControllerBase
    {
        private MyDataDBContext _context;

        public PinPanController(MyDataDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<JsonResultModel> GetPPLB()
        {

            var data = _context.TbBsPplb.ToList();

            return new JsonResultModel { status = 1, data = JsonConvert.SerializeObject(data), msg = "" };
        }
    }
}
