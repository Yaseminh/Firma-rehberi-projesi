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
    [RoutePrefix("api/PurcPackage")]
    public class PurcPackageController : ApiController
    {
        myPanelDBEntities db = new myPanelDBEntities();
        // GET: api/PurcPackage        
        public SiteResponse<List<PuchPackage>> Get()
        {
            //Tüm firma listesi
            var response = new SiteResponse<List<PuchPackage>>();
            var list = db.PurchacingPackages.ToList();
            response.Status = list.Any();
            if (response.Status)
            {
                response.Data = list.Select(c => new PuchPackage(c)).ToList();
                response.Message = "Kayıtlar gösteriliyor";
            }
            else
            {
                response.Message = "Kayıtlar bulunamadı";
            }
            return response;
        }

        // GET: api/PurcPackage/5
        public string Get(int id)
        {
            return "value";
        }
        // POST: api/PurcPackage/editP
        [Route("editp")]
        [HttpPost]
        public SiteResponse<object> EditP(PuchPackage pack)
        {
            var response = new SiteResponse<object>();
            response.Status = pack == null;
            if (response.Status)
            {
                response.Message = "Düzenlemek istenen paket tanımsız!";
                return response;
            }
            var oldCategory = db.PurchacingPackages.Find(pack.Id);
            response.Status = oldCategory == null;

            if (response.Status)
            {
                response.Message = "Düzenlemek istenen paket bulunamadı!";
                return response;
            }
            oldCategory.Id = pack.Id;
            oldCategory.PackageId = pack.PackageId;
            oldCategory.Adress = pack.Adress;
            oldCategory.TaxAdministration = pack.TaxAdministration;
            oldCategory.TaxNumber = pack.TaxNumber;
            oldCategory.CompanyName = pack.CompanyName;
            oldCategory.Member_Id = pack.Member_Id;
            db.Entry(oldCategory).State = EntityState.Modified;
            db.SaveChanges();
            response.Status = true;
            return response;
        }
        // POST: api/PurcPackage/addp
        [Route("addp")]
        [HttpPost]
        public SiteResponse<object> Addp(PuchPackage pack)
        {
            var dbCampaignSave = new Campaigns();
            Showcase dbshowSave = new Showcase();
            var response = new SiteResponse<object>();
            PurchacingPackages getpackage = new PurchacingPackages();
            var com = db.Companies.ToList();
            response.Status = pack == null;
            if (response.Status)
            {
                response.Message = "Bilgileri doldurunuz";
                return response;
            }              
                getpackage.PurchaseDate = DateTime.Now;
                getpackage.PackageId = pack.PackageId;
                getpackage.Member_Id =Convert.ToInt32( HttpContext.Current.Session["UserID"]);
                getpackage.Adress = pack.Adress;
                getpackage.TaxAdministration = pack.TaxAdministration;
                getpackage.TaxNumber = pack.TaxNumber;
                getpackage.CompanyName = pack.CompanyName;
                var camp = new CampaignView();               
                if (pack.PackageId == 1)
                {
                    var varmıCom = com.Where(c => c.CompanyName == pack.CompanyName).ToList().Any();
                    if (varmıCom)
                    {
                        camp.CompanyId = db.Companies.FirstOrDefault(s => s.CompanyName.Contains(pack.CompanyName)).Id;
                        var getComid = db.Companies.Find(camp.CompanyId);
                        if (camp.CompanyId != 0)
                        {                           
                            //dbCampaignSave.Id = 1;
                            dbCampaignSave.AddedDate = DateTime.Now;
                            dbCampaignSave.Companies_Id = getComid.Id;
                            dbCampaignSave.CampaignConditions = "yok";
                            dbCampaignSave.AdImage = "yok";
                            db.Entry(dbCampaignSave).State = EntityState.Added;
                            db.SaveChanges();
                        }
                }
                else
                {           
                        //dbCampaignSave.Id = 1;
                        dbCampaignSave.AddedDate = DateTime.Now;
                        dbCampaignSave.CampaignConditions = "yok";
                        dbCampaignSave.AdImage = "yok";
                        dbCampaignSave.CompanyName = pack.CompanyName;
                        db.Entry(dbCampaignSave).State = EntityState.Added;
                        db.SaveChanges();                  
                }
            }
                
            if (pack.PackageId == 2)
            {
                var varmıCom = com.Where(c => c.CompanyName == pack.CompanyName).ToList().Any();
                if (varmıCom)
                {
                    camp.CompanyId = db.Companies.FirstOrDefault(s => s.CompanyName.Contains(pack.CompanyName)).Id;
                    var getComid = db.Companies.Find(camp.CompanyId);
                    if (camp.CompanyId != 0)
                    {
                        //dbshowSave.Id = 1;
                        dbshowSave.AddedDate = DateTime.Now;
                        dbshowSave.Companies_Id = getComid.Id;
                        dbshowSave.CompanyName = pack.CompanyName;
                        db.Entry(dbCampaignSave).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else
                {
                    //dbshowSave.Id = 1;
                    dbshowSave.AddedDate = DateTime.Now;
                    dbshowSave.CompanyName = pack.CompanyName;
                    db.Entry(dbCampaignSave).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
                getpackage.BillingInfo = "yok";
                getpackage.Email = pack.Email;
                getpackage.Phone = pack.Phone;
                getpackage.Company_Id = pack.Company_Id;
                getpackage.isPurchasing = true;
                db.Entry(getpackage).State = EntityState.Added;
                db.SaveChanges();
                response.Status = getpackage.PackageId != 0;
                if (response.Status)
                return response;
            response.Message = "Paket eklenirken hata oluştu!";
            return response;
        }
        // DELETE: api/PurcPackage/deletePack/5
        [Route("deletePack/{id}")]
        [HttpGet]
        public SiteResponse<PuchPackage> DeletePack(int id)
        {
            var response = new SiteResponse<PuchPackage>();
            var purchpack = db.PurchacingPackages.Find(id);
            response.Status = purchpack != null;
            if (response.Status)
            {
                db.Entry(purchpack).State = EntityState.Deleted;
                db.SaveChanges();
                return response;
            }
            response.Message = $"Silinmesi istenen {id} no'lu kategori bulunamadı!";
            return response;
        }
    }
}
