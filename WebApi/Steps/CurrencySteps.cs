using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WebApi.Helpers;

namespace WebApi
{
    [Binding]
    public class CurrencySteps
    {
        public const string Url_json = "https://teamtreehouse.com/matthew.json";

        [Given(@"I get request to xeuri")]
        public void GivenIGetRequestToXeuri()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Url_json);
            var request = new RestRequest();
            var response = client.Execute(request);
            var statuscode = (int) response.StatusCode;

            ScenarioContext.Current.Add("responseStatus", statuscode);

            var json = response.Content;
            ScenarioContext.Current.Add("json", json);
        }

        [Given(@"I see the response code (.*)")]
        public void GivenISeeTheResponseCode(int p0)
        {
            var statuscode = ScenarioContext.Current.Get<int>("responseStatus");
            Assert.AreEqual(p0, statuscode);
        }

        [Then(@"I see the followng felds")]
        public void GivenISeeTheFollowngFelds(Table table)
        {
            var countryListResponse = ScenarioContext.Current.Get<Matthew>("CountryList");

            //Assert.AreEqual("matthew", countryListResponse.Profile_Name);
            // Assert.IsTrue(countryListResponse.Badges.Exists(x => x.Name == "Website Basics"));

            //Assert.AreEqual("matthew", countryListResponse.GetType().GetProperty("Profile_Name").GetValue(countryListResponse));

            Assert.IsNotNull(countryListResponse.GetType().GetProperty("Name"));
            Assert.IsNotNull(countryListResponse.GetType().GetProperty("Profile_Name"));
            Assert.IsNotNull(countryListResponse.GetType().GetProperty("Profile_Url"));

            foreach (var row in table.Rows)
            {
                var fieldName = row["field_name"];
                Assert.IsNotNull(countryListResponse.GetType().GetProperty(fieldName));
            }
        }

        [Given(@"I see that json content is deserialised")]
        [When(@"I see that json content is deserialised")]
        public void GivenISeeThatJsonContentIsDeserialised()
        {
            DeserializeResponse();
        }

        private static void DeserializeResponse()
        {
            var json = ScenarioContext.Current.Get<string>("json");

            var countryList = JsonConvert.DeserializeObject<Matthew>(json);

            ScenarioContext.Current.Add("CountryList", countryList);

            //Assert.AreEqual("matthew", countryList.Profile_Name);
        }

        [Then(@"I see the following values")]
        public void ThenISeeTheFollowingValues(Table table)
        {
            var countryListResponse = ScenarioContext.Current.Get<Matthew>("CountryList");

            foreach (var row in table.Rows)
            {
                var fieldName = row["field_name"];
                var fieldValue = row["field_value"];

                Assert.AreEqual(fieldValue,
                    countryListResponse.GetType().GetProperty(fieldName).GetValue(countryListResponse));
            }
        }

        [Then(@"Matthew should have the following values")]
        public void ThenCountryShouldHaveTheFollowingValues(Table table)
        {
            var actualCountry = ScenarioContext.Current.Get<Matthew>("CountryList");

            table.CompareToInstance(actualCountry);
        }

        [Then(@"I see that they are in ascending order")]
        public void ThenISeeThatTheyAreInAscendingOrder()
        {
            var countryListResponse = ScenarioContext.Current.Get<Matthew>("CountryList");

            var badgeResponse = countryListResponse.GetType().GetProperty("Badges").GetValue(countryListResponse);
            Assert.IsNotNull(badgeResponse);

            var badgeListUnsorted = badgeResponse as IEnumerable<Badge>;
            Assert.IsNotNull(badgeListUnsorted);

            IEnumerable<Badge> badgeListSorted = badgeListUnsorted.OrderBy(x => x.Id).ToList();

            Assert.IsNotNull(badgeListSorted);

            Assert.AreEqual(badgeListSorted, badgeListUnsorted, "Response is not sorted");

            //var item = badgeListUnsorted.Where(x => x.Name == "Website Basics").First();

            //Assert.AreEqual(badgeListSorted, badgeListUnsorted);
        }

        [When(@"I search for field name (.*)")]
        public void WhenISearchForFieldName(string p0)
        {
            var countryListResponse = ScenarioContext.Current.Get<Matthew>("CountryList");
            var badgeResponse = countryListResponse.GetType().GetProperty("Badges").GetValue(countryListResponse);
            Assert.IsNotNull(badgeResponse);

            var badgeListUnsorted = badgeResponse as IEnumerable<Badge>;
            Assert.IsNotNull(badgeListUnsorted);

            var item = badgeListUnsorted.First(x => x.Name == "Website Basics");

            ScenarioContext.Current.Add("BadgeForWebsiteBasics", item);
        }

        [Then(@"I should see the elements related to list")]
        public void ThenIShouldSeeTheElementsRelatedToList()
        {
            var badgeForWebsiteBasics = ScenarioContext.Current.Get<Badge>("BadgeForWebsiteBasics");

            Assert.IsNotNull(badgeForWebsiteBasics, "Cannot find any matching Badge");
        }
    }
}