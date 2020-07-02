using Gym.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Lib.Services.Security
{
    public interface IAuthService
    {
        bool CheckIfUserAlreadyExists(string email);

        string ValidateUser(string email, string password, out int userId);

        User GetUserById(int userId);
    }
}
