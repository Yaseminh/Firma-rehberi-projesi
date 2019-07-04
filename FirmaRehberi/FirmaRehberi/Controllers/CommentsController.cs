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
    [RoutePrefix("api/Comments")]
    public class CommentsController : ApiController
    {
        myPanelDBEntities db = new myPanelDBEntities();
        // GET: api/Comments      
        [Route("getComments/{id}")]
        [HttpGet]
        //GET: api/Comments/getComments/3
        public SiteResponse<List<Comment>> GetComments(int id)
        {
            var response = new SiteResponse<List<Comment>>();
            var commentlists = db.Comments.ToList();
            response.Status = commentlists.Any();
            if (response.Status)
            {
                response.Data = commentlists.Where(c => c.Company_Id == id).Select(c => new Comment(c)).ToList();
                response.Message = $"Aradığınız {id} comment veya commentler bulundu";
            }
            else
            {
                response.Message = $"Aradığınız {id} company commenti bulunamadı";
            }
            return response;
        }
        //POST:api/Comments/addPost
        [Route("addPost")]
        [HttpPost]
        public SiteResponse<Comment> AddPost(Comment comment)
        {
            var response = new SiteResponse<Comment>();
            Comments com = new Comments();
            response.Status = comment == null;
            if (response.Status)
            {
                response.Message = "Yorum eklenemedi";
                return response;
            }
            //com.Member_Id = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                com.Member_Id = comment.Member_Id; 
                com.Company_Id = comment.Company_Id;
                com.AddedDate = DateTime.Now;
                com.Text = comment.Text;
                com.ModifiedDate = null;
                db.Entry(com).State = EntityState.Added;
                db.SaveChanges();
                response.Message = "Yorum eklendi";
                response.Status = true;                 
            response.Status = com.Id != 0;
            if (response.Status)
            {
                return response;
            }
            else
            {
                response.Message = "Yorum eklenirken hata oldu";
                return response;
            }
        }
        // POST: api/Comments/edit
        [Route("edit")]
        [HttpPost]
        public SiteResponse<object> Edit(Comment comment)
        {
            var response = new SiteResponse<object>();
            response.Status = comment == null;
            if (response.Status)
            {
                response.Message = "Düzenlemek istenen comment tanımsız!";
                return response;
            }
            var oldComment = db.Comments.Find(comment.Id);
            response.Status = oldComment == null;

            if (response.Status)
            {
                response.Message = "Düzenlemek istenen comment bulunamadı!";
                return response;
            }
            oldComment.Id = comment.Id;
            oldComment.Member_Id = comment.Member_Id;
            oldComment.Company_Id = comment.Company_Id;
            oldComment.ModifiedDate = DateTime.Now;
            oldComment.Text = comment.Text;
            db.Entry(oldComment).State = EntityState.Modified;
            db.SaveChanges();
            response.Status = true;
            return response;
        }
        // DELETE: api/Comments/deletecomment/10
        [Route("deletecomment/{id}")]
        [HttpGet]
        public SiteResponse<Comment> Deletecomment(int id)
        {
            var response = new SiteResponse<Comment>();
            var comment = db.Comments.Find(id);
            response.Status = comment != null;
            if (response.Status)
            {
                db.Entry(comment).State = EntityState.Deleted;
                db.SaveChanges();
                return response;
            }
            response.Message = $"Silinmesi istenen {id} no'lu comment bulunamadı!";
            return response;
        }
    }
}
