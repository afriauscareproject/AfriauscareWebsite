//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Afriauscare.DataBaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class suburb
    {
        public int suburb_id { get; set; }
        public Nullable<int> state_id { get; set; }
        public string suburb_name { get; set; }
        public string suburb_postcode { get; set; }
        public string suburb_alias { get; set; }
    
        public virtual state state { get; set; }
    }
}
