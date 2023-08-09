Feature: Sdk Generator
Developer generates the SDK for backend API`s and compare it with the existing SDK

    Scenario: Generate SDK for backend APIs
        When the mobile SDK is generated
        Then the generated SDK source should be equal to content of "../../../../ClientSdk/ClientSdk.Customer.Mobile/CustomerMobileV1Client.g.cs"
        When the web SDK is generated
        Then the generated SDK source should be equal to content of "../../../../ClientSdk/ClientSdk.Customer.Web/CustomerWebV1Client.g.cs"