Feature: Country
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@where
Scenario: country list
	Given I have the Country data available
	And I have Enumerate the data
	Then I should see the list belong to continent asia

@find
Scenario: country list max of area
	Given I have the Country data available
	Then I should see the largest country as India

@space&regex
Scenario: country capital more than one word
	Given I have the Country data available
	Then I should see the capital with more than one word

@comparetoinstance
Scenario: verify list of fields available in country
	Given I have the Country data available
	Then I should see the following fields:
	|Name		|Area	   |Capital  |Continent|
	|India	    |12345	   |New Delhi|Asia	   |

@foreachvar
Scenario: verify list of field properties available in country
	Given I have the Country data available
	Then I should see the following field names:
	|field_name	|
	|Name		|
	|Area		|
	|Capital	|
	|Continent	| 
	And I should see that data arranged in ascending order by country name

	Scenario Outline: verify that the country data is sorted by specific fields
	Given I have the Country data available
	Then I should see the country data sorted by field <fieldname>:	
	|field_name	|
	|Name		|
	|Area		|
	|Capital	|
	|Continent	|
	Examples:
	|fieldname	| 
	|Name		|
	|Area		|
	|Capital	|
	|Continent	|
