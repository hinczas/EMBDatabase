using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("File")]
    public class File : BaseModel
    {
        [StringLength(32)]
        public string File_Type { get; set; }
        public string Hash_Name { get; set; }
        [StringLength(512)]
        [Index("UI_Name", 2, IsUnique = true)]
        public string File_Path { get; set; }

        public virtual IList<Project> Projects { get; set; }
        public virtual IList<Part> Parts { get; set; }
        public virtual IList<Module> Modules { get; set; }
    }
}