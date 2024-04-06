using AuthenticationExe.Business.Dtos;
using AuthenticationExe.Business.Service;
using AuthenticationExe.Business.Types;
using AuthenticationExe.Data.Entities;
using AuthenticationExe.Data.Repositories;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationExe.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDataProtector _dataProtector;
        public UserManager(IUserRepository userRepository, IDataProtectionProvider dataProtectionProvider)
        {
             _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        public ServiceMessage AddUser(RegisterDto registerDto)
        {
            var hasMail = _userRepository.GetAll( x => x.Email.ToLower()== registerDto.Email.ToLower()).ToList();
            if(hasMail.Any())
            {
                return new ServiceMessage()
                {
                    IsSucceed = false,
                    Message = "Bu email adresi zaten mevcut."
                };
            }

            var entity = new UserEntity()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Password = _dataProtector.Protect(registerDto.Password)
            };
            _userRepository.AddUser(entity);
            return new ServiceMessage()
            {
                IsSucceed = true,
                Message = "Kayıt işlemi başarılıyla tamamlandı."
            };
        }

        public UserInfoDto LoginUser(LoginDto loginDto)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == loginDto.Email.ToLower());
            if(userEntity is null)
            {
                return null;
            }
            var rawPassword = _dataProtector.Unprotect(userEntity.Password);
            if(rawPassword == loginDto.Password)
            {
                return new UserInfoDto()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
