using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("Package")]
    public class Package : BaseModel
    {

        [StringLength(64)]
        [Index("UI_Name", 2, IsUnique = true)]
        public string Number { get; set; }


        public virtual IList<Part> Parts { get; set; }

        [ForeignKey("File")]
        public long? File_Id { get; set; }
        public virtual File File { get; set; }
    }
}