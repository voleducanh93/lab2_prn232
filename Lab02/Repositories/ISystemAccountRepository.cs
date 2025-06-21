using BusinessObjects.Models;

namespace Repositories
{
    public interface ISystemAccountRepository
    {
        Task<SystemAccount> Login(string email, string password);
    }
}
