﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("Package")]
    public class Package : BaseModel
    {

        [StringLength(50)]
        public string Number { get; set; }



        public virtual File File { get; set; }
    }
}