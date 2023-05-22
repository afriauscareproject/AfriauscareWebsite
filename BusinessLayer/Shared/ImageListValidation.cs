using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

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

        public bool FileSizeValidation(HttpPostedFileBase[] fileList, int maxFileSize)
        {
            bool flag = true;

            foreach (var file in fileList)
            {
                if (file.ContentLength > maxFileSize)
                {
                    return false;
                }
            }

            return flag;
        }

        public bool FileExtensionValidation(HttpPostedFileBase[] fileList)
        {
            bool flag = true;
            List<string> FileExtensionPermitted = new List<string>() { ".jpg", ".jpeg", ".png" };

            foreach (var file in fileList)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (!FileExtensionPermitted.Contains(fileExtension))
                {
                    return false;
                }
            }

            return flag;
        }
    }
}
