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
        public string Code { get; set; }
        [StringLength(50)] 
        public string Version { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Log { get; set; }
        public string Folder_Location { get; set; }




        public virtual IList<File> File { get; set; }
        public virtual IList<Part> Part { get; set; }
        public virtual IList<PCB> PCB { get; set; }


    }
}