namespace election_municipale
{
	using System;
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
		public static void insertionDonneesParti(Parti[] parti)
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
						try
						{
							query = (from part in context.Parti
									 where part.code_nuance == partiTemp.code_nuance
									 select part.code_nuance).Single();
						}

						catch (InvalidOperationException e)
						{
							context.Parti.Add(parti[i]);
							try
							{
								context.SaveChanges();
							}

							catch (System.Data.Entity.Validation.DbEntityValidationException a)
							{
								Console.WriteLine("parti " + i + " : a échoué lors du savechanges");
							}

						}

					}



				}
			}
		}


	}
}
