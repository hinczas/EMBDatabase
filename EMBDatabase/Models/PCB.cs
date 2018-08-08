using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("PCB")]
    public class PCB : BaseModel
    {
        public string Version { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Total_Cost { get; set; }
        public string Changelog { get; set; }



        public virtual IList<File> File { get; set; }
        public virtual IList<Part> Part { get; set; }
        public virtual IList<Project> Project { get; set; }
    }
}