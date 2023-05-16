using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Afriauscare.BusinessLayer.Shared
{
    public class ImageListValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = (HttpPostedFileBase[])value;

            foreach (var item in list)
            {
                if (item == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
