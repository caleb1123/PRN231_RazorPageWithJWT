using BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private readonly EnglishPremierLeague2024DbContext context;

        private AccountDAO()
        {
            context = new EnglishPremierLeague2024DbContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public async Task<PremierLeagueAccount> Login(string email, string password)
        {
            
                PremierLeagueAccount account = await context.PremierLeagueAccounts.FirstOrDefaultAsync(a => a.EmailAddress == email && a.Password == password);
            if(account == null)
            {
                throw new Exception("Account not found");
            }    
            return account;
            
        }
    }
}
