using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		/// <summary>
		/// Fonction permettant d'insérer les données provenant d'un fichier csv dans la base de données
		/// </summary>
		/// <param name="sender">Bouton de MainWindow : buttonInsertionDonneesCsv</param>
		/// <param name="e">Evenement survenant après un click sur le bouton d'insertion de données issues d'un fichier csv</param>
		private void buttonInsertionDonneesCsv_Click(object sender, RoutedEventArgs e)
		{
			electionEDM.lireToutesLesDonnees();
			electionEDM.recuperationDesDonnees(this);
		}

		/// <summary>
		/// Permet de modifier le label indiquant le nombre de lignes qui ont été lues depuis le fichier csv
		/// </summary>
		public void modificationLabelInsertionLignes(int lignes)
		{
			lignesInsereesLabel.Content = "Ligne(s) lue(s) : ";
			lignesInsereesLabel.Content += Convert.ToString(lignes);
		}

		/// <summary>
		/// On insère dans la listBox le nom de toutes les tables
		/// </summary>
		private void insertionTablesListBox()
		{
			entitesListBox.Items.Add("");
		}

		/// <summary>
		/// Charge les éléments de la table Candidat dans la DataGrid
		/// </summary>
		/// <param name="sender">ListBoxItem : candidatItems</param>
		/// <param name="e">Click sur le ListBoxItem : candidatItems</param>
		private void candidatItems_Selected(object sender, RoutedEventArgs e)
		{
			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0)
			{
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
				}
			}



		}

		/// <summary>
		/// Charge les éléments de la table Commune dans le DataGrid principal
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void communeItems_Selected(object sender, RoutedEventArgs e)
		{

			//Si le nombre de colonnes est vide
			if (grilleDeDonnees.Columns.Count() == 0)
			{

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

				}//Fin du using

			} //Fin du if(grilleDeDonnees.Colums.Count() == 0)
		}
	}
}
