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
	/// Logique d'interaction pour TriDepartementWindow.xaml
	/// </summary>
	public partial class TriDepartementWindow : Window
	{
		/// <summary>
		/// Constructeur de TriDepartementWindow
		/// </summary>
		public TriDepartementWindow()
		{
			InitializeComponent();
			this.Owner = Application.Current.MainWindow;
		}

		/// <summary>
		/// Ferme la fenêtre de tri des départements
		/// </summary>
		/// <param name="sender">Bouton : quitterButton</param>
		/// <param name="e">Click sur le bouton : quitterButton</param>
		private void quitterButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Bouton qui permet de trier les départements selon les choix de l'utilisateur
		/// </summary>
		/// <param name="sender">Bouton : trierButton</param>
		/// <param name="e">Click sur le bouton : trierButton</param>
		private void trierButton_Click(object sender, RoutedEventArgs e)
		{
			//Si aucun tri n'a été sélectionné
			if ((TriComboBox.SelectedItem == null || TriComboBox.SelectedItem.Equals(aucunTriComboBox)))
			{
				MessageBox.Show("Vous n'avez choisi aucun tri");
			}

			//Si au moins un tri a été sélectionné
			else
			{
				List<Departement> departementTrie = null;

				departementTrie = SelectionTri(departementTrie);


				((MainWindow)this.Owner).afficherDepartementDataGrid(departementTrie);
				this.Close();

			}
		}//Fin de trierButton_Click

		/// <summary>
		/// Tri la liste des départements selon le critère choisi par l'utilisateur
		/// </summary>
		/// <param name="departementTrie">Liste des départements qui va être triée</param>
		/// <returns></returns>
		private List<Departement> SelectionTri(List<Departement> departementTrie)
		{
			IQueryable<Departement> query = null;

			using(var context = new electionEDM())
			{
				//Si on trie les départements selon leur numéro
				if (TriComboBox.SelectedItem.Equals(codeTriComboBox))
				{
					query = from dept in context.Departement
							orderby dept.code_du_departement
							select dept;
				}

				//Si le tri se porte sur le libellé du département
				else
				{
					query = from dept in context.Departement
							orderby dept.libelle_du_departement
							select dept;
				}

				departementTrie = query.ToList();
			}


			return departementTrie;
		}


	}
}
