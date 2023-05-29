using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afriauscare.BusinessLayer.Shared
{
    public class LogUserActivityModel
    {
        public int Log_user_id { get; set; }
        public int User_id { get; set; }
        public string User_Full_Name { get; set; }
        public string Module_Name { get; set; }
        public string Action_Excuted { get; set; }
        public DateTime Datetime_action { get; set; }
    }
}
