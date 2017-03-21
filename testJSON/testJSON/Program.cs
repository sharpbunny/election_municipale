using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace testJSON
{
	class Program
	{
		static void Main(string[] args)
		{
			Candidat[] candidat = new Candidat[5];
			Departement dept = new Departement();
			Commune comm = new Commune();
			stats_election stat = new stats_election();
			Parti[] parti = new Parti[5];
			Liste[] list = new Liste[5];
			calcul_sieges[] csieges = new calcul_sieges[5];
			election[] elect = new election[5];

			string[][] allData = lireToutesLesDonnees();

			for(int i=0; i < allData.Length; i++)
			{
				for(int colonne=0; colonne < 75; colonne++)
				{
					if(i > 0)
					{
						switch (colonne)
						{
							//code du département
							case 1:
								dept.code_du_departement = Convert.ToSByte(allData[i][colonne]);
								break;

							//type du scrutin
							case 2:
								break;
						
							//libelle_du_departement
							case 3:
								dept.libelle_du_departement = allData[i][colonne];
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
								elect[0].voix = Convert.ToSByte(allData[i][colonne]);
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

							case 34:
								candidat[1].prenom = allData[i][colonne];
								break;
							case 35:
								list[1].nomListe = allData[i][colonne];
								break;
							case 36:
								csieges[1].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;
							case 37:
								csieges[1].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;
							case 38:
								csieges[1].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;
							case 39:
								elect[1].voix = Convert.ToInt32(allData[i][colonne]);
								break;
								// code nuance_03
							case 42:
								parti[2].code_nuance = allData[i][colonne];
								break;
							case 43:
								candidat[2].sexe = allData[i][colonne];
								break;
							case 44:
								candidat[2].nom = allData[i][colonne];
								break;
							case 45:
								candidat[2].prenom = allData[i][colonne];
								break;
							case 46:
								list[2].nomListe = allData[i][colonne];
								break;
							case 47:
								csieges[2].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;
							case 48:
								csieges[2].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;
							case 49:
								csieges[2].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;
							case 50:
								elect[2].voix = Convert.ToInt32(allData[i][colonne]);
								break;
								// code nuance_04
							case 53:
								parti[3].code_nuance = allData[i][colonne];
								break;
							case 54:
								candidat[3].sexe = allData[i][colonne];
								break;
							case 55:
								candidat[3].nom = allData[i][colonne];
								break;
							case 56:
								candidat[3].prenom = allData[i][colonne];
								break;
							case 57:
								list[3].nomListe = allData[i][colonne];
								break;
							case 58:
								csieges[3].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;
							case 59:
								csieges[3].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;
							case 60:
								csieges[3].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;
							case 61:
								elect[3].voix = Convert.ToInt32(allData[i][colonne]);
								break;
								// code_nuance_05
							case 64:
								parti[4].code_nuance = allData[i][colonne];
								break;
							case 65:
								candidat[4].sexe = allData[i][colonne];
								break;
							case 66:
								candidat[4].nom = allData[i][colonne];
								break;
							case 67:
								candidat[4].prenom = allData[i][colonne];
								break;
							case 68:
								list[4].nomListe = allData[i][colonne];
								break;
							case 69:
								csieges[4].sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;
							case 70:
								csieges[4].sieges_secteurs = Convert.ToSByte(allData[i][colonne]);
								break;
							case 71:
								csieges[4].sieges_cc = Convert.ToSByte(allData[i][colonne]);
								break;
							case 72:
								elect[4].voix = Convert.ToInt32(allData[i][colonne]);
								break;




















						}
					}
				}
			}

			


		}

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

	}


}
