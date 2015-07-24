using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace RESTClient
{
    /*This static class holds function to perform HTTP request*/
    public static class RestManager
    {


        /*Performs a get request on a given uri*/
        public static HttpWebResponse GetResponse(string uri)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(uri));

            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException webEx)
            {
                res = (HttpWebResponse)webEx.Response;

                Console.WriteLine(res.StatusCode.ToString());

                res.Close();

                return null;
            }

            return res;
        }

        /*Retrieve an Json document from a given uri*/
        public static string GetJsonResponse(string uri)
        {
			string body;

            HttpWebResponse res = GetResponse(uri);

			if (res != null){
				using(var Stream = new StreamReader(res.GetResponseStream())){
					body = Stream.ReadToEnd ();
				}
			}
			else
			{
				return null;
			}
			res.Close();

			return body;
        }

        /*Performs a POST request on a given uri with the given xml*/
        public static HttpWebResponse PostRequest(string uri, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            request.Method = "Post";
            request.ContentType = "text/json";


			using (var writer = new StreamWriter (request.GetRequestStream ())) 
			{
				writer.Write (postData);
				writer.Flush ();
			}

            HttpWebResponse res;
            
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                res = (HttpWebResponse)webEx.Response;
                Console.WriteLine(res.StatusCode.ToString());
                res.Close();
                return null;
            }

            return res;
        }

        /*Performs a PUT request on a given uri with the given putData*/
        public static HttpWebResponse PutRequest(string uri, string putData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            request.Method = "Put";
			using (var writer = new StreamWriter (request.GetRequestStream ())) 
			{
				writer.Write (putData);
				writer.Flush ();
			}

            HttpWebResponse res;

            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                res = (HttpWebResponse)webEx.Response;

                Console.WriteLine(res.StatusCode.ToString());

                res.Close();

                return null;
            }

            return res;
        }

        /*Performs a Delete request on a given uri*/
        public static void DeleteRequest(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            request.Method = "DELETE";

            Console.WriteLine(uri);

            HttpWebResponse res;

            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webEx)
            {
                res = (HttpWebResponse)webEx.Response;

                Console.WriteLine(res.StatusCode.ToString());

                res.Close();
                return;
            }

            res.Close();
        }
    }
}
