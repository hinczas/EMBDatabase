using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("Project")]
    public class Project : BaseModel
    {        
        [StringLength(50)]
        [Index("UI_Name", 2, IsUnique = true)]
        public string Code { get; set; }
        [StringLength(50)]
        [Index("UI_Name", 3, IsUnique = true)]
        public string Version { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Log { get; set; }
        public string Folder_Location { get; set; }




        public virtual IList<File> File { get; set; }
        public virtual IList<Part> Part { get; set; }
        public virtual IList<Module> PCB { get; set; }


    }
}