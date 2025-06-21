using BusinessObjects.Models;

namespace Services
{
    public interface ISystemAccountService
    {
        Task<SystemAccount> Login(string email, string password);
    }
}
