using Newtonsoft.Json.Linq;
using RestSharp;

namespace Wwww
{
    public class TokenApiService
    {
        public static string tokenUrl = System.Configuration.ConfigurationManager.AppSettings["SecurityTokenServiceAuthority"];
        public static string ReferenceData_Client_ID = System.Configuration.ConfigurationManager.AppSettings["ReferenceData_Client_ID"];
        public static string ReferenceData_Client_Secret = System.Configuration.ConfigurationManager.AppSettings["ReferenceData_Client_Secret"];
        public static string ReferenceData_Scope = System.Configuration.ConfigurationManager.AppSettings["ReferenceData_Scope"];
        public static string Subscription_Scope = System.Configuration.ConfigurationManager.AppSettings["Subscription_Scope"];

        public static string StsAccessToken(string clientId, string clientSecret, string scope)
        {
            var client = new RestClient(tokenUrl);
            var request = new RestRequest("/connect/token", Method.POST);

            //var credentials = string.Format("{0}:{1}", clientId, clientSecret);
            //var headerValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));


            request.AddParameter("Client_ID", clientId);
            request.AddParameter("Client_Secret", clientSecret);
            request.AddParameter("Scope", scope);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("Content-Type", "application/x-www-form-urlencoded");
            // request.AddHeader("Authorization", string.Format("Basic " + headerValue));

            var response = client.Execute(request);
            var stsToken = response.Content;
            dynamic token = JObject.Parse(stsToken);
            var accessToken = token.access_token;
            return accessToken;
        }

        public static string StsAccessToken()
        {
            var client = new RestClient(tokenUrl);
            var request = new RestRequest("/connect/token", Method.POST);

            //var credentials = string.Format("{0}:{1}", clientId, clientSecret);
            //var headerValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));


            request.AddParameter("Client_ID", ReferenceData_Client_ID);
            request.AddParameter("Client_Secret", ReferenceData_Client_Secret);
            request.AddParameter("Scope", ReferenceData_Scope);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("Content-Type", "application/x-www-form-urlencoded");
            // request.AddHeader("Authorization", string.Format("Basic " + headerValue));

            var response = client.Execute(request);
            var stsToken = response.Content;
            dynamic token = JObject.Parse(stsToken);
            var accessToken = token.access_token;
            return accessToken;
        }

        public static string TamperedToken()
        {
            var validToken = StsAccessToken();
            var tamperedToken = string.Format("123" + validToken);
            return tamperedToken;
        }

    }
}