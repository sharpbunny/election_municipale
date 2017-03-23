namespace election_municipale
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Runtime.Serialization;
	using System.Xml.Serialization;

	[Table("Liste")]
	public partial class Liste
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Liste()
		{
			calcul_sieges = new HashSet<calcul_sieges>();
			Candidat = new HashSet<Candidat>();
		}

		[Key]
		public int idListe { get; set; }

		[Required]
		[StringLength(250)]
		public string nomListe { get; set; }

		[Required]
		[StringLength(5)]
		public string code_nuance { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<calcul_sieges> calcul_sieges { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Candidat> Candidat { get; set; }

		[DataMember]
		[ForeignKey("code_nuance")]
		public virtual Parti Parti { get; set; }
	}
}
