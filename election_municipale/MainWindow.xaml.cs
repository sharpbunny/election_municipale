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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace election_municipale
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private void LireJSon(string lienFichier)
		{
			using (StreamReader reader = File.OpenText(lienFichier))
			{
				JObject array = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
				foreach (var item in array)
				{
					titreLabel.Content = item.Key;
				}
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			LireJSon("../../election.json");
		}
	}
}
