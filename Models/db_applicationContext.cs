

namespace Personnel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
   
    public class db_applicationContext :DbContext
    {

        public db_applicationContext() : base("name=SearchingApp")
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professeur>()
                .Property(e => e.nom)
                .IsUnicode(false);
            modelBuilder.Entity<Cv_Prof>()
                .Property(e => e.apropos)
                .IsUnicode(false);
            modelBuilder.Entity<autres_experience>();
            modelBuilder.Entity<competence>();
            modelBuilder.Entity<exprerience_professionnelle>();
            modelBuilder.Entity<formation_etude>();
            modelBuilder.Entity<Contact>();

        }
        //entities
        public DbSet<Professeur> professeurs { get; set; }
        public DbSet<Cv_Prof> cvs { get; set; }
        public DbSet<autres_experience> autres_experiences { get; set; }
        public DbSet<competence> competences { get; set; }
        public DbSet<exprerience_professionnelle> exprerience_professionnelles { get; set; }
        public DbSet<formation_etude> formation_etudes { get; set; }
        public DbSet<Contact> contacts { get; set; }



    }
}