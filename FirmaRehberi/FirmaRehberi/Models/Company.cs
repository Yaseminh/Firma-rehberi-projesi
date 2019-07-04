using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string WorkingHours { get; set; }
        public string PaymentMethods { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int ? StarPoint { get; set; }
        public int ? StarGivenMemberCount { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ? ModifieddDate { get; set; }
        public int  Category_Id {get;set;}
        public string CompanyImage { get; set; }
        public int ? OpenOrClose { get; set; }
        public string CompanyName { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Brands { get; set; }
        public string Decriptions { get; set; }
        public string WebsiteName { get; set; }
        public int ? PurchasingPackage_Id { get; set; }
        public string NumberOfEmployees { get; set; }
        public string AuthorizedNameAndSurname { get; set; }
        public string AuthorizedDegree { get; set; }
        public string AuthorizedEmail { get; set; }
        public string Phone { get; set; }

        public Company()
        {

        }

        public Company(Companies com)
        {
            ID = com.Id;
            WorkingHours = com.WorkingHours;
            PaymentMethods = com.PaymentMethods;
            Latitude = com.Latitude;
            Longitude = com.Longitude;
            StarPoint = com.StarPoint;
            StarGivenMemberCount = com.StarGivenMemberCount;
            AddedDate = com.AddedDate;
            ModifieddDate = com.ModifiedDate;
            Category_Id = com.Category_Id;
            CompanyImage = com.CompanyImage;
            OpenOrClose = com.OpenOrClose;
            CompanyName = com.CompanyName;
            Adress = com.Adress;
            City = com.City;
            Town = com.Town;
            Brands = com.Brands;
            Decriptions = com.Descriptions;
            WebsiteName = com.WebsiteName;
            PurchasingPackage_Id = com.PurchasingPackage_Id;
            NumberOfEmployees = com.NumberOfEmployees;
            AuthorizedNameAndSurname = com.AuthorizedNameAndSurname;
            AuthorizedDegree = com.AuthorizedDegree;
            AuthorizedEmail = com.AuthorizedEmail;
            Phone = com.Phone;
        }

    }
}