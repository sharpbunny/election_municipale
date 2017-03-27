using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;

namespace election_municipale
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		/// <summary>
		/// Constructeur de MainWindow
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

		}

		// ****************************************
		//			METHODES INCLASSABLES
		// ****************************************
		#region inclassable
		/// <summary>
		/// Permet de modifier le label indiquant le nombre de lignes qui ont été lues depuis le fichier csv
		/// </summary>
		public void modificationLabelInsertionLignes(int lignes)
		{
			lignesInsereesLabel.Content = "Ligne(s) lue(s) : ";
			lignesInsereesLabel.Content += Convert.ToString(lignes);
		}

		/// <summary>
		/// Permet de quitter le programme
		/// </summary>
		/// <param name="sender">Le menuItem : quitterMenuItem</param>
		/// <param name="e">Click sur le menuItem : quitterMenuItem</param>
		private void quitterMenuItem_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		/// <summary>
		/// Permet de définir les propriétés d'une TextBox pour son affichage dans affichageStackPanel
		/// </summary>
		/// <param name="tb">La TextBox qui va voir ses propriétés définies</param>
		/// <returns></returns>
		private TextBox proprieteTextBoxAffichageSQL(TextBox tb)
		{
			tb.HorizontalAlignment = HorizontalAlignment.Stretch;
			tb.VerticalAlignment = VerticalAlignment.Stretch;
			tb.TextWrapping = TextWrapping.Wrap;
			tb.Height = 500;
			tb.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
			tb.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
			tb.ScrollToEnd();

			return tb;
		}

		/// <summary>
		/// Permet de retirer les enfants d'affichageStackPanel
		/// </summary>
		private void viderAffichageStackPanel()
		{
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image ||
				affichageStackPanel.Children[0] is TextBox)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}
		}

		#endregion

		// ****************************************
		//		     AFFICHAGE DATAGRID
		// ****************************************

		#region affichageDataGrid

		/// <summary>
		/// Affiche les candidats dans la datagrid quand une requête doit être faite sur la base de données
		/// </summary>
		private void afficherCandidatDataGrid()
		{

			viderAffichageStackPanel();

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0 || grilleDeDonnees.Columns[0].Header.ToString() != "idCandidat")
			{
				//Si le nombre de colonnes est supérieur à 0, c'est qu'une autre table était affichée avant
				if (grilleDeDonnees.Columns.Count > 0)
				{
					grilleDeDonnees.Items.Clear();
					grilleDeDonnees.Columns.Clear();
				}

				using (var context = new electionEDM())
				{

					context.Configuration.LazyLoadingEnabled = false;


					var query = from candidat in context.Candidat
								orderby candidat.idCandidat, candidat.nom, candidat.prenom
								select candidat;

					DataGridTextColumn col1 = new DataGridTextColumn();
					DataGridTextColumn col2 = new DataGridTextColumn();
					DataGridTextColumn col3 = new DataGridTextColumn();
					DataGridTextColumn col4 = new DataGridTextColumn();
					DataGridTextColumn col5 = new DataGridTextColumn();
					grilleDeDonnees.Columns.Add(col1);
					grilleDeDonnees.Columns.Add(col2);
					grilleDeDonnees.Columns.Add(col3);
					grilleDeDonnees.Columns.Add(col4);
					grilleDeDonnees.Columns.Add(col5);
					col1.Binding = new Binding("idCandidat");
					col2.Binding = new Binding("nom");
					col3.Binding = new Binding("prenom");
					col4.Binding = new Binding("sexe");
					col5.Binding = new Binding("idListe");
					col1.Header = "idCandidat";
					col2.Header = "nom";
					col3.Header = "prenom";
					col4.Header = "sexe";
					col5.Header = "idListe";

					foreach (var candid in query)
					{
						grilleDeDonnees.Items.Add(new Candidat()
						{
							idCandidat = candid.idCandidat,
							nom = candid.nom,
							prenom = candid.prenom,
							sexe = candid.sexe,
							idListe = candid.idListe
						});
					}

					grilleDeDonnees.Visibility = Visibility.Visible;
				}
			}
		}

		/// <summary>
		/// Affiche les candidats dans la datagrid quand une requête de tri a déjà été effectuée sur la page TriCandidat
		/// </summary>
		/// <param name="candidatTrie"></param>
		public void afficherCandidatDataGrid(List<Candidat> candidatTrie)
		{
			viderAffichageStackPanel();

				//Si le nombre de colonnes est supérieur à 0, c'est qu'une autre table était affichée avant
				if (grilleDeDonnees.Columns.Count > 0)
				{
					grilleDeDonnees.Items.Clear();
					grilleDeDonnees.Columns.Clear();
				}



				DataGridTextColumn col1 = new DataGridTextColumn();
				DataGridTextColumn col2 = new DataGridTextColumn();
				DataGridTextColumn col3 = new DataGridTextColumn();
				DataGridTextColumn col4 = new DataGridTextColumn();
				DataGridTextColumn col5 = new DataGridTextColumn();
				grilleDeDonnees.Columns.Add(col1);
				grilleDeDonnees.Columns.Add(col2);
				grilleDeDonnees.Columns.Add(col3);
				grilleDeDonnees.Columns.Add(col4);
				grilleDeDonnees.Columns.Add(col5);
				col1.Binding = new Binding("idCandidat");
				col2.Binding = new Binding("nom");
				col3.Binding = new Binding("prenom");
				col4.Binding = new Binding("sexe");
				col5.Binding = new Binding("idListe");
				col1.Header = "idCandidat";
				col2.Header = "nom";
				col3.Header = "prenom";
				col4.Header = "sexe";
				col5.Header = "idListe";

				foreach (var candid in candidatTrie)
				{
					grilleDeDonnees.Items.Add(new Candidat()
					{
						idCandidat = candid.idCandidat,
						nom = candid.nom,
						prenom = candid.prenom,
						sexe = candid.sexe,
						idListe = candid.idListe
					});
				}

				grilleDeDonnees.Visibility = Visibility.Visible;

			
		}

		/// <summary>
		/// Affiche les communes dans la datagrid quand une requête doit être faite sur la base de données
		/// </summary>
		private void afficherCommuneDataGrid()
		{

			viderAffichageStackPanel();

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0 || grilleDeDonnees.Columns[0].Header.ToString() != "insee")
			{
				if (grilleDeDonnees.Columns.Count > 0)
				{
					grilleDeDonnees.Items.Clear();
					grilleDeDonnees.Columns.Clear();

				}

				using (var context = new electionEDM())
				{

					context.Configuration.LazyLoadingEnabled = false;


					var query = from commune in context.Commune
								orderby commune.code_de_la_commune, commune.insee
								select commune;

					DataGridTextColumn col1 = new DataGridTextColumn();
					DataGridTextColumn col2 = new DataGridTextColumn();
					DataGridTextColumn col3 = new DataGridTextColumn();
					DataGridTextColumn col4 = new DataGridTextColumn();

					grilleDeDonnees.Columns.Add(col1);
					grilleDeDonnees.Columns.Add(col2);
					grilleDeDonnees.Columns.Add(col3);
					grilleDeDonnees.Columns.Add(col4);

					col1.Binding = new Binding("code_de_la_commune");
					col2.Binding = new Binding("insee");
					col3.Binding = new Binding("libelle_de_la_commune");
					col4.Binding = new Binding("code_du_departement");

					col1.Header = "code_de_la_commune";
					col2.Header = "insee";
					col3.Header = "libelle_de_la_commune";
					col4.Header = "code_du_departement";


					foreach (var commune in query)
					{
						grilleDeDonnees.Items.Add(new Commune()
						{
							code_de_la_commune = commune.code_de_la_commune,
							insee = commune.insee,
							libelle_de_la_commune = commune.libelle_de_la_commune,
							code_du_departement = commune.code_du_departement
						});
					}

					grilleDeDonnees.Visibility = Visibility.Visible;

				}//Fin du using

			} //Fin du if(grilleDeDonnees.Colums.Count() == 0)
		}

		/// <summary>
		/// Affiche les communes dans la datagrid quand une requête de tri a déjà été effectuée sur la page TriCommuneWindow
		/// </summary>
		/// <param name="communeTrie">Liste de communes triées selon des critères choisis précédemment</param>
		public void afficherCommuneDataGrid(List<Commune> communeTrie)
		{
			viderAffichageStackPanel();

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0 || grilleDeDonnees.Columns[0].Header.ToString() != "insee")
			{
				if (grilleDeDonnees.Columns.Count > 0)
				{
					grilleDeDonnees.Items.Clear();
					grilleDeDonnees.Columns.Clear();

				}

					DataGridTextColumn col1 = new DataGridTextColumn();
					DataGridTextColumn col2 = new DataGridTextColumn();
					DataGridTextColumn col3 = new DataGridTextColumn();
					DataGridTextColumn col4 = new DataGridTextColumn();

					grilleDeDonnees.Columns.Add(col1);
					grilleDeDonnees.Columns.Add(col2);
					grilleDeDonnees.Columns.Add(col3);
					grilleDeDonnees.Columns.Add(col4);

					col1.Binding = new Binding("code_de_la_commune");
					col2.Binding = new Binding("insee");
					col3.Binding = new Binding("libelle_de_la_commune");
					col4.Binding = new Binding("code_du_departement");

					col1.Header = "code_de_la_commune";
					col2.Header = "insee";
					col3.Header = "libelle_de_la_commune";
					col4.Header = "code_du_departement";


					foreach (var commune in communeTrie)
					{
						grilleDeDonnees.Items.Add(new Commune()
						{
							code_de_la_commune = commune.code_de_la_commune,
							insee = commune.insee,
							libelle_de_la_commune = commune.libelle_de_la_commune,
							code_du_departement = commune.code_du_departement
						});
					}

					grilleDeDonnees.Visibility = Visibility.Visible;

			} //Fin du if(grilleDeDonnees.Colums.Count() == 0)
		}

		/// <summary>
		/// Affiche les départements dans la datagrid quand une requête doit être faite sur la base de données
		/// </summary>
		private void afficherDepartementDataGrid()
		{
			viderAffichageStackPanel();

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0 || grilleDeDonnees.Columns[0].Header.ToString() != "code_du_departement")
			{
				if (grilleDeDonnees.Columns.Count > 0)
				{
					grilleDeDonnees.Items.Clear();
					grilleDeDonnees.Columns.Clear();

				}
				using (var context = new electionEDM())
				{

					context.Configuration.LazyLoadingEnabled = false;


					var query = from dept in context.Departement
								orderby dept.code_du_departement
								select dept;

					DataGridTextColumn col1 = new DataGridTextColumn();
					DataGridTextColumn col2 = new DataGridTextColumn();


					grilleDeDonnees.Columns.Add(col1);
					grilleDeDonnees.Columns.Add(col2);

					col1.Binding = new Binding("code_du_departement");
					col2.Binding = new Binding("libelle_du_departement");

					col1.Header = "code_du_departement";
					col2.Header = "libelle_du_departement";


					foreach (var dept in query)
					{
						grilleDeDonnees.Items.Add(new Departement()
						{
							code_du_departement = dept.code_du_departement,
							libelle_du_departement = dept.libelle_du_departement
						});
					}

					grilleDeDonnees.Visibility = Visibility.Visible;

				}//Fin du using

			} //Fin du if(grilleDeDonnees.Colums.Count() == 0)
		}

		/// <summary>
		/// Affiche les départements dans la datagrid quand une requête de tri a déjà été effectué sur la page TriDepartementWindow
		/// </summary>
		/// <param name="departementTrie">Liste de départements triés selon des critères choisis précédemment</param>
		public void afficherDepartementDataGrid(List<Departement> departementTrie)
		{
			viderAffichageStackPanel();

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0 || grilleDeDonnees.Columns[0].Header.ToString() != "code_du_departement")
			{
				if (grilleDeDonnees.Columns.Count > 0)
				{
					grilleDeDonnees.Items.Clear();
					grilleDeDonnees.Columns.Clear();

				}


					DataGridTextColumn col1 = new DataGridTextColumn();
					DataGridTextColumn col2 = new DataGridTextColumn();


					grilleDeDonnees.Columns.Add(col1);
					grilleDeDonnees.Columns.Add(col2);

					col1.Binding = new Binding("code_du_departement");
					col2.Binding = new Binding("libelle_du_departement");

					col1.Header = "code_du_departement";
					col2.Header = "libelle_du_departement";


					foreach (var dept in departementTrie)
					{
						grilleDeDonnees.Items.Add(new Departement()
						{
							code_du_departement = dept.code_du_departement,
							libelle_du_departement = dept.libelle_du_departement
						});
					}

					grilleDeDonnees.Visibility = Visibility.Visible;


			} //Fin du if(grilleDeDonnees.Colums.Count() == 0)
		}

		/// <summary>
		/// Permet d'afficher la liste des partis politiques quand une requête doit être faite sur la base de données
		/// </summary>
		private void afficherPartiDataGrid()
		{
			viderAffichageStackPanel();

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0 || grilleDeDonnees.Columns[0].Header.ToString() != "code_nuance")
			{
				//Si le nombre de colonnes est supérieur à 0, c'est que l'on affichait une table avant
				//On efface donc toutes les données
				if (grilleDeDonnees.Columns.Count > 0)
				{
					grilleDeDonnees.Items.Clear();
					grilleDeDonnees.Columns.Clear();

				}
				using (var context = new electionEDM())
				{

					context.Configuration.LazyLoadingEnabled = false;


					var query = from parti in context.Parti
								orderby parti.code_nuance
								select parti;

					DataGridTextColumn col1 = new DataGridTextColumn();


					grilleDeDonnees.Columns.Add(col1);

					col1.Binding = new Binding("code_nuance");

					col1.Header = "code_nuance";


					foreach (var parti in query)
					{
						grilleDeDonnees.Items.Add(new Parti()
						{
							code_nuance = parti.code_nuance
						});
					}

					grilleDeDonnees.Visibility = Visibility.Visible;

				}//Fin du using

			} //Fin du if(grilleDeDonnees.Colums.Count() == 0)
		}

		/// <summary>
		/// Permet d'afficher la liste des partis politiques quand une requête doit être faite sur la base de données
		/// </summary>
		/// <param name="partiTrie">Liste de partis politiques triés selon des critères choisis précédemment</param>
		public void afficherPartiDataGrid(List<Parti> partiTrie)
		{
			viderAffichageStackPanel();

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0 || grilleDeDonnees.Columns[0].Header.ToString() != "code_nuance")
			{
				//Si le nombre de colonnes est supérieur à 0, c'est que l'on affichait une table avant
				//On efface donc toutes les données
				if (grilleDeDonnees.Columns.Count > 0)
				{
					grilleDeDonnees.Items.Clear();
					grilleDeDonnees.Columns.Clear();

				}


					DataGridTextColumn col1 = new DataGridTextColumn();


					grilleDeDonnees.Columns.Add(col1);

					col1.Binding = new Binding("code_nuance");

					col1.Header = "code_nuance";


					foreach (var parti in partiTrie)
					{
						grilleDeDonnees.Items.Add(new Parti()
						{
							code_nuance = parti.code_nuance
						});
					}

					grilleDeDonnees.Visibility = Visibility.Visible;


			} //Fin du if(grilleDeDonnees.Colums.Count() == 0)
		}
		#endregion

		// ****************************************
		//		     CHARGEMENT DES DONNEES
		// ****************************************

		#region chargementDonnees

		/// <summary>
		/// Permet d'afficher le MCD ou MLD dans le StackPanel d'affichage
		/// </summary>
		/// <param name="sender">MenuItem : AffichageMCDMenuItem ou AffichageMLDMenuItem</param>
		/// <param name="e">Click sur le menuItem : AffichageMCDMenuItem ou AffichageMLDMenuItem</param>
		private void AfficherMCDMLD_Click(object sender, RoutedEventArgs e)
		{
			if (affichageStackPanel.Children.Count != 0)
			{
				affichageStackPanel.Children.Clear();
			}

			Image imageMCDMLD = new Image();
			imageMCDMLD.HorizontalAlignment = HorizontalAlignment.Stretch;
			imageMCDMLD.VerticalAlignment = VerticalAlignment.Stretch;
			imageMCDMLD.Width = 1000;
			imageMCDMLD.Height = 450;



			//Si le sender est le MenuItem : AffichageMCDMenuItem
			if (sender.Equals(affichageMCDMenuItem)) imageMCDMLD.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("MCDjpeg.jpg");

			//Sinon si le sender est le MenuItem : AffichageMLDMenuItem
			else if (sender.Equals(affichageMLDMenuItem)) imageMCDMLD.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("MLDjpeg.jpg");

			affichageStackPanel.Children.Add(imageMCDMLD);

		}

		/// <summary>
		/// Charge les éléments de la table Candidat dans la DataGrid
		/// </summary>
		/// <param name="sender">Le MenuItem : candidatItems</param>
		/// <param name="e">Click sur le MenuItem : candidatItems</param>
		private void candidatMenuItem_Click(object sender, RoutedEventArgs e)
		{
			afficherCandidatDataGrid();
		}

		/// <summary>
		/// Affiche la requête SQL pour la création des tables de la base de données election_municipale
		/// </summary>
		/// <param name="sender">MenuItem : creationTablesSQLMenuItem</param>
		/// <param name="e">Click sur le MenuItem : creationTablesSQLMenuItem</param>
		private void chargerScriptSQLMenuItem_Click(object sender, RoutedEventArgs e)
		{
			//On créé la textbox qui recevra les requêtes SQL de création de tables
			TextBox requeteSQL = new TextBox();
			requeteSQL = proprieteTextBoxAffichageSQL(requeteSQL);
			string file = "";

			//Suivant le MenuItem qui a appelé cette méthode, on choisit le fichier .sql correspondant
			//Scripts de création de tables
			if (sender.Equals(creationTablesMySQLMenuItem)) file = "creationTablesMySQL.sql";
			else if (sender.Equals(creationTablesOracleMenuItem)) file = "creationTablesOracle.sql";
			else if (sender.Equals(creationTablesSQLiteMenuItem)) file = "creationTablesSQLite.sql";
			else if (sender.Equals(creationTablesSQLServerMenuItem)) file = "creationTablesSQLServer.sql";

			//Script de suppression de tous les enregistrements
			else if (sender.Equals(supprEnregSQLServerMenuItem)) file = "SuppressionEnregistrements.sql";

			//Scripts des jointures
			else if (sender.Equals(candidatListeJointureMenuItem)) file = "jointureCandidatListe.sql";
			else if (sender.Equals(candListPartiJointureMenuItem)) file = "jointureCandidatListeParti.sql";
			else if (sender.Equals(comCandListPartiJointureMenuItem)) file = "jointureCommuneCandidatListeParti.sql";
			else if (sender.Equals(elecAnneeComCandJointureMenuItem)) file = "jointureElectionCommuneCandidatAnnee.sql";
			else if (sender.Equals(annCalSieDeptComListJointureMenuItem)) file = "jointureCalcSiegesComDepartListe.sql";
			else if (sender.Equals(annComStatElecJointureMenuItem)) file = "jointureCommuneAnneeStats_Election.sql";

			//MenuItem de suppression des enregistrements des tables à l'unité
			else if (sender.Equals(anneeElectionSupprMenuItem)) file = "SupprAnneeElection.sql";
			else if (sender.Equals(calculSiegesSupprMenuItem)) file = "SupprCalculSieges.sql";
			else if (sender.Equals(candidSupprMenuItem)) file = "SupprCandidat.sql";
			else if (sender.Equals(communeSupprMenuItem)) file = "SupprCommune.sql";
			else if (sender.Equals(departementSupprMenuItem)) file = "SupprDepartement.sql";
			else if (sender.Equals(electionSupprMenuItem)) file = "SupprElection.sql";
			else if (sender.Equals(listeSupprMenuItem)) file = "SupprListe.sql";
			else if (sender.Equals(partiSupprMenuItem)) file = "SupprParti.sql";
			else if (sender.Equals(statsElectionSupprMenuItem)) file = "SupprStatsElection.sql";


			//On envoie le chemin du fichier à lire dans la fonction lectureFichierSQL
			requeteSQL.Text = lectureFichierSQL(file);

			//On vide le stackPanel d'affichage de données et on y insère la TextBox
			affichageStackPanel.Children.Clear();
			affichageStackPanel.Children.Add(requeteSQL);


		}

		/// <summary>
		/// Charge les éléments de la table Commune dans le DataGrid principal
		/// </summary>
		/// <param name="sender">Le MenuItem : communeMenuItem</param>
		/// <param name="e">Click sur le MenuItem : communeMenuItem</param>
		private void communeMenuItem_Click(object sender, RoutedEventArgs e)
		{
			afficherCommuneDataGrid();
		}

		/// <summary>
		/// Charge les éléments de la table Département dans le DataGrid
		/// </summary>
		/// <param name="sender">MenuItem : departementMenuItem</param>
		/// <param name="e">Click sur le MenuItem : departementMenuItem</param>
		private void departementMenuItem_Click(object sender, RoutedEventArgs e)
		{
			afficherDepartementDataGrid();
		}

		/// <summary>
		/// Fonction permettant d'insérer les données provenant d'un fichier csv dans la base de données
		/// </summary>
		/// <param name="sender">Bouton de MainWindow : buttonInsertionDonneesCsv</param>
		/// <param name="e">Evenement survenant après un click sur le bouton d'insertion de données issues d'un fichier csv</param>
		private void InsertionDonneesCsv_Click(object sender, RoutedEventArgs e)
		{
			electionEDM.lireToutesLesDonnees();
			electionEDM.recuperationDesDonnees(this);
		}

		/// <summary>
		/// Permet de lire un fichier de type .sql
		/// </summary>
		/// <param name="fichierALire">Chaîne de caractère indiquant l'emplacement du fichier .sql à lire</param>
		/// <returns></returns>
		private string lectureFichierSQL(string fichierALire)
		{
			//On essaie de lire la requête depuis le fichier dans laquelle elle est stockée
			try
			{
				using (StreamReader sr = new StreamReader(fichierALire))
				{
					String line = sr.ReadToEnd();
					return line;
				}
			}

			//Si la lecture du fichier échoue, on affiche un message d'erreur
			catch (Exception a)
			{
				MessageBox.Show(a.Message);
				string line = "Erreur dans la lecture du fichier !";
				return line;
			}
		}

		/// <summary>
		/// Charge les éléments concernant la table Parti dans le DataGrid : grilleDeDonnees
		/// </summary>
		/// <param name="sender">MenuItems : partiItems</param>
		/// <param name="e">Click sur le MenuItem : partiItems</param>
		private void partiMenuItem_Click(object sender, RoutedEventArgs e)
		{
			afficherPartiDataGrid();
		}
		#endregion


		// ************************
		//		  LIEN WEB
		// ************************

		#region lien web

		/// <summary>
		/// Accède au site internet data.gouv.fr pour retrouver la source des données
		/// </summary>
		/// <param name="sender">Le menuItem : dataGouvMenuItem</param>
		/// <param name="e">Click sur le menuItem : dataGouvMenuItem</param>
		private void dataGouvMenuItem_Click(object sender, RoutedEventArgs e)
		{
			RedirectionWeb redirection = new RedirectionWeb();
			redirection.menuItemAppelant = dataGouvMenuItem;
			redirection.ShowDialog();

		}

		/// <summary>
		/// Affiche la page du fonctionnement des élections municipales(page web du service public)
		/// </summary>
		/// <param name="sender">MenuItem : servicePublicElectionMenuItem</param>
		/// <param name="e">Click sur le MenuItem : servicePublicElectionMenuItem</param>
		private void servicePublicElectionMenuItem_Click(object sender, RoutedEventArgs e)
		{
			RedirectionWeb redirection = new RedirectionWeb();
			redirection.menuItemAppelant = servicePublicElectionMenuItem;
			redirection.ShowDialog();
		}

		/// <summary>
		/// Affiche la page du fonctionnement des élections municipales(page web wikipedia)
		/// </summary>
		/// <param name="sender">MenuItem : wikipediaElectionMenuItem</param>
		/// <param name="e">Click sur le MenuItem : wikipediaElectionMenuItem</param>
		private void wikipediaElectionMenuItem_Click(object sender, RoutedEventArgs e)
		{
			//On indique à la page redirectionWeb qui l'a appelée pour adapter le site web à afficher
			RedirectionWeb redirection = new RedirectionWeb();
			redirection.menuItemAppelant = wikipediaElectionMenuItem;
			redirection.ShowDialog();

		}

		#endregion


		// ************************
		//		  REQUETES
		// ************************

		#region requêtes
		/// <summary>
		/// Affiche la liste des femmes/hommes qui étaient au second tour des élections municipales
		/// </summary>
		/// <param name="sender">femmesCandidatsMenuItem ou hommesCandidatsMenuItem</param>
		/// <param name="e">Click sur le MenuItem : femmesCandidatsMenuItem ou hommesCandidatsMenuItem</param>
		private void afficherLesHommesOuLesFemmes(object sender, RoutedEventArgs e)
		{
			List<Candidat> sexeCandidat = null;
			string sexe;
			if (sender.Equals(hommesCandidatsMenuItem)) sexe = "M";
			else sexe = "F";

			using(var context = new electionEDM())
			{
				//On ne selectionne que les candidats du genre choisi dans le menuItem et on les trie par nom, puis par prénom
				var querySexe = from candidat in context.Candidat
								  where candidat.sexe == sexe
								  orderby candidat.nom, candidat.prenom
								  select candidat;

				sexeCandidat = querySexe.ToList();
			}

			afficherCandidatDataGrid(sexeCandidat);

		}

		/// <summary>
		/// Appelle la fonction prénom féminin qui a été le plus fréquemment élu lors des élections municipales
		/// </summary>
		/// <param name="sender">MenuItem : prenomFPlusFrequentMenuItem</param>
		/// <param name="e">Click sur le MenuItem : prenomFPlusFrequentMenuItem</param>
		private void prenomPlusFrequentMenuItem_Click(object sender, RoutedEventArgs e)
		{
			string sexe = "";
			if (sender.Equals(prenomFPlusFrequentMenuItem)) sexe = "F";
			else if (sender.Equals(prenomMPlusFrequentMenuItem)) sexe = "M";

			using (var context = new electionEDM())
			{
				//Récupère les candidats féminins
				var queryPrenom = from listedesprenoms in context.Candidat
								  where listedesprenoms.sexe == sexe
							      select listedesprenoms;

				//Groupe les candidats féminins par prénom
				var prenomTrouve = from prenomtrouve in queryPrenom
										  orderby prenomtrouve.prenom
										  group prenomtrouve by prenomtrouve.prenom into nombredeprenomtrouve
										  select new
										  {
											  prenom = nombredeprenomtrouve.Key,
											  count = nombredeprenomtrouve.Count()

										  };

				int i = 0; //index dans le foreach
				int BestPrenom = 0; //Nombre d'occurences du prénom le plus utilisé
				string MeilleurPrenom = "";
				foreach (var PrenomPlusSouventPresent in prenomTrouve)
				{
					if (i == 0 || PrenomPlusSouventPresent.count > BestPrenom)
					{
						BestPrenom = PrenomPlusSouventPresent.count;
						MeilleurPrenom = PrenomPlusSouventPresent.prenom;

					}
					i++;

				}

				//On affiche les résultats dans une textbox
				TextBox tb = new TextBox();
				tb = proprieteTextBoxAffichageSQL(tb);
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(tb);

				if (sender.Equals(prenomFPlusFrequentMenuItem)) tb.Text = "Le prénom féminin le plus fréquent parmi les élues est " + MeilleurPrenom + " avec " + BestPrenom + " occurences.";
				else tb.Text = "Le prénom masculin le plus fréquent parmi les élus est " + MeilleurPrenom + " avec " + BestPrenom + " occurences.";

			} //Fin du using

		}//Fin de prenomPlusFrequentMenuItem_Click

		#endregion


		// ************************
		//		     TRI
		// ************************

		#region tri
		/// <summary>
		/// Ouvre la fenêtre pour sélectionner la façon dont le candidat sera trié
		/// </summary>
		/// <param name="sender">Le MenuItem : candidatTriMenuItem</param>
		/// <param name="e">Click sur le MenuItem : candidatTriMenuItem</param>
		private void candidatTriMenuItem_Click(object sender, RoutedEventArgs e)
		{
			TriCandidat triCandidatWindow = new TriCandidat();
			triCandidatWindow.ShowDialog();
		}

		/// <summary>
		/// Ouvre la fenêtre pour sélectionner la façon dont les communes seront triées
		/// </summary>
		/// <param name="sender">MenuItem : communeTriMenuItem</param>
		/// <param name="e">Click sur le MenuItem : communeTriMenuItem</param>
		private void communeTriMenuItem_Click(object sender, RoutedEventArgs e)
		{
			TriCommuneWindow communeWindow = new TriCommuneWindow();
			communeWindow.ShowDialog();
		}

		/// <summary>
		/// Ouvre la fenêtre pour sélectionner la façon dont les départements seront triés
		/// </summary>
		/// <param name="sender">MenuItem : departementTriMenuItem</param>
		/// <param name="e">Click sur le MenuItem : departementTriMenuItem</param>
		private void departementTriMenuItem_Click(object sender, RoutedEventArgs e)
		{
			TriDepartementWindow departementWindow = new TriDepartementWindow();
			departementWindow.ShowDialog();
		}

		/// <summary>
		/// Ouvre la fenêtre pour sélectionner la façon dont les partis politiques seront triés
		/// </summary>
		/// <param name="sender">MenuItem : partiTriMenuItem</param>
		/// <param name="e">Click sur le MenuItem : partiTriMenuItem</param>
		private void partiTriMenuItem_Click(object sender, RoutedEventArgs e)
		{
			TriPartisWindow partiWindow = new TriPartisWindow();
			partiWindow.ShowDialog();
		}

		#endregion

		/// <summary>
		/// Affiche la commune qui a le plus fort taux d'abstentions ou le plus fort taux de votants selon l'objet qui déclenche cet évènement
		/// </summary>
		/// <param name="sender">MenuItem : comPlusForTauxAbsMenuItem ou comPlusForTauxVotMenuItem</param>
		/// <param name="e">Click sur le MenuItem : comPlusForTauxAbsMenuItem ou comPlusForTauxVotMenuItem</param>
		private void comPlusForTauxMenuItem_Click(object sender, RoutedEventArgs e)
		{
			using (var context = new electionEDM())
			{
				var query = from statistiques in context.stats_election
							select statistiques;

				float taux = 0F, bestTaux = 0; //Soit les taux d'abstentions, soit les taux de votants.
				string inseeBestCommune = ""; //code insee de la commune avec le meilleur taux (abstentions ou votants)
				int i = 0; //index du foreach

				//On parcourt tous les éléments de stats_election pour trouver le plus fort taux (soit d'abstentions, soit de votants)
				foreach (var statoche in query)
				{
					if(sender.Equals(comPlusForTauxAbsMenuItem)) taux = (float)((float)statoche.abstentions / (float)statoche.inscrits) * 100;
					else if (sender.Equals(comPlusForTauxVotMenuItem)) taux = (float)((float)statoche.votants / (float)statoche.inscrits) * 100;

					if (i == 0 || taux > bestTaux)
					{
						bestTaux = taux;
						inseeBestCommune = statoche.insee;
					}

					i++;
				}

				//On récupère le libelle de la commune selon le code insee de la commune avec le plus fort taux issu de stats_election
				try
				{
					var queryCommune = (from communiste in context.Commune
										where communiste.insee == inseeBestCommune
										select communiste.libelle_de_la_commune).Single();

					TextBox tb = new TextBox();
					proprieteTextBoxAffichageSQL(tb);
					affichageStackPanel.Children.Clear();
					affichageStackPanel.Children.Add(tb);
					if (sender.Equals(comPlusForTauxAbsMenuItem)) tb.Text = "La commune avec le plus fort taux d'abstentions est " + queryCommune + " avec " + bestTaux + "% d'abstentions";
					else if (sender.Equals(comPlusForTauxVotMenuItem)) tb.Text = "La commune avec le plus fort taux de votants est " + queryCommune + " avec " + bestTaux + "% de votants";
				}

				catch
				{
					MessageBox.Show("Il y'a eu un problème dans la récupération de la commune avec \n le plus fort taux de votants.");
				}

			} //Fin du using
		} //Fin de comPlusForTauxMenuItem
	}
}
