using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetsORM.Entites;

namespace ProjetsORM.Persistence
{
    public class ProjetsORMContexte : DbContext
    {
        #region Propriétés DBSet
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<Projet> Projets { get; set; }
        public virtual DbSet<Client> Clients { get; set; }

        #endregion Propriétés DBSet
        //==============================================================================================
        #region Constructeur
        public ProjetsORMContexte(DbContextOptions<ProjetsORMContexte> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        #endregion Constructeur
        //==============================================================================================
        #region Configuration modèle
        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*** Entité Client ***/
            //Clé unique
            builder.Entity<Client>()
                    .HasIndex(c => c.NoEnregistrement)
                    .IsUnique();

            /*** Entité Employe ***/
            //Clé unique
            builder.Entity<Employe>()
                .HasIndex(e => e.NAS)
                .IsUnique();

            //Lien vers Employé: "Superviseur"
            builder.Entity<Employe>()
                .HasOne(emp => emp.Superviseur)                                //HasOne ? 0-1
                .WithMany(employes => employes.EmployesSupervises)
                .OnDelete(DeleteBehavior.Restrict);

            /*** Entité Projet ***/
            //Clé primaire
            builder.Entity<Projet>()
                .HasKey(projet => new { projet.NomProjet, projet.NomClient });

            //Lien vers Client
            builder.Entity<Projet>()
                .HasOne(projet => projet.Client)
                .WithMany(client => client.ListeProjets)
                .OnDelete(DeleteBehavior.Restrict);

            //Lienvers Employé¸: "Gestionnaire"
            builder.Entity<Projet>()
                .HasOne(projet => projet.EmployeGestionnaire)
                .WithMany(employe => employe.ProjetsGestionnaires)
                .OnDelete(DeleteBehavior.Restrict);                             //Delete Restrict sur pointillés ?

            //Lien vers Employé: "Contact_Client"
            builder.Entity<Projet>()
                .HasOne(projet => projet.ContactDuClient)
                .WithMany(employe => employe.ProjetsContactClient)
                .OnDelete(DeleteBehavior.Restrict);
        }
        #endregion  Configuration modèle
    }
}
