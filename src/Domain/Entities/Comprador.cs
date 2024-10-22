﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comprador:Usuario
    {
        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;
    }
}
