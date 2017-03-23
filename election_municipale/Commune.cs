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


	[Table("Commune")]
	public partial class Commune
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Commune()
		{
			calcul_sieges = new HashSet<calcul_sieges>();
			election = new HashSet<election>();
			stats_election = new HashSet<stats_election>();
		}

		[Key]
		[StringLength(5)]
		public string insee { get; set; }

		[Required]
		[StringLength(5)]
		public string code_de_la_commune { get; set; }

		[Required]
		[StringLength(60)]
		public string libelle_de_la_commune { get; set; }

		[StringLength(40)]
		public string geo_point_2d { get; set; }

		[StringLength(2250)]
		public string geo_shape { get; set; }

		public short code_du_departement { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<calcul_sieges> calcul_sieges { get; set; }

		[DataMember]
		[ForeignKey("code_du_departement")]
		public virtual Departement Departement { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<election> election { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<stats_election> stats_election { get; set; }

		/// <summary>
		/// Ins�re la cl� �trang�re dans la classe Commune
		/// </summary>
		/// <param name="com">La commune</param>
		/// <param name="dept">Le d�partement auquel appartient la commune</param>
		/// <returns></returns>
		public void insertionCleEtrangereCommune(Departement dept, short numDept, string libelleDepartement)
		{
			if (this.code_de_la_commune == "") this.code_de_la_commune = "vide";
			Departement queryDepartement;

			//Si le d�partement existait d�j�, alors il a �t� mis � 0
			if (dept.code_du_departement == 0)
			{

				using (var context = new electionEDM())
				{
					//On va donc aller chercher dans la BDD le numdept gard� en m�moire depuis le fichier csv. Si il existe dans la bdd
					//on attribut la valeur du code departement en cl� �trang�re de la commune
					try
					{
						queryDepartement = (from dpt in context.Departement
											where dpt.code_du_departement == numDept
											select dpt).Single();

						this.Departement = null; //On annule l'objet D�partement afin qu'entity n'essaie pas de l'ins�rer une nouvelle fois
												//dans la BDD
						this.code_du_departement = queryDepartement.code_du_departement;

					}

					catch
					{
						this.Departement = null;
						this.code_du_departement = numDept;
					}
				}

			}

			//Si le code_du_d�partement n'est pas �gal � 0, c'est qu'il n'�tait pas entr� dans la bdd
			else
			{
				using (var context = new electionEDM())
				{
					try
					{
						//par s�curit�, on s'assure qu'il n'existait pas d�j� quand m�me
						queryDepartement = (from dpt in context.Departement
											where dpt.code_du_departement == numDept
											select dpt).Single();

						this.Departement = null;
						this.code_du_departement = queryDepartement.code_du_departement;

					}

					//Si il n'existait pas, on assigne en cl� �trang�re de la commune, le num�ro de d�partement gard� en m�moire
					//lors de la r�cup�ration des donn�es dans le fichier csv
					catch
					{
						this.Departement = null;
						this.code_du_departement = numDept;
					}
				}



			}
		}

		/// <summary>
		/// Insertion Donn�es de la commune
		/// </summary>
		/// <param name="com"></param>
		public void insertionDonneesCommune(Departement dept)
		{

			using (var context = new electionEDM())
			{
				context.Configuration.LazyLoadingEnabled = false;
				string query;

				//On effectue une requ�te pour voir si la commune n'existe pas d�j� dans la bdd
				try
				{
					query = (from comm in context.Commune
							 where comm.insee == this.insee
							 select this.insee).Single();
				}

				//Si elle n'�tait pas pr�sente dans la bdd, on l'ins�re
				catch (InvalidOperationException e)
				{
					context.Commune.Add(this);

					try
					{
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
					}

				}



			}
		}

		/// <summary>
		/// Permet de r�initialiser un objet Commune � null
		/// </summary>
		/// <param name="comm">Commune</param>
		/// <returns></returns>
		public void reinitialisationCommune()
		{
			this.insee = "";
			this.libelle_de_la_commune = "";
			this.geo_shape = "";
			this.geo_point_2d = "";
			this.Departement = null;
			this.stats_election = null;
			this.calcul_sieges = null;
			this.election = null;
			this.code_du_departement = 0;
		}

	}
}
