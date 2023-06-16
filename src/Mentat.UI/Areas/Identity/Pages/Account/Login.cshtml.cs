﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Mentat.UI.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Mentat.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<MentatUser> _userManager;
        private readonly SignInManager<MentatUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly RoleManager<MentatUserRole> _roleManager;

        public LoginModel(SignInManager<MentatUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<MentatUser> userManager,
            RoleManager<MentatUserRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Email or Username")]
            public string EmailOrUsername { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
                // Try to find user by email address:
                var user = await _userManager.FindByEmailAsync(Input.EmailOrUsername);
                if (user == null)
                {
                    // User not found by email; try to find user by username:
                    user = await _userManager.FindByNameAsync(Input.EmailOrUsername);
                }

                // User found; attempt to sign in with their email/username and password:
                if (user != null)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    if(!await _roleManager.RoleExistsAsync("Mentor"))
                    {
                        await _roleManager.CreateAsync(new MentatUserRole { Id = Guid.NewGuid(), Name = "Mentor" });
                    }
                    if (!await _roleManager.RoleExistsAsync("Student"))
                    {
                        await _roleManager.CreateAsync(new MentatUserRole { Id = Guid.NewGuid(), Name = "Student" });
                    }
                    var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        if(! await _userManager.IsInRoleAsync(user, user.UserType))
                        {
                            await _userManager.AddToRoleAsync(user, user.UserType);
                        }

                        _logger.LogInformation("User logged in.");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", user.UserType);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                }

                // If user not found or sign-in attempt failed, show error message:
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
