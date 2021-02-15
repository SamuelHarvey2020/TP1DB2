﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetsORM.Entites
{
    [Table("PROJET")]
    public class Projet
    {
        #region Propriétés
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string NomProjet { get; set; }

        [Required]
        [Column("NOM_CLIENT", TypeName = "varchar(10)")]
        public string NomClient { get; set; }

        [Column("DATE_DEBUT", TypeName = "datetime")]
        public DateTime? DateDebut { get; set; }

        [Column("DATE_FIN", TypeName = "datetime")]
        public DateTime? DateFin { get; set; }

        [Column("BUDGET", TypeName = "decimal(6,0)")]
        public decimal? Budget { get; set; }

        [Required]
        public short NoGestionnaire { get; set; }

        public short? NoContactClient { get; set; }
        #endregion Propriétés
        //==============================================================================================
        #region Propriétés de navigation 

        [ForeignKey("NomClient")]
        public virtual Client Client { get; set; }

        [ForeignKey("NoGestionnaire")]
        public virtual Employe EmployeGestionnaire { get; set; }

        [ForeignKey("NoContactClient")]
        public virtual Employe ContactDuClient { get; set; }

        #endregion Propriétés de navigation 
        //==============================================================================================
        #region Constructeur
        #endregion Constructeur


    }
}
