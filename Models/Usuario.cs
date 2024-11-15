namespace tl2_tp6_2024_julietacolque.Models;
public class Usuario
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public AccessLevel AccessLevel { get; set; }


}
public enum AccessLevel
{
    Admin,
    Editor,
    Invitado,
    Comun,
    Empleado

}