namespace testJSON
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Commune")]
    public partial class Commune
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commune()
        {
            calcul_sieges = new HashSet<calcul_sieges>();
            elections = new HashSet<election>();
            stats_election = new HashSet<stats_election>();
        }

        [Key]
        [StringLength(5)]
        public string insee { get; set; }

        [Required]
        [StringLength(4)]
        public string code_de_la_commune { get; set; }

        [Required]
        [StringLength(50)]
        public string libelle_de_la_commune { get; set; }

        [StringLength(30)]
        public string geo_point_2d { get; set; }

        [StringLength(500)]
        public string geo_shape { get; set; }

        public short code_du_departement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<calcul_sieges> calcul_sieges { get; set; }

        public virtual Departement Departement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<election> elections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stats_election> stats_election { get; set; }
    }
}
