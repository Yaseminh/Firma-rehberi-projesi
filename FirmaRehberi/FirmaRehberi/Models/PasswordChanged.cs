using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class PasswordChanged
    {
        public List<string> Password { get; set; }
        public List<string> Name { get; set; }
        public List<string> Email { get; set; }
        public PasswordChanged()
        {

        }
        public PasswordChanged(List<Members> mem)
        {
            List<string> password = new List<string>();
            List<string> name = new List<string>();
            List<string> email = new List<string>();
            foreach (var item in mem)
            {
                if (item.ModifiedDate != null)
                {
                    password.Add(item.Password);
                    name.Add(item.Name);
                    email.Add(item.Email);
                }
            }

            Password = password.ToList();
            Name = name.ToList();
            Email = email.ToList();
        }
    }
}