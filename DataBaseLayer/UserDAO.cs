using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afriauscare.BusinessLayer;

namespace Afriauscare.DataBaseLayer
{
    public class UserDAO
    {
        public void CreateUser(UserModel objUserModel)
        {
            using (var DataBase = new AfriAusEntities())
            {
                User objUser = new User()
                {
                    Username = objUserModel.Username,
                    UserLastName = objUserModel.UserLastName,
                    UserEmail = objUserModel.UserEmail,
                    UserPassword = objUserModel.UserPassword,
                    UserActive = objUserModel.UserActive
                };

                DataBase.Users.Add(objUser);
                DataBase.SaveChanges();
            }
           
        }

        public User getUserbyUserandPassword (UserModel objUserModel)
        {
            var user = new User();

            using (var DataBase = new AfriAusEntities())
            {
                user = DataBase.Users.FirstOrDefault(u => u.UserEmail == objUserModel.UserEmail && u.UserPassword == objUserModel.UserPassword);
            }

            return user;
        }

        public List<User> getUsersAll()
        {
            List<User> userList = new List<User>();

            using (var DataBase = new AfriAusEntities())
            {
                userList = DataBase.Users.ToList();
            }

            return userList;
        }
    }
}
