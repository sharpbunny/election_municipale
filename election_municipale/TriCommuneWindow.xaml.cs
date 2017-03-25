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
using System.Windows.Shapes;

namespace election_municipale
{
	/// <summary>
	/// Logique d'interaction pour TriCommuneWindow.xaml
	/// </summary>
	public partial class TriCommuneWindow : Window
	{
		/// <summary>
		/// Constructeur de TriCommuneWindow
		/// </summary>
		public TriCommuneWindow()
		{
			InitializeComponent();
			this.Owner = Application.Current.MainWindow;
		}

		/// <summary>
		/// Ferme la fenêtre de tri des communes
		/// </summary>
		/// <param name="sender">Bouton : quitterButton</param>
		/// <param name="e">Click sur le bouton : quitterButton</param>
		private void quitterButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Bouton qui permet de lancer le tri des communes selon les choix de l'utilisateur
		/// </summary>
		/// <param name="sender">Bouton : trierButton</param>
		/// <param name="e">Click sur le bouton : trierButton</param>
		private void trierButton_Click(object sender, RoutedEventArgs e)
		{

			//Si aucun tri n'a été sélectionné
			if ((TriComboBox.SelectedItem == null || TriComboBox.SelectedItem.ToString() == "Aucun"))
			{
				MessageBox.Show("Vous n'avez choisi aucun tri");
			}

			//Si au moins un tri a été sélectionné
			else
			{
				List<Commune> communeTrie = null;

				communeTrie = SelectionTri(communeTrie);
				

				((MainWindow)this.Owner).afficherCommuneDataGrid(communeTrie);
				this.Close();

			}
		} //Fin de trierButton_Click

		/// <summary>
		/// Permet de trier les communes selon le type de tri choisi par l'utilisateur
		/// </summary>
		/// <param name="communeTrie">Liste de communes</param>
		/// <returns></returns>
		private List<Commune> SelectionTri(List<Commune> communeTrie)
		{
			IQueryable<Commune> query = null;

			using(var context = new electionEDM())
			{
				if (TriComboBox.SelectedItem.Equals(inseeTriComboBox))
				{
					query = from commune in context.Commune
							orderby commune.insee
							select commune;
				}

				else if (TriComboBox.SelectedItem.Equals(codeTriComboBox))
				{
					query = from commune in context.Commune
							orderby commune.code_de_la_commune
							select commune;
				}

				else if (TriComboBox.SelectedItem.Equals(libelleTriComboBox))
				{
					query = from commune in context.Commune
							orderby commune.libelle_de_la_commune
							select commune;
				}

				else if (TriComboBox.SelectedItem.Equals(codeInseeTriComboBox))
				{
					query = from commune in context.Commune
							orderby commune.code_de_la_commune, commune.insee
							select commune;
				}

				//Si on trie d'abord par le code de la commune puis le libellé
				else
				{
					query = from commune in context.Commune
							orderby commune.code_de_la_commune, commune.libelle_de_la_commune
							select commune;
				}

				communeTrie = query.ToList();
			}

				return communeTrie;
		}


	}
}
