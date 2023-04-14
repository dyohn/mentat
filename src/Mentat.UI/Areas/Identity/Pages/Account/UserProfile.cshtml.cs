using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Mentat.UI.Areas.Identity.Data;
using Mentat.UI.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Mentat.UI.Areas.Identity.Pages.Account
{
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<MentatUser> _userManager;
        private readonly SignInManager<MentatUser> _signInManager;

        public UserProfileModel(UserManager<MentatUser> userManager, SignInManager<MentatUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Input = new InputModel();
        }

        [BindProperty] public InputModel Input { get; set; }


        public class InputModel
        {
            [Required]
            [Display(Name = "User ID:")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email:")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current Password:")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "New Password:")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm New Password:")]
            [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid use profile access attempt.");
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null || !User.Identity.IsAuthenticated)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Assign the Input model before returning the page
            Input.UserName = user.UserName;
            Input.Email = user.Email;

            /*Currently we do not have it setup so that emails are verified. Thus this part is not needed yet. 
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "You need to confirm your email before you can update your profile");
                return Page();
            } */

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            var passwordCorrect = await _userManager.CheckPasswordAsync(user, Input.Password);
            if (!passwordCorrect)
            {
                ModelState.AddModelError(string.Empty, "Incorrect password.");
                return Page();
            }

            if (Input.UserName != user.UserName)
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.UserName);
                if (!setUserNameResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "UserName not updated.");
                }
            }

            if (Input.Email != user.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Email address not updated.");
                }
            }

            if (!string.IsNullOrEmpty(Input.NewPassword) && Input.NewPassword == Input.ConfirmPassword)
            {
                var changePasswordResult =
                    await _userManager.ChangePasswordAsync(user, Input.Password, Input.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return Page();
                }
            }

            var result = await _userManager.UpdateAsync(user); // update user
            await _signInManager.RefreshSignInAsync(user); // update userName in _LoginPartial

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Redirect("~/"); // Return user to root page
        }
    }
}
