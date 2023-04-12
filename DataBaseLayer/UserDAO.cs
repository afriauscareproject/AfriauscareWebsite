using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afriauscare.BusinessLayer.User;

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
            using (var DataBase = new AfriAusEntities())
            {
                return DataBase.Users.ToList();
            }
        }

        public User getUserbyUserId(int userId)
        {
            var user = new User();

            using (var DataBase = new AfriAusEntities())
            {
                user = DataBase.Users.Where(u => u.UserId == userId).FirstOrDefault();
            }

            return user;
        }

        public void ModifyUser (User objUser)
        {
            using(var DataBase = new AfriAusEntities())
            {
                DataBase.Users.Add(objUser);
                DataBase.Entry(objUser).State = System.Data.EntityState.Modified;
                DataBase.SaveChanges();
            }
        }
    }
}
