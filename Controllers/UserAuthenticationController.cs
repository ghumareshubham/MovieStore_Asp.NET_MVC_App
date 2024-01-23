﻿using Microsoft.AspNetCore.Mvc;
using MovieStoreMvc.Models.DTO;
using MovieStoreMvc.Repositories.Abstract;

namespace MovieStoreMvc.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }
        /* We will create a user with admin rights, after that we are going
          to comment this method because we need only
          one user in this application 
          If you need other users ,you can implement this registration method with view
          I have create a complete tutorial for this, you can check the link in description box
         */


        public IActionResult Registration()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            /*    var model1 = new RegistrationModel
                {
                    Email = "admin@gmail.com",
                    Username = "admin",
                    Name = "Shubham",
                    Password = "Admin@123",
                    PasswordConfirm = "Admin@123",
                    Role = "Admin"
                };
                //if you want to register with user , Change Role = "User"
                var result = await authService.RegisterAsync(model1);
                return Ok(result.Message);*/


             if (!ModelState.IsValid)
                  return View(model);

              

              var result = await authService.RegisterAsync(model);
              if (result.StatusCode == 1)
                  return RedirectToAction("Index", "Home");
              else
              {
                  TempData["msg"] = "Could not Register..";
                  return RedirectToAction(nameof(Login));
              }
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not logged in..";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
           await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}
