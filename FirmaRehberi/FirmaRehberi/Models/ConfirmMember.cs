using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class ConfirmMember
    {
        public List<string> Email { get; set; }
        public List<bool> EmailConfirmed { get; set; }

        public ConfirmMember()
        {

        }

        public ConfirmMember(List<Members> mem)
        {
            List<string> membmail = new List<string>();
            List<bool> conmail = new List<bool>();

            foreach (var item in mem)
            {
                membmail.Add(item.Email);
                conmail.Add(item.EmailConfirmed);
            }


            Email = membmail.ToList();
            EmailConfirmed = conmail.ToList();
        }

    }
}
