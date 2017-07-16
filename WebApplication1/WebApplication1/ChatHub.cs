using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using WebApplication1;
using WebApplication1.Models;
using System.Linq;

public class ChatHub : Hub
{
    private EfContext database;

    private List<User> users;

    private List<User> roomUsers; 

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

    public void Send(string name, string message, List<User> chatUsers)
    {
        Clients.All.broadcastMessage(name, message, chatUsers, chatUsers.Count);
    }

    public void SignIn(string login, string password)
    {
        User user = getUserByLogin(login);
        Clients.All.isSignedIn(user != null && user.Password == HashHelper.GetPasswordFromHash(password), users, users.Count);
    }

    public void CreateChatRoom(string[] array)
    {
        var newUsers = new List<User>();
        roomUsers = new List<User>();

        foreach (var item in array)
        {
            newUsers.Add(getUserByLogin(item));
        }

        Clients.All.chatRoomCreatedFor(newUsers, newUsers.Count);
    }

    public void Register(string login, string password)
    {
        if (users.Contains(getUserByLogin(login)))
        {
            Clients.All.isRegistered("Register failed, duplicated login");
            return;
        }

        if (password.Length < 3)
        {
            Clients.All.isRegistered("Register failed, short password, must be >= 3");
            return;
        }

        database.Users.Add(new User()
        {
            Login = login,
            Password = HashHelper.GetPasswordFromHash(password),
            CanSend = true,
        });
        database.SaveChanges();
        Clients.All.isRegistered("Register success");
    }

    public void CreateRoom()
    {
        var users = database.Users.ToList();
        Clients.All.createPrivateRoom(users, users.Count);
    }
}