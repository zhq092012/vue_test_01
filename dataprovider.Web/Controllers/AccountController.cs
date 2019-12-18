using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dataprovider.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dataprovider.Web.Controllers
{  public class AccountController : Controller  {    private readonly SignInManager<IdentityUser> signInManager;    public readonly UserManager<IdentityUser> userManager;    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)    {      this.signInManager = signInManager;      this.userManager = userManager;    }    // GET: /<controller>/    public IActionResult Login()    {      return View();    }    [HttpPost]    public async Task<IActionResult> Login(LoginViewModel loginViewModel)    {      if (!ModelState.IsValid)      {        return View(loginViewModel);      }      var user = await userManager.FindByNameAsync(loginViewModel.UserName);      if (user != null)      {        var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);        if (result.Succeeded)        {          return RedirectToAction("Index", "Home");        }      }      ModelState.AddModelError("", "用户名/密码不正确");      return View(loginViewModel);    }    public IActionResult Register()    {      return View();    }    [HttpPost]    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)    {      if (ModelState.IsValid)      {        var user = new IdentityUser        {          UserName = registerViewModel.UserName        };        var result = await userManager.CreateAsync(user, registerViewModel.Password);        if (result.Succeeded)        {          return RedirectToAction("Index", "Home");        }      }      return View(registerViewModel);    }    public async Task<IActionResult> Logout()    {      await signInManager.SignOutAsync();      return RedirectToAction("Index", "Home");    }  }
}
