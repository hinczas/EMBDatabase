﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("Location")]
    public class Location : BaseModel
    {
        public virtual IList<Part> Parts { get; set; }

        [ForeignKey("File")]
        public long? File_Id { get; set; }
        public virtual File File { get; set; }
    }
}