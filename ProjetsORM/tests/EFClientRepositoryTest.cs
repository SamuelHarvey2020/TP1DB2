﻿using Microsoft.EntityFrameworkCore;
using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProjetsORM.AccesDonnees
{
    public class EFClientRepositoryTest
    {
        private EFClientRepository repoClient;
        private EFProjetRepository repoProjet;

        private void CreerContexteEtReposDeTests()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "client_db");   // Database en mémoire
            var contexte = new ProjetsORMContexte(builder.Options);
            repoClient = new EFClientRepository(contexte);
            repoProjet = new EFProjetRepository(contexte);
        }

        [Fact]
        public void AjouterClient_DoitAjouterClient()
        {
            
            CreerContexteEtReposDeTests();
            Client coveo = new Client { NomClient = "Coveo", NoEnregistrement = 111, Ville = "Québec", CodePostal = "G3G1G1" };

            repoClient.AjouterClient(coveo);

            Assert.Equal(coveo, repoClient.ObtenirClient(coveo.NomClient));
            
        }

        [Fact]
        public void ObtenirClient_QuandClientExistePas()
        {
            
            CreerContexteEtReposDeTests();
            string nomClientARechercher = "Coveo";
            Client webLab = new Client { NomClient = "WebLab", NoEnregistrement = 1100, Ville = "Matane", CodePostal = "G5L1G1" };
            Client cgi = new Client { NomClient = "CGI", NoEnregistrement = 22100, Ville = "Québec", CodePostal = "G7L1G1" };
            Client spektrum = new Client { NomClient = "Spektrum", NoEnregistrement = 5400, Ville = "Lévis", CodePostal = "T5L1V1" };
            repoClient.AjouterClient(webLab);
            repoClient.AjouterClient(cgi);
            repoClient.AjouterClient(spektrum);

            Assert.Throws<ArgumentException>(() => repoClient.ObtenirClient(nomClientARechercher));
            
        }

        [Fact]
        public void ObtenirClient_QuandClientExiste()
        {
            
            CreerContexteEtReposDeTests();
            string nomClientARechercher = "Coveo";
            Client webLab = new Client { NomClient = "WebLab", NoEnregistrement = 1100, Ville = "Matane", CodePostal = "G5L1G1" };
            Client coveo = new Client { NomClient = nomClientARechercher, NoEnregistrement = 1111, Ville = "Québec", CodePostal = "G3G1G1" };
            Client spektrum = new Client { NomClient = "Spektrum", NoEnregistrement = 5400, Ville = "Lévis", CodePostal = "T5L1V1" };
            repoClient.AjouterClient(webLab);
            repoClient.AjouterClient(coveo);
            repoClient.AjouterClient(spektrum);

            Client clientTrouve = repoClient.ObtenirClient(nomClientARechercher);

            Assert.Equal(coveo, clientTrouve);
            Assert.Same(coveo, clientTrouve);
            
        }

        [Fact]
        public void RechercherClientParVille_QuandAucunClient()
        {
            
            CreerContexteEtReposDeTests();
            string nomVilleARechercher = "Ste-Catherine";
            Client webLab = new Client { NomClient = "WebLab", NoEnregistrement = 1100, Ville = "Matane", CodePostal = "G5L1G1" };
            Client cgi = new Client { NomClient = "CGI", NoEnregistrement = 22100, Ville = "Québec", CodePostal = "G7L1G1" };
            Client spektrum = new Client { NomClient = "Spektrum", NoEnregistrement = 5400, Ville = "Lévis", CodePostal = "T5L1V1" };
            repoClient.AjouterClient(webLab);
            repoClient.AjouterClient(cgi);
            repoClient.AjouterClient(spektrum);

            ICollection<Client> clientsTrouves = repoClient.RechercherClientParVille(nomVilleARechercher);

            Assert.Empty(clientsTrouves);
            
        }

        [Fact]
        public void RechercherClientParVille_QuandUnClient()
        {
            
            CreerContexteEtReposDeTests();
            string nomVilleARechercher = "Québec";
            Client webLab = new Client { NomClient = "WebLab", NoEnregistrement = 1100, Ville = "Matane", CodePostal = "G5L1G1" };
            Client cgi = new Client { NomClient = "CGI", NoEnregistrement = 22100, Ville = nomVilleARechercher, CodePostal = "G7L1G1" };
            Client spektrum = new Client { NomClient = "Spektrum", NoEnregistrement = 5400, Ville = "Lévis", CodePostal = "T5L1V1" };
            ICollection<Client> clientsQuebec = new List<Client> { cgi };
            repoClient.AjouterClient(webLab);
            repoClient.AjouterClient(cgi);
            repoClient.AjouterClient(spektrum);

            ICollection<Client> clientsTrouves = repoClient.RechercherClientParVille(nomVilleARechercher);

            Assert.Equal(clientsQuebec, clientsTrouves);
            
        }

        [Fact]
        public void RechercherClientParVille_QuandPlusieursClients()
        {
            
            CreerContexteEtReposDeTests();
            string nomVilleARechercher = "Québec";
            Client webLab = new Client { NomClient = "WebLab", NoEnregistrement = 1100, Ville = "Matane", CodePostal = "G5L1G1" };
            Client cgi = new Client { NomClient = "CGI", NoEnregistrement = 22100, Ville = nomVilleARechercher, CodePostal = "G7L1G1" };
            Client coveo = new Client { NomClient = "Coveo", NoEnregistrement = 1111, Ville = nomVilleARechercher, CodePostal = "G3G1G1" };
            Client spektrum = new Client { NomClient = "Spektrum", NoEnregistrement = 5400, Ville = "Lévis", CodePostal = "T5L1V1" };
            Client ubisoft = new Client { NomClient = "Ubisoft", NoEnregistrement = 1111, Ville = "Gaspé", CodePostal = "G3G1G1" };
            Client uLaval = new Client { NomClient = "ULaval", NoEnregistrement = 5400, Ville = nomVilleARechercher, CodePostal = "T5L1V1" };
            ICollection<Client> clientsQuebec = new List<Client> { cgi, coveo, uLaval };
            repoClient.AjouterClient(webLab);
            repoClient.AjouterClient(cgi);
            repoClient.AjouterClient(coveo);
            repoClient.AjouterClient(spektrum);
            repoClient.AjouterClient(ubisoft);
            repoClient.AjouterClient(uLaval);

            ICollection<Client> clientsTrouves = repoClient.RechercherClientParVille(nomVilleARechercher);

            Assert.Equal(clientsQuebec, clientsTrouves);
            
        }

        [Fact]
        public void ObtenirProjetsPourUnClient_QuandClientAPlusieursProjets()
        {

            CreerContexteEtReposDeTests();
            Client coveo = new Client { NomClient = "Coveo", NoEnregistrement = 111, Ville = "Québec", CodePostal = "G3G1G1" };
            Projet AI = new Projet {NomProjet = "ArtificialIntelligence", NomClient = "Coveo", NoGestionnaire = 123};
            Projet RelationsClients = new Projet { NomProjet = "RelationsClient", NomClient = "Coveo", NoGestionnaire = 123 };
            Projet CyberSecurity = new Projet { NomProjet = "CyberSecurity", NomClient = "Coveo", NoGestionnaire = 123 };
            ICollection<Projet> projetsCoveo = new List<Projet> {AI, RelationsClients, CyberSecurity};
            repoClient.AjouterClient(coveo);
            repoProjet.AjouterProjet(AI);
            repoProjet.AjouterProjet(RelationsClients);
            repoProjet.AjouterProjet(CyberSecurity);

            ICollection<Projet> projetsTrouves = repoClient.ObtenirProjetsPourUnClient("Coveo");

            Assert.Equal(projetsCoveo, projetsTrouves);
        }

        [Fact]
        public void ObtenirProjetsEnCoursPourUnClient_QuandClientAUnProjetTermine()
        {

            CreerContexteEtReposDeTests();
            Client coveo = new Client { NomClient = "Coveo", NoEnregistrement = 111, Ville = "Québec", CodePostal = "G3G1G1" };
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = "Coveo", NoGestionnaire = 123 };
            Projet RelationsClients = new Projet { NomProjet = "RelationsClient", NomClient = "Coveo", NoGestionnaire = 123, DateFin = new DateTime(2020, 12, 25) };
            Projet CyberSecurity = new Projet { NomProjet = "CyberSecurity", NomClient = "Coveo", NoGestionnaire = 123 };
            ICollection<Projet> projetsCoveo = new List<Projet> { AI, CyberSecurity };
            repoClient.AjouterClient(coveo);
            repoProjet.AjouterProjet(AI);
            repoProjet.AjouterProjet(RelationsClients);
            repoProjet.AjouterProjet(CyberSecurity);

            ICollection<Projet> projetsTrouves = repoClient.ObtenirProjetsEnCoursPourUnClient("Coveo");

            Assert.Equal(projetsCoveo, projetsTrouves);
        }
    }
}
