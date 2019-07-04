using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class Showcase
    {
        public int Id { get; set; }
        public int ? Companies_Id { get; set; }
        public DateTime AddedDate { get; set; }
        public int ? OrderNumber { get;  set; }
        public string CompanyName { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public Showcase()
        {

        }
        public Showcase(Showcases show)
        {
            Id = show.Id;
            Companies_Id = show.Companies_Id;
            AddedDate = show.AddedDate;
            OrderNumber = show.OrderNumber;
            CompanyName = show.CompanyName;
            ModifiedDate = show.ModifiedDate;
        }        
    }
}