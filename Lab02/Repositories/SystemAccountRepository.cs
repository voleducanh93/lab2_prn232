﻿using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public async Task<SystemAccount> Login(string email, string password)
        {
            return await SystemAccountDAO.Instance.Login(email, password);
        }
    }
}
