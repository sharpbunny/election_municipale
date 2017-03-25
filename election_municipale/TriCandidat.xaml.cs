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
			//Si aucun tri n'a été sélectionné
			if((TriComboBox.SelectedItem == null || TriComboBox.SelectedItem.ToString() == "Aucun"))
			{
				MessageBox.Show("Vous n'avez choisi aucun tri");
			}

			//Si au moins un tri a été sélectionné
			else
			{
				List<Candidat> candidatTrie = null;
				candidatTrie = selectionDuTri(candidatTrie);


				((MainWindow)this.Owner).afficherCandidatDataGrid(candidatTrie);
				this.Close();

			} //Fin du else

		}

		/// <summary>
		/// Tri la liste des candidats en fonction de ce qu'a choisi l'utilisateur
		/// </summary>
		/// <param name="candidat"></param>
		/// <returns></returns>
		private List<Candidat> selectionDuTri (List<Candidat> candidatTrie)
		{
			IQueryable<Candidat> query = null;

			using(var context = new electionEDM())
			{
				if (TriComboBox.SelectedItem.Equals(idTriComboBox))
				{
					query = from candidat in context.Candidat
								orderby candidat.idCandidat
								select candidat;
				}

				else if (TriComboBox.SelectedItem.Equals(nomTriComboBox))
				{
					query = from candidat in context.Candidat
								orderby candidat.nom
								select candidat;
				}

				else if (TriComboBox.SelectedItem.Equals(prenomTriComboBox))
				{
					query = from candidat in context.Candidat
								orderby candidat.prenom
								select candidat;
				}

				else if (TriComboBox.SelectedItem.Equals(idNomTriComboBox))
				{
					query = from candidat in context.Candidat
								orderby candidat.idCandidat, candidat.nom
								select candidat;
				}

				else if (TriComboBox.SelectedItem.Equals(idPrenomTriComboBox))
				{
					query = from candidat in context.Candidat
								orderby candidat.idCandidat, candidat.prenom
								select candidat;
				}

				else if (TriComboBox.SelectedItem.Equals(nomPrenomTriComboBox))
				{
					query = from candidat in context.Candidat
								orderby candidat.nom, candidat.prenom
								select candidat;
				}

				//On tri par le prénom, puis par le nom
				else
				{
					query = from candidat in context.Candidat
								orderby candidat.prenom, candidat.nom
								select candidat;
				}

				candidatTrie = query.ToList();

			} // Fin du using

			return candidatTrie;
		}
	}
}
