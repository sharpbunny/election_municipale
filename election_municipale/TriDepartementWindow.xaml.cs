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

		}
	}
}
