namespace Mentat.UI.ViewModels;


public class RegisterViewModel
{
    public string UserName { get; set; }
    
    public string Password { get; set; }

    public int UserType { get; set; } // 0 for students, 1 for mentors

}
