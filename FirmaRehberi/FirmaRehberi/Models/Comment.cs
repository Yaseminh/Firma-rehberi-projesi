using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Member_Id { get; set; }
        public int Company_Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public Comment()
        {

        }
        public Comment(Comments comment)
        {
            Id = comment.Id;
            Text = comment.Text;
            Member_Id = comment.Member_Id;
            Company_Id = comment.Company_Id;
            AddedDate = comment.AddedDate;
            ModifiedDate = comment.ModifiedDate;
        }
    }
}