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
    public static class Util
    {
		private static Deserializer _ds;

		static Util(){
			_ds = new Deserializer();
		}
        /*The base uri of the board*/
		public static string boardUri = "http://localhost:8080";

        /*Display the menu and return the choice of the user*/
        public static int menu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("0: Exit.");
            Console.WriteLine("1: Print out a twitt user's information from specified screen name.");
            Console.WriteLine("2: Print all Twitt Users from the board.");
            Console.WriteLine("3: Print specified Twitt User from the board.");
            Console.WriteLine("4: Forward one Twitt User from specified screen name to the board.");
            Console.WriteLine("5: Modify a Twitt User description from the board.");
            Console.WriteLine("6: Delete twitt user");
			Console.WriteLine("7: Open message manager");

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
        public static void printTwittUser()
        {
            Console.WriteLine("Enter the screen name of the twitt user you want to print out:");
            string screenName = Console.ReadLine();

            TwittUser tu = TwittManager.GetTwittUser(screenName);
            if (tu == null) return;
            tu.printOut();
        }

        /*Print all the twitt users from the board*/
        public static void printTwittUsersFromBoard()
        {
            string uri = Util.boardUri + "/twittusers";

			var res = RestManager.GetJsonResponse(uri);
            if (res == null)
                return;

			foreach (var user in _ds.DeserializeUsers (res)) {
				user.printOut ();
			}
        }

        /*Print a specified twitt user from the board*/
        public static void printTwittUserFromBoard()
        {
			TwittUser user;
            Console.WriteLine("Enter the id of the twitt user you want to retrieve:");
            string id = Console.ReadLine();

            string uri = Util.boardUri + "/twittusers/" + id;

			var res = RestManager.GetJsonResponse (uri);
			if (res == null)
				return;

			user = _ds.DeserializeUser (res);
			user.printOut ();
        }

        /*Forward a twitt user to the board*/
        public static void forwardTwittUser()
        {
            Console.WriteLine("Enter the screen name of the twitt user you want to forward:");
            string screenName = Console.ReadLine();

            TwittUser tu = TwittManager.GetTwittUser(screenName);
            if (tu == null) return;
			RestManager.PostRequest(Util.boardUri + "/twittusers", tu.toJsonString());
        }


        /*Modify the description of a specified twitt user*/
        public static void changeTwittUser()
        {
            int n;
            string description;

            do
            {
                Console.WriteLine("Enter the id of the twitt user you want to modify:");
                n = int.Parse(Console.ReadLine());
            } while (n < 1);

            do
            {
                Console.WriteLine("Enter the new description:");
                description = Console.ReadLine();
            } while (description == null);


            String uri = Util.boardUri + "/twittusers/" + n.ToString();

			var res = RestManager.GetJsonResponse (uri);
			TwittUser tu = _ds.DeserializeUser(res);
            tu.description = description;

            RestManager.PutRequest(uri, tu.toJsonString());
        }

        /*Delete a twitt status*/
        public static void deleteTwittUser()
        {
            Console.WriteLine("Enter the id of the user to delete:");
            string id = Console.ReadLine();
            string uri = Util.boardUri + "/twittusers/" + id;

            RestManager.DeleteRequest(uri);

        }

    }
}
