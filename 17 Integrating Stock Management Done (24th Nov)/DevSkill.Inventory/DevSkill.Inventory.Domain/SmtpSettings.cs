﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain
{
    public class SmtpSettings
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public SmtpEncryptionTypes SmtpEncryption { get; set; }
    }
}
