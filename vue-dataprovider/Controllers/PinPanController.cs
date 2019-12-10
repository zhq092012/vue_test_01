//
//  PinPanController.cs
//
//  Author:
//       zhanghuqiang <1169071140@qq.com>
//
//  Copyright (c) 2019 
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dataprovider.EF;
using dataprovider.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace dataprovider.Controllers
{

    [EnableCors("Domain")]
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class PinPanController : ControllerBase
    {
        private MyDataDBContext _context;

        public PinPanController(MyDataDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets the pplb.
        /// </summary>
        /// <returns>The pplb.</returns>
        [HttpGet]
        public ActionResult<JsonResultModel> GetPPLB()
        {

            var pplbs = _context.TbBsPplb.ToList();
            if (pplbs != null && pplbs.Count > 0)
            {
                return new JsonResultModel { status = 0, data = pplbs, msg = "获取品牌列表成功" };
            }
            else
            {
                return new JsonResultModel { status = 1, data = "", msg = "获取品牌列表失败" };
            }

        }
    }
}
