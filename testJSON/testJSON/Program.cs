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
			Liste list = new Liste();
			calcul_sieges csieges = new calcul_sieges();
			election elect = new election();

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
								list.nomListe = allData[i][colonne];
								break;

							//sieges_elu_01
							case 25:
								csieges.sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_secteur_01
							case 26:
								csieges.sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//sieges_cc_01
							case 27:
								csieges.sieges_elus = Convert.ToSByte(allData[i][colonne]);
								break;

							//voix_01
							case 28:
								elect.voix = Convert.ToSByte(allData[i][colonne]);
								break;

							//code_nuance_02
							case 29:
								parti[1].code_nuance = allData[i][colonne];
								break;

								

						}
					}
				}

				//Insérer les requêtes pour la bdd ici

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

		/// <summary>
		/// On va insérer les données relatives aux candidats à l'élection municipale dans la base de données
		/// </summary>
		public static void insertionDonneesCandidat(Candidat[] candidat)
		{
			using (var context = new election_municipaleEntities())
			{
				for(int i =0; i < candidat.Length; i++)
				{
					if(candidat[i] != null)
					{
						context.Candidat.Add(candidat[i]);
					}

				}

				context.SaveChanges();
			}
		}

		/// <summary>
		/// On va insérer les données relatives aux départements
		/// </summary>
		/// <param name="dpt">Le département</param>
		public static void insertionDonneesDepartement(Departement dpt)
		{
			using(var context = new election_municipaleEntities())
			{
				context.Departement.Add(dpt);
				context.SaveChanges();
			}

			
		}

	}


}
