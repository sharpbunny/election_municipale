namespace election_municipale
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
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

		/// <summary>
		/// On va ins�rer les donn�es relatives aux d�partements
		/// </summary>
		/// <param name="dpt">Le d�partement</param>
		public void insertionDonneesDepartement()
		{



			using (var context = new electionEDM())
			{
				context.Configuration.LazyLoadingEnabled = false;
				short query;
				try
				{
					query = (from dept in context.Departement
							 where dept.code_du_departement == this.code_du_departement
							 select dept.code_du_departement).Single();

				}


				catch (InvalidOperationException e)
				{
					Console.WriteLine("query catch exception");
					if (this.code_du_departement != 0)
					{
						context.Departement.Add(this);
						try
						{
							Console.WriteLine(this.code_du_departement);
							context.SaveChanges();
						}
						catch (System.Data.Entity.Validation.DbEntityValidationException a)
						{
							foreach (var eve in a.EntityValidationErrors)
							{
								Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
									eve.Entry.Entity.GetType().Name, eve.Entry.State);
								foreach (var ve in eve.ValidationErrors)
								{
									Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
										ve.PropertyName, ve.ErrorMessage);
								}
							}
							throw;
						} //Fin du catch
					}

				}//Fin du catch sur le try de la requ�te

			} //Fin du using


		}

		/// <summary>
		/// Permet de r�initialiser les attributs d'un objet de type D�partement � null
		/// </summary>
		/// <param name="dept">D�partement</param>
		/// <returns></returns>
		public void reinitialisationDepartement()
		{
			this.code_du_departement = 0;
			this.libelle_du_departement = "";
			this.Commune = null;
		}


	}
}
