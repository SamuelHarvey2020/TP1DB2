using Microsoft.EntityFrameworkCore;
using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using Xunit;

namespace ProjetsORM.AccesDonnees
{
    public class EFProjetRepositoryTest
    {
        private EFClientRepository repoClient;
        private EFProjetRepository repoProjet;
        private EFEmployeRepository repoEmploye;
    
        private void CreerContexteEtReposDeTests()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "projet_db");   // Database en mémoire
            var contexte = new ProjetsORMContexte(builder.Options);
            repoClient = new EFClientRepository(contexte);
            repoProjet = new EFProjetRepository(contexte);
            repoEmploye = new EFEmployeRepository(contexte);
        }

        [Fact]
        public void AjouterProjet_DoitAjouterProjet()
        {

            CreerContexteEtReposDeTests();
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = "Coveo", NoGestionnaire = 123 };

            repoProjet.AjouterProjet(AI);

            Assert.Equal(AI, repoProjet.ObtenirProjet(AI.NomProjet, AI.NomClient));

        }

        [Fact]
        public void ModifierProjet_DoitModifierProjet()
        {

            CreerContexteEtReposDeTests();
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = "Coveo", NoGestionnaire = 123 };
            repoProjet.AjouterProjet(AI);

            repoProjet.ModifierProjet(AI);

            Assert.Equal(AI, repoProjet.ObtenirProjet(AI.NomProjet, AI.NomClient));

        }

        [Fact]
        public void SupprimerProjet_DoitSupprimerProjet()
        {

            CreerContexteEtReposDeTests();
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = "Coveo", NoGestionnaire = 123 };
            repoProjet.AjouterProjet(AI);

            repoProjet.SupprimerProjet(AI);

            Assert.Throws<ArgumentException>(() => repoProjet.ObtenirProjet(AI.NomProjet, AI.NomClient));

        }

        [Fact]
        public void ObtenirProjet_QuandProjetExistePas()
        {
            CreerContexteEtReposDeTests();
            string nomProjetAChercher = "Stages";
            string nomClientAChercher = "Coveo";
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = "Coveo", NoGestionnaire = 123 };
            Projet RelationsClients = new Projet { NomProjet = "RelationsClient", NomClient = "Coveo", NoGestionnaire = 123 };
            Projet CyberSecurity = new Projet { NomProjet = "CyberSecurity", NomClient = "Coveo", NoGestionnaire = 123 };
            repoProjet.AjouterProjet(AI);
            repoProjet.AjouterProjet(RelationsClients);
            repoProjet.AjouterProjet(CyberSecurity);

            Assert.Throws<ArgumentException>(() => repoProjet.ObtenirProjet(nomProjetAChercher, nomClientAChercher));
        }

        [Fact]
        public void ObtenirProjet_QuandProjetExiste()
        {
            CreerContexteEtReposDeTests();
            string nomProjetAChercher = "RelationsClient";
            string nomClientAChercher = "Coveo";
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = "Coveo", NoGestionnaire = 123 };
            Projet RelationsClients = new Projet { NomProjet = nomProjetAChercher, NomClient = nomClientAChercher, NoGestionnaire = 123 };
            Projet CyberSecurity = new Projet { NomProjet = "CyberSecurity", NomClient = "Coveo", NoGestionnaire = 123 };
            repoProjet.AjouterProjet(AI);
            repoProjet.AjouterProjet(RelationsClients);
            repoProjet.AjouterProjet(CyberSecurity);

            Projet projetTrouve = repoProjet.ObtenirProjet(nomProjetAChercher, nomClientAChercher);

            Assert.Equal(RelationsClients, projetTrouve);
            Assert.Same(RelationsClients, projetTrouve);
        }

        [Fact]
        public void ObtenirBudgetTotalPourUnClient_QuandClientAUnProjet() {

            CreerContexteEtReposDeTests();
            string nomClient = "Coveo";
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = nomClient, NoGestionnaire = 123, Budget = 150000 };
            decimal? sommeBudget = AI.Budget;
            repoProjet.AjouterProjet(AI);

            Assert.Equal(sommeBudget, repoProjet.ObtenirBudgetTotalPourUnClient(nomClient));
        }

        [Fact]
        public void ObtenirBudgetTotalPourUnClient_QuandClientAPlusieursProjets()
        {

            CreerContexteEtReposDeTests();
            string nomClient = "Coveo";
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = nomClient, NoGestionnaire = 123, Budget = 150000 };
            Projet RelationsClients = new Projet { NomProjet = "RelationsClient", NomClient = nomClient, NoGestionnaire = 123, Budget = 100000 };
            Projet CyberSecurity = new Projet { NomProjet = "CyberSecurity", NomClient = nomClient, NoGestionnaire = 123, Budget = 150000 };
            decimal? sommeBudget = (AI.Budget + RelationsClients.Budget + CyberSecurity.Budget);
            repoProjet.AjouterProjet(AI);
            repoProjet.AjouterProjet(RelationsClients);
            repoProjet.AjouterProjet(CyberSecurity);

            Assert.Equal(sommeBudget, repoProjet.ObtenirBudgetTotalPourUnClient(nomClient));
        }

        [Fact]
        public void ObtenirBudgetMoyenPourUnClient_QuandClientAUnProjet()
        {

            CreerContexteEtReposDeTests();
            string nomClient = "Coveo";
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = nomClient, NoGestionnaire = 123, Budget = 150000 };
            decimal? moyenneBudget = AI.Budget / 1;
            repoProjet.AjouterProjet(AI);

            Assert.Equal(moyenneBudget, repoProjet.ObtenirBudgetMoyenPourUnClient(nomClient));
        }

        [Fact]
        public void ObtenirBudgetMoyenPourUnClient_QuandClientAPlusieursProjets()
        {

            CreerContexteEtReposDeTests();
            string nomClient = "Coveo";
            Projet AI = new Projet { NomProjet = "ArtificialIntelligence", NomClient = nomClient, NoGestionnaire = 123, Budget = 150000 };
            Projet RelationsClients = new Projet { NomProjet = "RelationsClient", NomClient = nomClient, NoGestionnaire = 123, Budget = 100000 };
            Projet CyberSecurity = new Projet { NomProjet = "CyberSecurity", NomClient = nomClient, NoGestionnaire = 123, Budget = 150000 };
            decimal? moyenneBudget = (AI.Budget + RelationsClients.Budget + CyberSecurity.Budget) / 3;
            repoProjet.AjouterProjet(AI);
            repoProjet.AjouterProjet(RelationsClients);
            repoProjet.AjouterProjet(CyberSecurity);

            Assert.Equal(moyenneBudget, repoProjet.ObtenirBudgetMoyenPourUnClient(nomClient));
        }

        // TESTS GROUP BY ( JE N'AI PAS TROUVÉ COMMENT TESTER LES FONCTIONS GROUP BY )

    }
}
