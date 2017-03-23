namespace election_municipale
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using System.Windows;
	using System.Data.Entity.Spatial;

	[Table("AnneeElection")]
	public partial class AnneeElection
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public AnneeElection()
		{
			calcul_sieges = new HashSet<calcul_sieges>();
			election = new HashSet<election>();
			stats_election = new HashSet<stats_election>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int annee { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<calcul_sieges> calcul_sieges { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<election> election { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<stats_election> stats_election { get; set; }

		/// <summary>
		/// Insertion dans la base de données de l'entité AnneeElection
		/// </summary>
		/// <param name="year">Entité : AnneeElection</param>
		public void insertionAnnee()
		{
			using (var context = new electionEDM())
			{
				try
				{
					AnneeElection query = (from annee in context.AnneeElection
										   where annee.annee == this.annee
										   select annee).Single();
				}

				catch
				{
					context.AnneeElection.Add(this);
					try
					{
						context.SaveChanges();
					}

					//Si l'insertion dans la base de données échoue
					catch
					{
						MessageBox.Show("L'insertion de l'année dans la base de données a échoué");
					}
				}



			}
		}
	}
}
