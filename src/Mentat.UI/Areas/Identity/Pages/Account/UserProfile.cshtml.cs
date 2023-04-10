using System;
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

        public InputModel Input { get; set; }

        public string returnUrl{ get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }

        public string CurrentPassword{get;set;}

        public class InputModel
        {
            [Required]
            [Display(Name = "User Name")]
            [BindProperty]
            public string UserName {get; set;}

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            [BindProperty]
            public string Email {get; set;}

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Current Password")]
            [BindProperty]
            public string Password {get;set;}
            
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New Password")]
            [BindProperty]
            public string NewPassword { get; set; }
            
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm New Password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            [BindProperty]
            public string ConfirmPassword { get; set; }

             [Required]public string UserType { get; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid use profile access attempt.");
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null || !User.Identity.IsAuthenticated)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

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
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !User.Identity.IsAuthenticated)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Assign the Input model before returning the page
            Input = new InputModel
            {
                UserName = user.UserName,
                Email = user.Email
            };

            /* Currently we do not have it setup so that emails are verified. Thus this part is not needed yet.
            
            if(!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty,"You must have a confirmed email to update your profile.");
                return Page();
            }
 */
            if (!ModelState.IsValid)
            {
                
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user); 
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var username = await _userManager.GetUserNameAsync(user);
            if (Input.UserName != username)
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.UserName);
                if (!setUserNameResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting user name for user with ID '{userId}'.");
                }
            }
            if (!string.IsNullOrEmpty(Input.NewPassword))
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.Password, Input.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("UserProfile", new { returnUrl = returnUrl });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Assign the Input model before returning the page
            Input.UserName = user.UserName;
            Input.Email = user.Email;

            return Page();
        }
    }
}
