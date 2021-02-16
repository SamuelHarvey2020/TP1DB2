using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetsORM.Entites
{
    [Table("CLIENT")]
    public class Client
    {
        #region Propriétés

        [Key]
        [Column("NOM_CLIENT", TypeName = "varchar(10)")]
        public string NomClient { get; set; }

        [Required]
        public short NoEnregistrement { get; set; }

        [Column("RUE", TypeName = "varchar(10)")]
        public string Rue { get; set; }

        [Required]
        [Column("VILLE", TypeName = "varchar(10)")]
        public string Ville { get; set; }

        [Required]
        [Column("CODE_POSTAL", TypeName = "char(10)")]
        public string CodePostal { get; set; }                         // char ?= string

        [Column("TELEPHONE", TypeName = "decimal(10,0)")]
        public decimal? Telephone { get; set; }

        #endregion Propriétés
        //=========================================================================================

        #region Propriétés de navigation 
        public virtual ICollection<Projet> ListeProjets { get; set; }

        #endregion Propriétés de navigation 
        //=========================================================================================

        #region Constructeur
        public Client()
        {
            ListeProjets = new List<Projet>();
        }

        #endregion Constructeur

    }
}
