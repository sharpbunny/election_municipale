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
	/// Logique d'interaction pour TriCandidat.xaml
	/// </summary>
	public partial class TriCandidat : Window
	{
		/// <summary>
		/// Constructeur de la fenêtre TriCandidat
		/// </summary>
		public TriCandidat()
		{
			InitializeComponent();
			this.Owner = Application.Current.MainWindow;
		}

		/// <summary>
		/// Ferme la fenêtre des options de tri des candidats aux élections municipales
		/// </summary>
		/// <param name="sender">Le bouton : quitterButton</param>
		/// <param name="e">Click sur le bouton : quitterButton</param>
		private void quitterButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Tri les candidats selon l'ordre des colonnes choisies par l'utilisateur
		/// </summary>
		/// <param name="sender">Button : trierButton</param>
		/// <param name="e">Click sur le bouton : trierButton</param>
		private void trierButton_Click(object sender, RoutedEventArgs e)
		{
			List<Candidat> candidatTrie = null;

			//Si aucun tri n'a été sélectionné
			if((premierTriComboBox.SelectedItem == null || premierTriComboBox.SelectedItem == aucunPremierTriComboBox) &&
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
					candidatTrie = tri(premierTriComboBox);
				}

				//On teste si un tri a été choisi dans la deuxième comboBox
				if (comboBoxChoisie(deuxiemeTriComboBox))
				{
					if (candidatTrie == null) candidatTrie = tri(deuxiemeTriComboBox); //Si on n'a pas encore opéré de tri, on interroge la bdd
					else candidatTrie = tri(deuxiemeTriComboBox, candidatTrie); //Sinon on trie avec le second paramètre la liste ayant déjà subi un
																//premier tri.
				}

				//On teste si un tri a été choisi dans la troisième comboBox
				if (comboBoxChoisie(deuxiemeTriComboBox))
				{
					if (candidatTrie == null) candidatTrie = tri(troisiemeTriComboBox); //Si on n'a pas encore opéré de tri, on interroge la bdd
					else candidatTrie = tri(troisiemeTriComboBox, candidatTrie); //Sinon on trie avec le second paramètre la liste ayant déjà subi un
																 //premier tri.
				}

				((MainWindow)this.Owner).afficherCandidatDataGrid(candidatTrie);
				this.Close();

			} //Fin du else

		}

		/// <summary>
		/// Teste si un item a été sélectionné dans la comboBox
		/// </summary>
		/// <param name="cb">ComboBox sur laquelle on va tester si un item a été sélectionné</param>
		/// <returns></returns>
		private bool comboBoxChoisie(ComboBox cb)
		{
			bool unItemAEteSelectionne;
			if(cb.SelectedItem == null || cb.SelectedItem == aucunPremierTriComboBox)
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
		/// Permet de trier la table candidat selon le choix de l'utilisateur dans les comboBox
		/// </summary>
		/// <param name="cb">ComboBox contenant le choix du tri de l'utilisateur</param>
		/// <returns></returns>
		private List<Candidat> tri(ComboBox cb)
		{
			using(var context = new electionEDM())
			{
				//Si l'id est choisi, on trie les candidats par leur id
				if(cb.SelectedItem == idPremierTriComboBox || cb.SelectedItem == idDeuxiemeTriComboBox || cb.SelectedItem == idTroisiemeTriComboBox)
				{
					var query = from candidat in context.Candidat
								orderby candidat.idCandidat
								select candidat;

					return query.ToList();
				}

				//Sinon si le nom est choisi, on trie le nom
				else if (cb.SelectedItem == nomPremierTriComboBox || cb.SelectedItem == nomDeuxiemeTriComboBox || cb.SelectedItem == nomTroisiemeTriComboBox)
				{
					var query = from candidat in context.Candidat
								orderby candidat.nom
								select candidat;

					return query.ToList();
				}

				//Sinon on tri par le prénom
				else
				{
					var query = from candidat in context.Candidat
								orderby candidat.prenom
								select candidat;

					return query.ToList();
				}

			}
		}

		/// <summary>
		/// Permet de trier une collection de Candidats déjà triés auparavant
		/// </summary>
		/// <param name="cb">ComboBox contenant le choix du tri de l'utilisateur</param>
		/// <param name="candidat">Liste de candidat déjà trié auparavant</param>
		/// <returns></returns>
		private List<Candidat> tri (ComboBox cb, List<Candidat> candidatDejaTrie)
		{

			//Si l'id est choisi, on trie les candidats par leur id
			if (cb.SelectedItem == idPremierTriComboBox || cb.SelectedItem == idDeuxiemeTriComboBox || cb.SelectedItem == idTroisiemeTriComboBox)
			{
				var query = from candidat in candidatDejaTrie
							orderby candidat.idCandidat
							select candidat;

				return query.ToList();
			}

			//Sinon si le nom est choisi, on trie le nom
			else if (cb.SelectedItem == nomPremierTriComboBox || cb.SelectedItem == nomDeuxiemeTriComboBox || cb.SelectedItem == nomTroisiemeTriComboBox)
			{
				var query = from candidat in candidatDejaTrie
							orderby candidat.nom
							select candidat;

				return query.ToList();
			}

			//Sinon on tri par le prénom
			else
			{
				var query = from candidat in candidatDejaTrie
							orderby candidat.prenom
							select candidat;

				return query.ToList();
			}

		}

	}
}
