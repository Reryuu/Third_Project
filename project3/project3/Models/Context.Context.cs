﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project3.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class project3Entities : DbContext
    {
        public project3Entities()
            : base("name=project3Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<applicant> applicants { get; set; }
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<description> descriptions { get; set; }
        public virtual DbSet<details_interview> details_interview { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<vacancy> vacancies { get; set; }
    }
}
