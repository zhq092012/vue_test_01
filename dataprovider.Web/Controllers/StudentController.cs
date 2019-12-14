using System;
using System.Linq;
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

    public IActionResult Index()
    {
      var students = _repository.GetAll().Result.Select(s => new StudentModel
      {
        Name = s.Name,
        Age = DateTime.Now.Subtract(s.Birthday).Days / 356
      });
      return View(students);
    }
  }
}