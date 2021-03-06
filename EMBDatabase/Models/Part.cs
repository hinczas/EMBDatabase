﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EMBDatabase.Models
{
    [Table("Part")]
    public class Part : BaseModel
    {

        [StringLength(64)]
        [Index("UI_Name", 2, IsUnique = true)]
        public string Number { get; set; }
        public string Keywords { get; set; }
        public decimal? Voltage { get; set; }
        public decimal? Current { get; set; }
        public int Quantity { get; set; }
        public int? Pin_Count { get; set; }
        public decimal Price { get; set; }


        [ForeignKey("Manufacturer")]
        public long? Manufacturer_Id { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [ForeignKey("Package")]
        public long? Package_Id { get; set; }
        public virtual Package Package { get; set; }
        [ForeignKey("Location")]
        public long? Location_Id { get; set; }
        public virtual Location Location { get; set; }
        [ForeignKey("Type")]
        public long? Type_Id { get; set; }
        public virtual Type Type { get; set; }

        public virtual IList<Module> Modules { get; set; }
        public virtual IList<File> Files { get; set; }
        public virtual IList<Project> Projects { get; set; }

    }
}