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
		/// On va ins�rer les donn�es relatives aux d�partements
		/// </summary>
		/// <param name="dpt">Le d�partement</param>
		public void insertionDonneesDepartement()
		{



			using (var context = new electionEDM())
			{
				context.Configuration.LazyLoadingEnabled = false;
				short query;

				//On fait une requ�te pour voir si le d�partement n'existe pas d�j� dans la base de donn�es
				try
				{
					query = (from dept in context.Departement
							 where dept.code_du_departement == this.code_du_departement
							 select dept.code_du_departement).Single();

				}

				//Si le d�partement n'existe pas dans la base de donn�es
				catch (InvalidOperationException e)
				{
					if (this.code_du_departement != 0)
					{
						context.Departement.Add(this);

						//On ins�re le d�partement dans la base de donn�es
						try
						{
							context.SaveChanges();
						}

						//Si l'insertion du d�partement �choue
						catch (System.Data.Entity.Validation.DbEntityValidationException a)
						{

							MessageBox.Show("L'insertion du d�partement dans la base de donn�es a �chou�");
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
