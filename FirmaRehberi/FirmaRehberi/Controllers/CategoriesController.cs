using FirmaRehberi.db;
using FirmaRehberi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace FirmaRehberi.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        myPanelDBEntities db = new myPanelDBEntities();
        // GET: api/Categories/get
        [Route("get")]
        public  SiteResponse<List<Category>>  Get()
        {
            //Tüm kategori listesi
            var sessionID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
            var response = new SiteResponse<List<Category>>();          
           var Categories = db.Categories.ToList();
            response.Status = Categories.Any();
            if (response.Status)
            {
                response.Data = Categories.Select(c => new Category(c)).ToList();
                response.Message = "Kategoriler listelendi";
            }
            else
            {
                response.Message = "Kategoriler listelenirken hata oluştu";              
            }
            return response;
        }

        // GET: api/Categories/getDetail/2
        [Route("getDetail/{id}")]
        public SiteResponse<Category> GetDetail(int id)
        {
            //seçilen kategori
            var response = new SiteResponse<Category>();
            var getcatid = db.Categories.Find(id);
            response.Status = getcatid != null;
            if (response.Status)
            {
                response.Data = new Category(getcatid);
                response.Message = $"Kategori {id} gösterildi";
            }
            else
            {
                response.Message = $"Kategori {id} bulunamadı";
            }
            return response;
        }
        // POST: api/Categories/add
        [Route("add")]
        [HttpPost]
        public SiteResponse<object> Add(Category cat)
        {
            //kategori ekleme
            var response =new  SiteResponse<object>();
            response.Status = cat == null;
            if (response.Status)
            {
                response.Message = "Kategori tanımsız";
                return response;
            }
            var newCategory = new Categories();
            newCategory.Id = cat.ID;
            newCategory.Name = cat.Name;
            newCategory.Description = cat.Description;
            newCategory.UserBy = cat.UserBy;
            newCategory.AddedDate = DateTime.Now;
            //newCategory.ModifiedDate = cat.ModifiedDate;
            newCategory.MainCategoryName = cat.MainCategoryName;
            db.Entry(newCategory).State = EntityState.Added;
            db.SaveChanges();
            response.Status = newCategory.Id != 0;
            if (response.Status)
            {
                response.Message = "Kategori eklendi";
                return response;
            }
            else
            {
                response.Message = "Kategori id tanımsız";
                return response;
            }
        }
        // POST: api/Categories/edit
        [Route("edit")]
        [HttpPost]
        public SiteResponse<object> Edit(Category category)
        {
            var response = new SiteResponse<object>();
            response.Status = category == null;
            if (response.Status)
            {
                response.Message = "Düzenlemek istenen kategori tanımsız!";
                return response;
            }
            var oldCategory = db.Categories.Find(category.ID);
            response.Status = oldCategory == null;

            if (response.Status)
            {
                response.Message = "Düzenlemek istenen kategori bulunamadı!";
                return response;
            }
            oldCategory.Id = category.ID;
            oldCategory.Name = category.Name;
            oldCategory.Description = category.Description;
            oldCategory.UserBy = category.UserBy;
            oldCategory.ModifiedDate = DateTime.Now;
            oldCategory.MainCategoryName = category.MainCategoryName;
            db.Entry(oldCategory).State = EntityState.Modified;
            db.SaveChanges();
            response.Status = true;
            return response; 
        }
        // DELETE: api/Categories/deletecategory/5
        [Route("deletecategory/{id}")]
        [HttpGet]
        public SiteResponse<Category> Deletecategory(int id)
        {
            var response = new SiteResponse<Category>();
            var category = db.Categories.Find(id);
            response.Status = category != null;
            if (response.Status)
            {
                db.Entry(category).State = EntityState.Deleted;
                db.SaveChanges();
                return response;
            }
            response.Message = $"Silinmesi istenen {id} no'lu kategori bulunamadı!";
            return response;
        }
    }
}
