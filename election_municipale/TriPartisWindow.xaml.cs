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
	/// Logique d'interaction pour TriPartisWindow.xaml
	/// </summary>
	public partial class TriPartisWindow : Window
	{
		/// <summary>
		/// Constructeur de TriPartisWindow
		/// </summary>
		public TriPartisWindow()
		{
			InitializeComponent();
			this.Owner = Application.Current.MainWindow;
		}

		/// <summary>
		/// Ferme la fenêtre de tri des partis politiques
		/// </summary>
		/// <param name="sender">Bouton : quitterButton</param>
		/// <param name="e">Click sur le bouton : quitterButton</param>
		private void quitterButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Tri les partis politiques selon les choix de l'utilisateur
		/// </summary>
		/// <param name="sender">Bouton : trierButton</param>
		/// <param name="e">Click sur le bouton : trierButton</param>
		private void trierButton_Click(object sender, RoutedEventArgs e)
		{
			if(TriComboBox.SelectedItem == null || TriComboBox.SelectedItem.Equals(aucunTriComboBox))
			{
				MessageBox.Show("Vous n'avez choisi aucun type de tri.");
			}

			else
			{
				List<Parti> partiTrie = null;
				IQueryable<Parti> query = null;

				using(var context = new electionEDM())
				{
					query = from partis in context.Parti
							orderby partis.code_nuance
							select partis;

					partiTrie = query.ToList();
				}

				((MainWindow)this.Owner).afficherPartiDataGrid(partiTrie);
				this.Close();
			}
		}
	}
}
