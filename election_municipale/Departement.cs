namespace election_municipale
{
	using System;
	using System.Windows;
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
		/// On va insérer les données relatives aux départements
		/// </summary>
		/// <param name="dpt">Le département</param>
		public void insertionDonneesDepartement()
		{



			using (var context = new electionEDM())
			{
				context.Configuration.LazyLoadingEnabled = false;
				short query;

				//On fait une requête pour voir si le département n'existe pas déjà dans la base de données
				try
				{
					query = (from dept in context.Departement
							 where dept.code_du_departement == this.code_du_departement
							 select dept.code_du_departement).Single();

				}

				//Si le département n'existe pas dans la base de données
				catch (InvalidOperationException e)
				{
					if (this.code_du_departement != 0)
					{
						context.Departement.Add(this);

						//On insère le département dans la base de données
						try
						{
							context.SaveChanges();
						}

						//Si l'insertion du département échoue
						catch (System.Data.Entity.Validation.DbEntityValidationException a)
						{

							MessageBox.Show("L'insertion du département dans la base de données a échoué");
						} //Fin du catch
					}

				}//Fin du catch sur le try de la requête

			} //Fin du using


		}

		/// <summary>
		/// Permet de réinitialiser les attributs d'un objet de type Département à null
		/// </summary>
		/// <param name="dept">Département</param>
		/// <returns></returns>
		public void reinitialisationDepartement()
		{
			this.code_du_departement = 0;
			this.libelle_du_departement = "";
			this.Commune = null;
		}


	}
}
