using System.Collections.Generic;
using System.Linq;
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
                user = DataBase.Users.FirstOrDefault(u => u.UserEmail == objUserModel.UserEmail && u.UserPassword == objUserModel.UserPassword && u.UserActive == true);
            }

            return user;
        }

        public List<UserModel> getUsersAll()
        {
            List<UserModel> listUser = new List<UserModel>();

            using (var DataBase = new AfriAusEntities())
            {
                var list = DataBase.Users.ToList();

                foreach (var item in list)
                {
                    UserModel objUser = new UserModel()
                    {
                        UserId = item.UserId,
                        Username = item.Username,
                        UserLastName = item.UserLastName,
                        UserEmail = item.UserEmail,
                        UserPassword = item.UserPassword,
                        UserActive = item.UserActive
                    };
                    listUser.Add(objUser);
                }
            }

            return listUser;
        }

        public User getUserbyUserIdActiveUser(int userId)
        {
            var user = new User();

            using (var DataBase = new AfriAusEntities())
            {
                user = DataBase.Users.Where(u => u.UserId == userId && u.UserActive == true).FirstOrDefault();
            }

            return user;
        }

        public UserModel getUserbyUserId(int userId)
        {
            var user = new User();
            UserModel objUserModel = new UserModel();

            using (var DataBase = new AfriAusEntities())
            {
                user = DataBase.Users.Where(u => u.UserId == userId).FirstOrDefault();
                objUserModel.UserId = user.UserId;
                objUserModel.Username = user.Username;
                objUserModel.UserLastName = user.UserLastName;
                objUserModel.UserEmail = user.UserEmail;
                objUserModel.UserPassword = user.UserPassword;
                objUserModel.UserActive = user.UserActive;
            }

            return objUserModel;
        }

        public void ModifyUser (UserModel objUserPar)
        {
            using(var DataBase = new AfriAusEntities())
            {
                User objUser = new User()
                {
                    UserId = objUserPar.UserId,
                    Username = objUserPar.Username,
                    UserLastName = objUserPar.UserLastName,
                    UserEmail = objUserPar.UserEmail,
                    UserPassword = objUserPar.UserPassword,
                    UserActive = objUserPar.UserActive
                };

                DataBase.Users.Add(objUser);
                DataBase.Entry(objUser).State = System.Data.EntityState.Modified;
                DataBase.SaveChanges();
            }
        }

        public void DisableUser (int id)
        {
            using (var DataBase = new AfriAusEntities())
            {
                User objUser = (from u in DataBase.Users
                                where u.UserId == id
                                select u).SingleOrDefault();

                objUser.UserActive = false;
                DataBase.SaveChanges();
            }
        }
    }
}
