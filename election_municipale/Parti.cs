namespace election_municipale
{
	using System;
	using System.Windows;
	using System.Linq;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	[Table("Parti")]
	public partial class Parti
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Parti()
		{
			Liste = new HashSet<Liste>();
		}

		[Key]
		[StringLength(5)]
		public string code_nuance { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Liste> Liste { get; set; }

		/// <summary>
		/// Insertion des données dans la BDD des partis politiques
		/// </summary>
		/// <param name="parti">Parti politique</param>
		public void insertionDonneesParti(Parti[] parti)
		{

			using (var context = new electionEDM())
			{

				string query;

				//On va parcourir le tableau de partis
				for (int i = 0; i < parti.Length; i++)
				{
					Parti partiTemp = parti[i];

					if (partiTemp.code_nuance != null)
					{

						//On regarde si le parti n'existe pas déjà dans la base de données
						try
						{
							query = (from part in context.Parti
									 where part.code_nuance == partiTemp.code_nuance
									 select part.code_nuance).Single();
						}

						//Si le parti n'existe pas
						catch (InvalidOperationException e)
						{
							context.Parti.Add(parti[i]);

							//On insère le Parti dans la base de données
							try
							{
								context.SaveChanges();
							}

							//Si l'insertion du parti dans la base de données échoue
							catch (System.Data.Entity.Validation.DbEntityValidationException a)
							{
								MessageBox.Show("L'insertion du parti " + i + " : a échoué lors du savechanges");
							}

						}

					}



				}
			}
		}


	}
}
