using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace xyz
{
    [Binding]
    public class GetPerilInfoSteps
    {
        public static string Url = ConfigurationManager.AppSettings["Url"];
        public static string env;

        [When(@"I request a GET for version details")]
        public void WhenIRequestAGETForVersionDetails()
        {
            var accessToken = ScenarioContext.Current.Get<string>("accessToken");

            var Endpoint = string.Format("/Peril");

            var response = BasicApiService.GetRequestResponse(accessToken, Endpoint);

            AddToContext("response", response);
        }

        [When(@"I request a swagger for Peril services (.*)")]
        public void WhenIRequestASwaggerForPerilServices(string Endpoint)
        {
            var accessToken = ScenarioContext.Current.Get<string>("accessToken");

            var response = BasicApiService.GetRequestResponse(accessToken, Endpoint);

            AddToContext("response", response);
        }

        [Then(@"I should see the following version details:")]
        public void ThenIShouldSeeTheFollowingVersionDetails(Table table)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            var ODataResponse =
                JsonConvert.DeserializeObject<Version>(jsonResponse);

            Assert.IsNotNull(ODataResponse.version, "Version should have a value in the response");

            // override version before comparing entire response because it is not constant
            ODataResponse.version = ".";
            table.CompareToInstance<Version>(ODataResponse);

        }

        [Then(@"the response should contain following heartmonitor details:")]
        public void ThenTheResponseShouldContainFollowingHeartmonitorDetails(Table table)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            var ODataResponse =
                JsonConvert.DeserializeObject<ExpectedMonitorModel<MonitorResults>>(jsonResponse);

            table.CompareToInstance<ExpectedMonitorModel<MonitorResults>>(ODataResponse);
        }

        [Then(@"the response should contain following heartbeat details:")]
        public void ThenTheResponseShouldContainFollowingHeartbeatDetails(Table table)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            var ODataResponse =
                JsonConvert.DeserializeObject<ExpectedHeartbeatModel>(jsonResponse);

            table.CompareToInstance<ExpectedHeartbeatModel>(ODataResponse);
        }

        public class MonitorInstances
        {
            public string Type { get; set; }
            public int NumberofInstances { get; set; }
        }

        [Then(@"the response should contain following heartMonitor instances:")]
        public void ThenTheResponseShouldContainFollowingHeartMonitorInstances(Table table)
        {
            //var expectedResults = table.CreateInstance<MonitorInstances>();

            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            var deserializeResponse = JsonConvert.DeserializeObject<ExpectedMonitorModel<MonitorResults>>(jsonResponse);

            Assert.AreEqual(table.RowCount, deserializeResponse.Results.Count, $"Monitor report should contain {table.RowCount} checks");

        }

        [Then(@"the swagger document will contain version,title,schemes,host,scopes and authorizationUrl")]
        public void ThenTheSwaggerDocumentWillContainVersionTitleSchemesHostScopesAndAuthorizationUrl()
        {
            if (Url.Contains("-dev-"))
            {
                env = "dev";
            }
            else if (Url.Contains("-qa-"))
            {
                env = "qa";
            }
            else if (Url.Contains("-sit-"))
            {
                env = "sit";
            }
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            Assert.IsTrue(jsonResponse.Contains("\"v1\""));
            Assert.IsTrue(jsonResponse.Contains("\"Willis Towers Watson - Client Facing Technology\""));
            Assert.IsTrue(jsonResponse.Contains("\"schemes\":[\"https\"]"));
            Assert.IsTrue(jsonResponse.Contains("\"swagger\":\"2.0\""));
            Assert.IsTrue(jsonResponse.Contains($"\"host\":\"wtw-cft-uk-s-{env}-ref-data-api-app.azurewebsites.net\""));
            Assert.IsTrue(jsonResponse.Contains($"\"authorizationUrl\":\"https://www-cft-uk-s-{env}-sts-api-app.azurewebsites.net//connect/token\""));
            Assert.IsTrue(jsonResponse.Contains($"\"authorizationUrl\":\"https://www-cft-uk-s-{env}-sts-api-app.azurewebsites.net//connect/token\""));
            Assert.IsTrue(jsonResponse.Contains("\"scopes\":{\"IntegrationLayer.Documentation\":\"Read access to the service documentation.\",\"ReferenceData.Read\":\"Read access to the service data.\""));
            Assert.IsTrue(jsonResponse.Contains("/odata/bubblo"));
            Assert.IsTrue(jsonResponse.Contains("/odata/bubblos({id})"));
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