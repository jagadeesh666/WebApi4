@smoketest
Feature: SmokeTest
	As a trusted Application or Platform Service 
	I'm able to get the list of bubblo services
	so that I can consume it in my application

@smoketest
Scenario Outline: Service should be restricted access to Client information by the scope provided in an authenticated request
Given I have STS token with client <clientId> clientSecret <ClientSecret> and scope <scope>
When I request a list of Bubblo s <Endpoint>
Then the response status code should be <status>
Examples:
| Scenario       | Endpoint               | clientId                       | ClientSecret                | scope               | status |
| Valid Scope    | Bubblo /Odata/Bubblo s | ReferenceData_Client_Bubblo ID | ReferenceData_Client_Secret | ReferenceData_Scope | 200    |
| Invalid Scope  | Bubblo /Odata/Bubblo s | ReferenceData_Client_ID        | ReferenceData_Client_Secret | Subscription_Scope  | 403    |
| No_Scope       | Bubblo /Odata/Bubblo s | ReferenceData_Client_ID        | ReferenceData_Client_Secret | No_Scope            | 401    |
| Document_Scope | Bubblo /Odata/Bubblo s | ReferenceData_Client_ID        | ReferenceData_Client_Secret | Document_Scope      | 403    |
| Valid Scope    | Bubblo /Odata/Bubblo s | ReferenceData_Client_ID        | ReferenceData_Client_Secret | ReferenceData_Scope | 200    |
| Invalid Scope  | Bubblo /Odata/Bubblo s | ReferenceData_Client_ID        | ReferenceData_Client_Secret | Subscription_Scope  | 403    |
| No_Scope       | Bubblo /Odata/Bubblo s | ReferenceData_Client_ID        | ReferenceData_Client_Secret | No_Scope            | 401    |
| Document_Scope | Bubblo /Odata/Bubblo s | ReferenceData_Client_ID        | ReferenceData_Client_Secret | Document_Scope      | 403    |