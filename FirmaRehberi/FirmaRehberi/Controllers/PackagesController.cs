using FirmaRehberi.db;
using FirmaRehberi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirmaRehberi.Controllers
{
    [RoutePrefix("api/Packages")]
    public class PackagesController : ApiController
    {
        myPanelDBEntities db = new myPanelDBEntities();
        // GET: api/Packages
        [Route("get")]
        [HttpGet]
        public SiteResponse<List<Package>> Get()
        {
            var response = new SiteResponse<List<Package>>();
            var allpack = db.Packages.ToList();
            response.Status = allpack.Any();
            if (response.Status)
            {
                response.Data = allpack.Select(c => new Package(c)).ToList();
                response.Message = "Paketler listelendi";
            }
            else
            {
                response.Message = "Paketler Listelenemedi";
            }
            return response;
        }

        // GET: api/Packages/5
        [Route("getid")]
        [HttpGet]
        public SiteResponse<Package> Getid(int id)
        {
            var response = new SiteResponse<Package>();
            var getid = db.Packages.Find(id);
            response.Status = getid != null;
            if (response.Status)
            {
                response.Data= new Package(getid);
                response.Message = $"Paket {id} detay";
            }
            else
            {
                response.Message = $"Paket {id} bulunamadı";
            }
            return response;

        }

        // POST: api/Packages
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Packages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Packages/5
        public void Delete(int id)
        {
        }
    }
}
