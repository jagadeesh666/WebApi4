using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using WTW.CFT.SharedServices.ReferenceData.PerilService.PerilGateway.PerilGateway.TestAutomation.Helpers;
using WTW.CFT.SharedServices.ReferenceData.PerilService.PerilGateway.PerilGateway.TestAutomation.Helpers.Models;

namespace WTW.CFT.SharedServices.ReferenceData.PerilService.PerilGateway.PerilGateway.TestAutomation.Features
{
    [Binding]
    public class GetPerilDataSteps
    {
        private static string url = ConfigurationManager.AppSettings["Url"];

        [When(@"I request a GET to list of perils (.*) by expand=description")]
        [When(@"I request a GET for metadata (.*)")]
        [When(@"I request a GET for health check monitor (.*)")]
        [When(@"I request a GET for health check heartbeat (.*)")]
        [When(@"I request a list of perils (.*) ordered by name descending")]
        [When(@"I retrieve Endpoints (.*) by Id")]
        [When(@"I retrieve Endpoints (.*) with skip and top using count")]
        [When(@"I request a list of perils (.*)")]
        public void WhenIRequestAListOfPerils(string Endpoint)
        {
            var accessToken = ScenarioContext.Current.Get<string>("accessToken");

            var response = BasicApiService.GetRequestResponse(accessToken, Endpoint);

            AddToContext("response", response);
        }

        [Then(@"the Peril Domain list should have expanded")]
        public void ThenThePerilDomainListShouldHaveExpanded()
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var perilResponse = response.Content;

            var deserializeResponse = JsonConvert.DeserializeObject<OdataListResponse<ExpectedPeril>>(perilResponse);

            // var perilIdResp = ScenarioContext.Current.Get<IEnumerable<ExpectedPeril>>("perilResponse");
            var actual = deserializeResponse.Value.FirstOrDefault();

            Assert.IsNotNull(actual?.description,
                $"Peril by description: should expanded: {actual?.description}");
            Assert.IsNotNull(actual.description.perilId, $"Peril by description Id should expanded:{actual.description.perilId}");
        }


        //[Then(@"the Peril Domain list should have expanded")]
        //public void ThenThePerilDomainListShouldHaveExpanded()
        //{
        //    var response = ScenarioContext.Current.Get<IRestResponse>("response");

        //    var perilResponse = response.Content;

        //    var deserializeResponse = JsonConvert.DeserializeObject<OdataListResponse<ExpectedPeril>>(perilResponse);

        //   // var perilIdResp = ScenarioContext.Current.Get<IEnumerable<ExpectedPeril>>("perilResponse");
        //    var actual = deserializeResponse.Value.FirstOrDefault();

        //    Assert.IsNotNull(actual?.description,
        //        $"Peril by description: should expanded: {actual?.description}");
        //    //Assert.IsNotNull(actual.description.perilId, $"Peril by description Id should expanded:{actual.description.perilId}");
        //}

        [Then(@"the perils response body should contain top (.*) records in descending name order")]
        public void ThenThePerilssBResponseBodyShouldContainTopRecordsInDescendingNameOrder(int top)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;
            
            var deserializeResponse = JsonConvert.DeserializeObject<OdataListResponse<ExpectedPeril>>(jsonResponse);

            var list = deserializeResponse.Value.ToList();

            list = list.OrderByDescending(x => x.name).ToList();

            list.Count.ShouldBeEquivalentTo(top, $"expected list {deserializeResponse} but actual list is {list}");
        }

        [Then(@"the response body for peril should contain the following Data:")]
        public void ThenTheResponseBodyForperilShouldContainTheFollowingData(Table table)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            var deserializeResponse = JsonConvert.DeserializeObject<OdataListResponse<ExpectedPeril>>(jsonResponse);

            var actual = deserializeResponse.Value.First();
            foreach (var row in table.Rows)
            {
                var lookup = row["Field_Name"];
                var value = actual.GetType().GetProperty(lookup).GetValue(actual);
            }
        }

        [Then(@"the response should have (.*) records")]
        public void ThenTheResponseShouldHaveRecords(int expectedCount)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            var ODataResponse =
                JsonConvert.DeserializeObject<OdataListResponse<ExpectedPeril>>(jsonResponse);

            Debug.Assert(ODataResponse != null, "ODataResponse != null");
            var actualCount = ODataResponse.Value.Count();

            Assert.AreEqual(expectedCount, (int)actualCount, $"Expected count is {expectedCount} but actual response count is {actualCount}");
        }


        [Then(@"the peril response body should contain (.*)")]
        public void ThenThePerilResponseBodyShouldContain(string Id)
        {
            var response = ScenarioContext.Current.Get<IRestResponse>("response");

            var jsonResponse = response.Content;

            int StatusCode = (int)response.StatusCode;

            switch (StatusCode)
            {
                case 200:
                {
                    var ODataResponse = JsonConvert.DeserializeObject<ExpectedPeril>(jsonResponse);
                    ODataResponse.id.Should().Equals(Id);
                    break;

                }
                case 404:
                {
                    System.Console.WriteLine("404 status not found");
                    break;
                }
                case 400:
                {
                    System.Console.WriteLine("Bad Request");
                    break;
                }
                case 500:
                {
                    System.Console.WriteLine("Internal Server Error");
                    break;
                }
                default:
                {
                    System.Console.WriteLine($" {response.Content}");
                    break;
                }

            }


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