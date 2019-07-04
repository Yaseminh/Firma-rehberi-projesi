using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserBy { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public string MainCategoryName { get; set; }

        public Category()
        {

        }

        public Category(Categories cat)
        {
            ID = cat.Id;
            Name = cat.Name;
            Description = cat.Description;
            UserBy = cat.UserBy;
            AddedDate = cat.AddedDate;
            ModifiedDate = cat.ModifiedDate;
            MainCategoryName = cat.MainCategoryName;
        }
    }
}