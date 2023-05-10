using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Objects.SqlClient;

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
                return new SelectList(list, "Value", "Text");
            }
        }
    }
}
