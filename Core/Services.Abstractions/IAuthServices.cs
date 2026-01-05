using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstractions
{
    public interface IAuthServices
    {
        //Task<string> GenerateEmailConfirmationTokenAsync(string email);
        //Task<string> ConfirmEmailAsync(string email, string token);
        //Task<string> GenerateForgotPasswordTokenAsync(string email);
        //Task<string> ResetPasswordAsync(string email, string token, string newPassword);
        Task<UserResultDto> login(LoginDto loginDto);
        Task<UserResultDto> Register(RegisterDto registerDto);
    }
}
