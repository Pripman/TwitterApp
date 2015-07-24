using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;

namespace RESTClient
{
	/*This class is the data model for a twitt user*/
	/*Serializable enable to serialize this object*/
	[Serializable()]
	public class TwittStatus
	{
		/*Fields are declared public for conveniance*/
		public string id;
		public string text;
		public string userid;
		public string created_at;

		/*Empty constructor required to serialize*/
		public TwittStatus() { }

		/*Basic contructor*/
		public TwittStatus(string id, string text, string user, string created_at)
		{
			this.id = id;
			this.text = text;
			this.userid = user;
			this.created_at = created_at;
		}

		/*Print the object in clear text*/
		public void printOut()
		{
			Console.WriteLine ("\n");
			Console.WriteLine("Id: " + this.id);
			Console.WriteLine("text: " + this.text);
			Console.WriteLine("user: " + this.userid);
			Console.WriteLine("created_at: " + this.created_at);
			Console.WriteLine ("\n");
		}

		/*Serialize the object to an xml string*/
		public string toJsonString()
		{
			return JsonConvert.SerializeObject (this);

		}
	}
}


