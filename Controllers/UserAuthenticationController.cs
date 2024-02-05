using Microsoft.AspNetCore.Mvc;
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
      


        public IActionResult Registration()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
         
             if (!ModelState.IsValid)
                  return View(model);

              

              var result = await authService.RegisterAsync(model);
            if (result.StatusCode == 1)
            {
                TempData["SuccessMsg"] = "Register Successfully....";
                return RedirectToAction("Login", "UserAuthentication");
            }
            else
            {
                TempData["ErrorMsg"] = "Failed To Register...";
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
            {
                TempData["SuccessMsg"] = "Logged In....";
              
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMsg"] = "Invalid Credentials...";
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
