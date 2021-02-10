using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetsORM.AccesDonnees
{
    class EFEmployeRepository
    {
        #region Propriétés
        private ProjetsORMContexte contexte;
        #endregion Propriétés
        //==============================================================================================
        #region Constructeur
        public EFEmployeRepository(ProjetsORMContexte ctx)
        {
            contexte = ctx;
        }
        #endregion Constructeur
        //==============================================================================================
        #region Méthodes

        public void AjouterEmploye(Employe employe)
        {
            contexte.Employes.Add(employe);
            contexte.SaveChanges();
        }

        public Employe ObtenirEmploye(short idEmploye)
        {
            return contexte.Employes.Find(idEmploye);
        }

        public ICollection<Employe> RechercherTousEmployes()
        {
            return contexte.Employes.ToList();
        }

        public ICollection<Employe> RechercherEmployesParNom(string nom, string prenom)
        {
            IEnumerable<Employe> employes = this.RechercherTousEmployes().Where(e => e.Nom == nom && e.Prenom == prenom);
            if (employes.Count() > 0)
            {
                return employes.ToList();
            }
            else
            {
                return null;
            }
        }

        public ICollection<Employe> RechercherTousSuperviseurs()
        {
            throw new NotImplementedException();
        }

        public ICollection<Employe> ObtenirEmployesSupervises(short superviseurId)
        {
            throw new NotImplementedException();
        }
        #endregion Méthodes
    }
}
