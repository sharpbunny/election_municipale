using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;

namespace election_municipale
{
	class Program
	{
		

		static void Main(string[] args)
		{
			int choixMenu = 0;

			do
			{
				choixMenu = affichageDuMenu();
				Console.Clear();
				fonctionAppeleeEnFonctionDuChoixDuMenu(choixMenu);


			} while (true);





		} //Fin du main

		// ******************************************************
		//				MENU DU PROGRAMME
		// ******************************************************

		/// <summary>
		/// Permet d'afficher le menu du programme
		/// </summary>
		public static int affichageDuMenu()
		{
			int choixMenu;
			do
			{
				Console.WriteLine("---- Menu des elections municipales ----");
				Console.WriteLine("1. Insérer les données depuis un fichier csv");
				Console.WriteLine("2. Avoir la liste de toutes les communes par ordre alphabétique");
				Console.WriteLine("3. Liste des communes groupés par département et classées par ordre alphabétique");
				Console.WriteLine("4. Afficher la commune ayant le plus fort taux de votants");
				Console.WriteLine("5. Afficher la commune ayant le plus fort taux d'abstentions");
				Console.WriteLine("6. Connaitre le pourcentage de femmes elues");
				Console.WriteLine("7. Connaître le prénom masculin et féminin le plus fréquent parmi les sièges élues");
				Console.WriteLine("8. Connaître la commune ayant le plus d'élues femme en nombre");
				Console.WriteLine("9. Connaître la commune ayant le plus d'élues femme en pourcentage");
				Console.WriteLine("10. Connaître le département ayant le plus d'élues femme en pourcentage");
				Console.Write("Votre choix : ");
				
			} while (!int.TryParse(Console.ReadLine(), out choixMenu) && choixMenu<1 && choixMenu>10);

			return choixMenu;
		}

		/// <summary>
		/// On appelle la fonction correspondant au choix de l'utilisateur dans le menu
		/// </summary>
		/// <param name="choixMenu"></param>
		public static void fonctionAppeleeEnFonctionDuChoixDuMenu(int choixMenu)
		{
			switch (choixMenu)
			{
				case 1:
					recuperationDesDonnees();
					break;

				case 2:
					listeCommunesOrdreAlphabetique();
					break;

				case 3:
					listeCommunesGroupesParDepartement();
					break;

				case 4:
					communeAyantLePlusFortTauxDeVotants();
					break;

				case 5:
					communeAyantLePlusFortTauxAbstentions();
					break;

				case 6:
					pourcentageFemmesElues();
					break;

				case 7:
					AfficherLePrenomFemininEtMasculinLePlusFrequentParmiLesElus();
					break;

				case 8:
					break;

				case 9:
					break;

				case 10:
					break;

				default:
					break;

			}
		}

		// ******************************************************
		//				METHODES UTILES
		// ******************************************************

		/// <summary>
		/// Fonction permettant de réinitialiser le double tableau qui récupère les données dans le fichier csv
		/// </summary>
		/// <param name="allData">Tableau de données qui récupère les données dans le fichier csv</param>
		public static void reinitialisationAllData(String [][] allData)
		{
			for(int i=0; i < allData.Length; i++)
			{
				for(int j = 0; j < 75; j++)
				{
					allData[i][j] = null;
				}
			}
		}

		/// <summary>
		/// Permet de réinitialiser tous les tableaux utilisés dans le programme à null
		/// </summary>
		/// <param name="candidat">Tableau de candidats</param>
		/// <param name="parti">Tableau contenant les partis politiques</param>
		/// <param name="list">Tableau contenant les listes electorales</param>
		/// <param name="csiege">Tableau contenant les tables associations : calcul_sieges</param>
		/// <param name="elec">Tableau contenant les tables association : elec</param>
		public static void reinitialisationTableauDeDonnees(Candidat[] candidat, Parti[] parti, Liste[] list, calcul_sieges[] csiege, election[] elec)
		{
			for (int i = 0; i < candidat.Length; i++)
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
			dept.code_du_departement = 0;
			dept.libelle_du_departement = "";
			dept.Commune = null;
			return dept;
		}

		/// <summary>
		/// Permet de réinitialiser un objet Commune à null
		/// </summary>
		/// <param name="comm">Commune</param>
		/// <returns></returns>
		public static Commune reinitialisationCommune(Commune comm)
		{
			comm.insee = "";
			comm.libelle_de_la_commune = "";
			comm.geo_shape = "";
			comm.geo_point_2d = "";
			comm.Departement = null;
			comm.stats_election = null;
			comm.calcul_sieges = null;
			comm.election = null;
			comm.code_du_departement = 0;
			return comm;
		}

		/// <summary>
		/// Permet de réinitialiser la table association : stats_election à null
		/// </summary>
		/// <param name="stat">Table association : stats_election</param>
		/// <returns></returns>
		public static stats_election reinitialisationStatsElection(stats_election stat)
		{
			stat.inscrits = 0;
			stat.votants = 0;
			stat.exprimes = 0;
			stat.blancs_et_nuls = 0;
			stat.AnneeElection = null;
			stat.Commune = null;
			stat.abstentions = 0;
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

		/// <summary>
		/// Permet de récupérer toutes les données issues du fichier csv election_municipale_2014
		/// </summary>
		public static void recuperationDesDonnees()
		{
			Candidat[] candidat = new Candidat[5];
			Departement dept = new Departement();
			Commune comm = new Commune();
			stats_election stat = new stats_election();
			Parti[] parti = new Parti[5];
			Liste[] list = new Liste[5];
			calcul_sieges[] csieges = new calcul_sieges[5];
			election[] elect = new election[5];
			bool leDepartementExiste;


			AnneeElection year = new AnneeElection();
			year.annee = 2014;
			insertionAnnee(year);

			string[][] allData = lireToutesLesDonnees(); //Lire toutes les données depuis le fichier csv et les stocker dans allData

			for (int i = 0; i < allData.Length; i++)
			{
				reinitialisationTableauDeDonnees(candidat, parti, list, csieges, elect);
				comm = reinitialisationCommune(comm);
				dept = reinitialisationDepartement(dept);
				stat = reinitialisationStatsElection(stat);


				for (int colonne = 0; colonne < 75; colonne++)
				{



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
								leDepartementExiste = leDepartementExisteDeja(Convert.ToSByte(allData[i][1]));
								if (!leDepartementExiste) dept.libelle_du_departement = allData[i][colonne];
								//comm.Departement.libelle_du_departement = allData[i][colonne];
								break;

							//code de la commune
							case 4:
								if (allData[i][colonne] == "") comm.code_de_la_commune = "vide";
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
								if (allData[i][colonne] != "") csieges[4].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_secteur_05
							case 70:
								if (allData[i][colonne] != "") csieges[4].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_cc_05
							case 71:
								if (allData[i][colonne] != "") csieges[4].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;

							//voix_05
							case 72:
								if (allData[i][colonne] != "") elect[4].voix = Convert.ToInt32(allData[i][colonne]);
								break;

						} //Fin du switch

						if (colonne == 74)
						{


							using (var context = new electionEDM())
							{

								insertionDonneesDepartement(dept);
								insertionDonneesParti(parti);

								comm = insertionCleEtrangereCommune(comm, dept, Convert.ToSByte(allData[i][1]), allData[i][3]);
								insertionDonneesCommune(comm, dept);

								list = insertionCleEtrangereListe(list, parti);
								insertionDonneesListe(list);

								candidat = insertionCleEtrangereCandidat(candidat, list);
								insertionDonneesCandidat(candidat);

								elect = insertionCleEtrangereElection(elect, year, candidat, comm);
								insertionDonneesElection(elect);

								stat = insertionCleEtrangereStatsElection(stat, year, comm);
								insertionDonneesStatElection(stat, year, comm);

								csieges = insertionCleEtrangereCalculSieges(csieges, comm, year, list);
								insertionDonneesCalculSieges(csieges, comm, year, list);

								Console.WriteLine(" --- Fin de l'insertion de la ligne " + i);
							}
						}


					} //Fin du if



				}//Fin du for des colonnes


			} //Fin du for pour les lignes
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
		public static Commune insertionCleEtrangereCommune(Commune com, Departement dept, short numDept, string libelleDepartement)
		{
			if (com.code_de_la_commune == "") com.code_de_la_commune = "vide";
			Departement queryDepartement;

			if (dept.code_du_departement == 0)
			{
				Console.WriteLine("La variable département est nulle.");
				using (var context = new electionEDM())
				{
					try
					{
						queryDepartement = (from dpt in context.Departement
											where dpt.code_du_departement == numDept
											select dpt).Single();

						com.Departement = null;
						com.code_du_departement = queryDepartement.code_du_departement;

					}

					catch
					{
						com.Departement = null;
						com.code_du_departement = numDept;
					}
				}

			}
			else
			{
				using (var context = new electionEDM())
				{
					try
					{
						queryDepartement = (from dpt in context.Departement
											where dpt.code_du_departement == numDept
											select dpt).Single();

						com.Departement = null;
						com.code_du_departement = queryDepartement.code_du_departement;

					}

					catch
					{
						com.Departement = null;
						com.code_du_departement = numDept;
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
		public static Liste[] insertionCleEtrangereListe(Liste[] liste, Parti[] parti)
		{
			for (int i = 0; i < liste.Length; i++)
			{
				if (liste[i].code_nuance != "" && parti[i].code_nuance != "")
				{
					liste[i].Parti = null;
					liste[i].code_nuance = parti[i].code_nuance;
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
		public static Candidat[] insertionCleEtrangereCandidat(Candidat[] candidat, Liste[] list)
		{
			for (int i = 0; i < candidat.Length; i++)
			{

				if (candidat[i].nom != null && list[i].nomListe != null)
				{
					candidat[i].Liste = null;
					candidat[i].idListe = list[i].idListe;
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
		public static election[] insertionCleEtrangereElection(election[] elect, AnneeElection year, Candidat[] candidat, Commune comm)
		{
			for (int i = 0; i < elect.Length; i++)
			{
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

				stat.AnneeElection = null;
				stat.Commune = null;
				stat.annee = year.annee;
				stat.insee = comm.insee;

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
		public static calcul_sieges[] insertionCleEtrangereCalculSieges(calcul_sieges[] csiege, Commune comm, AnneeElection year, Liste[] liste)
		{
			int attentes = 4;
			using(var context = new electionEDM())
			{
				context.Liste.Load();
				Liste listeTemp;
				for (int i = 0; i < csiege.Length; i++)
				{
					listeTemp = liste[i];
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
						int attente = 2;
					}

					catch
					{
						Console.WriteLine("La requête d'insertion de clé étrangère dans calcul_sieges a échoué");
					}

				} //fin de la boucle for

			} //fin du using

			return csiege;
		}


		// ******************************************************
		//				INSERTION DONNEES BDD
		// ******************************************************

		/// <summary>
		/// Insertion dans la base de données de l'entité AnneeElection
		/// </summary>
		/// <param name="year">Entité : AnneeElection</param>
		public static void insertionAnnee(AnneeElection year)
		{
			using(var context = new electionEDM())
			{
				try
				{
					AnneeElection query = (from annee in context.AnneeElection
										   where annee.annee == year.annee
										   select annee).Single();
				}

				catch
				{
					context.AnneeElection.Add(year);
					context.SaveChanges();
				}



			}
		}

		/// <summary>
		/// On va insérer les données relatives aux candidats à l'élection municipale dans la base de données
		/// </summary>
		public static void insertionDonneesCandidat(Candidat[] candidat)
		{
			using (var context = new electionEDM())
			{

				for (int i = 0; i < candidat.Length; i++)
				{

					int query;

					if (candidat[i].nom != null)
					{
						//On fait une requête pour voir si l'id du candidat n'existe pas déjà dans la bdd
						try
						{
							query = (from candid in context.Candidat
									 where candid.nom == candidat[i].nom && candid.prenom == candidat[i].prenom
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



			using (var context = new electionEDM())
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
					if (dpt.code_du_departement != 0)
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



			using (var context = new electionEDM())
			{
				context.Configuration.LazyLoadingEnabled = false;
				string query;

				try
				{
					query = (from comm in context.Commune
							 where comm.insee == com.insee
							 select com.insee).Single();
				}
				catch (InvalidOperationException e)
				{
					context.Commune.Add(com);

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
		/// insertion des listes éléctorales dans la base de données
		/// </summary>
		/// <param name="list">Tableau de listes électorales</param>
		public static void insertionDonneesListe(Liste[] list)
		{
			using (var context = new electionEDM())
			{
				//On parcourt le tableau de listes electorales
				for (int i = 0; i < list.Length; i++)
				{
					Liste listTemp = list[i];
					string query;

					if (list[i].nomListe != null)
					{
						try
						{
							query = (from liste in context.Liste
									 where liste.nomListe == listTemp.nomListe
									 select liste.nomListe).Single();
						}

						catch (InvalidOperationException e)
						{
							context.Liste.Add(list[i]);
							try
							{
								context.SaveChanges();
							}

							catch (System.Data.Entity.Validation.DbEntityValidationException a)
							{
								Console.WriteLine("list " + i + " : a échoué");
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

		/// <summary>
		/// Insertion des données concernant la table association "election"
		/// </summary>
		/// <param name="elect">Table association : election</param>
		public static void insertionDonneesElection(election[] elect)
		{
			using (var context = new electionEDM())
			{
				for (int i = 0; i < elect.Length; i++)
				{
					if (elect[i].voix != 0)
					{
						context.election.Add(elect[i]);
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

		/// <summary>
		/// insertion de la table stockant le nombre de sièges affectés à une commune
		/// </summary>
		/// <param name="csiege"></param>
		public static void insertionDonneesCalculSieges(calcul_sieges[] csiege, Commune com, AnneeElection year, Liste[] list)
		{
			using(var context = new electionEDM())
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

						catch
						{
							Console.WriteLine("Erreur lors de l'insertion de calcul_sieges dans la BDD");
						}
					}

				}
			}

		}


		// ******************************************************
		//				REQUETES SUR LA BASE DE DONNEES
		// ******************************************************

		/// <summary>
		/// Permet de tester si le département existe déjà dans la base de données
		/// </summary>
		/// <param name="code_du_departement">Numéro du département</param>
		/// <returns></returns>
		public static bool leDepartementExisteDeja(short code_du_departement)
		{
			bool leDepartementExiste = false;

			using (var context = new electionEDM())
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

		/// <summary>
		/// Affiche la liste des communes classées par ordre alphabétique
		/// </summary>
		public static void listeCommunesOrdreAlphabetique()
		{
			using(var context = new electionEDM())
			{
				var query = from com in context.Commune
							orderby com.libelle_de_la_commune ascending
							select com;

				foreach(Commune comm in query)
				{
					Console.WriteLine(comm.libelle_de_la_commune);
				}
			}
		}

		/// <summary>
		/// Avoir la liste des communes groupées par département et classées par ordre alphabétique en fonction du nom de la ville 
		/// </summary>
		public static void listeCommunesGroupesParDepartement()
		{
			using(var context = new electionEDM())
			{
				var query = from com in context.Commune
							orderby com.code_du_departement, com.libelle_de_la_commune
							select com;

				foreach(var commune in query)
				{
					Console.WriteLine("Département "+commune.code_du_departement+" : "+commune.libelle_de_la_commune);
				}
			}
		}

		/// <summary>
		/// Affiche la commune ayant le plus fort taux de votants
		/// </summary>
		public static void communeAyantLePlusFortTauxDeVotants()
		{
			using(var context = new electionEDM())
			{
				var query = from statistiques in context.stats_election
							select statistiques;

				float taux_votants, bestTauxVotants = 0;
				string inseeBestCommune = "";
				int i = 0;

				foreach(var statoche in query)
				{
					taux_votants = (float) ((float)statoche.votants / (float)statoche.inscrits) * 100;
					if (i == 0 || taux_votants > bestTauxVotants)
					{
						bestTauxVotants = taux_votants;
						inseeBestCommune = statoche.insee;
					}

					i++;
				}

				try
				{
					var queryCommune = (from communiste in context.Commune
										where communiste.insee == inseeBestCommune
										select communiste.libelle_de_la_commune).Single();

					Console.WriteLine(queryCommune);
				}

				catch
				{
					Console.WriteLine("Il y'a eu un problème dans la récupération de la commune avec \n le plus fort taux de votants.");
				}
 							

			}
		}

		/// <summary>
		/// Affiche la commune ayant le plus fort taux d'absentions
		/// </summary>
		public static void communeAyantLePlusFortTauxAbstentions()
		{
			using (var context = new electionEDM())
			{
				var query = from statistiques in context.stats_election
							select statistiques;

				float taux_abstentions, bestTauxVotants = 0;
				string inseeBestCommune = "";
				int i = 0;

				foreach (var statoche in query)
				{
					taux_abstentions = (float)((float)statoche.abstentions / (float)statoche.inscrits) * 100;
					if (i == 0 || taux_abstentions > bestTauxVotants)
					{
						bestTauxVotants = taux_abstentions;
						inseeBestCommune = statoche.insee;
					}

					i++;
				}

				try
				{
					var queryCommune = (from communiste in context.Commune
										where communiste.insee == inseeBestCommune
										select communiste.libelle_de_la_commune).Single();

					Console.WriteLine(queryCommune);
				}

				catch
				{
					Console.WriteLine("Il y'a eu un problème dans la récupération de la commune avec \n le plus fort taux de votants.");
				}


			}
		}

		/// <summary>
		/// Affiche le pourcentage de femmes élues
		/// </summary>
		public static void pourcentageFemmesElues()
		{
			using (var context = new electionEDM())
			{
				

				var queryWomanNpCry = from listeDeFemme in context.Candidat
									  where listeDeFemme.sexe == "F"
									  select listeDeFemme;

				float pctageDeFemmes = (float)((float)queryWomanNpCry.Count() / (float)context.Candidat.Count());
				Console.WriteLine(pctageDeFemmes*100+" %");


			}	
		}

		public static void AfficherLePrenomFemininEtMasculinLePlusFrequentParmiLesElus()
		{
			using(var context = new electionEDM())
			{
				var queryPrenomFeminin = from listedesprenomsF in context.Candidat
										 where listedesprenomsF.sexe == "F"
										 select listedesprenomsF;

				var prenomFemininTrouve = from prenomfeminintrouve in queryPrenomFeminin
										  orderby prenomfeminintrouve.prenom
										  group prenomfeminintrouve by new { prenomfeminintrouve.prenom } into nombredeprenomfeminintrouve
										  select nombredeprenomfeminintrouve;
										  
			}
		}
	}
}









