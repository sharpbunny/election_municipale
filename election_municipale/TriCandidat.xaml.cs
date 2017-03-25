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
		public TriCandidat()
		{
			InitializeComponent();
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
	}
}
