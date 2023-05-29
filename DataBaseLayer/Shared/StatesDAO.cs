using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Objects.SqlClient;
using Afriauscare.BusinessLayer.Shared;

namespace Afriauscare.DataBaseLayer.Shared
{
    public class StatesDAO
    {
        public IEnumerable<SelectListItem> GetStates()
        {
            using (var DataBase = new AfriAusEntities())
            {
                List<SelectListItem> list = DataBase.states.AsNoTracking()
                                            .OrderBy(n => n.state_name)
                                            .Select(n => new SelectListItem
                                            {
                                                Value = SqlFunctions.StringConvert((double)n.state_id),
                                                Text = n.state_name
                                            }).ToList();
                var first_item = new SelectListItem()
                {
                    Value = "",
                    Text = "--- Select State ---"
                };
                list.Insert(0, first_item);
                foreach(var item in list)
                {
                    item.Value = item.Value.Trim();
                }

                return new SelectList(list, "Value", "Text");
            }
        }

        public string GetStateNameById(int stateId)
        {
            string stateName = string.Empty;

            using (var DataBase = new AfriAusEntities())
            {
                var result = (from s in DataBase.states
                              where s.state_id == stateId
                              select new
                              {
                                 s.state_name
                              }).SingleOrDefault();

                stateName = result.state_name;
            }

            return stateName;
        }
    }
}
