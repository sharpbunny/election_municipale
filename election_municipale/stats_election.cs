namespace election_municipale
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Runtime.Serialization;
	using System.Xml.Serialization;

	public partial class stats_election
	{
		public int? inscrits { get; set; }

		public int? abstentions { get; set; }

		public int? blancs_et_nuls { get; set; }

		public int? exprimes { get; set; }

		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int annee { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(5)]
		public string insee { get; set; }

		public int? votants { get; set; }
		[DataMember]
		[ForeignKey("annee")]
		public virtual AnneeElection AnneeElection { get; set; }

		[DataMember]
		[ForeignKey("insee")]
		public virtual Commune Commune { get; set; }

		/// <summary>
		/// Insertion des clés étrangères relatives à la table association : stats_election
		/// </summary>
		/// <param name="stat">La table association : stats_election</param>
		/// <param name="year">Année de l'élection municipale</param>
		/// <param name="comm">Nom de la commune de laquelle on va récupérer des statistiques</param>
		/// <returns></returns>
		public static stats_election insertionCleEtrangereStatsElection(stats_election stat, AnneeElection year, Commune comm)
		{

			stat.AnneeElection = null;
			stat.Commune = null;
			stat.annee = year.annee;
			stat.insee = comm.insee;

			return stat;
		}

		/// <summary>
		/// Insertion des données dans la BDD des stats relatives aux élections pour une commune
		/// </summary>
		/// <param name="stat"></param>
		public static void insertionDonneesStatElection(stats_election stat, AnneeElection year, Commune comm)
		{
			using (var context = new electionEDM())
			{

				//On regarde si stats_election est déjà dans la BDD
				try
				{
					var query = (from stats in context.stats_election
								 where (stats.annee == year.annee && comm.insee == stats.insee)
								 select stats).Single();

					Console.WriteLine("Cet objet stats_election est déjà dans la BDD");
				}

				//Si stats_election n'est pas dans la BDD, on l'insère
				catch
				{
					context.stats_election.Add(stat);
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

		/// <summary>
		/// Permet de réinitialiser les attributs de la table-association stats_election à null
		/// </summary>
		/// <param name="stat">Table association : stats_election</param>
		/// <returns></returns>
		public void reinitialisationStatsElection()
		{
			this.inscrits = 0;
			this.votants = 0;
			this.exprimes = 0;
			this.blancs_et_nuls = 0;
			this.AnneeElection = null;
			this.Commune = null;
			this.abstentions = 0;

		}
	}
}
