using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace testJSON
{
	class Program
	{
		static void Main(string[] args)
		{

			string[][] allData = lireToutesLesDonnees();

			for(int i=0; i < allData.Length; i++)
			{
				for(int j=0; j < 75; j++)
				{
					
					if(j == 22)
					{
						Candidat johnny = new Candidat();
						johnny.nom = allData[i][j];
						Console.WriteLine(johnny.nom);
					}
				}
			}

			


		}

		/// <summary>
		/// A un moment, tu peux lire le JSON, mais sa mère le csv c'est mieux
		/// </summary>
		/// <returns></returns>
		public static string[][] lireToutesLesDonnees()
		{
			string[] line;
			string[][] allData = new String[232][];

			line = File.ReadAllLines(@"election.csv");

			for (int i = 0; i < line.Length; i++)
			{
				allData[i] = line[i].Split(';');
			}

			return allData;
		}

	}


}
