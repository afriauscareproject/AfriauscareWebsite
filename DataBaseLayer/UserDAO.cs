using System.Collections.Generic;
using System.Linq;
using Afriauscare.BusinessLayer.User;

namespace Afriauscare.DataBaseLayer
{
    public class UserDAO
    {
        /// <summary>
        /// Method to create a User using Linq syntax and Entity Framework
        /// </summary>
        /// <param name="objUserModel"></param>
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

        /// <summary>
        /// Method to obtain a User by Username and Password as parameters
        /// </summary>
        /// <param name="objUserModel"></param>
        /// <returns></returns>
        public User getUserbyUserandPassword (UserModel objUserModel)
        {
            var user = new User();

            using (var DataBase = new AfriAusEntities())
            {
                user = DataBase.Users.FirstOrDefault(u => u.UserEmail == objUserModel.UserEmail && u.UserPassword == objUserModel.UserPassword && u.UserActive == true);
            }

            return user;
        }

        /// <summary>
        /// Method which obtains all the users without filters
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Method to return a User active by its ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User getUserbyUserIdActiveUser(int userId)
        {
            var user = new User();

            using (var DataBase = new AfriAusEntities())
            {
                user = DataBase.Users.Where(u => u.UserId == userId && u.UserActive == true).FirstOrDefault();
            }

            return user;
        }

        /// <summary>
        /// Method to obtain a User by its ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method which updates a User record on the database
        /// </summary>
        /// <param name="objUserPar"></param>
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

        /// <summary>
        /// Method which disables a User from the User table
        /// </summary>
        /// <param name="id"></param>
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
        
        /// <summary>
        /// Method which obtains a User records by its Email address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool getUserbyEmail(ForgotPasswordModel model)
        {
            bool flag = false;

            using (var DataBase = new AfriAusEntities())
            {
                if (DataBase.Users.Any(u => u.UserEmail == model.UserEmail))
                {
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        /// Method which updates a User password
        /// </summary>
        /// <param name="model"></param>
        public void ChangeUserPassword(ForgotPasswordModel model)
        {
            using (var DataBase = new AfriAusEntities())
            {
                User objUser = new User()
                {
                    UserId = model.UserId,
                    UserPassword = model.UserPassword,
                    UserActive = false
                };

                DataBase.Users.Attach(objUser);
                DataBase.Entry(objUser).Property(u => u.UserPassword).IsModified = true;
                DataBase.Entry(objUser).Property(u => u.UserActive).IsModified = true;
                DataBase.SaveChanges();
            }
        }

        /// <summary>
        /// Method which activates a User by changing the IsActive flag to 1
        /// </summary>
        /// <param name="model"></param>
        public void ActivateUser(ForgotPasswordModel model)
        {
            using (var DataBase = new AfriAusEntities())
            {
                User objUser = new User()
                {
                    UserId = model.UserId,
                    UserPassword = model.UserPassword,
                    UserActive = true

                };

                DataBase.Users.Attach(objUser);
                DataBase.Entry(objUser).Property(u => u.UserPassword).IsModified = true;
                DataBase.Entry(objUser).Property(u => u.UserActive).IsModified = true;
                DataBase.SaveChanges();
            }
        }

        /// <summary>
        /// Method to check if the temporary password is correct depending on the parameters
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True of False</returns>
        public bool ValidateTemporaryPassword(ForgotPasswordModel model)
        {
            bool flag = false;

            using (var DataBase = new AfriAusEntities())
            {
                if (DataBase.Users.Any(u => u.UserEmail == model.UserEmail && u.UserPassword == model.TemporaryPassword))
                {
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        /// Method which returns the User Id based on the user email
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>User Id</returns>
        public int getUserIdByEmail(string emailAddress)
        {
            int userId = 0;

            using (var DataBase = new AfriAusEntities())
            {
                var user = DataBase.Users.Where(u => u.UserEmail == emailAddress).SingleOrDefault();
                userId = user.UserId;
            }

            return userId;
        }
    }
}
