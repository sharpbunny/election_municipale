namespace election_municipale
{
	using System;
	using System.Windows;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Linq;
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

		/// <summary>
		/// Insertion de la clé étrangère Parti dans les listes éléctorales
		/// </summary>
		/// <param name="liste">Tableau de listes électorales</param>
		/// <param name="parti">Tableau de partis politiques</param>
		/// <returns></returns>
		public Liste[] insertionCleEtrangereListe(Liste[] liste, Parti[] parti)
		{
			//Pour chaque liste on insère sa clé étrangère : code_nuance (PRIMARY KEY de la table Parti)
			for (int i = 0; i < liste.Length; i++)
			{
				if (liste[i].code_nuance != "" && parti[i].code_nuance != "")
				{
					liste[i].Parti = null;
					liste[i].code_nuance = parti[i].code_nuance;
				}
			}

			return liste;
		}

		/// <summary>
		/// insertion des listes éléctorales dans la base de données
		/// </summary>
		/// <param name="list">Tableau de listes électorales</param>
		public void insertionDonneesListe(Liste[] list)
		{
			using (var context = new electionEDM())
			{
				//On parcourt le tableau de listes electorales
				for (int i = 0; i < list.Length; i++)
				{
					Liste listTemp = list[i];
					string query;

					if (list[i].nomListe != null)
					{
						//On regarde si la liste n'existe pas déjà dans la table
						try
						{
							query = (from liste in context.Liste
									 where liste.nomListe == listTemp.nomListe
									 select liste.nomListe).Single();
						}

						//Si la liste n'existe pas dans la table
						catch (InvalidOperationException e)
						{
							context.Liste.Add(list[i]);

							//On insère la liste dans la base de données
							try
							{
								context.SaveChanges();
							}

							//Si l'insertion de la liste a échoué
							catch (System.Data.Entity.Validation.DbEntityValidationException a)
							{
								MessageBox.Show("L'insertion de la liste " + i + " : a échoué");
							}

						}


					}
				}

			}
		}
	}
}
