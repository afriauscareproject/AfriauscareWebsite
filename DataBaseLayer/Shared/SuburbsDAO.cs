using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Objects.SqlClient;

namespace Afriauscare.DataBaseLayer.Shared
{
    public class SuburbsDAO
    {
        public IEnumerable<SelectListItem> GetSuburbs(string state_id)
        {
            int state_id_converted = Int16.Parse(state_id);

            using (var DataBase = new AfriAusEntities())
            {
                List<SelectListItem> list = DataBase.suburbs.AsNoTracking()
                                            .Where(n => n.state_id == state_id_converted)
                                            .OrderBy(n => n.suburb_name)
                                            .Select(n => new SelectListItem
                                            {
                                                Value = SqlFunctions.StringConvert((double)n.suburb_id),
                                                Text = n.suburb_name
                                            }).ToList();
                var first_item = new SelectListItem()
                {
                    Value = "",
                    Text = "--- Select Suburb ---"
                };
                list.Insert(0, first_item);
                foreach (var item in list)
                {
                    item.Value = item.Value.Trim();
                }

                return new SelectList(list, "Value", "Text");
            }
        }
    }
}
