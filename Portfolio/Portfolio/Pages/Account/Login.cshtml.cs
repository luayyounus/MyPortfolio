using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ProjectDbContext _context;
        public LoginModel(ProjectDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User CurrentUser { get; set; }

        public IActionResult OnPost()
        {
            // Checking email and password if exist in the database
            var emailAddress = _context.Users.Any(x => x.EmailAddress == CurrentUser.EmailAddress);
            var password = _context.Users.Any(y => y.Password == CurrentUser.Password);

            // Both email and password should be correct for the user to login
            var isValidUser = emailAddress && password;

            if (!isValidUser)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return Page();
            }

            // Use Authentication for Email and password with Cookies
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            // Adding the user's name to their claim within the Authentication Scheme
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, CurrentUser.EmailAddress) }, scheme));

            // Sign in with claim and credentials
            SignIn(user, scheme);
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostLogout()
        {
            // Removing the user login authentications info from the Cookies
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
    }
}