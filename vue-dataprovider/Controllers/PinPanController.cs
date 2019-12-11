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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace dataprovider.Controllers
{
    /// <summary>
    /// 品牌API
    /// </summary>
    [EnableCors("Domain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PinPanController : ControllerBase
    {
        private MyDataDBContext _context;

        public PinPanController(MyDataDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 获取全部品牌列表
        /// </summary>
        /// <returns>The pplb.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //OK 返回的是JsonResult
            try
            {
                var pplbs = await _context.TbBsPplb.ToListAsync();
                if (pplbs != null && pplbs.Count > 0)
                {
                    return Ok(pplbs);
                    //return new JsonResultModel { status = 0, data = pplbs, msg = "获取品牌列表成功" };
                }
                return NotFound();
                //return new JsonResultModel { status = 1, data = "", msg = "获取品牌列表失败" };
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 添加品牌列表
        /// </summary>
        /// <param name="pplb"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromForm]TbBsPplb pplb)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                pplb.Ctime = DateTime.Now;
                _context.TbBsPplb.Add(pplb);
                var res = await _context.SaveChangesAsync();
                if (res == 1)
                {
                    return Ok();
                }
                return StatusCode(500, "添加失败");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 删除品牌列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pplb = await _context.TbBsPplb.FirstOrDefaultAsync(s => s.Id == id);
                if (pplb != null)
                {
                    _context.TbBsPplb.Remove(pplb);
                    var res = await _context.SaveChangesAsync();
                    if (res == 1)
                    {
                        return Ok();
                    }
                }
                return StatusCode(500, "删除失败");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
