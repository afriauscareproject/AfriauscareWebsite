using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;

using System.Web.Mvc;

namespace Afriauscare.DataBaseLayer.Shared
{
    public class BanksDAO
    {
        public IEnumerable<SelectListItem> GetBanks()
        {
            using (var DataBase = new AfriAusEntities())
            {
                List<SelectListItem> list = DataBase.banks.AsNoTracking()
                                            .OrderBy(b => b.bank_name)
                                            .Select(b => new SelectListItem
                                            {
                                                Value = SqlFunctions.StringConvert((double)b.bank_id),
                                                Text = b.bank_name
                                            }).ToList();
                var first_item = new SelectListItem()
                {
                    Value = "",
                    Text = "--- Select Bank ---"
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
