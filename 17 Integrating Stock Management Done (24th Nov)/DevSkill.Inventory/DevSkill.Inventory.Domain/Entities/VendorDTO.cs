﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class VendorDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";
    }
}
