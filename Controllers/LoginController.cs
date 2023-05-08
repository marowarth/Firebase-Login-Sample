using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder.Extensions;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Net;
using Firebase.Utils;
using System.Text.Json.Nodes;
using System;
// Configura Firebase con tus credenciales de Google


namespace FirebaseLoginCustom.Controllers
{

    public class LoginController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {

            FirebaseAuthConfig config = new FirebaseAuthConfig();
            config.AuthDomain = "loginmvc-d6c97.firebaseapp.com";
            config.ApiKey = "AIzaSyAL4Yl_10YN7BfsVfgYCnp8Sf4mT7LKLUs";
            config.Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                };
            Firebase.Auth.FirebaseAuthClient authClient = new FirebaseAuthClient(config);

            try
            {
                var user = await authClient.SignInWithEmailAndPasswordAsync(username, password);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Usuario")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                ViewBag.Error = "Credenciales inválidas";
                return View();

            }
        }
        public async Task<IActionResult> Login(string user)
        {
            try
            {
                GoogleLoginObject googleLoginObject= Newtonsoft.Json.JsonConvert.DeserializeObject < GoogleLoginObject > (user);
                // Verifica las credenciales de autenticación con Firebase
                var firebaseToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(googleLoginObject.stsTokenManager.accessToken);

                // Maneja el inicio de sesión exitoso
                var userId = firebaseToken.Uid;
                // Hacer algo aquí con el ID del usuario autenticado
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, googleLoginObject.displayName),
                    new Claim(ClaimTypes.Role, "Usuario")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(principal);
                string url = Url.Action("Index", "Home");
                return Json(new { data = url });
            }
            catch (Firebase.Auth.FirebaseAuthException e)
            {
               return Json(new { data = "Credenciales inválidas" });
            }

        }
    }
}
