using AuthenticationExe.Business.Dtos;
using AuthenticationExe.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationExe.Business.Service
{
    public interface IUserService
    {
        ServiceMessage AddUser(RegisterDto registerDto);
        UserInfoDto LoginUser(LoginDto loginDto);
    }
}
