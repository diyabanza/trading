namespace App;

class User : ILogin
{
    public string Username;
    string _password;

    public User(string u, string p)
    {
        Username = u;
        _password = p;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }
}