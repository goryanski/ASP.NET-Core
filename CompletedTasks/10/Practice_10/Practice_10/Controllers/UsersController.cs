using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice_10.Business.Interfaces;
using Practice_10.Models.Users;

namespace Practice_10.Controllers
{
    public class UsersController : Controller
    {
        // for validation on backend side I've used UsersService, because for this type validation I need to use DB, or some storage

        IUsersService usersService;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginUserViewModel user)
        {
            if(usersService.UserNotFound(user.Login))
            {
                ModelState.AddModelError("Login", "User Not Found");
                return View(user);
            }
            else
            {
                if (user.PasswordHasBeenForgotten)
                {
                    TempData["userLogin"] = user.Login;
                    return RedirectToAction("RestoreAccess");
                }

                if (usersService.IncorrectUserPassword(user.Login, user.Password))
                {
                    ModelState.AddModelError("Password", "Incorrect User Password");
                }

                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                TempData["userLogin"] = user.Login;
                return RedirectToAction("SuccessPage");
            }
        }



        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(NewUserViewModel user)
        {
            if(!usersService.IsUserLoginUnique(user.Login))
            {
                ModelState.AddModelError("Login", "Your login is not unique");
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                usersService.AddNewUser(user);
                return RedirectToAction("Index", "Home");
            }
        }



        public IActionResult RestoreAccess()
        {
            string userLogin = TempData["userLogin"] as string;
            return View(new RestoreUserViewModel
            { 
                Login = userLogin,
                SecretQuestion = usersService.GetSecretQuestion(userLogin)
            });
        }
        [HttpPost]
        public IActionResult RestoreAccess(RestoreUserViewModel user)
        {
            if(usersService.IsUserAnswerRight(user.Login, user.SecretQuestionAnswer))
            {
                TempData["userLogin"] = user.Login;
                return RedirectToAction("SuccessPage");
            }
            else
            {
                ModelState.AddModelError("SecretQuestionAnswer", "The answer isn't right");
                return View(user);
            }
        }



        public IActionResult SuccessPage()
        {
            ViewData["userLogin"] = TempData["userLogin"] as string;
            return View();
        }
    }
}
