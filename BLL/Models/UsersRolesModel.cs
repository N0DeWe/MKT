using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{

    public partial class UsersRolesModel
    {
        public int users_roles_id { get; set; }
        public int user_id_FK { get; set; }

        public int role_id_FK { get; set; }

        public virtual Roles Roles { get; set; }

        public virtual Users Users { get; set; }

        public UsersRolesModel()
        {
        }
        public UsersRolesModel(Users_roles usersRoles)
        {
            users_roles_id = usersRoles.users_roles_id;
            role_id_FK = usersRoles.role_id_FK;
            user_id_FK = usersRoles.user_id_FK;
        }
    }
}
