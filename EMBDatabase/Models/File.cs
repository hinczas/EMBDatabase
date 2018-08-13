using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("File")]
    public class File : BaseModel
    {
        [StringLength(50)]
        public string File_Type { get; set; }
        public string Hash_Name { get; set; }
        public string File_Path { get; set; }

        public virtual IList<Project> Project { get; set; }
        public virtual IList<Part> Part { get; set; }
        public virtual IList<PCB> PCB { get; set; }
    }
}