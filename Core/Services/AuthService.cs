using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthServices
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<UserResultDto> login(LoginDto loginDto)
        {
          var user=  await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new Exception(); 
            var flag = await _userManager.CheckPasswordAsync(user, loginDto.Password);
             if (!flag) throw new Exception();
            return new UserResultDto 
            {
             DisplayName= user.DisplayName,
                Email= user.Email,
                Username= user.UserName,
                Token= "FakeToken"
            };


        }

        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {
           var flag =  _userManager.Users.Any(user=>user.Email==registerDto.Email);
            if (flag) throw new Exception();
            var checkUsername = _userManager.Users.Any(U=>U.UserName==registerDto.Username);
            if (checkUsername) { throw new Exception(); }
            var user = new AppUser {Email=registerDto.Email,UserName=registerDto.Username,DisplayName=registerDto.DisplayName,PhoneNumber=registerDto.phoneNumber };
           var result = await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) throw new Exception();
            return new UserResultDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Username = user.UserName,
                Token = "FakeToken"
            };

        }
    }
}
