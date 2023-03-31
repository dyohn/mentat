using System;

namespace Mentat.UI.ViewModels;


public class RegisterViewModel
{
    public string FirstName { get; set; } // new property for first name

    public string LastName { get; set; } // new property for last name

    public string Username { get; set; }
    
    public string Password { get; set; }

    public int UserType { get; set; } // 0 for students, 1 for mentors

    public bool RememberMe { get; set; } // true if "Remember Me" checkbox is checked

    public string Email { get; set; } // new property for email

    public DateTime BirthDate { get; set; } // new property for birth date
}
