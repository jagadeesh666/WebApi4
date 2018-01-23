Feature: GetBubblo Info
	As a trusted Application or Platform Service 
	I'm able to get the info of Willis Bubblo s like heartbeat,version,metadate & swagger
	so that the behaviour of Bubblo  is documented in API service

Background: 
    Given I have STS accessToken

@14288-1
Scenario Outline: Retrieve version details of Bubblo  Reference Data
	Given I have the STS token with client <clientId> clientSecret <ClientSecret> and scope <scope>
	When I request a GET for version details
    Then I should see the following version details:
	| Field_Name | Value                                           |
	| name       | bublo LTD - Bubblo US technologies |
	| company    | bublo LTD                            |
	| copyright  | Copyright © bublo LTD 2017           |
	| version    | .                                               |
Examples:
| Scenario       |   clientId                | ClientSecret                | scope               | status |
| Valid Scope    |   ReferenceData_Client_ID | ReferenceData_Client_Secret | ReferenceData_Scope | 200    |

@14288-2
Scenario Outline: Get Swagger documentation details for user profile
	Given I have the STS token with client <clientId> clientSecret <ClientSecret> and scope <scope>
	When I request a swagger for Bubblo  services <Endpoint>
	Then the swagger document will contain version,title,schemes,host,scopes and authorizationUrl
Examples:
| Scenario    | Endpoint              | clientId                | ClientSecret                | scope               | status |
| Valid Scope | Bubblo /swagger/docs/v1 | ReferenceData_Client_ID | ReferenceData_Client_Secret | ReferenceData_Scope | 200    |
| Valid Scope | Bubblo /swagger/docs/v1 | ReferenceData_Client_ID | ReferenceData_Client_Secret | Document_Scope      | 200    |

@14288-3 
Scenario Outline: Health check Heartbeat
	Given I have the STS token with client <clientId> clientSecret <ClientSecret> and scope <scope>
	When I request a GET for health check heartbeat <Endpoint>
	Then the response code is <status>
	And the response should contain following heartbeat details:
	| Field_Name  | Value                                                               |
	| Name        | CFT HealthCheck                                                     |
	| Version     | 1.0.2.0                                                             |
	| ServiceName | bublo LTD - Bubblo US technologies - Bubblo  Service   |
	| Results     | Service is available                                                |	
Examples:
| Scenario       | Endpoint           | clientId                | ClientSecret                | scope               | status |
| Valid Scope    | Bubblo /hc/heartbeat | ReferenceData_Client_ID | ReferenceData_Client_Secret | healthcheck_Scope   | 200    |
| Invalid Scope  | Bubblo /hc/heartbeat | ReferenceData_Client_ID | ReferenceData_Client_Secret | ReferenceData_Scope | 200    |
| Document_Scope | Bubblo /hc/heartbeat | ReferenceData_Client_ID | ReferenceData_Client_Secret | Document_Scope      | 200    | 

@14288-4 
Scenario Outline: Health check monitor
	Given I have the STS token with client <clientId> clientSecret <ClientSecret> and scope <scope>
	When I request a GET for health check monitor <Endpoint>
	Then the response code is <status>
	And the response should contain following heartmonitor details:
	| Field_Name  | Value                                                             |
	| Name        | CFT HealthCheck                                                   |
	| Version     | 1.0.2.0                                                           |
	| ServiceName | bublo LTD - Bubblo US technologies - Bubblo  Service |
	And the response should contain following heartMonitor instances:
	| Type                     | NumberofInstances |
	| KeyVaultCheck            | 1                 |
	| SqlServerCheck           | 1                 |
	| ApplicationInsightsCheck | 1                 |
Examples:
| Scenario    | Endpoint         | clientId                | ClientSecret                | scope             | status |
| Valid Scope | Bubblo /hc/monitor | ReferenceData_Client_ID | ReferenceData_Client_Secret | healthcheck_Scope | 200    |

@14288-5 
Scenario Outline: meta-data for Bubblo  services
When I request a GET for metadata <Endpoint>
Then the response code is 200
Examples:
| Scenario    | Endpoint              |
| Valid Scope | Bubblo /Odata/$metadata |
