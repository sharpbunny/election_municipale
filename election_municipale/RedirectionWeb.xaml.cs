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
		public RedirectionWeb()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Affichage la page web data.gouv contenant la source des données utilisées
		/// </summary>
		/// <param name="sender">Le bouton : continuerButton</param>
		/// <param name="e">Click sur le bouton : continuerButton</param>
		private void continuerButton_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.data.gouv.fr/fr/datasets/elections-municipales-2014-les-candidats-du-2e-tour-communes-de-1000-hab-et-plus-idf/");
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
