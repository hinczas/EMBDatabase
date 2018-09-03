using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMBDatabase.Models
{
    [Table("Manufacturer")]
    public class Manufacturer : BaseModel
    {
        public string Full_Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public virtual IList<Part> Parts { get; set; }

        [ForeignKey("File")]
        public long? File_Id { get; set; }
        public virtual File File { get; set; }
    }
}