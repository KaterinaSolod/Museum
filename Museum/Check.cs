using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museum
{
    public class Check
    {
        public string Login { get; set; }
        public bool Utype { get; }

        public string Status => Utype ? "Адміністратор" : "Користувач";

        public Check(string login, bool utype)
        {
            Login = login.Trim();
            Utype = utype;
        }
    }
}
