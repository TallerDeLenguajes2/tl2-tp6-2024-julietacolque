namespace tl2_tp6_2024_julietacolque.ViewModels;
public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ErrorMessage { get; set; }
    public bool IsAuthenticated { get; set; }
}