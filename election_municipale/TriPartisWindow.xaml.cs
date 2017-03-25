﻿using System;
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
	}
}
