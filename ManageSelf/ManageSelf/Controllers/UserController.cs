using Common;
using Data;
using ManageSelf.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ManageSelf.Controllers
{
    public class UserController : Controller
    {
        UserData UserData = new UserData();
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="returnUrl">返回Url</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserData.Find(n => n.Username == loginViewModel.UserName);
                if (user == null) ModelState.AddModelError("UserName", "用户名不存在");
                else if (user.Password == Security.Sha256(loginViewModel.Password))
                {
                    user.LoginTime = System.DateTime.Now;
                    user.LoginIP = Request.UserHostAddress;
                    UserData.Update(user);

                    var _identity = UserData.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = loginViewModel.RememberMe }, _identity);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("Password", "密码错误");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (UserData.Exist(n => n.Username == register.UserName)) ModelState.AddModelError("UserName", "用户名已存在");
                else
                {
                    User user = new User()
                    {
                        Username = register.UserName,
                        //默认用户组代码写这里
                        DisplayName = register.DisplayName,
                        Password = Security.Sha256(register.Password),
                        //邮箱验证与邮箱唯一性问题
                        Email = register.Email,
                        RegistrationTime = System.DateTime.Now
                    };
                    user = UserData.Add(user);
                    if (user.Id > 0)
                    {
                        RoleData roleData = new Data.RoleData();
                        var role = roleData.Find(n => n.Name == "普通用户");
                        user.Role.Add(role);
                        UserData.Save();
                        var _identity = UserData.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignIn(_identity);
                        return RedirectToAction("Index", "Home");
                    }
                    else { ModelState.AddModelError("", "注册失败！"); }
                }
            }
            return View(register);
        }

        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
    }
}