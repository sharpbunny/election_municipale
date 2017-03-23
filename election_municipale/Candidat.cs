namespace election_municipale
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Candidat")]
    public partial class Candidat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Candidat()
        {
            election = new HashSet<election>();
        }

        [Key]
        public int idCandidat { get; set; }

        [Required]
        [StringLength(40)]
        public string nom { get; set; }

        [StringLength(40)]
        public string prenom { get; set; }

        [Required]
        [StringLength(1)]
        public string sexe { get; set; }

        public int idListe { get; set; }

        public virtual Liste Liste { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<election> election { get; set; }
    }
}
