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
        #endregion Propriétés
        [Key]
        [Column("NOM_CLIENT", TypeName = "varchar(10)")]
        public short NomClient { get; set; }

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
        public string Telephone { get; set; }

        #region Propriétés de navigation 
        #endregion Propriétés de navigation 
        public virtual ICollection<Projet> Projets { get; set; }

        #region Constructeur
        #endregion Constructeur
        public Client()
        {
            Projets = new List<Projet>();
        }
    }
}
