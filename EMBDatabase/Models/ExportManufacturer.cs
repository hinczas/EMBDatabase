using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EMBDatabase.Models
{
    public class ExportManufacturer
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Full_Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}