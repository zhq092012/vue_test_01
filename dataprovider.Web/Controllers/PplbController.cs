using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dataprovider.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dataprovider.Web.Controllers
{
  public class PplbController : Controller
  {
    private ProvContext ProvContext;

    public PplbController(ProvContext provContext)
    {
      ProvContext = provContext;
    }

    // GET: /<controller>/
    public IActionResult Index()
    {
      var pplb = ProvContext.TbBsPplb.ToList();
      return View(pplb);
    }
  }
}
