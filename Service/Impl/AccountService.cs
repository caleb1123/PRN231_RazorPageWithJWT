using BOs.Resquest;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class AccountService :IAccountService
    {
        private  IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<string> Login(LoginRequest request)
        {
            return await _accountRepository.Login(request);
        }
    }
}
