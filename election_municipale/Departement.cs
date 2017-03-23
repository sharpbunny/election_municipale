namespace election_municipale
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("Departement")]
	public partial class Departement
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Departement()
		{
			Commune = new HashSet<Commune>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public short code_du_departement { get; set; }

		[Required]
		[StringLength(50)]
		public string libelle_du_departement { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Commune> Commune { get; set; }
	}
}
