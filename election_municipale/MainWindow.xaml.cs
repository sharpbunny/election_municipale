﻿using System;
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

											// **********************************
											//			METHODES
											// **********************************


		/// <summary>
		/// Permet de modifier le label indiquant le nombre de lignes qui ont été lues depuis le fichier csv
		/// </summary>
		public void modificationLabelInsertionLignes(int lignes)
		{
			lignesInsereesLabel.Content = "Ligne(s) lue(s) : ";
			lignesInsereesLabel.Content += Convert.ToString(lignes);
		}


										// FONCTIONS DES ELEMENTS DU MENU
		#region fonctionMenu

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

			//Si le sender est le MenuItem : AffichageMCDMenuItem
			if(sender.Equals(affichageMCDMenuItem))imageMCDMLD.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("MCDjpeg.jpg");

			//Sinon si le sender est le MenuItem : AffichageMLDMenuItem
			else if (sender.Equals(affichageMLDMenuItem)) imageMCDMLD.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("MLDjpeg.jpg");

			affichageStackPanel.Children.Add(imageMCDMLD);

		}

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
		/// Charge les éléments de la table Candidat dans la DataGrid
		/// </summary>
		/// <param name="sender">Le MenuItem : candidatItems</param>
		/// <param name="e">Click sur le MenuItem : candidatItems</param>
		private void candidatMenuItem_Click(object sender, RoutedEventArgs e)
		{
			afficherCandidatDataGrid();
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
		/// Charge les éléments concernant la table Parti dans le DataGrid : grilleDeDonnees
		/// </summary>
		/// <param name="sender">MenuItems : partiItems</param>
		/// <param name="e">Click sur le MenuItem : partiItems</param>
		private void partiMenuItem_Click(object sender, RoutedEventArgs e)
		{
			afficherPartiDataGrid();
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

		#endregion

										// DIVERSES METHODES

		/// <summary>
		/// Affiche les candidats dans la datagrid quand une requête doit être faite sur la base de données
		/// </summary>
		private void afficherCandidatDataGrid()
		{
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}

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
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0 || grilleDeDonnees.Columns[0].Header.ToString() != "idCandidat")
			{
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
		}

		/// <summary>
		/// Affiche les communes dans la datagrid quand une requête doit être faite sur la base de données
		/// </summary>
		private void afficherCommuneDataGrid()
		{
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}

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
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}

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
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}

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
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}

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
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}

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
			//Si le stackPanel d'affichage affiche déjà une image, on l'enlève du stackpanel pour pouvoir y insérer le datagrid
			if (affichageStackPanel.Children[0] is Image)
			{
				affichageStackPanel.Children.Clear();
				affichageStackPanel.Children.Add(grilleDeDonnees);
			}

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
	}
}
