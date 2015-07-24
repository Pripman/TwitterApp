using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;


namespace RESTClient
{
	public class Deserializer
	{
	



		public TwittUser ConvertUserJson(JObject obj){

			var id = makeProperty ("id", obj);
			var name = makeProperty ("name", obj);
			var screenName = makeProperty ("screenName", obj);
			var location = makeProperty ("location", obj);
			var description = makeProperty ("description", obj);
			var user = new TwittUser(id,  name,  screenName,  location,  description); 
			return user;
		}

		public TwittStatus ConvertStatusJson(JObject obj){

			var id = makeProperty ("id", obj);
			var text = makeProperty ("text", obj);
			var userid = makeProperty ("userid", obj);
			var created_at = makeProperty ("created_at", obj);
			var status = new TwittStatus(id,  text,  userid,  created_at); 
			return status;
		}

		public TwittUser DeserializeUser(string Json)
		{
			var obj = JObject.Parse(Json);
			return ConvertUserJson (obj);

		}

		public List<TwittUser> DeserializeUsers(string Json)
		{
			Debug.WriteLine ("Trying to deserialize users string: {0}", Json);
			List<TwittUser> users = new List<TwittUser>();
			var objs = JArray.Parse(Json);
			foreach(JObject user in objs)
			{

				users.Add(ConvertUserJson(user));
			} 

			return users;
		}

		public TwittStatus DeserializeStatus(string Json)
		{
			var obj = JObject.Parse(Json);
			return ConvertStatusJson (obj);

		}

		public List<TwittStatus> DeserializeStatuses(string Json)
		{
			Debug.WriteLine ("Trying to deserialize Status string: {0}", Json);
			List<TwittStatus> statuses = new List<TwittStatus>();
			var objs = JArray.Parse(Json);
			foreach(JObject user in objs)
			{

				statuses.Add(ConvertStatusJson(user));
			} 

			return statuses;
		}


			
		private string makeProperty(string property, JObject obj)
		{
			var value = (string)obj[property];
			if (value == null) {
				Debug.WriteLine ("ERROR deserializing json property: " + property);
				throw new System.NullReferenceException("ERROR deserializing json property: " + property);
			}
			return value;
		}
	}
}

