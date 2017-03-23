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

	[Table("election")]
	public partial class election
	{
		public int? voix { get; set; }

		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int annee { get; set; }

		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int idCandidat { get; set; }

		[Key]
		[Column(Order = 2)]
		[StringLength(5)]
		public string insee { get; set; }

		[DataMember]
		[ForeignKey("annee")]
		public virtual AnneeElection AnneeElection { get; set; }

		[DataMember]
		[ForeignKey("idCandidat")]
		public virtual Candidat Candidat { get; set; }

		[DataMember]
		[ForeignKey("insee")]
		public virtual Commune Commune { get; set; }

		/// <summary>
		/// insertion des cl�s �trang�res de la table association election
		/// </summary>
		/// <param name="elect">La table association election</param>
		/// <param name="year">Ann�e de l'election municipale</param>
		/// <param name="candidat">Candidats � l'election municipales</param>
		/// <param name="comm">La commune o� a eu lieu l'election</param>
		/// <returns></returns>
		public void insertionCleEtrangereElection(election[] elect, AnneeElection year, Candidat[] candidat, Commune comm)
		{
			for (int i = 0; i < elect.Length; i++)
			{
				//Pour chaque entit� election on ins�re ses cl�s �trang�res : idCandidat, insee(Commune), annee(AnneeElection)
				if (elect[i].voix != 0)
				{
					elect[i].Candidat = null;
					elect[i].Commune = null;
					elect[i].AnneeElection = null;
					elect[i].idCandidat = candidat[i].idCandidat;
					elect[i].insee = comm.insee;
					elect[i].annee = year.annee;
				}
			}
		}

		/// <summary>
		/// Insertion des donn�es concernant la table association "election"
		/// </summary>
		/// <param name="elect">Table association : election</param>
		public void insertionDonneesElection(election[] elect)
		{
			using (var context = new electionEDM())
			{
				for (int i = 0; i < elect.Length; i++)
				{
					if (elect[i].voix != 0)
					{
						context.election.Add(elect[i]);

						//On ins�re les entit�s election dans la base de donn�es
						try
						{
							context.SaveChanges();
						}

						//Si l'insertion �choue
						catch
						{
							MessageBox.Show("L'insertion de l'entit� election "+i+" a �chou�");
						}

					}
				}
			}
		}
	}
}
