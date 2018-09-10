using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("Module")]
    public class Module : BaseModel
    {
        [StringLength(16)]
        [Index("UI_Name", 2, IsUnique = true)]
        public string Version { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Total_Cost { get; set; }
        public string Changelog { get; set; }


        [ForeignKey("Type")]
        public long? Type_Id { get; set; }
        public virtual Type Type { get; set; }

        public virtual IList<File> Files { get; set; }
        public virtual IList<Part> Parts { get; set; }
        public virtual IList<Project> Projects { get; set; }
    }
}