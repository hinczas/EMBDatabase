using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EMBDatabase.Models
{
    public class ExportPart
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Number { get; set; }
        public string Keywords { get; set; }
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public int Quantity { get; set; }
        public int Pin_Count { get; set; }
        public decimal Price { get; set; }
        
        public long? Manufacturer_Id { get; set; }
        public long? Package_Id { get; set; }
        public long? Location_Id { get; set; }
        public long? Type_Id { get; set; }
    }
}