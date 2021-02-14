﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetsORM.Entites
{
    [Table("EMPLOYE")]
    public class Employe
    {
        #region Propriétés
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short NoEmploye { get; set; }

        [Required]
        [Column("NAS", TypeName = "decimal(9,0)")]
        public decimal NAS { get; set; }

        [Required]
        [Column("NOM", TypeName = "varchar(10)")]
        public string Nom { get; set; }

        [Required]
        [Column("PRENOM", TypeName = "varchar(10)")]
        public string Prenom { get; set; }

        [Column("DATE_NAISSANCE", TypeName = "datetime")]
        public DateTime? DateNaissance { get; set; }

        [Required]
        [Column("DATE_EMBAUCHE", TypeName = "datetime")]
        public DateTime DateEmbauche { get; set; }

        [Column("SALAIRE", TypeName = "decimal(6,0)")]
        public decimal? Salaire { get; set; }

        [Column("TELEPHONE_BUREAU", TypeName = "decimal(10,0)")]
        public decimal? TelephoneBureau { get; set; }


        // string pas besoin ? car si vide il est null
        public string Adresse { get; set; }

        public short? NoSuperviseur { get; set; }

        [ForeignKey("NoSuperviseur")]
        public virtual Employe Superviseur { get; set; }

        #endregion Propriétés      
        //=============================================================================================
        #region Propriétés de navigation

        public virtual ICollection<Projet> ProjetsGestionnaires { get; set; }
        public virtual ICollection<Projet> ProjetsContactClient { get; set; }
        public virtual ICollection<Employe> EmployesSupervises { get; set; }

        //Méthodes
        public override string ToString()
        {
            return Nom + "," + Prenom + "," + DateEmbauche.ToString();
        }

        #endregion Propriétés de navigation
        //=============================================================================================
        #region Constructeur

        public Employe()
        {
            ProjetsGestionnaires = new List<Projet>();
            ProjetsContactClient = new List<Projet>();
            EmployesSupervises = new List<Employe>();
        }

        #endregion Constructeur
    }
}

