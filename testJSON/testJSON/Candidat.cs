using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace testJSON
{
	public class Candidat
	{
		[JsonProperty(PropertyName = "nom_01")]
		public string nom { get; set; }

	}
}
