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
using System.Windows.Shapes;

namespace election_municipale
{
	/// <summary>
	/// Logique d'interaction pour RedirectionWeb.xaml
	/// </summary>
	public partial class RedirectionWeb : Window
	{
		public MenuItem menuItemAppelant;

		/// <summary>
		/// Constructeur de la page RedirectionWeb
		/// </summary>
		public RedirectionWeb()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Affichage d'une page web en fonction du choix de l'utilisateur dans la MainWindow
		/// </summary>
		/// <param name="sender">Le bouton : continuerButton</param>
		/// <param name="e">Click sur le bouton : continuerButton</param>
		private void continuerButton_Click(object sender, RoutedEventArgs e)
		{


			if(menuItemAppelant.Header.ToString() == "Page Wikipedia : élection municipale")
			{
				//On fait un try sur l'ouverture de la page web
				try
				{
					Process.Start("https://fr.wikipedia.org/wiki/%C3%89lection_municipale_en_France");
				}

				//Si l'ouverture de la page web échoue
				catch
				{
					MessageBox.Show("L'ouverture de la page web a échoué.");
				}
			}

			else if(menuItemAppelant.Header.ToString() == "Service public : élection municipale")
			{
				//On fait un try sur l'ouverture de la page web
				try
				{
					Process.Start("https://www.service-public.fr/particuliers/vosdroits/F1952");
				}

				//Si l'ouverture de la page web échoue
				catch
				{
					MessageBox.Show("L'ouverture de la page web a échoué.");
				}
			}

			else if(menuItemAppelant.Header.ToString() == "Source des données utilisées")
			{
				//On fait un try sur l'ouverture de la page web
				try
				{
					Process.Start("https://www.data.gouv.fr/fr/datasets/elections-municipales-2014-les-candidats-du-2e-tour-communes-de-1000-hab-et-plus-idf/");
				}

				//Si l'ouverture de la page web échoue
				catch
				{
					MessageBox.Show("L'ouverture de la page web a échoué.");
				}
			}

			else if (menuItemAppelant.Header.ToString() == "Ouvrir le fichier csv")
			{
				try
				{
					Process.Start("election.csv");
				}

				catch
				{
					MessageBox.Show("L'ouverture du fichier csv a échoué.");
				}
			}

			else if (menuItemAppelant.Header.ToString() == "Ouvrir le fichier json")
			{
				try
				{
					Process.Start("election.json");
				}

				catch
				{
					MessageBox.Show("L'ouverture du fichier json a échoué.");
				}
			}

			else
			{
				MessageBox.Show("L'ouverture de la page web a échoué");
			}


			this.Close();
		}

		/// <summary>
		/// Ferme la fenêtre RedirectionWeb sans envoyer l'utilisateur sur la page web data.gouv
		/// </summary>
		/// <param name="sender">Le bouton : exitButton</param>
		/// <param name="e">Click sur le bouton : exitButton</param>
		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
