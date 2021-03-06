namespace election_municipale
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	using System.Runtime.Serialization;
	using System.Xml.Serialization;

	public partial class calcul_sieges
    {
        public short? sieges_elus { get; set; }

        public short? sieges_cc { get; set; }

        public short? sieges_secteurs { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string insee { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int annee { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idListe { get; set; }

		[DataMember]
		[ForeignKey("annee")]
		public virtual AnneeElection AnneeElection { get; set; }

		[DataMember]
		[ForeignKey("idListe")]
		public virtual Liste Liste { get; set; }

		[DataMember]
		[ForeignKey("insee")]
		public virtual Commune Commune { get; set; }
    }
}
