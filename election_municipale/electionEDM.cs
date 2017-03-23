namespace election_municipale
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using System.IO;

	public partial class electionEDM : DbContext
	{
		public electionEDM()
			: base("name=electionEDM")
		{
		}

		public virtual DbSet<AnneeElection> AnneeElection { get; set; }
		public virtual DbSet<calcul_sieges> calcul_sieges { get; set; }
		public virtual DbSet<Candidat> Candidat { get; set; }
		public virtual DbSet<Commune> Commune { get; set; }
		public virtual DbSet<Departement> Departement { get; set; }
		public virtual DbSet<election> election { get; set; }
		public virtual DbSet<Liste> Liste { get; set; }
		public virtual DbSet<Parti> Parti { get; set; }
		public virtual DbSet<stats_election> stats_election { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AnneeElection>()
				.HasMany(e => e.calcul_sieges)
				.WithRequired(e => e.AnneeElection)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AnneeElection>()
				.HasMany(e => e.election)
				.WithRequired(e => e.AnneeElection)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AnneeElection>()
				.HasMany(e => e.stats_election)
				.WithRequired(e => e.AnneeElection)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<calcul_sieges>()
				.Property(e => e.insee)
				.IsUnicode(false);

			modelBuilder.Entity<Candidat>()
				.Property(e => e.nom)
				.IsUnicode(false);

			modelBuilder.Entity<Candidat>()
				.Property(e => e.prenom)
				.IsUnicode(false);

			modelBuilder.Entity<Candidat>()
				.Property(e => e.sexe)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Candidat>()
				.HasMany(e => e.election)
				.WithRequired(e => e.Candidat)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.insee)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.code_de_la_commune)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.libelle_de_la_commune)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.geo_point_2d)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.Property(e => e.geo_shape)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.HasMany(e => e.calcul_sieges)
				.WithRequired(e => e.Commune)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Commune>()
				.HasMany(e => e.election)
				.WithRequired(e => e.Commune)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Commune>()
				.HasMany(e => e.stats_election)
				.WithRequired(e => e.Commune)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Departement>()
				.Property(e => e.libelle_du_departement)
				.IsUnicode(false);

			modelBuilder.Entity<Departement>()
				.HasMany(e => e.Commune)
				.WithRequired(e => e.Departement)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<election>()
				.Property(e => e.insee)
				.IsUnicode(false);

			modelBuilder.Entity<Liste>()
				.Property(e => e.nomListe)
				.IsUnicode(false);

			modelBuilder.Entity<Liste>()
				.Property(e => e.code_nuance)
				.IsUnicode(false);

			modelBuilder.Entity<Liste>()
				.HasMany(e => e.calcul_sieges)
				.WithRequired(e => e.Liste)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Liste>()
				.HasMany(e => e.Candidat)
				.WithRequired(e => e.Liste)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Parti>()
				.Property(e => e.code_nuance)
				.IsUnicode(false);

			modelBuilder.Entity<Parti>()
				.HasMany(e => e.Liste)
				.WithRequired(e => e.Parti)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<stats_election>()
				.Property(e => e.insee)
				.IsUnicode(false);
		}

		/// <summary>
		/// Lecture des données à partir du fichier csv présent dans bin/Debug
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
			year.insertionAnnee();

			string[][] allData = lireToutesLesDonnees(); //Lire toutes les données depuis le fichier csv et les stocker dans allData

			for (int i = 0; i < allData.Length; i++)
			{
				reinitialisationTableauDeDonnees(candidat, parti, list, csieges, elect);
				comm.reinitialisationCommune();
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

	}
}
