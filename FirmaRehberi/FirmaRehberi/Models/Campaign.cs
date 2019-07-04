using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public string CampaignConditions { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddImage { get; set; }
        public int ? Companies_Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public Campaign()
        {

        }
        public Campaign(Campaigns camp)
        {
            Id = camp.Id;
            CampaignConditions = camp.CampaignConditions;
            AddedDate = camp.AddedDate;
            AddImage = camp.AdImage;
            Companies_Id = camp.Companies_Id;
            CompanyName = camp.CompanyName;
            ModifiedDate = camp.ModifiedDate;         
        }
    }
}