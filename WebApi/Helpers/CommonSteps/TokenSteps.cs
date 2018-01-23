using System.Configuration;
using System.Linq;
using TechTalk.SpecFlow;

namespace Www
{
    [Binding]
    public class TokenSteps
    {
        [Given(@"I have the valid STS token")]
        public void GivenIHaveTheValidSTSToken()
        {
            ScenarioContext.Current.Add("accessToken", TokenApiService.StsAccessToken());
        }

        [Given(@"I have STS accessToken")]
        public void GivenIHaveSTSAccessToken()
        {
            ScenarioContext.Current.Add("accessToken", TokenApiService.StsAccessToken());
        }

        [Given(@"I do not supply a token")]
        public void GivenIDoNotSupplyAToken()
        {
            if (ScenarioContext.Current.ContainsKey("accessToken"))
            {
                ScenarioContext.Current["accessToken"] = null;
            }
            else
            {
                ScenarioContext.Current.Add("accessToken", null);
            }
        }

        [Given(@"I have the STS token with client (.*) clientSecret (.*) and scope (.*)")]
        public void GivenIHaveTheSTSTokenWithClientClientSecretAndScope(string clientId, string clientSecret, string scope)
        {
            var Client_ID = ConfigurationManager.AppSettings[clientId];
            var Client_Secret = ConfigurationManager.AppSettings[clientSecret];
            var Scope = ConfigurationManager.AppSettings[scope];

            var accessToken = TokenApiService.StsAccessToken(Client_ID, Client_Secret, Scope);

            //Assert.IsNotNull(accessToken, $"Access token should not be null given client {clientId} and scope {scope}");
            AddToContext("accessToken", accessToken);
        }

        [Given(@"I have the tampered STS token")]
        public void GivenIHaveTheTamperedSTSToken()
        {
            var tamperedToken = TokenApiService.TamperedToken();
            AddToContext("accessToken", tamperedToken);
        }

        public void AddToContext(string key, object value)
        {

            bool exists = ScenarioContext.Current.Any(x => x.Key == key);
            if (exists)
            {
                ScenarioContext.Current.Remove(key);
            }
            ScenarioContext.Current.Add(key, value);
        }

    }
}
