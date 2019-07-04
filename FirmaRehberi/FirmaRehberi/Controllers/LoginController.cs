using FirmaRehberi.db;
using FirmaRehberi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace FirmaRehberi.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController 
    {

        myPanelDBEntities db = new myPanelDBEntities();
        //GET: api/Login/userLogin
        
        [Route("UserLogin")]
        [HttpGet]
        public  SiteResponse<Member> UserLogin(Member member)
        {
            var response = new SiteResponse<Member>();
            var getid = db.Members.Find(member.Id);         
            response.Status = member == null;
            if (response.Status)
            {
                response.Message = "Bilgiler girilmedi";
                return response;
            }
            var User = db.Members.ToList();
            var kullaniciGet = User.Where(u => u.Password == member.Password && u.Email == member.Email).Any();
            if (kullaniciGet)
            {
                response.Status = true;
                response.Data = new Member(getid);
                HttpContext.Current.Session["UserID"] = member.Id;         
                var sessionıD = HttpContext.Current.Session["UserID"] ;
                response.Message = "Giriş Yapıldı";
            }
            return response;
        }
       
    }
}
