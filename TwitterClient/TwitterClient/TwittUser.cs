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
    public class TwittUser
    {
        /*Fields are declared public for conveniance*/
        public string id;
        public string name;
        public string screenName;
        public string location;
        public string description;

        /*Empty constructor required to serialize*/
        public TwittUser() { }

        /*Basic contructor*/
        public TwittUser(string id, string name, string screenName, string location, string description)
        {
            this.id = id;
            this.name = name;
            this.screenName = screenName;
            this.location = location;
            this.description = description;
        }

        /*Print the object in clear text*/
        public void printOut()
        {
			Console.WriteLine ("\n");
            Console.WriteLine("Id: " + this.id);
            Console.WriteLine("Name: " + this.name);
            Console.WriteLine("Screen Name: " + this.screenName);
            Console.WriteLine("Location: " + this.location);
            Console.WriteLine("Description: " + this.description);
			Console.WriteLine ("\n");
        }

        /*Serialize the object to an xml string*/
        public string toJsonString()
        {
			return JsonConvert.SerializeObject (this);

        }
    }
}
