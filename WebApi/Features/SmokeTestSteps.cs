using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WTW.CFT.SharedServices.ReferenceData.PerilService.PerilGateway.PerilGateway.TestAutomation.Helpers;

namespace WTW.CFT.SharedServices.ReferenceData.PerilService.PerilGateway.PerilGateway.TestAutomation.Features
{
    [Binding]
    public class SmokeTestSteps
    {
        [Given(@"I have STS token with client (.*) clientSecret (.*) and scope (.*)")]
        public void GivenIHaveSTSTokenWithClientClientSecretAndScope(string clientId, string clientSecret, string scope)
        {
            var Client_ID = ConfigurationManager.AppSettings[clientId];
            var Client_Secret = ConfigurationManager.AppSettings[clientSecret];
            var Scope = ConfigurationManager.AppSettings[scope];

            var accessToken = TokenApiService.StsAccessToken(Client_ID, Client_Secret, Scope);

            //Assert.IsNotNull(accessToken, $"Access token should not be null given client {clientId} and scope {scope}");
            AddToContext("accessToken", accessToken);
        }
    }
}
