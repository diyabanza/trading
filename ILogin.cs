namespace App;

interface ILogin
{
    public bool TryLogin(string username, string password);
}