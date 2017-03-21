namespace testJSON
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class stats_election
    {
        public int? inscrits { get; set; }

        public int? abstentions { get; set; }

        public int? blancs_et_nuls { get; set; }

        public int? exprimes { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int annee { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string insee { get; set; }
<<<<<<< HEAD

=======
        public Nullable<int> votants { get; set; }
    
>>>>>>> develop
        public virtual AnneeElection AnneeElection { get; set; }

        public virtual Commune Commune { get; set; }
    }
}
