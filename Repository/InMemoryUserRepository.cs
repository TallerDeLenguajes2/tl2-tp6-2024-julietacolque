
using tl2_tp6_2024_julietacolque.Models;
//interface
public class InMemoryUserRepository:IUserRepository
{
    private readonly List<Usuario> _usuarios;

    public InMemoryUserRepository()
    {
        _usuarios = new List<Usuario>{
            new Usuario{ Id=1,UserName="Admin",Password="pass", AccessLevel = AccessLevel.Admin},
            new Usuario{ Id=1,UserName="editor",Password="pass2", AccessLevel = AccessLevel.Editor},
            new Usuario{ Id=1,UserName="empleado",Password="pass3", AccessLevel = AccessLevel.Empleado}

        };
    }
    public Usuario GetUser(string username, string password)
    {
        return _usuarios.Where(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                                 && u.Password.Equals(password, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
    }
}

/**
public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users;

    public InMemoryUserRepository()
    {
        // Lista de usuarios predefinidos para simular una base de datos
        _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "password123", AccessLevel = AccessLevel.Admin },
            new User { Id = 2, Username = "manager", Password = "password123", AccessLevel = AccessLevel.Editor },
            new User { Id = 3, Username = "employee", Password = "password123", AccessLevel = AccessLevel.Empleado },
            new User { Id = 4, Username = "guest", Password = "password123", AccessLevel = AccessLevel.Invitado }
        };
    }

    public User GetUser(string username, string password)
    {        
        return _users
        .Where(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(password, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
    }
}


*/