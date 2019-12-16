using System;
using System.Linq;
using dataprovider.Models;
using dataprovider.Service;
using Microsoft.AspNetCore.Mvc;

namespace dataprovider.Web.ViewComponents
{
    /// <summary>
    /// 添加视图组件，相当与一个小型的MVC
    /// </summary>
    public class WelcomeViewComponent:ViewComponent
    {
        private readonly IRepository<Student> repository;

        public WelcomeViewComponent(IRepository<Student> repository)
        {
            this.repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            var count = repository.GetAll().Count();
            return View("Default",count.ToString());
        }
    }
}
