using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afriauscare.BusinessLayer.Shared
{
    public abstract class ViewModelBase
    {
        public string Address { get; set; }

    }
    
    public class HomeViewModel : ViewModelBase
    {

    }
}
