using System;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;

namespace www
{
    [Binding]
    public class ResponseSteps
    {
        public static string Url = ConfigurationManager.AppSettings["Url"];
        public static string env;

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statuscode)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");
            var actualStatus = (int)response.StatusCode;

            Assert.AreEqual(statuscode, actualStatus, $"Expected Successful response but received {response.Content}");
        }

        [Then(@"the response code is (.*)")]
        public void ThenTheResponseCodeIs(int statuscode)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");
            var actualStatus = (int)response.StatusCode;

            Assert.AreEqual(statuscode, actualStatus, $"Because {response.Content}");
        }

        [Then(@"the response body should have list of count value (.*)")]
        public void ThenTheResponseBodyShouldHaveListOfCountValue(int count)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            //dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            var ODataResponse = JsonConvert.DeserializeObject<OdataListResponse<Object>>(jsonResponse);

            // Assert.AreEqual(ODataResponse.Value.Count, count);

            ScenarioContext.Current.Add("ODataResponse", ODataResponse);

            Assert.AreEqual(count, ODataResponse.Value.Count,
                $"Expected {count} but actual {ODataResponse.Value.Count} ");
        }

        [Then(@"the response body should have Next link")]
        public void ThenTheResponseBodyShouldHaveNextLink()
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            var ODataResponse = JsonConvert.DeserializeObject<OdataPagedList<Object>>(jsonResponse);

            var nextLink = ODataResponse.odatanextLink;

            nextLink.Should().NotBeNull($"expected 100 records but actual count is {ODataResponse.value.Length}");
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