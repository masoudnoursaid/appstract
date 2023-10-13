@PurchaseCloudSimCard
Feature: Purchase Cloud SIM Card

As a user, I want to get available sim cards and prices and purchase them by any available payment method in system.
And user currency is "MYR"

    @WI8853
    Scenario: User proceeds to purchase sim card successfully
        Given user culture in Cloud SIM Card page is "en-US"
        When the purchase Cloud SIM Card component is initialized
        Then the country and carrier table loading spinner visibility is "true"
        And the delegate to get country and carriers of Cloud SIM Card is called
        And progress steps indicator in purchase Cloud SIM Card show as below
          | Title             | Status   |
          | Country & Carrier | Current  |
          | Mobile Numbers    | Upcoming |
          | Type & Period     | Upcoming |
          | Payment           | Upcoming |
        When the delegate to get available country and carriers of Cloud SIM Card answered with below data
          | CountryCode | Carrier | MonthlyFee | DomesticSmsFee | DomesticCallFee | NumberRange     | Currency |
          | MYS         | Maxis   | 100        | 10             | 10              | +60 1x-xxx xxxx | MYR      |
          | NLD         | KPN     | 90.6       | 9.06           | 9.06            | +31 6 xxxx xxxx | MYR      |
          | USA         | AT&T    | 135.9      | 13.59          | 13.59           | +1 xxx xxx xxxx | MYR      |
        Then the country and carrier table loading spinner visibility is "false"
        And user can view below country and carriers of available Cloud SIM Cards
          | Country       | Carrier | MonthlyFee | DomesticSmsFee | DomesticCallFee | NumberRange     |
          | Malaysia      | Maxis   | RM 100.00  | RM 10.00       | RM 10.00        | +60 1x-xxx xxxx |
          | Netherlands   | KPN     | RM 90.60   | RM 9.06        | RM 9.06         | +31 6 xxxx xxxx |
          | United States | AT&T    | RM 135.90  | RM 13.59       | RM 13.59        | +1 xxx xxx xxxx |
        When user check Original Currency checkbox of country and carrier page
        Then the country and carrier table loading spinner visibility is "true"
        When the delegate to get available country and carriers of Cloud SIM Card answered with below data
          | CountryCode | Carrier | MonthlyFee | DomesticSmsFee | DomesticCallFee | NumberRange     | Currency |
          | MYS         | Maxis   | 100        | 10             | 10              | +60 1x-xxx xxxx | MYR      |
          | NLD         | KPN     | 20         | 2              | 2               | +31 6 xxxx xxxx | EUR      |
          | USA         | AT&T    | 30         | 3              | 3               | +1 xxx xxx xxxx | USD      |
        Then the country and carrier table loading spinner visibility is "false"
        And user can view below country and carriers of available Cloud SIM Cards
          | Country       | Carrier | MonthlyFee | DomesticSmsFee | DomesticCallFee | NumberRange     |
          | Malaysia      | Maxis   | RM 100.00  | RM 10.00       | RM 10.00        | +60 1x-xxx xxxx |
          | Netherlands   | KPN     | € 20.00    | € 2.00         | € 2.00          | +31 6 xxxx xxxx |
          | United States | AT&T    | $ 30.00    | $ 3.00         | $ 3.00          | +1 xxx xxx xxxx |
        When user select the table row with "Netherlands" as country and "KPN" as carrier
        Then the Cloud SIM Card mobile numbers table loading spinner visibility is "true"
        And the Cloud SIM Card mobile numbers table filters is as below
          | key     | value       |
          | Country | Netherlands |
          | Carrier | KPN         |
        And progress steps indicator in purchase Cloud SIM Card show as below
          | Title             | Status    |
          | Country & Carrier | Completed |
          | Mobile Numbers    | Current   |
          | Type & Period     | Upcoming  |
          | Payment           | Upcoming  |
        When the delegate to get available mobile numbers of Cloud SIM Card answered with below data
          | CountryCode | Carrier | MobileNumber | SimSetupFee | PortSetupFee | MonthlyFee | DomesticSmsFee | DomesticCallFee | Currency |
          | NLD         | KPN     | +31688884444 | 50          | 0            | 5          | 0.05           | 0.05            | EUR      |
          | NLD         | KPN     | +31698761964 | 50          | 0            | 5          | 0.05           | 0.05            | EUR      |
          | NLD         | KPN     | +31697650942 | 50          | 0            | 5          | 0.05           | 0.05            | EUR      |
          | NLD         | KPN     | +31697650943 | 50          | 0            | 5          | 0.05           | 0.05            | EUR      |
          | NLD         | KPN     | +31697650944 | 50          | 0            | 5          | 0.05           | 0.05            | EUR      |
        Then the Cloud SIM Card mobile numbers table loading spinner visibility is "false"
        And user can view below available mobile numbers of Cloud SIM Card as below
          | Country     | Carrier | MobileNumber | SimSetupFee | MonthlyFee | DomesticSmsFee | DomesticCallFee |
          | Netherlands | KPN     | +31688884444 | € 50.00     | € 5.00     | € 0.05         | € 0.05          |
          | Netherlands | KPN     | +31698761964 | € 50.00     | € 5.00     | € 0.05         | € 0.05          |
          | Netherlands | KPN     | +31697650942 | € 50.00     | € 5.00     | € 0.05         | € 0.05          |
          | Netherlands | KPN     | +31697650943 | € 50.00     | € 5.00     | € 0.05         | € 0.05          |
          | Netherlands | KPN     | +31697650944 | € 50.00     | € 5.00     | € 0.05         | € 0.05          |
        And the next button of Cloud SIM Card mobile numbers page enable is "false"
        When user select the Cloud SIM Card mobile numbers table row with "+31697650942" as mobile number
        And user select the Cloud SIM Card mobile numbers table row with "+31697650943" as mobile number
        Then the next button of Cloud SIM Card mobile numbers page enable is "true"
        And below numbers exists in drop down of the next button of Cloud SIM Card mobile numbers page
          | MobileNumber |
          | +31697650942 |
          | +31697650943 |
        When user click the next button of Cloud SIM Card mobile numbers page
        Then delegate to get cloud sim card port price info is called
        When delegate to get cloud sim card port price info response as below
          | PortSetupFee | Currency |
          | 50.00        | EUR      |
        Then user can view price info of selected Cloud SIM Cards as below
          | MobileNumber | PortType       | PriceInfo                                                   |
          | +31697650942 | Dedicated Port | Only 12 Months x € 5.00 + € 50.00 + € 0.00 = € 110.00 Total |
          | +31697650943 | Dedicated Port | Only 12 Months x € 5.00 + € 50.00 + € 0.00 = € 110.00 Total |
        And total checkout in cloud sim card port type page is "€ 220.00"
        When user click on Payment button in Cloud SIM Card port type page
        Then the Cloud SIM Card payment page loading spinner visibility is "true"
        And delegate to get user wallet info in Cloud SIM Card payment page is called
        When delegate to get user wallet info in Cloud SIM Card payment page response as below
          | Discount |
          | 0.10     |
        Then the Cloud SIM Card payment page loading spinner visibility is "false"
        And user in Cloud SIM Card payment page can see payment detail info as below
          | Key      | Value                         |
          | Discount | 10.00% = € 22.00              |
          | Total    | € 220.00 - € 22.00 = € 198.00 |
