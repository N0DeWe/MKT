using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using System.Data.SqlClient;

namespace DAL.Repository
{
    public class AuthorizationRepositorySQL : IAuthorizationRepository
    {
        private PcShopContext db;
        public AuthorizationRepositorySQL(PcShopContext database)
        {
            this.db = database;
        }

        public void AddAccount(Users users)
        {
            List<Users_roles> usersRolesList = new List<Users_roles>();
            Users_roles usersRoles = new Users_roles();
            usersRolesList = db.Users_roles.ToList();
            usersRoles.users_roles_id = usersRolesList.Count();
            usersRoles.user_id_FK = users.user_id;
            usersRoles.role_id_FK = 2;

            db.Users.Add(users);
            db.Users_roles.Add(usersRoles);
            Save();
        }
        public List<Users> GetList()
        {
            return db.Users.ToList();
        }
        public List<Users_roles> GetAllUsersRoles()
        {
            return db.Users_roles.ToList();
        }
        public bool isUserEmailExists(string userEmail)
        {     
            try
            {
                db.Users.First(i => i.user_login.Equals(userEmail));
                return true;
            }
            catch (InvalidOperationException ex)
            {
                return false;
            }
        }

        public bool isLoginDataValid(string userPassword, string userEmail)
        {

            if (db.Users.Single(i => i.user_login == userEmail).user_password == userPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
