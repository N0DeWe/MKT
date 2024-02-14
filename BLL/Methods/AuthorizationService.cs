using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using DAL.Repository;
using BLL;

namespace BLL.Methods
{
    public class AuthorizationService : IAuthorizationService
    {
        IDatabaseRepository db;
        public AuthorizationService(IDatabaseRepository database)
        {
            db = database;
        }

        public void AddAccount(UsersModel usersModel)
        {
            db.AuthorizationRepository.AddAccount(new Users()
            {
                user_id = usersModel.user_id,
                user_login = usersModel.user_login,
                user_password = usersModel.user_password
            });

        }
        public List<UsersRolesModel> GetAllUsersRoles()
        {
            return db.AuthorizationRepository.GetAllUsersRoles().Select(i => new UsersRolesModel(i)).ToList();
        }
        public List<UsersModel> GetUsersList()
        {
            return db.AuthorizationRepository.GetList().Select(i => new UsersModel(i)).ToList();
        }

        public bool isUserEmailExists(string userEmail)
        {
            return db.AuthorizationRepository.isUserEmailExists(userEmail);
        }

        public bool isLoginDataValid(string userPassword, string userEmail)
        {
            return db.AuthorizationRepository.isLoginDataValid(userPassword, userEmail);
        }

        public void Save()
        {
            db.Save();
        }
    }
}
