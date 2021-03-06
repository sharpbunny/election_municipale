namespace election_municipale
{
	using System;
	using System.Windows;
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
		/// Insertion des cl�s �trang�res relatives � la table association : stats_election
		/// </summary>
		/// <param name="stat">La table association : stats_election</param>
		/// <param name="year">Ann�e de l'�lection municipale</param>
		/// <param name="comm">Nom de la commune de laquelle on va r�cup�rer des statistiques</param>
		/// <returns></returns>
		public void insertionCleEtrangereStatsElection(AnneeElection year, Commune comm)
		{
			//Insertion des cl�s �trang�res dans stats_election : annee(AnneeElection), insee(Commune)
			this.AnneeElection = null;
			this.Commune = null;
			this.annee = year.annee;
			this.insee = comm.insee;

		}

		/// <summary>
		/// Insertion des donn�es dans la BDD des stats relatives aux �lections pour une commune
		/// </summary>
		/// <param name="stat"></param>
		public void insertionDonneesStatElection(AnneeElection year, Commune comm)
		{
			using (var context = new electionEDM())
			{

				//On regarde si stats_election est d�j� dans la BDD
				try
				{
					var query = (from stats in context.stats_election
								 where (stats.annee == year.annee && stats.insee == comm.insee)
								 select stats).Single();

				}

				//Si stats_election n'est pas dans la BDD, on l'ins�re
				catch
				{
					context.stats_election.Add(this);

					//On l'ins�re dans la base de donn�es
					try
					{
						context.SaveChanges();
					}

					//Si l'insertion �choue
					catch
					{

					}
				}

			}
		}

		/// <summary>
		/// Permet de r�initialiser les attributs de la table-association stats_election � null
		/// </summary>
		/// <param name="stat">Table association : stats_election</param>
		/// <returns></returns>
		public void reinitialisationStatsElection()
		{
			this.insee = "";
			this.annee = 0;
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
