using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public partial class UsersModel
    {
        public UsersModel()
        {
            Users_roles = new HashSet<Users_roles>();
        }
        public int user_id { get; set; }
        public string user_login { get; set; }

        public string user_password { get; set; }
        public virtual ICollection<Users_roles> Users_roles { get; set; }

        public UsersModel(Users users)
        {
            user_id = users.user_id;
            user_login = users.user_login;
            user_password = user_password;
        }
    }
}
