using System;
using System.Linq;
using System.Threading.Tasks;
using dataprovider.Models;
using dataprovider.Service;
using dataprovider.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace dataprovider.Web.Controllers
{
  public class StudentController : Controller
  {
    private readonly IRepository<Student> _repository;
    public StudentController(IRepository<Student> repository)
    {
      _repository = repository;
    }
    [HttpGet]
    public IActionResult Index()
    {
      var students = _repository.GetAll().Select(s => new StudentViewModel
      {
        Id = s.Id,
        Name = s.Name,
        Gender = Enum.GetName(typeof(GenderEnum), s.Gender),
        Age = DateTime.Now.Subtract(s.Birthday).Days / 356

      });
      return View(students);
    }
    [HttpGet]
    public IActionResult Detail(int id)
    {
      var stu = _repository.GetById(id);
      var stuViewModel = new StudentViewModel();
      if (stu != null)
      {
        stuViewModel.Id = stu.Id;
        stuViewModel.Name = stu.Name;
        stuViewModel.Age = DateTime.Now.Subtract(stu.Birthday).Days / 356;
        stuViewModel.Gender = Enum.GetName(typeof(GenderEnum), stu.Gender);
      }

      return View(stuViewModel);
    }
    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }
    /// <summary>
    /// 添加学生（注意：Post请求必须加 [ValidateAntiForgeryToken]验证，防止跨域攻击）
    /// </summary>
    /// <param name="student"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm]Student student)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return View();
        }
        var boolRes = _repository.Add(student, true);
        if (boolRes)
        {
          return RedirectToAction(nameof(Index));//便于重构
        }
        return StatusCode(500, "添加失败");
      }
      catch (Exception ex)
      {

        return StatusCode(500, ex.Message);
      }

    }
  }
}