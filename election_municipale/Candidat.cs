namespace election_municipale
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Xml.Serialization;

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

		[DataMember]
		[ForeignKey("idListe")]
		public virtual Liste Liste { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<election> election { get; set; }

		/// <summary>
		/// Insertion de la clé étrangère Liste dans la classe Candidat
		/// </summary>
		/// <param name="candidat">Tableau de candidats</param>
		/// <param name="list">Tableau de listes électorales</param>
		/// <returns></returns>
		public static Candidat[] insertionCleEtrangereCandidat(Candidat[] candidat, Liste[] list)
		{
			for (int i = 0; i < candidat.Length; i++)
			{

				if (candidat[i].nom != null && list[i].nomListe != null)
				{
					candidat[i].Liste = null;
					candidat[i].idListe = list[i].idListe;
				}

			}

			return candidat;

		}

		/// <summary>
		/// On va insérer les données relatives aux candidats à l'élection municipale dans la base de données
		/// </summary>
		public static void insertionDonneesCandidat(Candidat[] candidat)
		{
			using (var context = new electionEDM())
			{

				for (int i = 0; i < candidat.Length; i++)
				{

					int query;

					if (candidat[i].nom != null)
					{
						//On fait une requête pour voir si l'id du candidat n'existe pas déjà dans la bdd
						try
						{
							query = (from candid in context.Candidat
									 where candid.nom == candidat[i].nom && candid.prenom == candidat[i].prenom
									 select candid.idCandidat).Single();
						}

						catch
						{
							context.Candidat.Add(candidat[i]);
							try
							{
								context.SaveChanges();
							}

							catch
							{

							}

						}

					}

				}

			}
		}
	}
}
