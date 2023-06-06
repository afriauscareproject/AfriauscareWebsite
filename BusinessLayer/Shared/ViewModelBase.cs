using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afriauscare.BusinessLayer.Shared
{
    /// <summary>
    /// Base model class to send information to Layout view
    /// </summary>
    public abstract class ViewModelBase
    {
        public string Address { get; set; }

    }
    
    public class HomeViewModel : ViewModelBase
    {

    }
}
