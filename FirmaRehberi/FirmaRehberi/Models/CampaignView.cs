using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class CampaignView
    {
        public int PackageId { get; set; }
        public string CompanyName { get; set; }
        public string Adress { get; set; }
        public DateTime AddedDate { get; set; }
        public int CompanyId { get; set;}
        public List<Company> mycom { get; set; }
        public CampaignView()
        {

        }
        public CampaignView(PurchacingPackages pack)
        {
            
        }
    }
}