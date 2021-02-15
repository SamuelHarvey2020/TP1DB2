using ProjetsORM.EntiteDTOs;
using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetsORM.AccesDonnees
{
    class EFProjetRepository
    {
        #region Propriétés
        private ProjetsORMContexte contexte;
        #endregion Propriétés
        //==============================================================================================
        #region Constructeur
        public EFProjetRepository(ProjetsORMContexte ctx)
        {
            contexte = ctx;
        }
        #endregion Constructeur
        //==============================================================================================
        #region Méthodes
        public void AjouterProjet(Projet projet)
        {
            contexte.Projets.Add(projet);
            contexte.SaveChanges();
        }

        public Projet ObtenirProjet(string nomProjet, string nomClient)
        {
            return contexte.Projets.Find(nomProjet, nomClient);
        }

        public void ModifierProjet(Projet projet)
        {
            contexte.Projets.Update(projet);
            contexte.SaveChanges();
        }

        public void SupprimerProjet(Projet projet)
        {
            contexte.Projets.Remove(projet);
            contexte.SaveChanges();
        }

        public decimal? ObtenirBudgetTotalPourUnClient(string nomClient)
        {
            return contexte.Projets.Where(proj => proj.NomClient == nomClient).Sum(proj => proj.Budget);
        }

        public decimal? ObtenirBudgetMoyenPourUnClient(string nomClient)
        {
            return contexte.Projets.Where(proj => proj.NomClient == nomClient).Average(proj => proj.Budget);
        }

        public ICollection<StatsClient> RechercherClientsAvecNombreProjetsEtBudgetTotalEtBudgetMoyen()
        {
            var resultatGb = contexte.Projets.GroupBy(proj => proj.NomClient)
                                        .Select(groupe => new StatsClient
                                        {
                                            NomClient = groupe.Key,
                                            NombreProjets = groupe.Count(),
                                            BudgetTotal = groupe.Sum(proj => proj.Budget),
                                            BudgetMoyen = groupe.Average(proj => proj.Budget)
                                        }).ToList();

            return resultatGb;
        }
        #endregion Méthodes
    }

}