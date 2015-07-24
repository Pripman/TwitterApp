using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net;

namespace RESTClient
{
	/*Static class holding utility functions*/
	public static class StatusUtil
	{
		private static Deserializer _ds;

		static StatusUtil(){
			_ds = new Deserializer();
		}
		/*The base uri of the board*/
		public static string boardUri = "http://localhost:8080";

		/*Display the menu and return the choice of the user*/
		public static int menu()
		{
			Console.WriteLine("Menu:");
			Console.WriteLine("0: Exit.");
			Console.WriteLine("1: Print the last n tweet statuses from specified screen name.");
			Console.WriteLine("2: Print all tweet status from board");
			Console.WriteLine("3: Print specified tweet status from board.");
			Console.WriteLine("4: Print all tweet status from board from specific user.");
			Console.WriteLine("5: Forward the last N twitt status from specified screen name to board");
			Console.WriteLine("6: Modify tweet status text from the board");
			Console.WriteLine("7: Delete twitt status");

			bool p = false;
			int choice;
			do
			{
				Console.WriteLine("Enter your choice:");
				p = int.TryParse(Console.ReadLine(), out choice);
			} while (choice < 0 || choice > 12 || !p);

			return choice;
		}


		/*Forward a twitt user to the board*/
		public static void printTwittStatusFromUser()
		{
			int limit;
			bool p;
			string screenName;
			do {
				Console.WriteLine ("Enter the screen name of the twitt user:");
				screenName = Console.ReadLine ();
				Console.WriteLine ("Enter number of messages:");
				p = int.TryParse (Console.ReadLine (), out limit);
			} while(screenName == "" || !p);


			var statusList = TwittManager.GetTwittStatusList(screenName);
			if (statusList == null) return;
			for (var i = 0; i < statusList.Count(); i++) 
			{
				if (i < limit) 
				{
					Console.WriteLine ("\n\n");
					statusList [i].printOut ();
				}
				else 
				{
					break;
				}
			}
		}

		//print all statuses in board
		public static void PrintTwittStatusesFromBoard()
		{
			string uri = Util.boardUri + "/status";

			var res = RestManager.GetJsonResponse(uri);
			if (res == null)
				return;

			foreach (var status in _ds.DeserializeStatuses (res)) {
				status.printOut ();
			}

		}

		public static void PrintTwittStatusFromBoard()
		{
			TwittStatus status;
			string id; 
			Console.WriteLine ("Enter the id of the twitt status you want to retrieve:");
			id = Console.ReadLine ();
			
			string uri = Util.boardUri + "/status/" + id;

			var res = RestManager.GetJsonResponse (uri);
			if (res == null)
				return;

			status = _ds.DeserializeStatus (res);
			status.printOut ();
		}


		public static void PrintTwittStatusesFromUser()
		{

			Console.WriteLine ("Enter the id of the user you want to retrieve tweets from:");
			string screenName = Console.ReadLine ();
			string useruri = Util.boardUri + "/twittusers/screenname/" + screenName;

			var userres = RestManager.GetJsonResponse (useruri);
			if (userres == null)
				return;
			
			var user = _ds.DeserializeUser (userres);

			string statusuri = Util.boardUri + "/twittusers/" + user.id + "/status";

			var statusres = RestManager.GetJsonResponse(statusuri);
			if (statusres == null)
				return;

			foreach (var status in _ds.DeserializeStatuses (statusres)) {
				status.printOut ();
			}

		}

		public static void ForwardTwittStatusFromUser()
		{
			Console.WriteLine("Enter the screen name from which to forward:");
			string screenName = Console.ReadLine();

			Console.WriteLine("Enter the number of status tweets you want to forward:");
			int n = int.Parse(Console.ReadLine());

			var statusList = TwittManager.GetTwittStatusList(screenName);
			if (statusList == null) return;
			for (var i = 0; i < statusList.Count(); i++) 
			{
				if (i < n) 
				{
					RestManager.PostRequest(Util.boardUri + "/status", statusList[i].toJsonString());
				}
				else 
				{
					break;
				}
			}
		}

		public static void ModifyTweetText()
		{
			Console.WriteLine ("Enter the id of the tweet text you want to modify:");
			string id = Console.ReadLine ();
			Console.WriteLine ("Enter new text:");
			string newText = Console.ReadLine ();

			RestManager.PutRequest(Util.boardUri + "/status/" + id + "/text", newText);
		}

		public static void DeleteTwitt()
		{
			Console.WriteLine ("Enter the id of the tweet you want to delete:");
			string id = Console.ReadLine ();
			RestManager.DeleteRequest (Util.boardUri + "/status/" + id);
		}
	}
}


