//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirmaRehberi.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class AddToSite
    {
        public int Id { get; set; }
        public string SearchingCode { get; set; }
        public string CompanyCardCode { get; set; }
        public string RosetteCode { get; set; }
        public int Company_Id { get; set; }
        public System.DateTime AddedDate { get; set; }
        public int Member_Id { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
    
        public virtual Companies Companies { get; set; }
        public virtual Members Members { get; set; }
    }
}
