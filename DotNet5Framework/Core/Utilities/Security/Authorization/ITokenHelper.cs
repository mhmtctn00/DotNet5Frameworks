using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Authorization.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Authorization
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<Role> roles, bool refreshToken = true);
    }
}