using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FirmaRehberi.Models
{
    public class PuchPackage
    {
        public int Id { get; set; }
        public string BillingInfo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Member_Id { get; set; }
        public string DiscountCode { get; set; }
        public string NameAndSurname { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ? Company_Id { get; set; }
        public string TaxAdministration { get; set; }
        public string TaxNumber { get; set; }
        public bool ? isPurchasing { get; set; }
        public string CompanyName { get; set; }
        public int PackageId { get; set; }
        public PuchPackage()
        {

        }

        public PuchPackage(PurchacingPackages pack)
        {
            Id = pack.Id;
            BillingInfo = pack.BillingInfo;
            PurchaseDate = pack.PurchaseDate;
            Member_Id = pack.Member_Id;
            Adress = pack.Adress;
            Email = pack.Email;
            Phone = pack.Phone;
            Company_Id = pack.Company_Id;
            TaxAdministration = pack.TaxAdministration;
            TaxNumber = pack.TaxNumber;
            isPurchasing = pack.isPurchasing;
            CompanyName = pack.CompanyName;
            PackageId = pack.PackageId;
        }

    }


}