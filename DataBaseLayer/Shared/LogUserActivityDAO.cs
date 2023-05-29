using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afriauscare.BusinessLayer.Shared;

namespace Afriauscare.DataBaseLayer.Shared
{
    public class LogUserActivityDAO
    {
        public void CreateLogUserActivity(LogUserActivityModel  objLogUserModel)
        {
            using (var DataBase = new AfriAusEntities())
            {
                log_user_activity objLogUser = new log_user_activity
                {
                    user_id = objLogUserModel.User_id,
                    module_name = objLogUserModel.Module_Name,
                    action_executed = objLogUserModel.Action_Excuted,
                    datetime_action = objLogUserModel.Datetime_action
                };

                DataBase.log_user_activity.Add(objLogUser);
                DataBase.SaveChanges();
            }

        }

        public List<LogUserActivityModel> GetLogUserInformation()
        {
            List<LogUserActivityModel> listReturn = new List<LogUserActivityModel>();

            using (var DataBase = new AfriAusEntities())
            {
                var LogUserList = (from L in DataBase.log_user_activity
                                           join U in DataBase.Users
                                           on L.user_id equals U.UserId
                                           select new
                                           {
                                               L.log_user_id,
                                               L.user_id,
                                               U.Username,
                                               U.UserLastName,
                                               L.module_name,
                                               L.action_executed,
                                               L.datetime_action
                                           }).ToList();

                foreach (var item in LogUserList)
                {
                    LogUserActivityModel objGallery = new LogUserActivityModel
                    {
                        Log_user_id = item.log_user_id,
                        User_id = item.user_id,
                        User_Full_Name = item.Username + " " + item.UserLastName,
                        Module_Name = item.module_name,
                        Action_Excuted = item.action_executed,
                        Datetime_action = item.datetime_action
                    };
                    listReturn.Add(objGallery);
                }
            }

            return listReturn;
        }
    }
}
