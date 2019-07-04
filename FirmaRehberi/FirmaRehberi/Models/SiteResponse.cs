using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class SiteResponse<T> where T : class, new()
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}