using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class ShowcaseView
    {
        public int PackageId { get; set; }
        public int CompanyId { get; set; }
        public DateTime AddedDate { get; set; }
        public int? OrderNumber { get; set; }
        public string CompanyName { get; set; }
        public string Adress { get; set; }
        public List<Company> mycom { get; set; }
        public ShowcaseView()
        {

        }
    }
}