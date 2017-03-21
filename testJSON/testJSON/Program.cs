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
			Candidat[] candidat = new Candidat[5];
			Departement dept = new Departement();
			string[][] allData = lireToutesLesDonnees();

			for(int i=0; i < allData.Length; i++)
			{
				for(int colonne=0; colonne < 75; colonne++)
				{
					if(i > 0)
					{
						switch (colonne)
						{
							//code du département
							case 1:
								dept.code_du_departement = Convert.ToSByte(allData[i][colonne]);
								break;
							//type du scrutin
							case 2:
								break;

							
							//libelle_du_departement
							case 3:
								dept.libelle_du_departement = allData[i][colonne];
								break;
						}
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
