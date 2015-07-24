using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace RESTClient
{
    class Program
    {
        /*Entry point of the program*/
        static void Main(string[] args)
        {
            int choice = -1;

            while (choice != 0)
            {
                choice = Util.menu();

                switch (choice)
                {
                    case 1:
                        Util.printTwittUser();
                        break;
                    case 2:
                        Util.printTwittUsersFromBoard();
                        break;
                    case 3:
                        Util.printTwittUserFromBoard();
                        break;
                    case 4:
                        Util.forwardTwittUser();
                        break;
                    case 5:
                        Util.changeTwittUser();
                        break;
                    case 6:
                        Util.deleteTwittUser();
                        break;

					//Print last N Twitt status from specified user
				case 7:
					messagesChosen ();
						break;

				}

            }
        }

		public static void messagesChosen(){
			int choice = -1;

			while (choice != 0)
			{
				choice = StatusUtil.menu();

				switch (choice)
				{
				case 1:
					StatusUtil.printTwittStatusFromUser();
					break;
				case 2:
					StatusUtil.PrintTwittStatusesFromBoard();
					break;
				case 3:
					StatusUtil.PrintTwittStatusFromBoard();
					break;
				case 4:
					StatusUtil.PrintTwittStatusesFromUser();
					break;
				case 5:
					StatusUtil.ForwardTwittStatusFromUser ();
					break;
				case 6:
					StatusUtil.ModifyTweetText();
					break;
				case 7:
					StatusUtil.DeleteTwitt();
					break;
				}
			}
		}
    }
}
