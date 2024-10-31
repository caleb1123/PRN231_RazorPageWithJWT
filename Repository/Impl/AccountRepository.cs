using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration; // Thêm namespace này


namespace Repository.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration configuration; // Thêm dòng này
        public AccountRepository(IConfiguration configuration)
        {
            this.configuration = configuration; // Thêm dòng này
        }
        public async Task<string> Login(string email, string password)
        {

            string success = "success";
            var account =  await AccountDAO.Instance.Login(email, password);
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
                return tokenService.GenerateToken(email, account.Role);
            }


            return "failed";
        }
    }
}
