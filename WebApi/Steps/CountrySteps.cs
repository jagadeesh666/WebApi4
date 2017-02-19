using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace WebApi.Steps
{
    [Binding]
    public class CountrySteps
    {

        private List<Country> CountryList = new List<Country>
        {
            new Country {Name = "India", Area = 12345, Capital = "New Delhi", Continent = "Asia"},
            new Country {Name = "Pak", Area = 1234, Capital = "Karachi", Continent = "Asia"},
            new Country {Name = "SriLanka", Area = 2345, Capital = "Colombo ", Continent = "Asia"},
            new Country {Name = "Bangladesh", Area = 2555, Capital = "Dhakha", Continent = "Asia"},
            new Country {Name = "England", Area = 500, Capital = "London", Continent = "Europe"}

        };

        [Given(@"I have the Country data available")]
        public void GivenIHaveTheCountryDataAvailable()
        {
            ;
        }

        [Given(@"I have Enumerate the data")]
        public void GivenIHaveEnumerateTheData()
        {
            var countryListEnum = CountryList as IEnumerable<Country>;
            ScenarioContext.Current.Add("CountryListEnum", countryListEnum);
        }
        
        [Then(@"I should see the list belong to continent asia")]
        public void ThenIShouldSeeTheListBelongTocontinentAsia()
        {
           var asia = CountryList.Where(x => x.Continent == "Asia");
            Assert.IsNotNull(asia);
            Assert.AreNotEqual(0,asia.Count());
        }

        [Then(@"I should see the largest country as India")]
        public void ThenIShouldSeeTheLargestCountryAsIndia()
        {
           var maxArea =  CountryList.Max(x => x.Area);
            Assert.IsNotNull(maxArea);
           var countryLargest = CountryList.Find(x => x.Area == maxArea);
            Assert.AreEqual("India",countryLargest.Name);

            var list = CountryList.OrderByDescending(x => x.Area);
            Assert.AreEqual("India",list.First());
        }

        [Then(@"I should see the capital with more than one word")]
        public void ThenIShouldSeeTheCapitalWithMoreThanOneWord()
        {
            //var capital = CountryList.Find(x => x.Capital.Contains(" "));
            //Assert.AreEqual("New Delhi",capital.Capital);

            var regex = new Regex(@"\w\s\w");
            var capitalList = CountryList.Where(x => regex.IsMatch(x.Capital));

            Assert.IsNotNull(capitalList);
        }

        [Then(@"I should see the following fields:")]
        public void ThenIShouldSeeTheFollowingFields(Table table)
        {
            //var firstRow = CountryList.FindAll(x=>x.Name=="India");
            //table.CompareToSet(firstRow);

            var firstRow = CountryList.Find(x => x.Name == "India");
            table.CompareToInstance(firstRow);
        }

        [Then(@"I should see the following field names:")]
        public void ThenIShouldSeeTheFollowingFieldNames(Table table)
        {
            var item = CountryList.First();

            foreach (var row in table.Rows)
            {
                var fieldName = row["field_name"];
                Assert.IsNotNull(item.GetType().GetProperty(fieldName));

                //Assert.IsNotNull(CountryList.GetType().GetProperty(fieldName));
                //Assert.IsNotNull(countryListResponse.GetType().GetProperty(fieldName));
            }
        }

        [Then(@"I should see that data arranged in ascending order by country name")]
        public void ThenIShouldSeeThatDataArrangedInAscendingOrderByCountryName()
        {
            var countrylistEnum = CountryList as IEnumerable<Country>;
            var list = countrylistEnum.OrderBy(x => x.Name);

        }



    }
}
