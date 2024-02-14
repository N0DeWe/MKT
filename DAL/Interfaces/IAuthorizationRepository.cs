using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAuthorizationRepository
    {
       void AddAccount(Users users);
       List<Users> GetList();
       List<Users_roles> GetAllUsersRoles();
       bool isUserEmailExists(string userEmail);
       bool isLoginDataValid(string userPassword, string userEmail);
       void Save();
    }
}
