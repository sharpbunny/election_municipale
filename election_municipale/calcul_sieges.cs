namespace election_municipale
{
	using System;
	using System.Windows;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity;
	using System.Data.Entity.Spatial;
	using System.Runtime.Serialization;
	using System.Linq;
	using System.Xml.Serialization;

	public partial class calcul_sieges
	{
		public short? sieges_elus { get; set; }

		public short? sieges_cc { get; set; }

		public short? sieges_secteurs { get; set; }

		[Key]
		[Column(Order = 0)]
		[StringLength(5)]
		public string insee { get; set; }

		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int annee { get; set; }

		[Key]
		[Column(Order = 2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int idListe { get; set; }

		[DataMember]
		[ForeignKey("annee")]
		public virtual AnneeElection AnneeElection { get; set; }

		[DataMember]
		[ForeignKey("idListe")]
		public virtual Liste Liste { get; set; }

		[DataMember]
		[ForeignKey("insee")]
		public virtual Commune Commune { get; set; }

		/// <summary>
		/// Insertion des clés étrangères relatives au calcul des sièges alloués selon les résultats des élections
		/// </summary>
		/// <param name="csiege">Tableau de table association : calcul_sieges</param>
		/// <param name="comm">Commune dans laquelle on indique les sièges alloués à certaines listes</param>
		/// <param name="year">Année de l'election</param>
		/// <param name="liste">Tableau de listes electorales</param>
		/// <returns></returns>
		public calcul_sieges[] insertionCleEtrangereCalculSieges(calcul_sieges[] csiege, Commune comm, AnneeElection year, Liste[] liste)
		{
			using (var context = new electionEDM())
			{
				context.Liste.Load();
				Liste listeTemp;
				for (int i = 0; i < csiege.Length; i++)
				{
					listeTemp = liste[i];
					//On effectue une requête pour savoir si la liste à laquelle est liée calcul_sieges existe bien dans la BDD
					try
					{
						var query = (from list in context.Liste
									 where list.nomListe == listeTemp.nomListe
									 select list.idListe).Single();

						csiege[i].Commune = null;
						csiege[i].AnneeElection = null;
						csiege[i].Liste = null;
						csiege[i].insee = comm.insee;
						csiege[i].annee = year.annee;
						csiege[i].idListe = query;
					}

					//Si la liste n'existe pas dans la BDD
					catch
					{

					}

				} //fin de la boucle for

			} //fin du using

			return csiege;
		}

		/// <summary>
		/// insertion de la table stockant le nombre de sièges affectés à une commune
		/// </summary>
		/// <param name="csiege"></param>
		public void insertionDonneesCalculSieges(calcul_sieges[] csiege, Commune com, AnneeElection year, Liste[] list)
		{
			using (var context = new electionEDM())
			{
				for (int i = 0; i < csiege.Length; i++)
				{
					//On recherche dans la BDD si l'objet calcul_sieges existe déjà
					try
					{
						var query = (from csieges in context.calcul_sieges
									 where csieges.insee == com.insee && csieges.annee == year.annee && csieges.idListe == list[i].idListe
									 select csieges).Single();
					}

					//Si il n'existe pas, on l'insère dans la BDD
					catch
					{
						context.calcul_sieges.Add(csiege[i]);

						try
						{
							context.SaveChanges();
						}

						//Si l'insertion dans la base de données échoue
						catch
						{

						}
					}

				}
			}

		}
	}
}
