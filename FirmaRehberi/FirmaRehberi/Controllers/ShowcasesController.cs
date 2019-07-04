using FirmaRehberi.db;
using FirmaRehberi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirmaRehberi.Controllers
{
    [RoutePrefix("api/Showcases")]
    public class ShowcasesController : ApiController
    {
        myPanelDBEntities db = new myPanelDBEntities();
        // GET: api/Showcases/get
        [Route("get")]
        [HttpGet]
        public SiteResponse<List<ShowcaseView>> Get()
        {
            myPanelDBEntities db = new myPanelDBEntities();
            var response = new SiteResponse<List<ShowcaseView>>();
            var allpack = db.PurchacingPackages.ToList();
            var com = db.Companies.ToList();
            response.Status = allpack.Any();
            if (response.Status)
            {
                //packageid=1 kampanyalar bölümü veritabanında olmayan kampanyaların company_idsi 0dır.
                response.Message = "Showcase listeleniyor";
                var showInfo = new List<ShowcaseView>();
                foreach (var purchacing in allpack)
                {
                    var show = new ShowcaseView();
                    if (purchacing.PackageId == 2)
                    {
                        var varmıCom = com.Where(c => c.CompanyName == purchacing.CompanyName).ToList().Any();
                        if (varmıCom)
                        {
                            show.Adress = purchacing.Adress;
                            show.CompanyName = purchacing.CompanyName;
                            show.PackageId = purchacing.PackageId;
                            show.AddedDate = purchacing.PurchaseDate;
                            show.CompanyId = db.Companies.FirstOrDefault(s => s.CompanyName.Contains(purchacing.CompanyName)).Id;
                            showInfo.Add(show);
                            var getComid = db.Companies.Find(show.CompanyId);
                            var comidList = new List<Companies>();
                            //veritabanında kayıtlı olan firmalar
                            if (show.CompanyId != 0)
                            {
                                var İnfoShowLists = new List<Company>();
                                var myCom = new Company(getComid);
                                İnfoShowLists.Add(myCom);
                                show.mycom = İnfoShowLists.ToList();
                            }
                        }
                        else
                        {
                            show.Adress = purchacing.Adress;
                            show.CompanyName = purchacing.CompanyName;
                            show.PackageId = purchacing.PackageId;
                            show.AddedDate = purchacing.PurchaseDate;
                            showInfo.Add(show);
                        }
                    }
                }
                response.Data = showInfo;
            }
            else
            {
                response.Message = "Listelenecek Showcase yok";
            }
            return response;
        }
        // POST: api/Showcases/add
        [Route("add")]
        [HttpPost]
        public SiteResponse<object> Add(Showcase show)
        {
            //showcase ekleme
            var response = new SiteResponse<object>();
            response.Status = show == null;
            if (response.Status)
            {
                response.Message = "Showcase tanımsız";
                return response;
            }
            var newShowcase = new Showcases();
            newShowcase.AddedDate = DateTime.Now;
            newShowcase.Id = show.Id;
            newShowcase.CompanyName = show.CompanyName;
            newShowcase.Companies_Id = show.Companies_Id;
            db.Entry(newShowcase).State = EntityState.Added;
            db.SaveChanges();
            response.Status = newShowcase.Id != 0;
            if (response.Status)
            {
                response.Message = "Showcase eklendi";
                return response;
            }
            else
            {
                response.Message = "Showcase id tanımsız";
                return response;
            }
        }
        [Route("edit")]
        [HttpPost]
        public SiteResponse<object> Edit(Showcase show)
        {
            var response = new SiteResponse<object>();
            response.Status = show == null;
            if (response.Status)
            {
                response.Message = "Düzenlemek istenen kategori tanımsız!";
                return response;
            }
            var oldshow = db.Showcases.Find(show.Id);
            response.Status = oldshow == null;

            if (response.Status)
            {
                response.Message = "Düzenlemek istenen kategori bulunamadı!";
                return response;
            }
            oldshow.Id = show.Id;
            oldshow.CompanyName = show.CompanyName;
            oldshow.ModifiedDate = DateTime.Now;
            oldshow.Companies_Id = show.Companies_Id;
            db.Entry(oldshow).State = EntityState.Modified;
            db.SaveChanges();
            response.Status = true;
            return response;
        }
        // DELETE: api/Showcases/deleteshowcase/5
        [Route("deleteshowcase/{id}")]
        [HttpGet]
        public SiteResponse<Showcase> Deleteshowcase(int id)
        {
            var response = new SiteResponse<Showcase>();
            var showcase = db.Showcases.Find(id);
            response.Status = showcase != null;
            if (response.Status)
            {
                db.Entry(showcase).State = EntityState.Deleted;
                db.SaveChanges();
                return response;
            }
            response.Message = $"Silinmesi istenen {id} no'lu firma bulunamadı!";
            return response;
        }
    }
}
