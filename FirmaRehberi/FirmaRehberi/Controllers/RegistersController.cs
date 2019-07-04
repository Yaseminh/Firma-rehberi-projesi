using FirmaRehberi.db;
using FirmaRehberi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace FirmaRehberi.Controllers
{
    [RoutePrefix("api/registers")]
    public class RegistersController : ApiController
    {
        string mailadresdondur = "";
        public static int kullaniciID = 0;
        myPanelDBEntities db = new myPanelDBEntities();
        List<string> membmail = new List<string>();
        List<bool> conmail = new List<bool>();
        // GET: api/Registers
        public SiteResponse<List<Member>> Get()
        {            
            //Tüm kişiler listesi
            var response = new SiteResponse<List<Member>>();
            var list = db.Members.ToList();
            response.Status = list.Any();
            if (response.Status)
            {
                response.Data = list.Select(m => new Member(m)).ToList();
                response.Message = "Kayıtlar gösteriliyor";
            }
            else
            {
                response.Message = "Kayıtlar bulunamadı";
            }
            return response;
        }
        // GET: api/Registers/getDetail/9
        [Route("getDetail/{id}")]
        public SiteResponse<Member> GetDetail(int id)
        {
            //seçilen kişi
            var response = new SiteResponse<Member>();
            var getmemid = db.Members.Find(id);
            response.Status = getmemid != null;
            if (response.Status)
            {
                response.Data = new Member(getmemid);
                response.Message = $"Kişi {id} gösterildi";
            }
            else
            {
                response.Message = $"Kişi {id} bulunamadı";
            }
            return response;
        }
        [Route("searchCity/{city}")]
        [HttpGet]
        //GET: api/Registers/SearchCity/istanbul
        public SiteResponse<List<Member>> SearchCity(string city)
        {
            var response = new SiteResponse<List<Member>>();
            var memberlists = db.Members.ToList();
            response.Status = memberlists.Any();
            if (response.Status)
            {
                response.Data = memberlists.Where(c => c.City == city).Select(m => new Member(m)).ToList();
                response.Message = $"Aradığınız {city} de kişi veya kişiler bulundu";
            }
            else
            {
                response.Message = $"Aradığınız {city} de kişi bulunamadı";
            }
            return response;
        }
        // POST: api/Registers/add
        [Route("add")]
        [HttpPost]
        public SiteResponse<object> Add(Member member)
        {
            //kişi ekleme
            var response = new SiteResponse<object>();
            response.Status = member == null;
            if (response.Status)
            {
                response.Message = "Üye tanımsız";
                return response;
            }
            var newMember = new Members();
            newMember.Id = member.Id;
            newMember.Name = member.Name;
            newMember.Surname = member.Surname;
            newMember.Password = member.Password;
            newMember.Email = member.Email;
            newMember.City = member.City;
            newMember.AddedDate = DateTime.Now;
            var emailzatenkayıtlı = db.Members.Where(m => m.Email == member.Email).Any();
            if (!emailzatenkayıtlı)
            {
                db.Entry(newMember).State = EntityState.Added;
                db.SaveChanges();
            }
            response.Status = newMember.Id != 0;
            if (response.Status)
            {
                response.Message = "Kişi eklendi";
                return response;
            }
            else
            {
                response.Message = "Kişi id tanımsız";
                return response;
            }
        }
        // GET: api/registers/sendconfirmationmail
        [Route("sendconfirmationmail")]
        [HttpGet]
        public  SiteResponse<Members> Sendconfirmationmail(string input_email, string password, string Name, string Surname)
        {
            //mail gönderme
            //veritabanında var mı yok mu
            SiteResponse<Members> response = new SiteResponse<Members>();
            var emailzatenkayıtlı = db.Members.Where(m => m.Email == input_email).Any();
            if (!emailzatenkayıtlı)
            {
                int confirmation;
                Random rnd = new Random();
                confirmation = rnd.Next(10000, 99999);
                string input_name = $"http://localhost:49906/api/registers/GetConfirmationMail?confirmation={confirmation}&mailName={input_email}&password={password}&Name={Name}&surname={Surname}";
                string input_message = "Doğrulama Ekranı";
                SmtpClient client = new SmtpClient();
                MailAddress from = new MailAddress("yaseminsolmaz18@gmail.com");
                MailAddress to = new MailAddress(input_email);//bizim mail adresi
                MailMessage msg = new MailMessage(from, to);
                msg.IsBodyHtml = true;
                msg.Subject = "E-mail adresinizi doğrulayın";
                msg.Body += "Gönderen Mail Adresi " + " yaseminsolmaz18@gmail.com | <a href=\"" + input_name + "\">Doğrulamak için tıklayın</ID></a>"; //burada başında gönderen kişinin mail adresi geliyor.                                                                                                                                                      //msg.CC.Add(input_email);//herkes görür
                NetworkCredential info = new NetworkCredential("yaseminsolmaz18@gmail.com", "solmaz123");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Credentials = info;
                client.Send(msg);
                Members sendMailMembers = new Members();
                sendMailMembers.Email = input_email;
                response.Data = sendMailMembers;
                response.Status = true;
                response.Message = "Kullanıcıya mail göndeildi";               
                //request yapılan
                //Request.RequestUri.GetLeftPart(UriPartial.Authority);
            } else{
                response.Message = "E-posta gönderilirken hata oldu";
                response.Status = false;
            }
            return response;
        }
        // GET: api/registers/getConfirmationMail
        [Route("getConfirmationMail")]
        [HttpGet]
        public  SiteResponse<Members> GetConfirmationMail(int confirmation ,string mailName,string password, string Name, string surname)
        {
            var response = new SiteResponse<Members>();
            var myresponse = Request.CreateResponse(HttpStatusCode.Moved);
            //mail doğrulandı
            var emailzatenkayıtlı = db.Members.Where(m => m.Email == mailName).Any();
            if (mailName != null && !emailzatenkayıtlı)
            {
                //kullanıcı veritabanına eklenebilir.
                response.Status = true;
                response.Message = "Kullanıcılar linke tıkladılar.";
                var newMember = new Members();
                newMember.Email = mailName;
                newMember.Password = password;
                newMember.Name = Name;
                newMember.Surname = surname;
                newMember.AddedDate = DateTime.Now;
                newMember.EmailConfirmed = true;
                newMember.TwoFactorEnabled = false;
                newMember.PhoneNumberConfirmed = "yok";
                newMember.LockoutEnabled = false;
                newMember.AcessFailedCount = 0;
                db.Entry(newMember).State = EntityState.Added;
                db.SaveChanges();
                response.Data = newMember;
                response.Status = true;
                response.Message = "Confirm olan kullanıcı";
                myresponse.Headers.Location = new Uri("http://localhost:49906/");            
            }                        
            return response;
        }
        // GET: api/registers/forgatPassword
        [Route("ForgatPassword")]
        [HttpGet]
        public SiteResponse<Members> ForgatPassword(string input_email)
        {
            SiteResponse<Members> response = new SiteResponse<Members>();
            if (input_email != null)
            {
                int confirmation;
                Random rnd = new Random();
                confirmation = rnd.Next(10000, 99999);
                string input_name = $"http://localhost:49906/api/registers/SendForgatPassword?mailaddress={input_email}&confirmation={confirmation}";
                string input_message = "Doğrulama Ekranı";
                SmtpClient client = new SmtpClient();
                MailAddress from = new MailAddress("yaseminsolmaz18@gmail.com");
                MailAddress to = new MailAddress(input_email);//bizim mail adresi
                MailMessage msg = new MailMessage(from, to);
                msg.IsBodyHtml = true;
                msg.Subject = "Şifre değiştirme";
                msg.Body += "Gönderen Mail Adresi " + " yaseminsolmaz18@gmail.com | <a href=\"" + input_name + "\">Parolanızı değiştirmek için tıklayınız</ID></a>"; //burada başında gönderen kişinin mail adresi geliyor
                                                                                                                                                                     //msg.CC.Add(input_email);//herkes görür
                NetworkCredential info = new NetworkCredential("yaseminsolmaz18@gmail.com", "solmaz123");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Credentials = info;
                client.Send(msg);
                Members sendMailMembers = new Members();
                sendMailMembers.Email = input_email;
                response.Data = sendMailMembers;
                response.Status = true;
                response.Message = "Kullanıcı şifre değiştirmek istedi";
            }
            else
            {
                response.Message = "Şifre değiştirme işleminde hata oldu";
                response.Status = false;
            }
            return response;
        }
        // GET: api/registers/sendForgatPassword
        [Route("SendForgatPassword")]
        [HttpGet]
        public HttpResponseMessage SendForgatPassword(int confirmation, string mailaddress)
        {
            //artık parola değiştirilebilir           
             var newParolaId = db.Members.FirstOrDefault(f => f.Email.Contains(mailaddress)).Id;
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri($"http://localhost:49906/api/registers/NewPassword?mailaddress={mailaddress}&confirmation={confirmation}&password=newPassword&id={newParolaId}");
            return response;
        }
        // GET: api/registers/newPassword
        [Route("NewPassword")]
        [HttpGet]
        public SiteResponse<Members> NewPassword(int confirmation, string mailaddress, string password, int id)
        {
            var response = new SiteResponse<Members>();
            var emailvarmi =   db.Members.Where(m => m.Email == mailaddress).Any();
            if (emailvarmi && password!="newPassword")
            {
                var newParola = db.Members.Find(id);                
                newParola.Password = db.Members.FirstOrDefault(s => s.Email.Contains(mailaddress)).Password = password;
                newParola.ModifiedDate = DateTime.Now;
                newParola.Name = db.Members.FirstOrDefault(n => n.Email.Contains(mailaddress)).Name;
                newParola.Surname = db.Members.FirstOrDefault(s => s.Email.Contains(mailaddress)).Surname;
                newParola.AddedDate = db.Members.FirstOrDefault(a => a.Email.Contains(mailaddress)).AddedDate;
                newParola.EmailConfirmed = true;
                newParola.PhoneNumberConfirmed = "yok";
                newParola.TwoFactorEnabled = false;
                newParola.LockoutEnabled = false;
                newParola.AcessFailedCount = 0;
                newParola.ModifiedDate = DateTime.Now;
                db.Entry(newParola).State = EntityState.Modified;
                db.SaveChanges();
                response.Data = newParola;
                response.Status = true;
                response.Message = "Confirm passwpord olan kullanıcı";
            }
            else{ } 
            return response;

        }
        // PUT: api/Registers/edit
        [Route("edit")]
        [HttpPost]
        public SiteResponse<object> Edit(Member member)
        {
            var response = new SiteResponse<object>();
            response.Status = member == null;
            if (response.Status)
            {
                response.Message = "Düzenlemek istenen kişi tanımsız!";
                return response;
            }
            var oldMember = db.Members.Find(member.Id);
            response.Status = oldMember == null;

            if (response.Status)
            {
                response.Message = "Düzenlemek istenen kişi bulunamadı!";
                return response;
            }
            oldMember.Id = member.Id;
            oldMember.Name = member.Name;
            oldMember.Surname = member.Surname;
            oldMember.Email = member.Email;
            oldMember.Password = member.Password;
            oldMember.City = member.City;
            oldMember.Gender = member.Gender;
            oldMember.ModifiedDate = DateTime.Now;
            db.Entry(oldMember).State = EntityState.Modified;
            db.SaveChanges();
            response.Status = true;
            return response;
        }
        // DELETE: api/Registers/deletecategory/5
        [Route("deletecategory/{id}")]
        [HttpGet]
        public SiteResponse<Member> Deletecategory(int id)
        {
            var response = new SiteResponse<Member>();
            var member = db.Members.Find(id);
            response.Status = member != null;
            if (response.Status)
            {
                db.Entry(member).State = EntityState.Deleted;
                db.SaveChanges();
                return response;
            }
            response.Message = $"Silinmesi istenen {id} no'lu kategori bulunamadı!";
            return response;
        }
    }
}
