using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BOs.Resquest; // Thêm namespace này


namespace Repository.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration configuration; // Thêm dòng này
        public AccountRepository(IConfiguration configuration)
        {
            this.configuration = configuration; // Thêm dòng này
        }
        public async Task<string> Login(LoginRequest request)
        {

            string success = "success";
            var account =  await AccountDAO.Instance.Login(request);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            else
            {
                var tokenService = new JWT(
            configuration["Jwt:SecretKey"],
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"]);
                return tokenService.GenerateToken(request.Email, account.Role);
            }


            return "failed";
        }
    }
}
