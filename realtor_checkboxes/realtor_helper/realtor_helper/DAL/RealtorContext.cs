using realtor_helper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace realtor_helper.DAL
{
    public class RealtorContext : DbContext
    {

        public RealtorContext() : base("RealtorContext")
        {
            
        }

        public DbSet<HouseListing> Houses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Feature> Features { get; set; }

       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
        } */

    }
}