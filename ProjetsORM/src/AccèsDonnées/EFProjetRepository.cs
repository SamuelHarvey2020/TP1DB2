﻿using ProjetsORM.EntiteDTOs;
using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;

namespace ProjetsORM.AccesDonnees
{
    class EFProjetRepository
    {
        #region Propriétés
        private ProjetsORMContexte contexte;
        #endregion Propriétés
        //==============================================================================================
        #region Constructeur
        public EFProjetRepository (ProjetsORMContexte ctx)
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
            throw new NotImplementedException();
        }

        public void SupprimerProjet(Projet projet)
        {
            throw new NotImplementedException();
        }

        public decimal? ObtenirBudgetTotalPourUnClient(string nomClient)
        {
            throw new NotImplementedException();
        }

        public decimal? ObtenirBudgetMoyenPourUnClient(string nomClient)
        {
            throw new NotImplementedException();
        }

        public ICollection<StatsClient> RechercherClientsAvecNombreProjetsEtBudgetTotalEtBudgetMoyen()
        {
            throw new NotImplementedException();

        }
        #endregion Méthodes
    }
}
