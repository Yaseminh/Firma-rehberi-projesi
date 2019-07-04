using FirmaRehberi.db;
using FirmaRehberi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FirmaRehberi.Controllers
{
    [RoutePrefix("api/Companies")]
    public class CompaniesController : ApiController
    {
        myPanelDBEntities db = new myPanelDBEntities();
        int baslangic = 0;
        List<Companies> listcom = new List<Companies>();
        Companies addsearch;
        // GET: api/Companies
        public SiteResponse<List<Company>> Get()
        {
            //Tüm firma listesi
            var response = new SiteResponse<List<Company>>();
            var list = db.Companies.ToList();
            response.Status = list.Any();
            if (response.Status)
            {
                response.Data = list.Select(c => new Company(c)).ToList();
                response.Message = "Kayıtlar gösteriliyor";
            }
            else
            {
                response.Message = "Kayıtlar bulunamadı";          
            }
            return response;
        }

        // GET: api/Companies/GetCompaniesWithCat/2
        [Route("GetCompaniesWithCat/{categoryid}")]
        public SiteResponse<List<Company>> GetCompanieswithCat(int categoryid)
        {
            //seçilen kategodeki firmalar listesi
            var response = new SiteResponse<List<Company>>();           
            var companylists = db.Companies.ToList();
            response.Status = companylists.Any();
            if (response.Status)
            {
                response.Data = companylists.Where(c => c.Category_Id == categoryid).Select(c => new Company(c)).ToList();
                response.Message = "Kategorilere göre firmalar gösteriliyor";
            }
            else {
                response.Message = "Aradığınız kategoride firma yoktur";
            }
            return response;      
        }

        [Route("get/{id}")]
        // GET: api/Companies/get/2
        public SiteResponse<Company> Get(int id)
        {
            //seçilen kategodeki firmalar listesi
            var response = new SiteResponse<Company>();
            var getid = db.Companies.Find(id);
            response.Status = getid != null;
            if (response.Status)
            {
                response.Data = new Company(getid);
                response.Message = $"{id} no'lu Company bulundu!";
            }
            else
            {
                response.Message = $"{id} no'lu Company bulunamadı!";
            }
            return response;
        }
        // POST: api/Companies/add
        [Route("add")]
        [HttpPost]
        public SiteResponse<object> Add(Company com)
        {
            //kategori ekleme
            var response = new SiteResponse<object>();
            response.Status = com == null;
            if (response.Status)
            {
                response.Message = "Firma tanımsız";
                return response;
            }
            var newCompany = new Companies();
            newCompany.Id = com.ID;
            newCompany.WorkingHours = com.WorkingHours;
            newCompany.PaymentMethods = com.PaymentMethods;
            newCompany.Latitude = com.Latitude;
            newCompany.Longitude = com.Longitude;
            newCompany.AddedDate = DateTime.Now;
            newCompany.Category_Id = com.Category_Id;
            newCompany.CompanyImage = com.CompanyImage;
            newCompany.OpenOrClose = com.OpenOrClose;
            newCompany.CompanyName = com.CompanyName;
            newCompany.Adress = com.Adress;
            newCompany.City = com.City;
            newCompany.Town = com.Town;
            newCompany.Brands = com.Brands;
            newCompany.Descriptions = com.Decriptions;
            newCompany.WebsiteName = com.WebsiteName;
            newCompany.NumberOfEmployees = com.NumberOfEmployees;
            newCompany.Phone = com.Phone;
            db.Entry(newCompany).State = EntityState.Added;
            db.SaveChanges();
            response.Status = newCompany.Id != 0;
            if (response.Status)
            {
                response.Message = "Firma eklendi";
                return response;
            }
            else
            {
                response.Message = "Firma id tanımsız";
                return response;
            }
        }
        // POST: api/Companies/edit
        [Route("edit")]
        [HttpPost]
        public SiteResponse<object> Edit(Company com)
        {
            var response = new SiteResponse<object>();
            response.Status = com == null;
            if (response.Status)
            {
                response.Message = "Düzenlemek istenen firma tanımsız!";
                return response;
            }
            var oldCompany = db.Companies.Find(com.ID);
            response.Status = oldCompany == null;

            if (response.Status)
            {
                response.Message = "Düzenlemek istenen firma bulunamadı!";
                return response;
            }
            //oldCompany.Id = com.ID;
            oldCompany.WorkingHours = com.WorkingHours;
            oldCompany.PaymentMethods = com.PaymentMethods;
            oldCompany.Latitude = com.Latitude;
            oldCompany.Longitude = com.Longitude;
            oldCompany.AddedDate = DateTime.Now;
            oldCompany.Category_Id = com.Category_Id;
            oldCompany.CompanyImage = com.CompanyImage;
            oldCompany.OpenOrClose = com.OpenOrClose;
            oldCompany.CompanyName = com.CompanyName;
            oldCompany.Adress = com.Adress;
            oldCompany.City = com.City;
            oldCompany.Town = com.Town;
            oldCompany.Brands = com.Brands;
            oldCompany.Descriptions = com.Decriptions;
            oldCompany.WebsiteName = com.WebsiteName;
            oldCompany.NumberOfEmployees = com.NumberOfEmployees;
            oldCompany.Phone = com.Phone;
            db.Entry(oldCompany).State = EntityState.Modified;
            db.SaveChanges();
            response.Status = true;
            return response;
        }
        [Route("searchCity/{city}")]
        [HttpGet]
        //GET: api/Companies/SearchCity/istanbul
        public SiteResponse<List<Company>> SearchCity(string city)
        {
            var response = new SiteResponse<List<Company>>();
            var companylists = db.Companies.ToList();
            response.Status = companylists.Any();
            if (response.Status)
            {
                response.Data = companylists.Where(c => c.City == city).Select(c => new Company(c)).ToList();
                response.Message = $"Aradığınız {city} şehir veya şehirler bulundu";
            }
            else
            {
                response.Message = $"Aradığınız {city} şehri bulunamadı";
            }
            return response;
        }
        [Route("searchEverything")]
        [HttpGet]
        //GET: api/Companies/searchEverything/
        public SiteResponse<List<Company>> SearchEverything(string search)
        {
            var response = new SiteResponse<List<Company>>();
            var list = db.Companies.ToList();
            var searchString    = list.Select(c => new Company(c)).ToList();

            if (search != null) { 
                searchString = searchString.Where(s => s.CompanyName.Contains(search)).ToList();
                response.Data = searchString;
                response.Message = "Listelendi";
                response.Status = true;
                if (searchString.Count == 0)
                {
                    response.Message = "Aradığınız şey bulunamadı";
                    response.Status = false;
                    return response;
                }
                return response;
            }
            else
            {
                response.Message = "Arama yapmadınız";
                response.Status = false;
                return response;
            }
          
        }
        // DELETE: api/Companies/deletecompany/5
        [Route("deletecompany/{id}")]
        [HttpGet]
        public SiteResponse<Company> Deletecompany(int id)
        {
            var response = new SiteResponse<Company>();
            var company = db.Companies.Find(id);
            response.Status = company != null;
            if (response.Status)
            {
                db.Entry(company).State = EntityState.Deleted;
                db.SaveChanges();
                return response;
            }
            response.Message = $"Silinmesi istenen {id} no'lu firma bulunamadı!";
            return response;
        }
    }
}
