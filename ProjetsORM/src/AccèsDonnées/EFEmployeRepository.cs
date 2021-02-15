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
            Employe employe = contexte.Employes.Find(idEmploye);
            if (employe != null)
            {
                return employe;
            }
            else
            {
                throw new ArgumentException("employee does not exist");
            }
        }

        public ICollection<Employe> RechercherTousEmployes()
        {
            IEnumerable < Employe > employes = contexte.Employes.ToList();
            if (employes.Count() > 0)
            {
                return employes.ToList();
            }
            else
            {
                return new List<Employe>();
            }
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
                return new List<Employe>();
            }
        }
        
        public ICollection<Employe> RechercherTousSuperviseurs()
        {
            //IEnumerable<Employe> superviseurs = this.RechercherTousEmployes().Where(e => e.EmployesSupervises.Count() >= 0);
            IEnumerable<Employe> superviseurs = this.RechercherTousEmployes().Where(e => e.Superviseur != null).Select(e => e.Superviseur);
            if (superviseurs.Count() > 0)
            {
                return superviseurs.ToList();
            }
            else
            {
                return new List<Employe>();
            }
            
        }
     
        public ICollection<Employe> ObtenirEmployesSupervises(short superviseurId)
        {
            ICollection<Employe> employesSupervises = contexte.Employes.Where(e => e.NoSuperviseur == superviseurId).ToList();
            if (employesSupervises.Count() > 0)
            {
                return employesSupervises;
            }
            else
            {
                throw new ArgumentException("Ce superviseur n'a aucun employe supervisé");
            }
        }
        #endregion Méthodes
    }
}
