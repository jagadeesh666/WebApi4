Feature: currency
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given I get request to xeuri
	And I see the response code 200
	When I see that json content is deserialised
	Then I see the followng felds
	| field_name			|  
	|Profile_Name     |
	#| Name			| 
	#| Profile_Name	|
	| Profile_Url   | 
	| Gravatar_Url  | 
	| Gravatar_Hash |
	And I see the following values
	| field_name	|field_value |       
	| Name			|Matthew Kosloski |
	| Profile_Name	|matthew|
	| Profile_Url   | https://teamtreehouse.com/matthew|
	| Gravatar_Url  |https://uploads.teamtreehouse.com/production/profile-photos/118878/avatar_badlands.jpg |
	| Gravatar_Hash |5f99592344173c9f2dce0beb8821c3d8|
	
Scenario: See Elements are sorted
	Given I get request to xeuri
	When I see that json content is deserialised
	Then I see that they are in ascending order
	
Scenario: See Elements related to Particular values
	Given I get request to xeuri
	And I see that json content is deserialised
	When I search for field name <"Website Basics">
	Then I should see the elements related to list
	
Scenario: Check all values
	Given I get request to xeuri
	And I see the response code 200
	When I see that json content is deserialised
	Then Matthew should have the following values
	| field	|Value |       
	| Name			|Matthew Kosloski |
	| Profile_Name	|matthew|
	| Profile_Url   | https://teamtreehouse.com/matthew|
	| Gravatar_Url  |https://uploads.teamtreehouse.com/production/profile-photos/118878/avatar_badlands.jpg |
	| Gravatar_Hash |5f99592344173c9f2dce0beb8821c3d8|
	

