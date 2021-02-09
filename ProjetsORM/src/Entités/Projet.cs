using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetsORM.Entites
{
    [Table("PROJET")]
    public class Projet
    {
        #region Propriétés
        #endregion Propriétés
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short NomProjet { get; set; }

        [Required]
        [Column("NOM_CLIENT" , TypeName = "varchar(10)")]
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

        #region Propriétés de navigation 
        #endregion Propriétés de navigation 
        [ForeignKey("NomClient")]
        public virtual Client Client { get; set; }

        [ForeignKey("NoGestionnaire")]
        public virtual Employe EmployeGestionnaire { get; set; }
        #region Constructeur
        #endregion Constructeur

        [ForeignKey("NoContactClient")]
        public virtual Employe ContactDuClient { get; set; }
    }
}
