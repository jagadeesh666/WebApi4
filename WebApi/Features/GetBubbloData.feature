Feature: GetbubloData
	As a trusted Application or Platform Service 
	I'm able to get the list of google bublos
	so that I can consume it in my application

Background: 
    Given I have STS accessToken

@14288-1
Scenario Outline: Fetch list of bublo 
	When I request a list of bublos <Endpoint>
	Then the response code is 200
	And the response body for bublo should contain the following Data:
	| Field_Name     |
	| id             |
	| shortCode      |
	| name           |
	| lastUpdateTime |

Examples:
| Scenario      | Endpoint           |
| List of bublo | bublo/Odata/bublos |

@14288-2
Scenario Outline: I am able to expand using bublo description attribute
When I request a GET to list of bublos <Endpoint> by expand=description
Then the response code is 200
And the bublo Domain list should have expanded
Examples:
| Scenario       | Endpoint                               |
| List of bublos | bublo/Odata/bublos?$expand=description |

@14288-3
Scenario Outline: Get bublo service for Count using skip,top   
	When I retrieve Endpoints <Endpoint> with skip and top using count
	Then the response body should have list of count value 10
Examples:
| Scenario       | Endpoint                                       |
| List of bublos | bublo/Odata/bublos?$skip=0&$top=10&$count=true |

@14288-4
Scenario Outline: Get bublo services by ID number
	When I retrieve Endpoints <Endpoint> by Id 
	Then the response code is <status>
	And the bublo response body should contain <Id> 
Examples:
| Scenario      | Endpoint                  | status | Id    |
| List of bublo | bublo/Odata/bublos(10001) | 200    | 10001 |

@14288-5
Scenario Outline: Get bublo services by ID with space
	When I retrieve Endpoints <Endpoint> by Id 
	Then the response code is <status>
Examples:
| Scenario      | Endpoint             | status | Id |
| List of bublo | bublo/Odata/bublos() | 200    |    |

@14288-6 @pagination
Scenario Outline: Verify Pagination for bublo Services  
	When I request a list of bublos <Endpoint>
	Then the response code is 200
	And the response should have 100 records
	And the response body should have Next link
Examples:
| Scenario      | Endpoint                       |
| List of bublo | bublo/Odata/bublos?$count=true |

@14288-7
Scenario Outline: Retrieve N number of records ordered by Name descending
	When I request a list of bublos <Endpoint> ordered by name descending
	Then the bublos response body should contain top 10 records in descending name order
Examples:
| Scenario      | Endpoint                                       |
| List of bublo | bublo/Odata/bublos?&$top=10&$orderby=name desc | 

@14288-8
Scenario Outline: Fetch bublo services with no token      
Given I do not supply a token
When I request a list of bublos <Endpoint>
Then the response code is 401
Examples:
| Scenario      | Endpoint           |
| List of bublo | bublo/Odata/bublos |

@14288-9
Scenario Outline: Fetch bublo services with tampered token      
Given I have the tampered STS token
When I request a list of bublos <Endpoint>
Then the response code is 401
Examples:
| List of bublos | Endpoint                         |
| List of bublo  | bublo/Odata/bublos?$expand=Lines |

@14288-10
Scenario Outline: Service should be restricted access to Client information by the scope provided in an authenticated request
Given I have the STS token with client <clientId> clientSecret <ClientSecret> and scope <scope>
When I request a list of bublos <Endpoint>
Then the response status code should be <status>
Examples:
| Scenario       | Endpoint           | clientId                | ClientSecret                | scope               | status |
| Valid Scope    | bublo/Odata/bublos | ReferenceData_Client_ID | ReferenceData_Client_Secret | ReferenceData_Scope | 200    |
| Invalid Scope  | bublo/Odata/bublos | ReferenceData_Client_ID | ReferenceData_Client_Secret | Subscription_Scope  | 403    |
| No_Scope       | bublo/Odata/bublos | ReferenceData_Client_ID | ReferenceData_Client_Secret | No_Scope            | 401    |
| Document_Scope | bublo/Odata/bublos | ReferenceData_Client_ID | ReferenceData_Client_Secret | Document_Scope      | 403    |
| Valid Scope    | bublo/Odata/bublos | ReferenceData_Client_ID | ReferenceData_Client_Secret | ReferenceData_Scope | 200    |
| Invalid Scope  | bublo/Odata/bublos | ReferenceData_Client_ID | ReferenceData_Client_Secret | Subscription_Scope  | 403    |
| No_Scope       | bublo/Odata/bublos | ReferenceData_Client_ID | ReferenceData_Client_Secret | No_Scope            | 401    |
| Document_Scope | bublo/Odata/bublos | ReferenceData_Client_ID | ReferenceData_Client_Secret | Document_Scope      | 403    |
