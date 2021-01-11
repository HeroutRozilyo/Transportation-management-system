using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace DO
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public bool UserExist { get; set; }
        public string MailAddress { get; set; }


    }
}
