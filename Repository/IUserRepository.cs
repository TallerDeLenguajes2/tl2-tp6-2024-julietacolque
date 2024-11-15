using tl2_tp6_2024_julietacolque.Models;
public interface IUserRepository{
    Usuario GetUser(string nombre,string password);
}
