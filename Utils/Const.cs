namespace AlphaPoint_QA.Utils
{
    public static class Const
    {
        /// <summary>
        /// Config Files here
        /// </summary>

        public const string ConfigFileName = "appsettings.json";

        public const string TestDataFileName = "sharedsettings.json";

        public const string LogFileName = "log4net.config";

        /// <summary>
        /// Users defined here.
        /// </summary>

        //dhirender
        public const string USER1 = "User1";

        //magic1
        public const string USER2 = "User2";

        //magic2
        public const string USER3 = "User3";

        //magic3
        public const string USER4 = "User4";

        //User_1
        public const string USER5 = "User5";

        //User_2
        public const string USER6 = "User6";

        //magic4
        public const string USER7 = "User7";

        //User_3
        public const string USER8 = "User8";

        //User_4
        public const string USER9 = "User9";

        //User_5
        public const string USER10 = "User10";

        //User_6
        public const string USER11 = "User11";

        //User_7
        public const string USER12 = "User12";

        //User_8
        public const string USER13 = "User13";

        //Admin-dhirender
        public const string ADMIN1 = "Admin";

        /// <summary>
        /// API Key Constant
        /// </summary>

        public const string APIPermissions = "Permissions";

        public const string APIKey = "Key";

        public const string APISecret = "Secret";

        public const string APIDeleteButton = "Delete";

        public const string StopMarket = "StopMarket";

        public const string Limit = "Limit";

        public const string Market = "Market";

        public const string TrailingStopMarket = "TrailingStopMarket";

        public const string CancelledStatus = "Canceled";

        /// <summary>
        /// Assertion statements defined here. (Success MSG)
        /// </summary>
        public const string WithdrawSuccessMsg = "Your withdraw has been successfully added";

        public const string TransferSuccessMsg = "Transfer succeeded";

        public const string RequestTransferSuccessMsg = "Request transfer succeeded";
        
        public const string CopyAddressSuccessMsg = "The address has been copied to the clipboard.";

        public const string OrderSuccessMsg = "Your order has been successfully added";

        public const string OrderCancelledMsg = "Your Order has been Canceled";

        public const string AffiliateProgramSuccessMsg = "Affiliate Program verified successfully";

        public const string AffiliateProgramFailureMsg = "Affiliate Program verification failed";

        public const string APIKeyCreatedSuccessMsg = "API Key created and verified successfully";

        public const string APIKeyCreationFailureMsg = "Unable to create API Key";

        public const string ConfirmationModalFailureMsg = "Confirmation Modal is not displayed";

        public const string CreateAPIKeyBtnIsPresent = "API Key Button is present";

        public const string ClickedOnAPIKeyButton = "Clicked on API Key Button successfully";

        public const string APIKeyButtonDisabled = "Unable to click on API Key Button";

        public const string APIKeyCheckboxesArePresent = "Checkboxes[Trading, deposits and withdrawls] are present";

        public const string SelectedAPIKeyCheckboxes = "Checkboxes[Trading, deposits and withdrawls] are selected";

        public const string APIKeyConfirmationModalIsPresent = "Confirmation Modal is displayed with values []";

        public const string APIKeyCheckboxesAreNotPresent = "Checkboxes[Trading, deposits and withdraws] are not present";

        public const string APIKeyAddedIsPresentInTheList = "API key added is present in the Existing API List";

        public const string VerifiedSelectedPermissions = "Successfully verified the permissions added to the API Key";

        public const string APIKeyAddedIsNotPresentInTheList = "API key added is not present in the Existing API List";

        public const string SecretKeyVerificationFailed = "Verification failed: Secret Key is displayed in the API Keys List";

        public const string SecretKeyVerificationPassed = "Verification passed: Secret Key is not displayed in the API Keys List";

        public const string DeleteAPIKeySuccessMsg = "API Key deleted and verified successfully";

        public const string DeleteAPIKeyFailureMsg = "Verification Failed: Unable to delete API Key";

        public const string DeleteAPIKeyIsPresent = "Verification Passed: Delete button is present";

        public const string DeleteAPIKeyIsNotPresent = "Verification Passed: Delete button is present"; 

        public const string IOCOrderTypeSuccessMsg = "Verfiy Place By Order with Immediate Or Cancel Order Type passed successfully";

        public const string LimitBuyOrderFailureMsg = "Limit buy order Failed";

        public const string MarketSellOrderFailureMsg = "Market Sell Order Failed";

        /// <summary>
        /// Assertion statements defined here. (Failed MSG)
        /// </summary>

        public const string IOCOrderTypeFailedMsg = "Verfiy Place Buy Order with Immediate Or Cancel Order Type failed";

        public const string AskPrice = "Ask Price";

        public const string CreateAPIKeyFailed = "API Key creation test failed";

        public const string UserLoginFailed = "User Login failed";

        public const string UserLogoutFailed = "User logout failed";

        public const string AmountPersistenceFailureMsg = "Market amount persistence test failed";

        public const string MarketOrderVerifiedInFilledOrders = "Market Order successfully verified in Filled Orders tab";

        public const string TC36_SendExternalWallets = "Send External Wallets Test Failed.";

        public const string TC37_WalletsSendToEmailAddressTestFailed = "Wallets Send To Email Address Test Failed.";

        public const string TC39_WalletsSendToEmailAddress = "Wallets Receive Request By Email Test Failed.";

        public const string StatusIdNotFound = "Status Id Not Found in Ticket-> Withdraw Page.";
    }
}
