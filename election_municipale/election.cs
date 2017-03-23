namespace election_municipale
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("election")]
    public partial class election
    {
        public int? voix { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int annee { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idCandidat { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string insee { get; set; }

        public virtual AnneeElection AnneeElection { get; set; }

        public virtual Candidat Candidat { get; set; }

        public virtual Commune Commune { get; set; }
    }
}
