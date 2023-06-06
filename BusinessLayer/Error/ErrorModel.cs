using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afriauscare.BusinessLayer.Error
{
    /// <summary>
    /// Error model class to create errors and transport data to View
    /// </summary>
    public class ErrorModel
    {
        public int ErrorId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
