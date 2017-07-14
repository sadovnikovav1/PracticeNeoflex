using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using WebApplication1;
using WebApplication1.Models;
using System.Linq;

public class ChatHub : Hub
{
    private EfContext database;

    private List<User> users;

    public ChatHub()
    {
        database = new EfContext();
        users = database.Users.ToList();
    }

    protected bool isAuthenticatedUser(string login)
    {
        return users.Contains(getUserByLogin(login));
    }

    public User getUserByLogin(string login)
    {
        return users.FirstOrDefault(u => u.Login == login);
    }



    public void Send(string name, string message)
    {
        Clients.All.broadcastMessage(name, message);
    }

    public void SignIn(string login, string password)
    {
        User user = getUserByLogin(login);
        bool checker = user != null && user.Password == HashHelper.GetPasswordFromHash(password);
        
        Clients.All.isSignedIn(checker);
    }

    public void Register(string login, string password)
    {
        string message = null;
        if (users.Contains(getUserByLogin(login)))
        {
            message = "Register failed, duplicated login";
            Clients.All.isRegistered(message);
            return;
        }

        if (password.Length < 3)
        {
            message = "Register failed, short password, must be >= 3";
            Clients.All.isRegistered(message);
            return;
        }

        database.Users.Add(new User()
        {
            Login = login,
            Password = HashHelper.GetPasswordFromHash(password),
        });
        database.SaveChanges();
        message = "Register success";
        Clients.All.isRegistered(message);
    }
}