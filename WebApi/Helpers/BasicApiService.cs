using System;
using System.Configuration;
using RestSharp;

namespace Www
{
    public class BasicApiService
    {
        public static string productUrl = ConfigurationManager.AppSettings["productUrl"];
        public static string userprofileUrl = ConfigurationManager.AppSettings["userprofileUrl"];
        public static string loggingUrl = ConfigurationManager.AppSettings["loggingUrl"];
        public static string Url = ConfigurationManager.AppSettings["Url"];

        public static IRestResponse GetRequestResponse(string token, string urlParm)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(urlParm, Method.GET);
            request.AddParameter("Authorization", String.Format("bearer " + token), ParameterType.HttpHeader);

            var response = client.Execute(request);

            return response;
        }

        public static IRestResponse GetClientResponse(string token, string urlParm)
        {
            var client = new RestClient(urlParm);
            var request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", String.Format("bearer " + token), ParameterType.HttpHeader);

            var response = client.Execute(request);

            return response;
        }

    }
}