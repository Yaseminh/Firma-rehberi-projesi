using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string Price { get; set; }
        public string DiscountCode { get; set; }

        public Package()
        {

        }
        public Package(Packages pack)
        {
            Id = pack.Id;
            PackageName = pack.PackageName;
            Price = pack.Price;
            DiscountCode = pack.DiscountCode;
        }
    }
}