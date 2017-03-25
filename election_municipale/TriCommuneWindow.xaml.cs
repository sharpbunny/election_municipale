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
			List<Commune> communeTrie = null;

			//Si aucun tri n'a été sélectionné
			if ((premierTriComboBox.SelectedItem == null || premierTriComboBox.SelectedItem == aucunPremierTriComboBox) &&
			(deuxiemeTriComboBox.SelectedItem == null || deuxiemeTriComboBox.SelectedItem == aucunDeuxiemeTriComboBox) &&
			(troisiemeTriComboBox.SelectedItem == null || troisiemeTriComboBox.SelectedItem == aucunTroisiemeTriComboBox))
			{
				MessageBox.Show("Vous n'avez choisi aucun tri");
			}

			//Si au moins un tri a été sélectionné
			else
			{
				//On teste si un tri a été choisi dans la première comboBox
				if (comboBoxChoisie(premierTriComboBox))
				{
					communeTrie = tri(premierTriComboBox);
				}

				//On teste si un tri a été choisi dans la deuxième comboBox
				if (comboBoxChoisie(deuxiemeTriComboBox))
				{
					if (communeTrie == null) communeTrie = tri(deuxiemeTriComboBox); //Si on n'a pas encore opéré de tri, on interroge la bdd
					else communeTrie = tri(deuxiemeTriComboBox, communeTrie); //Sinon on trie avec le second paramètre la liste ayant déjà subi un
																				//premier tri.
				}

				//On teste si un tri a été choisi dans la troisième comboBox
				if (comboBoxChoisie(deuxiemeTriComboBox))
				{
					if (communeTrie == null) communeTrie = tri(troisiemeTriComboBox); //Si on n'a pas encore opéré de tri, on interroge la bdd
					else communeTrie = tri(troisiemeTriComboBox, communeTrie); //Sinon on trie avec le second paramètre la liste ayant déjà subi un
																				 //premier tri.
				}

				((MainWindow)this.Owner).afficherCommuneDataGrid(communeTrie);
				this.Close();

			}
		}

		/// <summary>
		/// Teste si un item a été sélectionné dans la comboBox
		/// </summary>
		/// <param name="cb">ComboBox sur laquelle on va tester si un item a été sélectionné</param>
		/// <returns></returns>
		private bool comboBoxChoisie(ComboBox cb)
		{
			bool unItemAEteSelectionne;
			if (cb.SelectedItem == null || cb.SelectedItem == aucunPremierTriComboBox)
			{
				unItemAEteSelectionne = false;
			}

			//Si un tri a bien été choisi
			else
			{
				unItemAEteSelectionne = true;
			}

			return unItemAEteSelectionne;
		}

		/// <summary>
		/// Permet de trier les communes selon le choix de l'utilisateur dans la comboBox
		/// </summary>
		/// <param name="cb">ComboBox contenant le choix du tri de l'utilisateur</param>
		/// <returns></returns>
		private List<Commune> tri(ComboBox cb)
		{
			using (var context = new electionEDM())
			{
				//Si l'id est choisi, on trie les candidats par leur id
				if (cb.SelectedItem == inseePremierTriComboBox || cb.SelectedItem == inseeDeuxiemeTriComboBox || cb.SelectedItem == inseeTroisiemeTriComboBox)
				{
					var query = from commune in context.Commune
								orderby commune.insee
								select commune;

					return query.ToList();
				}

				//Sinon si le nom est choisi, on trie le nom
				else if (cb.SelectedItem == codePremierTriComboBox || cb.SelectedItem == codeDeuxiemeTriComboBox || cb.SelectedItem == codeTroisiemeTriComboBox)
				{
					var query = from commune in context.Commune
								orderby commune.code_de_la_commune
								select commune;

					return query.ToList();
				}

				//Sinon on tri par le prénom
				else
				{
					var query = from commune in context.Commune
								orderby commune.libelle_de_la_commune
								select commune;

					return query.ToList();
				}

			}
		}

		/// <summary>
		/// Permet de trier une collection de Communes déjà triée auparavant
		/// </summary>
		/// <param name="cb">ComboBox contenant le choix du tri de l'utilisateur</param>
		/// <param name="candidat">Liste de communes déjà triée auparavant</param>
		/// <returns></returns>
		private List<Commune> tri(ComboBox cb, List<Commune> communeDejaTrie)
		{

			//Si l'id est choisi, on trie les candidats par leur id
			if (cb.SelectedItem == inseePremierTriComboBox || cb.SelectedItem == inseeDeuxiemeTriComboBox || cb.SelectedItem == inseeTroisiemeTriComboBox)
			{
				var query = from commune in communeDejaTrie
							orderby commune.insee
							select commune;

				return query.ToList();
			}

			//Sinon si le nom est choisi, on trie le nom
			else if (cb.SelectedItem == codePremierTriComboBox || cb.SelectedItem == codeDeuxiemeTriComboBox || cb.SelectedItem == codeTroisiemeTriComboBox)
			{
				var query = from commune in communeDejaTrie
							orderby commune.code_de_la_commune
							select commune;

				return query.ToList();
			}

			//Sinon on tri par le prénom
			else
			{
				var query = from commune in communeDejaTrie
							orderby commune.libelle_de_la_commune
							select commune;

				return query.ToList();
			}

		}

	}
}
