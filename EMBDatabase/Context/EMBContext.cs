using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EMBDatabase.Models;

namespace EMBDatabase.Context
{
    public class EMBContext : DbContext
    {
        public EMBContext()
            : base("EmbConnection")
        {
            Database.SetInitializer<EMBContext>(null);
        }

        public virtual DbSet<Part> Part { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PCB> PCB { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Models.Type> Type { get; set; }
    }
}