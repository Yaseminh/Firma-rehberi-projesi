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
    [RoutePrefix("api/Campaigns")]
    public class CampaignsController : ApiController
    {
        myPanelDBEntities db = new myPanelDBEntities();
        // GET: api/Campaigns/get
        [Route("get")]
        [HttpGet]
        public SiteResponse<List<CampaignView>> Get()
        {
            myPanelDBEntities db = new myPanelDBEntities();
            var response = new SiteResponse<List<CampaignView>>();
            var allpack = db.PurchacingPackages.ToList();
            var com = db.Companies.ToList();
            response.Status = allpack.Any();
            if (response.Status)
            {
                //packageid=1 kampanyalar bölümü veritabanında olmayan kampanyaların company_idsi 0dır.
                response.Message = "Kampanyalar listeleniyor";
                var campaignInfo = new List<CampaignView>();
                foreach (var purchacing in allpack)
                {
                    var camp = new CampaignView();
                    if (purchacing.PackageId == 1)
                    {
                        var varmıCom = com.Where(c => c.CompanyName == purchacing.CompanyName).ToList().Any();
                        if (varmıCom)
                        {
                            camp.Adress = purchacing.Adress;
                            camp.CompanyName = purchacing.CompanyName;
                            camp.PackageId = purchacing.PackageId;
                            camp.AddedDate = purchacing.PurchaseDate;                          
                            camp.CompanyId = db.Companies.FirstOrDefault(s => s.CompanyName.Contains(purchacing.CompanyName)).Id;
                            campaignInfo.Add(camp);
                            var getComid = db.Companies.Find(camp.CompanyId);
                            var comidList = new List<Companies>();
                            //veritabanında kayıtlı olan firmalar
                            if (camp.CompanyId != 0)
                            {                              
                                var İnfoCamLists = new List<Company>();
                                var myCom = new Company(getComid);
                                İnfoCamLists.Add(myCom);
                                camp.mycom = İnfoCamLists.ToList();                               
                            }
                        }
                        else
                        {
                            camp.Adress = purchacing.Adress;
                            camp.CompanyName = purchacing.CompanyName;
                            camp.PackageId = purchacing.PackageId;
                            camp.AddedDate = purchacing.PurchaseDate;
                            campaignInfo.Add(camp);
                        }
                    }
                }
                response.Data = campaignInfo;
            }
            else
            {
                response.Message = "Listelenecek kampanya yok";
            }
            return response;
        }
        // POST: api/Campaigns/add
        [Route("add")]
        [HttpPost]
        public SiteResponse<object> Add(Campaign cam)
        {
            //kampanya ekleme
            var response = new SiteResponse<object>();
            response.Status = cam == null;
            if (response.Status)
            {
                response.Message = "Kampanya tanımsız";
                return response;
            }
            var newCampaign = new Campaigns();
            newCampaign.AddedDate = DateTime.Now;
            newCampaign.CompanyName = cam.CompanyName;
            newCampaign.Companies_Id = cam.Companies_Id;           
            db.Entry(newCampaign).State = EntityState.Added;
            db.SaveChanges();
            response.Status = newCampaign.Id != 0;
            if (response.Status)
            {
                response.Message = "Kampanya eklendi";
                return response;
            }
            else
            {
                response.Message = "Kampanya id tanımsız";
                return response;
            }
        }
        [Route("edit")]
        [HttpPost]
        public SiteResponse<object> Edit(Campaign campaign)
        {
            var response = new SiteResponse<object>();
            response.Status = campaign == null;
            if (response.Status)
            {
                response.Message = "Düzenlemek istenen kategori tanımsız!";
                return response;
            }
            var oldCampaign = db.Campaigns.Find(campaign.Id);
            response.Status = oldCampaign == null;

            if (response.Status)
            {
                response.Message = "Düzenlemek istenen kategori bulunamadı!";
                return response;
            }
            oldCampaign.Id = campaign.Id;
            oldCampaign.CompanyName = campaign.CompanyName;
            oldCampaign.ModifiedDate = DateTime.Now;
            db.Entry(oldCampaign).State = EntityState.Modified;
            db.SaveChanges();
            response.Status = true;
            return response;
        }
        // DELETE: api/Campaigns/deletecampaign/5
        [Route("deletecampaign/{id}")]
        [HttpGet]
        public SiteResponse<Campaign> Deletecampaign(int id)
        {
            var response = new SiteResponse<Campaign>();
            var compaign = db.Campaigns.Find(id);
            response.Status = compaign != null;
            if (response.Status)
            {
                db.Entry(compaign).State = EntityState.Deleted;
                db.SaveChanges();
                return response;
            }
            response.Message = $"Silinmesi istenen {id} no'lu firma bulunamadı!";
            return response;
        }
    }
}
