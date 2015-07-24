using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Twitterizer;

namespace RESTClient
{
    public static class TwittManager
    {

        /*Retrieve a twitt user from a given screen name, see https://dev.twitter.com/docs/api/1/get/users/show*/
        public static TwittUser GetTwittUser(string screenName)
        {
            OAuthTokens tokens = new OAuthTokens();
			tokens.ConsumerKey = "Removed from github example";
			tokens.ConsumerSecret = "Removed from github example";
			tokens.AccessToken = "Removed from github example";
			tokens.AccessTokenSecret = "Removed from github example";

            var list = new List<string>();

			LookupUsersOptions options = new LookupUsersOptions(){UseSSL = true, APIBaseAddress="http://api.twitter.com/1.1/"};
            options.ScreenNames.Add(screenName);


			TwitterResponse<TwitterUserCollection> res = TwitterUser.Lookup(tokens, options);
            TwitterUserCollection users = res.ResponseObject;
            TwitterUser user = users.First();

            if (user == null) return null;

            TwittUser tu = new TwittUser(
                user.Id.ToString(),
                user.Name,
                user.ScreenName,
                user.Location,
                user.Description);
            return tu;

        }

		public static List<TwittStatus> GetTwittStatusList(string screenName)
		{
			OAuthTokens tokens = new OAuthTokens();
			tokens.ConsumerKey = "zPY6AwGePUOWAk0fTvrhZhgzg";
			tokens.ConsumerSecret = "VzBhawh55oWWocdDrn4MdLfSPcG5ypf7scFJZGrSyWkSuJAjDA";
			tokens.AccessToken = "50022775-djO15EBUOMT76TXswKa0XvwfDmM12Xo27NZmxyhwr";
			tokens.AccessTokenSecret = "QqpnD1Mq4AEQYW48NyauzAMDRGDyQ0QCTQjWNRFyFCZkz";

			var list = new List<TwittStatus>();

			UserTimelineOptions options = new UserTimelineOptions();
			options.APIBaseAddress = "https://api.twitter.com/1.1/";
			options.Count = 20;
			options.UseSSL = true;
			options.ScreenName = screenName;
			var resp = TwitterTimeline.UserTimeline (tokens, options);
			TwitterStatusCollection tweets = resp.ResponseObject;

			if (tweets == null) return null;

			foreach (var status in tweets) {
				TwittStatus ts = new TwittStatus (
					status.Id.ToString (),
					status.Text,
					status.User.Id.ToString(),
					status.CreatedDate.ToString());
				list.Add (ts);
			}
			return list;
		}
    }
}
