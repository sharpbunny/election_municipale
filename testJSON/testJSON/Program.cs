﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Data.Entity;

namespace testJSON
{
	class Program
	{
		static Candidat[] candidat = new Candidat[5];
		static Departement dept = new Departement();
		static Commune comm = new Commune();
		static stats_election stat = new stats_election();
		static Parti[] parti = new Parti[5];
		static Liste[] list = new Liste[5];
		static calcul_sieges[] csieges = new calcul_sieges[5];
		static election[] elect = new election[5];
		static bool leDepartementExiste;

		static void Main(string[] args)
		{
			AnneeElection year = new AnneeElection();
			year.annee = 2014;
			string[][] allData = lireToutesLesDonnees(); //Lire toutes les données depuis le fichier csv et les stocker dans allData

			for (int i = 0; i < allData.Length; i++)
			{
				reinitialisationTableauDeDonnees(candidat, parti, list, csieges, elect);
				for (int colonne = 0; colonne < 75; colonne++)
				{

					//comm = reinitialisationCommune(comm);
					//dept = reinitialisationDepartement(dept);
					//stat = reinitialisationStatsElection(stat);
					
					if (i > 0)
					{
						switch (colonne)
						{
							//code du département
							case 1:
								//Si le département n'existe pas, on modifie la classe Departement
								leDepartementExiste = leDepartementExisteDeja(Convert.ToSByte(allData[i][colonne]));
								if (!leDepartementExiste) dept.code_du_departement = Convert.ToSByte(allData[i][colonne]);
								//comm.Departement = new Departement();
								//comm.Departement.code_du_departement = Convert.ToSByte(allData[i][colonne]);
;
								break;

							//type du scrutin
							case 2:
								break;

							//libelle_du_departement
							case 3:

								if (!leDepartementExiste) dept.libelle_du_departement = allData[i][colonne];
								//comm.Departement.libelle_du_departement = allData[i][colonne];
								break;

							//code de la commune
							case 4:
								comm.code_de_la_commune = allData[i][colonne];
								break;

							//libelle_de_la_commune
							case 5:
								comm.libelle_de_la_commune = allData[i][colonne];
								break;

							//insee
							case 6:
								comm.insee = allData[i][colonne];
								break;

							//geo_point_2
							case 7:
								comm.geo_point_2d = allData[i][colonne];
								break;

							//geo_shape
							case 8:
								comm.geo_shape = allData[i][colonne];
								break;

							//inscrits
							case 9:
								stat.inscrits = Convert.ToInt32(allData[i][colonne]);
								break;

							//abstentions
							case 10:
								stat.abstentions = Convert.ToInt32(allData[i][colonne]);
								break;

							//votants
							case 12:
								stat.votants = Convert.ToInt32(allData[i][colonne]);
								break;

							//blancs_et_nuls
							case 14:
								stat.blancs_et_nuls = Convert.ToInt32(allData[i][colonne]);
								break;

							//exprimes
							case 17:
								stat.exprimes = Convert.ToInt32(allData[i][colonne]);
								break;

							//code_nuance
							case 20:
								parti[0].code_nuance = allData[i][colonne];
								break;

							//sexe_01
							case 21:
								candidat[0].sexe = allData[i][colonne];
								break;

							//nom_01
							case 22:
								candidat[0].nom = allData[i][colonne];
								break;

							//prenom_01
							case 23:
								candidat[0].prenom = allData[i][colonne];
								break;

							//liste_01
							case 24:
								list[0].nomListe = allData[i][colonne];
								break;

							//sieges_elu_01
							case 25:
								csieges[0].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_secteur_01
							case 26:
								csieges[0].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_cc_01
							case 27:
								csieges[0].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;

							//voix_01
							case 28:
								elect[0].voix = Convert.ToInt32(allData[i][colonne]);
								break;

							//code_nuance_02
							case 31:
								parti[1].code_nuance = allData[i][colonne];
								break;

							//sexe_02
							case 32:
								candidat[1].sexe = allData[i][colonne];
								break;

							//nom_02
							case 33:
								candidat[1].nom = allData[i][colonne];
								break;

							//prenom_02
							case 34:
								candidat[1].prenom = allData[i][colonne];
								break;
							
							//liste_02
							case 35:
								list[1].nomListe = allData[i][colonne];
								break;

							//sieges_elu_02
							case 36:
								csieges[1].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_secteur_02
							case 37:
								csieges[1].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_cc_02
							case 38:
								csieges[1].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;

							//voix_02
							case 39:
								elect[1].voix = Convert.ToInt32(allData[i][colonne]);
								break;

							// code nuance_03
							case 42:
								if (allData[i][colonne] != "") parti[2].code_nuance = allData[i][colonne];
								break;

							//sexe_03
							case 43:
								if (allData[i][colonne] != "") candidat[2].sexe = allData[i][colonne];
								break;

							//nom_03
							case 44:
								if (allData[i][colonne] != "") candidat[2].nom = allData[i][colonne];
								break;

							//prenom_03
							case 45:
								if (allData[i][colonne] != "") candidat[2].prenom = allData[i][colonne];
								break;

							//liste_03
							case 46:
								if (allData[i][colonne] != "") list[2].nomListe = allData[i][colonne];
								break;

							//sieges_elu_03
							case 47:
								if (allData[i][colonne] != "") csieges[2].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_secteur_03
							case 48:
								if (allData[i][colonne] != "") csieges[2].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_cc_03
							case 49:
								if (allData[i][colonne] != "") csieges[2].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;

							//voix_03
							case 50:
								if (allData[i][colonne] != "") elect[2].voix = Convert.ToInt32(allData[i][colonne]);
								break;

							// code nuance_04
							case 53:
								if (allData[i][colonne] != "") parti[3].code_nuance = allData[i][colonne];
								break;

							//sexe_04
							case 54:
								if (allData[i][colonne] != "") candidat[3].sexe = allData[i][colonne];
								break;

							//nom_04
							case 55:
								if (allData[i][colonne] != "") candidat[3].nom = allData[i][colonne];
								break;

							//prenom_04
							case 56:
								if (allData[i][colonne] != "") candidat[3].prenom = allData[i][colonne];
								break;

							//liste_04
							case 57:
								if (allData[i][colonne] != "") list[3].nomListe = allData[i][colonne];
								break;

							//sieges_elus_04
							case 58:
								if (allData[i][colonne] != "") csieges[3].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_secteur_04
							case 59:
								if (allData[i][colonne] != "") csieges[3].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_cc_04
							case 60:
								if (allData[i][colonne] != "") csieges[3].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;

							//voix_04
							case 61:
								if (allData[i][colonne] != "") elect[3].voix = Convert.ToInt32(allData[i][colonne]);
								break;

							// code_nuance_05
							case 64:
								if (allData[i][colonne] != "") parti[4].code_nuance = allData[i][colonne];
								break;

							//sexe_05
							case 65:
								if (allData[i][colonne] != "") candidat[4].sexe = allData[i][colonne];
								break;

							//nom_05
							case 66:
								if (allData[i][colonne] != "") candidat[4].nom = allData[i][colonne];
								break;

							//prenom_05
							case 67:
								if (allData[i][colonne] != "") candidat[4].prenom = allData[i][colonne];
								break;

							//liste_05
							case 68:
								if (allData[i][colonne] != "") list[4].nomListe = allData[i][colonne];
								break;

							//sieges_elu_05
							case 69:
								if(allData[i][colonne] != "") csieges[4].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_secteur_05
							case 70:
								if (allData[i][colonne] != "") csieges[4].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_cc_05
							case 71:
								if (allData[i][colonne] != "")  csieges[4].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;

							//voix_05
							case 72:
								if (allData[i][colonne] != "") elect[4].voix = Convert.ToInt32(allData[i][colonne]);
								break;

						} //Fin du switch

						if(colonne == 74)
						{


							using (var context = new election_municipaleEntities())
							{

								insertionDonneesDepartement(dept);
								insertionDonneesParti(parti);

								comm = insertionCleEtrangereCommune(comm, Convert.ToSByte(allData[i][1]), allData[i][3]);
								insertionDonneesCommune(comm, dept);

								list = insertionCleEtrangereListe(list, parti);
								insertionDonneesListe(list);

								candidat = insertionCleEtrangereCandidat(candidat, list);
								insertionDonneesCandidat(candidat);

								elect = insertionCleEtrangereElection(elect, year, candidat, comm);
								insertionDonneesElection(elect);

								stat = insertionCleEtrangereStatsElection(stat, year, comm);
								insertionDonneesStatElection(stat);

								csieges = insertionCleEtrangereCalculSieges(csieges, comm, year, list);
								insertionDonneesCalculSieges(csieges, comm, year, list);

								Console.WriteLine(i+ " insertion");
							}
						}


					} //Fin du if



				}//Fin du for des colonnes


			} //Fin du for pour les lignes

		} //Fin du main

		// ******************************************************
		//				METHODES UTILES
		// ******************************************************

		/// <summary>
		/// Permet de réinitialiser tous les tableaux utilisés dans le programme à null
		/// </summary>
		/// <param name="candidat">Tableau de candidats</param>
		/// <param name="parti">Tableau contenant les partis politiques</param>
		/// <param name="list">Tableau contenant les listes electorales</param>
		/// <param name="csiege">Tableau contenant les tables associations : calcul_sieges</param>
		/// <param name="elec">Tableau contenant les tables association : elec</param>
		public static void reinitialisationTableauDeDonnees(Candidat[] candidat, Parti [] parti, Liste [] list, calcul_sieges [] csiege, election [] elec)
		{
			for(int i=0; i < candidat.Length; i++)
			{
				candidat[i] = new Candidat(); ;
				parti[i] = new Parti();
				list[i] = new Liste();
				csiege[i] = new calcul_sieges(); ;
				elec[i] = new election();
			}
		}

		/// <summary>
		/// Permet de réinitialiser un objet Département à null
		/// </summary>
		/// <param name="dept">Département</param>
		/// <returns></returns>
		public static Departement reinitialisationDepartement(Departement dept)
		{
			dept = null;
			return dept;
		}

		/// <summary>
		/// Permet de réinitialiser un objet Commune à null
		/// </summary>
		/// <param name="comm">Commune</param>
		/// <returns></returns>
		public static Commune reinitialisationCommune(Commune comm)
		{
			comm = null;
			return comm;
		}

		/// <summary>
		/// Permet de réinitialiser la table association : stats_election à null
		/// </summary>
		/// <param name="stat">Table association : stats_election</param>
		/// <returns></returns>
		public static stats_election reinitialisationStatsElection(stats_election stat)
		{
			stat = null;
			return stat;
		}

		// *****************************************************************
		//			FONCTIONS DE LECTURE DE DONNEES DEPUIS LE CSV
		// *****************************************************************

		/// <summary>
		/// A un moment, tu peux lire le JSON, mais sa mère le csv c'est mieux
		/// </summary>
		/// <returns></returns>
		public static string[][] lireToutesLesDonnees()
		{
			string[] line;
			string[][] allData = new String[232][];

			line = File.ReadAllLines(@"election.csv");

			for (int i = 0; i < line.Length; i++)
			{
				allData[i] = line[i].Split(';');
			}

			return allData;
		}

		// ******************************************************
		//				AFFECTATION DES CLES ETRANGERES
		// ******************************************************

		/// <summary>
		/// Insère la clé étrangère dans la classe Commune
		/// </summary>
		/// <param name="com">La commune</param>
		/// <param name="dept">Le département auquel appartient la commune</param>
		/// <returns></returns>
		public static Commune insertionCleEtrangereCommune(Commune com, short numDept, string libelleDepartement)
		{

			Departement queryDepartement;

			if(dept.code_du_departement == 0)
			{
				Console.WriteLine("La variable département est nulle.");
				using(var context = new election_municipaleEntities())
				{
					try
					{
						queryDepartement = (from dpt in context.Departement
											where dpt.code_du_departement == numDept
											select dpt).Single();

						com.Departement = queryDepartement;

					}

					catch
					{
						com.Departement.code_du_departement = numDept;
					}
				}

			}
			else
			{
				using(var context = new election_municipaleEntities())
				{
					try
					{
						queryDepartement = (from dpt in context.Departement
											where dpt.code_du_departement == numDept
											select dpt).Single();

						com.Departement = queryDepartement;

					}

					catch
					{
						com.Departement.code_du_departement = numDept;
					}
				}



			}

			return com;
		}

		/// <summary>
		/// Insertion de la clé étrangère Parti dans les listes éléctorales
		/// </summary>
		/// <param name="liste">Tableau de listes électorales</param>
		/// <param name="parti">Tableau de partis politiques</param>
		/// <returns></returns>
		public static Liste [] insertionCleEtrangereListe(Liste[] liste, Parti [] parti)
		{
			for(int i = 0; i<liste.Length; i++)
			{
				if(liste[i] != null && parti[i] != null)
				{
					liste[i].Parti = parti[i];
				}
			}

			return liste;
		}

		/// <summary>
		/// Insertion de la clé étrangère Liste dans la classe Candidat
		/// </summary>
		/// <param name="candidat">Tableau de candidats</param>
		/// <param name="list">Tableau de listes électorales</param>
		/// <returns></returns>
		public static Candidat[] insertionCleEtrangereCandidat(Candidat [] candidat, Liste[] list)
		{
			for(int i = 0; i < candidat.Length; i++)
			{

					if (candidat[i] != null && list[i] != null)
					{
						candidat[i].Liste = list[i];
					}

			}

			return candidat;

		}

		/// <summary>
		/// insertion des clés étrangères de la table association election
		/// </summary>
		/// <param name="elect">La table association election</param>
		/// <param name="year">Année de l'election municipale</param>
		/// <param name="candidat">Candidats à l'election municipales</param>
		/// <param name="comm">La commune où a eu lieu l'election</param>
		/// <returns></returns>
		public static election [] insertionCleEtrangereElection(election[] elect, AnneeElection year, Candidat [] candidat, Commune comm)
		{
			for(int i=0; i<elect.Length; i++)
			{
				if(elect[i] != null)
				{
					elect[i].Candidat = candidat[i];
					elect[i].Commune = comm;
					elect[i].AnneeElection = year;
				}
			}

			return elect;
		}
		
		/// <summary>
		/// Insertion des clés étrangères relatives à la table association : stats_election
		/// </summary>
		/// <param name="stat">La table association : stats_election</param>
		/// <param name="year">Année de l'élection municipale</param>
		/// <param name="comm">Nom de la commune de laquelle on va récupérer des statistiques</param>
		/// <returns></returns>
		public static stats_election insertionCleEtrangereStatsElection(stats_election stat, AnneeElection year, Commune comm)
		{
			if(stat != null)
			{
				stat.AnneeElection = year;
				stat.Commune = comm;
			}

			return stat;
		}

		/// <summary>
		/// Insertion des clés étrangères relatives au calcul des sièges alloués selon les résultats des élections
		/// </summary>
		/// <param name="csiege">Tableau de table association : calcul_sieges</param>
		/// <param name="comm">Commune dans laquelle on indique les sièges alloués à certaines listes</param>
		/// <param name="year">Année de l'election</param>
		/// <param name="liste">Tableau de listes electorales</param>
		/// <returns></returns>
		public static calcul_sieges [] insertionCleEtrangereCalculSieges(calcul_sieges [] csiege, Commune comm, AnneeElection year, Liste[] liste)
		{
			for(int i = 0; i < csiege.Length; i++)
			{
				if(csiege[i] != null && liste[i] != null)
				{
					csiege[i].Commune = comm;
					csiege[i].AnneeElection = year;
					csiege[i].Liste = liste[i];
				}
			}

			return csiege;
		}


		// ******************************************************
		//				INSERTION DONNEES BDD
		// ******************************************************

		/// <summary>
		/// On va insérer les données relatives aux candidats à l'élection municipale dans la base de données
		/// </summary>
		public static void insertionDonneesCandidat(Candidat[] candidat)
		{
			using (var context = new election_municipaleEntities())
			{

				for (int i = 0; i < candidat.Length; i++)
				{

					int query;

					if (candidat[i] != null)
					{
						//On fait une requête pour voir si l'id du candidat n'existe pas déjà dans la bdd
						try
						{
							query = (from candid in context.Candidat
										 where candid.idCandidat == candidat[i].idCandidat
										 select candid.idCandidat).Single();
						}

						catch
						{
							context.Candidat.Add(candidat[i]);
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

			}
		}

		/// <summary>
		/// On va insérer les données relatives aux départements
		/// </summary>
		/// <param name="dpt">Le département</param>
		public static void insertionDonneesDepartement(Departement dpt)
		{



			using (var context = new election_municipaleEntities())
			{
				context.Configuration.LazyLoadingEnabled = false;
				short query;
				try
				{
					query = (from dept in context.Departement
							 where dept.code_du_departement == dpt.code_du_departement
							 select dept.code_du_departement).Single();

				}


				catch (InvalidOperationException e)
				{
					Console.WriteLine("query catch exception");
					if(dpt.code_du_departement != 0)
					{
						context.Departement.Add(dpt);
						try
						{
							Console.WriteLine(dpt.code_du_departement);
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


					//foreach (var departement in context.Departement)
					//{
					//	Console.WriteLine(departement.code_du_departement);
					//}
				}

			}


		}

		/// <summary>
		/// Insertion Données de la commune
		/// </summary>
		/// <param name="com"></param>
		public static void insertionDonneesCommune(Commune com, Departement dept)
		{



			using (var context = new election_municipaleEntities())
			{
				context.Configuration.LazyLoadingEnabled = false;
				string query;

				try
				{
					query = (from comm in context.Commune
								where comm.insee == com.insee
								select com.insee).Single();
				}
				catch(InvalidOperationException e)
				{
					context.Commune.Add(com);

					//try
					//{
					//	context.SaveChanges();
					//}

					//catch (System.Data.Entity.Validation.DbEntityValidationException a)
					//{
					//	foreach (var eve in a.EntityValidationErrors)
					//	{
					//		Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
					//			eve.Entry.Entity.GetType().Name, eve.Entry.State);
					//		foreach (var ve in eve.ValidationErrors)
					//		{
					//			Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
					//				ve.PropertyName, ve.ErrorMessage);
					//		}
					//	}
					//	throw;
					//}

				}



			}
		}

		/// <summary>
		/// Insertion des données dans la BDD des stats relatives aux élections pour une commune
		/// </summary>
		/// <param name="stat"></param>
		public static void insertionDonneesStatElection(stats_election stat)
		{
			using (var context = new election_municipaleEntities())
			{
				if(stat != null)
				{
					context.stats_election.Add(stat);
				}

			}
		}
		
		/// <summary>
		/// insertion des listes éléctorales dans la base de données
		/// </summary>
		/// <param name="list">Tableau de listes électorales</param>
		public static void insertionDonneesListe(Liste [] list)
		{
			using(var context = new election_municipaleEntities())
			{
				//On parcourt le tableau de listes electorales
				for(int i=0; i< list.Length; i++)
				{
					Liste listTemp = list[i];
					string query;

					if (list[i].nomListe != "")
					{
						try
						{
							query = (from liste in context.Liste
									 where liste.nomListe == listTemp.nomListe
									 select liste.nomListe).Single();
						}

						catch(InvalidOperationException e)
						{
							context.Liste.Add(list[i]);
							try
							{
								context.SaveChanges();
							}

							catch (System.Data.Entity.Validation.DbEntityValidationException a)
							{
								Console.WriteLine("list "+i+" : a échoué");
							}

						}


					}
				}

			}
		}

		/// <summary>
		/// Insertion des données dans la BDD des partis politiques
		/// </summary>
		/// <param name="parti">Parti politique</param>
		public static void insertionDonneesParti(Parti [] parti)
		{

			using(var context = new election_municipaleEntities())
			{

				string query;

				//On va parcourir le tableau de partis
				for(int i = 0; i < parti.Length; i++)
				{
					Parti partiTemp = parti[i];

					if(partiTemp.code_nuance != "")
					{
						try
						{
							query = (from part in context.Parti
									 where part.code_nuance == partiTemp.code_nuance
									 select part.code_nuance).Single();
						}

						catch(InvalidOperationException e)
						{
							context.Parti.Add(parti[i]);
							try
							{
								context.SaveChanges();
							}

							catch (System.Data.Entity.Validation.DbEntityValidationException a)
							{
								Console.WriteLine("parti "+i+" : a échoué lors du savechanges");
							}

						}

					}



				}
			}
		}

		/// <summary>
		/// Insertion des données concernant la table association "election"
		/// </summary>
		/// <param name="elect">Table association : election</param>
		public static void insertionDonneesElection(election [] elect)
		{
			using(var context = new election_municipaleEntities())
			{
				for(int i =0; i < elect.Length; i++)
				{
					if(elect[i] != null)
					{
						context.election.Add(elect[i]);
						try
						{

						}

						catch
						{
							context.SaveChanges();
						}

					}
				}
			}
		}

		/// <summary>
		/// insertion de la table stockant le nombre de sièges affectés à une commune
		/// </summary>
		/// <param name="csiege"></param>
		public static void insertionDonneesCalculSieges(calcul_sieges [] csiege, Commune com, AnneeElection year, Liste [] list)
		{
			for(int i=0; i < csiege.Length; i++)
			{
				if(csiege != null)
				{
					csiege[i].Commune = com;
					csiege[i].AnneeElection = year;
					csiege[i].Liste = list[i];
				}
			}
		}

		public static bool leDepartementExisteDeja(short code_du_departement)
		{
			bool leDepartementExiste = false;

			using(var context = new election_municipaleEntities())
			{
				try
				{
					var query = (from dept in context.Departement
								 where code_du_departement == dept.code_du_departement
								 select dept).Single();
					leDepartementExiste = true;
				}

				catch
				{
					leDepartementExiste = false;
				}

			}

			return leDepartementExiste;
		}
	}
}









