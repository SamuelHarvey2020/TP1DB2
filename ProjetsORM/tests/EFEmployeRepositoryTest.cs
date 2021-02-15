using Microsoft.EntityFrameworkCore;
using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProjetsORM.AccesDonnees
{
    public class EFEmployeRepositoryTest
    {
        private EFEmployeRepository repoEmploye;

        private void CreerContexteEtReposDeTests()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "employe_db");   // Database en mémoire
            var contexte = new ProjetsORMContexte(builder.Options);
            repoEmploye = new EFEmployeRepository(contexte);
        }

        [Fact]
        public void AjouterEmploye_DoitAjouterEmploye()
        {

            CreerContexteEtReposDeTests();
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };

            repoEmploye.AjouterEmploye(samuel);

            Assert.Equal(samuel, repoEmploye.ObtenirEmploye(samuel.NoEmploye));

        }

        [Fact]
        public void ObtenirEmploye_QuandEmployeExistePas()
        {
            CreerContexteEtReposDeTests();
            short numeroEmployeARechercher = 4;
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe tibo = new Employe { NoEmploye = 2, NAS = 000000002, Nom = "Harvey", Prenom = "Tibo", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe marnie = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25) };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(tibo);
            repoEmploye.AjouterEmploye(marnie);

            Assert.Throws<ArgumentException>(() => repoEmploye.ObtenirEmploye(numeroEmployeARechercher));

        }

        
        [Fact]
        public void ObtenirEmploye_QuandEmployeExiste()
        {
            CreerContexteEtReposDeTests();
            short numeroEmployeARechercher = 2;
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe tibo = new Employe { NoEmploye = numeroEmployeARechercher, NAS = 000000002, Nom = "Harvey", Prenom = "Tibo", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe marnie = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25) };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(tibo);
            repoEmploye.AjouterEmploye(marnie);

            Employe employeTrouve = repoEmploye.ObtenirEmploye(numeroEmployeARechercher);

            Assert.Equal(tibo, employeTrouve);
            Assert.Same(tibo, employeTrouve);
        }
        
        [Fact]
        public void RechercherTousEmployes_QuandAucunEmploye()
        {
            CreerContexteEtReposDeTests();
            ICollection<Employe> employesTrouves = repoEmploye.RechercherTousEmployes();

            Assert.Empty(employesTrouves);
        }
        
        [Fact]
        public void RechercherTousEmployes_QuandUnEmploye()
        {
            CreerContexteEtReposDeTests();
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            repoEmploye.AjouterEmploye(samuel);
            ICollection<Employe> employes = new List<Employe> { samuel };

            ICollection<Employe> employesTrouves = repoEmploye.RechercherTousEmployes();

            Assert.Equal(employes, employesTrouves);
        }
        
        [Fact]
        public void RechercherTousEmployes_QuandPlusieursEmployes()
        {
            CreerContexteEtReposDeTests();
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe tibo = new Employe { NoEmploye = 2, NAS = 000000002, Nom = "Harvey", Prenom = "Tibo", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe marnie = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25) };
            ICollection<Employe> employes = new List<Employe> { samuel, tibo, marnie };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(tibo);
            repoEmploye.AjouterEmploye(marnie);

            ICollection<Employe> employesTrouves = repoEmploye.RechercherTousEmployes();

            Assert.Equal(employes, employesTrouves);
        }
        
        [Fact]
        public void RechercherTousEmployesParNom_QuandAucunEmploye()
        {
            CreerContexteEtReposDeTests();
            string nomEmployeARechercher = "Ackerman";
            string prenomEmployeARechercher = "Mikasa";
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe tibo = new Employe { NoEmploye = 2, NAS = 000000002, Nom = "Harvey", Prenom = "Tibo", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe marnie = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25) };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(tibo);
            repoEmploye.AjouterEmploye(marnie);

            ICollection<Employe> employesTrouves = repoEmploye.RechercherEmployesParNom(nomEmployeARechercher, prenomEmployeARechercher);

            Assert.Empty(employesTrouves);
        }
        
        [Fact]
        public void RechercherTousEmployesParNom_QuandUnEmploye()
        {
            CreerContexteEtReposDeTests();
            string nomEmployeARechercher = "Harvey";
            string prenomEmployeARechercher = "Samuel";
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe tibo = new Employe { NoEmploye = 2, NAS = 000000002, Nom = "Harvey", Prenom = "Tibo", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe marnie = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25) };
            ICollection<Employe> employes = new List<Employe> { samuel };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(tibo);
            repoEmploye.AjouterEmploye(marnie);

            ICollection<Employe> employesTrouves = repoEmploye.RechercherEmployesParNom(nomEmployeARechercher, prenomEmployeARechercher);

            Assert.Equal(employes, employesTrouves);

        }
        
        [Fact]
        public void RechercherTousEmployesParNom_QuandPlusieursEmployes()
        {
            CreerContexteEtReposDeTests();
            string nomEmployeARechercher = "Harvey";
            string prenomEmployeARechercher = "Samuel";
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe samuel2 = new Employe { NoEmploye = 2, NAS = 000000002, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe samuel3 = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            ICollection<Employe> employes = new List<Employe> { samuel, samuel2, samuel3 };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(samuel2);
            repoEmploye.AjouterEmploye(samuel3);

            ICollection<Employe> employesTrouves = repoEmploye.RechercherEmployesParNom(nomEmployeARechercher, prenomEmployeARechercher);

            Assert.Equal(employes, employesTrouves);
        }
        
        [Fact]
        public void RechercherTousSuperviseurs_QuandAucunSuperviseur()
        {
            CreerContexteEtReposDeTests();
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = null };
            repoEmploye.AjouterEmploye(samuel);

            ICollection<Employe> superviseursTrouves = repoEmploye.RechercherTousSuperviseurs();

            Assert.Empty(superviseursTrouves);
        }
        
        [Fact]
        public void RechercherTousSuperviseurs_QuandUnSuperviseur()
        {
            CreerContexteEtReposDeTests();
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = 3 };
            Employe marnie = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = null};
            ICollection<Employe> superviseurs = new List<Employe> { marnie };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(marnie);

            ICollection<Employe> superviseursTrouves = repoEmploye.RechercherTousSuperviseurs();

            Assert.Equal(superviseurs, superviseursTrouves);
        }
        /*
        [Fact]
        public void RechercherTousSuperviseurs_QuandPlusieursSuperviseurs()
        {
            CreerContexteEtReposDeTests();
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe tibo = new Employe { NoEmploye = 2, NAS = 000000002, Nom = "Harvey", Prenom = "Tibo", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = 1 };
            Employe marnie = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = 1 };
            Employe jean = new Employe { NoEmploye = 4, NAS = 000000004, Nom = "Harvey", Prenom = "Jean", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe levi = new Employe { NoEmploye = 5, NAS = 000000005, Nom = "Harvey", Prenom = "Levi", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = 4 };
            ICollection<Employe> superviseurs = new List<Employe> { samuel, jean };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(tibo);
            repoEmploye.AjouterEmploye(marnie);
            repoEmploye.AjouterEmploye(jean);
            repoEmploye.AjouterEmploye(levi);

            ICollection<Employe> superviseursTrouves = repoEmploye.RechercherTousSuperviseurs();

            Assert.Equal(superviseurs, superviseursTrouves);
        }
        */
        [Fact]
        public void ObtenirEmployesSupervises_QuandAucunEmployeSupervise()
        {
            CreerContexteEtReposDeTests();
            short noSuperviseur = 3;
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25) };
            Employe marnie = new Employe { NoEmploye = noSuperviseur, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25) };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(marnie);

            Assert.Throws<ArgumentException>(() => repoEmploye.ObtenirEmployesSupervises(noSuperviseur));

        }

        [Fact]
        public void ObtenirEmployesSupervises_QuandUnEmployeSupervise()
        {
            CreerContexteEtReposDeTests();
            short noSuperviseur = 3;
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = 3 };
            Employe marnie = new Employe { NoEmploye = noSuperviseur, NAS = 000000003, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = null };
            ICollection<Employe> employeSupervise = new List<Employe> { samuel };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(marnie);

            Assert.Equal(employeSupervise, repoEmploye.ObtenirEmployesSupervises(noSuperviseur));

        }
        
        [Fact]
        public void ObtenirEmployesSupervises_QuandPlusieursEmployeSupervise()
        {
            CreerContexteEtReposDeTests();
            short noSuperviseur = 4;
            Employe samuel = new Employe { NoEmploye = 1, NAS = 123456789, Nom = "Harvey", Prenom = "Samuel", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = 4 };
            Employe tibo = new Employe { NoEmploye = 2, NAS = 000000002, Nom = "Harvey", Prenom = "Tibo", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = 4 };
            Employe jean = new Employe { NoEmploye = 3, NAS = 000000003, Nom = "Harvey", Prenom = "Jean", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = 4 };
            Employe marnie = new Employe { NoEmploye = noSuperviseur, NAS = 000000004, Nom = "Harvey", Prenom = "Marnie", DateEmbauche = new DateTime(2020, 12, 25), NoSuperviseur = null };
            ICollection<Employe> employesSupervises = new List<Employe> { samuel, tibo, jean };
            repoEmploye.AjouterEmploye(samuel);
            repoEmploye.AjouterEmploye(tibo);
            repoEmploye.AjouterEmploye(jean);
            repoEmploye.AjouterEmploye(marnie);

            Assert.Equal(employesSupervises, repoEmploye.ObtenirEmployesSupervises(noSuperviseur));

        }
        
    }
}
