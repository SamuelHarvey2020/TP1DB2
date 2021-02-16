using ProjetsORM.EntiteDTOs;
using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetsORM.AccesDonnees
{
    class EFClientRepository
    {
        #region Propriétés
        private ProjetsORMContexte contexte;
        #endregion Propriétés
        //===================================================================================
        #region Constructeur
        public EFClientRepository(ProjetsORMContexte ctx)
        {
            contexte = ctx;
        }
        #endregion Constructeur
        //===================================================================================
        #region Méthodes
        public void AjouterClient(Client client)
        {
            contexte.Clients.Add(client);
            contexte.SaveChanges();
        }
        public Client ObtenirClient(string nomClient)
        {
            Client client = contexte.Clients.Find(nomClient);
            if (client != null)
            {
                return client;
            }
            else
            {
                throw new ArgumentException("client does not exist");
            }

        }

        public ICollection<Client> RechercherClientParVille(string nomVille)
        {
            IEnumerable<Client> clients = contexte.Clients.ToList().Where(c => c.Ville == nomVille);
            if (clients.Count() > 0)
            {
                return clients.ToList();
            }
            else
            {
                return new List<Client>();
            }

        }

        public ICollection<Projet> ObtenirProjetsPourUnClient(string nomClient)
        {
            Client client = contexte.Clients.Find(nomClient);                                                                                       // Gêrer l'exception
            return client.ListeProjets;
        }

        public ICollection<Projet> ObtenirProjetsEnCoursPourUnClient(string nomClient)
        {
            return contexte.Projets.Where(p => p.NomClient == nomClient && p.DateFin == null).ToList();
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
