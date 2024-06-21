namespace jwt.Services
{
    public interface IAuth
    {
        public Task<bool> Register (string fullName,string username, string password);
        public Task<string> login (string username, string password);

    }
}
