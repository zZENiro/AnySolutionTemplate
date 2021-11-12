using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application._Common.Options
{
    public class AuthOptions
    {
        public string IssuerKey { get; set; }

        public string Authority { get; set; }

        public string Audience { get; set; }

        public int Lifetime { get; set; }

        public string Issuer { get; set; }
    }
}
