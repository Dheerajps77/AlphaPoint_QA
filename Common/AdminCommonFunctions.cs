using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Common
{
    public class AdminCommonFunctions
    {
        //ILog logger;
        Config data;
        public IWebDriver driver;
        string username;
        string password;
        private ProgressLogger logger;


        public AdminCommonFunctions(ProgressLogger logger)
        {

            this.logger = logger;
            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
        }

        //Below locators is used to fetch all the row details
        By elementList = By.XPath("//div[@class='ReactVirtualized__Table__row']");

        //Admin Logout window section locators
        By exchangesAdminButton = By.XPath("//span[@id='OpenExchangesAdmin']");
        By operatorAdminButton = By.XPath("//span[@id='OpenOperatorAdmin']");
        By aMAdminButton = By.XPath("//span[@id='OpenAMAdmin']");
        By oMSAdminButton = By.XPath("//span[@id='OpenOMSAdmin']");
        By openUsersMenuButton = By.XPath("//button[@id='OpenUserMenu']");

        //Locators for Admin-->OMS Admin
        By openCreateOMSButton = By.XPath("//button[@id='OpenCreateOMSForm']");
        By oMSAdiminstrationList = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']//div[@aria-label='row']");
        By apexQa20msRowButtonLink = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']//div[@aria-label='row']//div[text()='APEXQA2OMS']");
        By oMSNameTextField = By.XPath("//input[@id='OMSName']");
        By selectMarginMarketTypbutton = By.XPath("//select[@id='MarginMarketType']");
        By permissionMarketDataCheckboxButton = By.XPath("//input[@name='PermissionedMarketData' and @type='checkbox']");
        By assetManagerCoreUserTextField = By.XPath("//input[@id='AssetManagerCoreUser']");
        By assetManagerCorePasswordTextField = By.XPath("//input[@id='AssetManagerCorePassword']");
        By assetManagerIdTextField = By.XPath("//input[@id='AssetManagerId']");
        By submitOMSFormButton = By.XPath("//button[@id='SubmitOMSForm']");
        By editOMSWindow = By.XPath("//header[@class='modal-header']//p[text()='Edit OMS']//following::a");
        By updateLoyalityFeeConfigButton = By.XPath("//button[@id='OpenLoyaltyFeeConfig']");
        By loyaltyDiscountTextField = By.XPath("//input[@id='LoyaltyDiscount']");
        By selectReferenceProductIdLink = By.XPath("//select[@id='ReferenceProductId']");
        By referenceProductPriceTextField = By.XPath("//input[@id='ReferenceProductPrice']");
        By isEnabledButton = By.XPath("//input[@id='IsEnabled']");
        By deleteLoyaltyFeeConfigButton = By.XPath("//button[@id='DeleteLoyaltyFeeConfig']");
        By updateLoyaltyFeeConfigButton = By.XPath("//button[@id='UpdateLoyaltyFeeConfigForm']");
        By configureLoyaltyTokenWindow = By.XPath("//header[@class='modal-header']//p[text()='Configure Loyalty Token']//following::a");

        //locators for Admin-->Users Menu
        By usersTab = By.XPath("//a[@id='SelectTab0']");
        By usersMenuLink = By.XPath("//a[@href='#/users']");
        By selectUser = By.XPath("//div[@title='magic3']");
        By openAddUserPermissionButton = By.XPath("//button[@id='OpenAddPermissionForm']");
        By userPermissionList = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']//div[@class='ReactVirtualized__Table__row']");
        By refreshUsersTableButton = By.XPath("//button[@id='RefreshUsersTable']");
        By openUserByIdTextField = By.XPath("//input[@id='OpenUserByIdInput']");
        By openUserByIdButton = By.XPath("//button[@id='OpenUserById']");
        By OpenAddNewUserButton = By.XPath("//button[@id='OpenAddNewUserForm']");
        By newUserNameTextField = By.XPath("//input[@id='UserName']");
        By newUserEmailTextField = By.XPath("//input[@id='Email']");
        By newUserPasswordTextField = By.XPath("//input[@id='passwordHash']");
        By newUserConfirmPasswordTextField = By.XPath("//input[@id='passwordHashConfirmation']");
        By newUserSubmitButton = By.XPath("//button[@id='SubmitUserForm']");
        By searchTextBox = By.XPath("//input[@placeholder='Search...']");
        By submitBlockTradeCheckboxButton = By.XPath("//div[@class='form-group']//label[text()='SubmitBlockTrade']//following::input[@type='checkbox' and @name='SubmitBlockTrade']");
        By getOpenTradeReportsCheckboxButton = By.XPath("//div[@class='form-group']//label[text()='GetOpenTradeReports']//following::input[@type='checkbox' and @name='GetOpenTradeReports']");
        By permissionAddSuccessMessage = By.XPath("//span[text()='User permission added successfully']");
        By permissionRevokedSuccessMessage = By.XPath("//span[text()='User permission revoked successfully']");
        By userNameAccountButton = By.XPath("//section[@class='secondary_container']//p[text()='User Accounts']//following::table[1]//thead[1]//tr[1]//following::tbody[1]//tr[1]//td[1]");
        By showMoreLink = By.XPath("//span[text()='+ Show more' and @class='listToggle']");
        By showLessLink = By.XPath("//span[text()='– Show less' and @class='listToggle']");
        By closeUserPermissionWindowSection = By.XPath("//div[@class='modal-inner']//p[@class='title']//following::a[@id='CloseModal']");
        By openCreateUserAffiliateTagButton = By.XPath("//button[@id='OpenCreateUserAffiliateTag']");
        By affiliateTagTextField = By.XPath("//input[@id='AffiliateTag' and @type='text']");
        By submitAffiliateTagButton = By.XPath("//button[@id='SubmitAffiliateTagForm' and @type='button']");
        By affiliateWindowSection = By.XPath("//p[text()='Create affiliate tag']//following::a");
        By addAPIKeyButton = By.XPath("//button[@id='OpenAPIKeysForm']");
        By addMarketDataPermissionButton = By.XPath("//button[@id='OpenMarketDataPermissionsForm']");
        By selectInstrumentForMarketData = By.XPath("//select[@id='InstrumentId']");
        By selectPermissionForMarketData = By.XPath("//select[@id='permissions']");
        By submitMarketDataPermissionsButton = By.XPath("//button[@id='SubmitMarketDataPermissionsForm']");
        By editUserInformationLink = By.XPath("//p[text()='User Details']//following::a[@id='OpenEditUserForm']");
        By usernnme = By.XPath("//input[@id='UserName']");
        By userEmail = By.XPath("//input[@id='Email']");
        By emailVerifyCheckBoxButton = By.XPath("//input[@id='EmailVerified']");
        By selectAccountDropwdownValue = By.XPath("//select[@id='AccountId']");
        By use2FACheckboxButton = By.XPath("//input[@id='Use2FA']");
        By submitUserDetailsButton = By.XPath("//button[@id='SubmitUserDetailsForm']");
        By closeEditUserInformationWindow = By.XPath("//header[@class='modal-header']//p[text()='Edit user information']//following::a");
        By passwordResetEmailLink = By.XPath("//a[@id='SendPasswordResetEmail']");
        By openAssignAccountButton = By.XPath("//button[@id='OpenAssignAccountForm']");
        By accountIDTextField = By.XPath("//input[@name='AccountId']");
        By submitAssignAccountToUserButton = By.XPath("//button[@id='SubmitAssignAccountToUserForm']");
        By openUnassignAccountButton = By.XPath("//button[@id='OpenUnassignAccountForm']");
        By assignAccountWindow = By.XPath("//header[@class='modal-header']//p[contains(text(),'Assign account')]//following::a");        
        By usersListInContainer = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']");
        By singleRowList = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div");
        By singleUserdetials = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div/div");
        By badgeExitCreationMsg = By.XPath("//span[text()='Badge already exists']");
        By badgeExitOnAontherAccountMsg = By.XPath("//div[@class='mm-popup']//following::div[3]/span");
        By closeAddNewBadgeWindow = By.XPath("//p[contains(text(),'Add new badge to Account')]//following::a");
        By userByID = By.XPath("//input[@name='OpenUserByIdInput']");
        By openUserByButton = By.XPath("//button[@id='OpenUserById']");

        //Locators for Admin-->Accounts
        By accountsTabMenuLink = By.XPath("//a[@href='#/accounts']");
        By accountsTab = By.XPath("//a[@id='SelectTab0']");
        By accountBalancesTab = By.XPath("//a[@id='SelectTab1']");
        By refreshAccountsTableButton = By.XPath("//button[@id='RefreshAccountsTable']");
        By refreshAccountsBalancesTableButton = By.XPath("//button[@id='RefreshAccountsBalancesTable']");
        By openEditAccountInformationLink = By.XPath("//p[text()='Account Details']//following::a[@id='OpenEditAccountInformation']");
        By accountNameTextField = By.XPath("//input[@name='AccountName']");
        By selectAccountType = By.XPath("//select[@name='AccountType']");
        By selectRiskType = By.XPath("//select[@name='RiskType']");
        By selectVerificationLevel = By.XPath("//select[@name='VerificationLevel']");
        By loyaltyFeesEnabledCheckboxButton = By.XPath("//input[@name='LoyaltyEnabled' and @type='checkbox']");
        By saveAccountButton = By.XPath("//button[@id='SaveAccountForm']");
        By accountEditInformationWindow = By.XPath("//header[@class='modal-header']//p[text()='Edit account information']//following::a");
        By addconfigurationButton = By.XPath("//button[@id='OpenAddAccountSettings']");
        By keyTextField = By.XPath("//input[@id='Key']");
        By valueTextField = By.XPath("//input[@id='Value']");
        By saveAddAccountButton = By.XPath("//button[@id='SaveAccountSettings']");
        By addAccountConfigWindow = By.XPath("//p[text()='Add account configuration']//following::a");
        By accountList = By.XPath("//div[@class='ReactVirtualized__Table__row']");
        By singleUserAccountLink = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']//div[@aria-label='row']//div[@title='ksta1']");
        By openAddNewBadgeButton = By.XPath("//button[@id='OpenAddNewBadge']");
        By submitCreateAccountBadgeButton = By.XPath("//button[@id='SubmitCreateAccountBadge']");
        By badgeTextField = By.XPath("//input[@id='Badge']");
        By badgeAccountList = By.XPath("//ul[@class='account-badges-list']");



        By openAccountByID = By.XPath("//input[@id='OpenAccountByIdInput']");
        By openAccountButton = By.XPath("//button[@id='OpenAccountById']");

        //locators for Admin-->Trades Menu
        By tradeMenuLink = By.XPath("//a[@href='#/trades']");
        By counterPartyID = By.XPath("//label[text()='Counterparty:']");
        By blockTradesButton = By.XPath("//span[text()='Block Trades']");
        //By blockTradeTab = By.XPath("//span[text()='Block Trades']");
        By showSearchButton = By.XPath("//span[text()='Show Search']");
        By hideSearchButton = By.XPath("//span[text()='Hide Search']");
        By accountIdTextField = By.XPath("//input[@name='AccountId']");
        By userIdTextField = By.XPath("//input[@name='UserId']");
        By tradeIdTextField = By.XPath("//input[@name='TradeId']");
        By executionIdTextField = By.XPath("//input[@name='ExecutionId']");
        By searchTradesButton = By.XPath("//button[@id='SearchTrades']");
        By refreshTradeReportsTableButton = By.XPath("//button[@id='RefreshTradeReportsTable']");
        By selectOrders = By.XPath("//select[@name='Depth']");
        By selectInstrument = By.XPath("//select[@name='InstrumentId']");          
        By closeOrderHistoryModalWindow = By.XPath("//button[@id='CloseOrderHistoryModal']");
        By selectAnInstrumentInBlocTradeTab = By.XPath("//select[@id='InstrumentId']");
        
        //locators for Admin-->OMS Orders
        By omsOrdersMenuLink = By.XPath("//a[@href='#/orders']");
        By omsOpenOrdersTab = By.XPath("//span[text()='OMS Open Orders']");
        By omsOrdersHistoryTab = By.XPath("//span[text()='OMS Orders History']");
        By buySideBookWindow = By.XPath("//section[@class='secondary_container half_container']//p[text()='Buy side book' and @class='title']");
        By sellSideBookWindow = By.XPath("//section[@class='secondary_container half_container']//p[text()='Sell side book' and @class='title']");
        By buySideBookListOrderId = By.XPath("//section[@class='secondary_container half_container']//p[text()='Buy side book' and @class='title']//following::div[@aria-label='row']//div[1]");
        By executeOrderButton = By.XPath("//button[@id='ExecuteOrder']");
        By cancelOrderButton = By.XPath("//button[@id='CancelOrder']");
        By abortActionButtonForOrder = By.XPath("//button[contains(@class,'mm-popup__btn mm-popup__btn--mm-popup__btn') and text()='Abort action']");
        By cancelOrderButtonForOrder = By.XPath("//button[contains(@class,'mm-popup__btn mm-popup__btn--mm-popup__btn') and text()='Cancel order']");

        //locators for Admin-->Tickets
        By ticketsMenuLink = By.XPath("//a[@href='#/tickets']");
        By showTicketsSearchButton = By.XPath("//span[text()='Show Search']");
        By withdrawsTab = By.XPath("//a[@id='SelectTab0']");
        By depositsTab = By.XPath("//a[@id='SelectTab1']");
        By assetManagerWalletTab = By.XPath("//a[@id='SelectTab2']");
        //we can use below locators for for deposits and for asset manager wallet Tab
        By refreshTicketsTableButton = By.XPath("//button[@id='RefreshTicketsTable']");
        //we can use below locators for for deposits and for asset manager wallet Tab to fetch the list
        By withdrawTicketWindow = By.XPath("//p[contains(text(),'Withdraw Ticket')]//following::a[@id='CloseModal']");
        By depositsWindow = By.XPath("//p[contains(text(),'Deposit Ticket')]//following::a[@id='CloseModal']");
        By ticketID = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']//div//div[@title='346']");
        By depositeID = By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']//div//div[@title='523']");

        By acceptDepositeTicketButton = By.XPath("//button[@id='AcceptTicket']");
        By declineDepositeTicketButton = By.XPath("//button[@id='DeclineTicket']");
        By pendingDepositeTicketButton = By.XPath("//button[@id='PendingTicket']");

        //locators for Admin-->Users Verifications
        By userVerificationMenuLink = By.XPath("//a[@href='#/usersVerification']");
        By usersVerificationTab = By.XPath("//a[@id='SelectTab0']//span[text()='Users Verification']");        
        By refreshUsersVerificationTableButton = By.XPath("//button[@id='RefreshUsersVerificationTable']");
        By selectUnderReviewDropdownValue = By.XPath("//select[@name='status']");
        By userConfigWindow = By.XPath("//p[contains(text(),'Configuration')]//following::a");
        By usersConfigurationRejectButton = By.XPath("//button[@id='UsersConfigurationReject']");
        By usersConfigurationAcceptButton = By.XPath("//button[@id='UsersConfigurationAccept']");

        public IWebElement SelectAnInstrumentInBlocTradeTab()
        {
            return driver.FindElement(selectAnInstrumentInBlocTradeTab);
        }

        //This functions will select the Instrument in block trade Tab
        public void BlockTradeInstrumentSelection(string instrument)
        {
            Thread.Sleep(2000);
            try
            {
                SelectElement select = new SelectElement(SelectAnInstrumentInBlocTradeTab());
                Thread.Sleep(2000);
                select.SelectByText(instrument);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IWebElement OpenAccountByID()
        {
            return driver.FindElement(openAccountByID);
        }


        public IWebElement OpenAccountButton()
        {
            return driver.FindElement(openAccountButton);
        }


        public IWebElement UserByID()
        {
            return driver.FindElement(userByID);
        }


        public IWebElement OpenUserByButton()
        {
            return driver.FindElement(openUserByButton);
        }
       
        //This method Search account by putting the account id in the text field
        public void OpenAccountByIDText(string UserID)
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(OpenAccountByID(), UserID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method Search account by putting the account id in the text field
        public void UserBadgeIDValue(string ID)
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(BadgeTextField(), ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //This method will click on "Open" button while searching the accountID
        public void OpenAccountBtn()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OpenAccountButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method Search account by putting the user id in the text field
        public void UserByIDText(string UserID)
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(UserByID(), UserID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on "Open" button while searching the User
        public void OpenUserButton()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OpenUserByButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IWebElement UsersTab()
        {
            return driver.FindElement(usersTab);
        }

        public IWebElement UsersConfigurationAcceptButton()
        {
            return driver.FindElement(usersConfigurationAcceptButton);
        }

        public IWebElement UsersConfigurationRejectButton()
        {
            return driver.FindElement(usersConfigurationRejectButton);
        }

        public IWebElement OMSAdminButton()
        {
            return driver.FindElement(oMSAdminButton);
        }

        public IWebElement AMAdmin()
        {
            return driver.FindElement(aMAdminButton);
        }

        public IWebElement OperatorAdmin()
        {
            return driver.FindElement(operatorAdminButton);
        }

        public IWebElement ExchangesAdmin()
        {
            return driver.FindElement(exchangesAdminButton);
        }

        public IWebElement RefereshUsersTableButton()
        {
            return driver.FindElement(refreshUsersTableButton);
        }

        public int SingleRowList()
        {
            return driver.FindElements(singleRowList).Count;
        }

        public IWebElement AddNewUser()
        {
            return driver.FindElement(OpenAddNewUserButton);
        }

        public IWebElement NewUserName()
        {
            return driver.FindElement(newUserNameTextField);
        }

        public IWebElement NewUserEmail()
        {
            return driver.FindElement(newUserEmailTextField);
        }
        public IWebElement NewUserPassword()
        {
            return driver.FindElement(newUserPasswordTextField);
        }

        public IWebElement NewUserConfirmPassword()
        {
            return driver.FindElement(newUserConfirmPasswordTextField);
        }

        public IWebElement NewUserSubmit()
        {
            return driver.FindElement(newUserSubmitButton);
        }

        public IWebElement OpenAddUserPermissionButton()
        {
            return driver.FindElement(openAddUserPermissionButton);
        }

        public IWebElement SearchPermissionTextBox()
        {
            return driver.FindElement(searchTextBox);
        }

        public IWebElement SubmitBlockTradeCheckboxButton()
        {
            return driver.FindElement(submitBlockTradeCheckboxButton);
        }

        public IWebElement CloseUserPermissionWindowSection()
        {
            return driver.FindElement(closeUserPermissionWindowSection);
        }

        public IWebElement GetOpenTradeReportsCheckboxButton()
        {
            return driver.FindElement(getOpenTradeReportsCheckboxButton);
        }

        public IWebElement OpenCreateUserAffiliateTagButton()
        {
            return driver.FindElement(openCreateUserAffiliateTagButton);
        }

        public IWebElement AffiliateTagTextField()
        {
            return driver.FindElement(affiliateTagTextField);
        }

        public IWebElement SubmitAffiliateTagButton()
        {
            return driver.FindElement(submitAffiliateTagButton);
        }

        public IWebElement AffiliateWindowSection()
        {
            return driver.FindElement(affiliateWindowSection);
        }

        public IWebElement TicketsMenuLink()
        {
            return driver.FindElement(ticketsMenuLink);
        }

        public IWebElement WithdrawsTab()
        {
            return driver.FindElement(withdrawsTab);
        }

        public IWebElement DepositsTab()
        {
            return driver.FindElement(depositsTab);
        }

        public IWebElement TicketID()
        {
            return driver.FindElement(ticketID);
        }

        public IWebElement WithdrawTicketWindow()
        {
            return driver.FindElement(withdrawTicketWindow);
        }

        public IWebElement DepositsWindow()
        {
            return driver.FindElement(depositsWindow);
        }

        public IWebElement AcceptDepositeTicketButton()
        {
            return driver.FindElement(acceptDepositeTicketButton);
        }

        public IWebElement DeclineDepositeTicketButton()
        {
            return driver.FindElement(declineDepositeTicketButton);
        }

        public IWebElement PendingDepositeTicketButton()
        {
            return driver.FindElement(pendingDepositeTicketButton);
        }

        public IWebElement DepositeID()
        {
            return driver.FindElement(depositeID);
        }

        public IWebElement ConfigureLoyaltyTokenWindow()
        {
            return driver.FindElement(configureLoyaltyTokenWindow);
        }

        public IWebElement EditOMSWindow()
        {
            return driver.FindElement(editOMSWindow);
        }

        public IWebElement EditUserInformationLink()
        {
            return driver.FindElement(editUserInformationLink);
        }

        public IWebElement Usernnme()
        {
            return driver.FindElement(usernnme);
        }

        public IWebElement UserEmail()
        {
            return driver.FindElement(userEmail);
        }

        public IWebElement SubmitUserDetailsButton()
        {
            return driver.FindElement(submitUserDetailsButton);
        }

        public IWebElement ApexQa20msRowButtonLink()
        {
            return driver.FindElement(apexQa20msRowButtonLink);
        }

        public IWebElement UpdateLoyalityFeeConfigButton()
        {
            return driver.FindElement(updateLoyalityFeeConfigButton);
        }

        public IWebElement IsEnabledButton()
        {
            return driver.FindElement(isEnabledButton);
        }

        public IWebElement UserMenuSection()
        {
            return driver.FindElement(openUsersMenuButton);
        }

        public IWebElement AccountsTabMenuLink()
        {
            return driver.FindElement(accountsTabMenuLink);
        }

        public IWebElement SingleUserAccountLink()
        {
            return driver.FindElement(singleUserAccountLink);
        }

        public IWebElement OpenAddNewBadgeButton()
        {
            return driver.FindElement(openAddNewBadgeButton);
        }

        public IWebElement BadgeTextField()
        {
            return driver.FindElement(badgeTextField);
        }

        public IWebElement SubmitCreateAccountBadgeButton()
        {
            return driver.FindElement(submitCreateAccountBadgeButton);
        }

        public IWebElement BlockTradesTabButton()
        {
            return driver.FindElement(blockTradesButton);
            //return driver.FindElement(blockTradeTab);
        }
      
        public IWebElement CloseOrderHistoryModalWindow()
        {
            return driver.FindElement(closeOrderHistoryModalWindow);
        }

        public IWebElement TradeMenuLink()
        {
            return driver.FindElement(tradeMenuLink);
        }

        public IWebElement UserVerificationMenuLink()
        {
            return driver.FindElement(userVerificationMenuLink);
        }

        //This method will create an Affiliate Tag on user
        public void AffiliateTagCreations(string tagName)
        {
            try
            {
                UserSetFunctions.Click(OpenCreateUserAffiliateTagButton());
                UserSetFunctions.EnterText(AffiliateTagTextField(), tagName);
                Thread.Sleep(2000);
                UserSetFunctions.Click(SubmitAffiliateTagButton());
                Thread.Sleep(2000);
                UserSetFunctions.Click(AffiliateWindowSection());
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

       
         public void OpenAddNewBadgeButtonForUser()
            {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OpenAddNewBadgeButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //This method will click on "Add permission" button
        public void UserPermissionButton()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Click(OpenAddUserPermissionButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on Reject Button under user Configuration
        public void RejectUserConfig()
        {
            try
            {
                UserSetFunctions.Click(UsersConfigurationRejectButton());
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //This method will click on Accept Button under user Configuration
        public void AcceptUserConfig()
        {
            try
            {
                UserSetFunctions.Click(UsersConfigurationAcceptButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //This method will clear the userPermission textbox field
        public void ClearTextBox()
        {
            try
            {
                Thread.Sleep(2000);
                UserSetFunctions.Clear(SearchPermissionTextBox());
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will create submitBlockTrade permission on user
        public void AddSubmitBlockTradePermissions(string userPermissionName)
        {
            try
            {
                UserSetFunctions.EnterText(SearchPermissionTextBox(), userPermissionName);
                if (!SubmitBlockTradeCheckboxButton().Selected)
                {                   
                    UserSetFunctions.Click(SubmitBlockTradeCheckboxButton());
                } 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will create GetOpenTradeReports permission on user
        public void AddGetOpenTradeReportsPermissions(string userPermissionName)
        {
            try
            {
                UserSetFunctions.EnterText(SearchPermissionTextBox(), userPermissionName);
                if (!GetOpenTradeReportsCheckboxButton().Selected)
                {
                    UserSetFunctions.Click(GetOpenTradeReportsCheckboxButton());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will close the user permission window
        public void ClosePermissionWindow()
        {
            try
            {
                UserSetFunctions.Click(CloseUserPermissionWindowSection());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will create a new User
        public void AddNewUser(string userName, string userEmail, string passWord, string confirmPassword)
        {
            try
            {
                UserSetFunctions.Click(AddNewUser());
                UserSetFunctions.EnterText(NewUserName(), userName);
                UserSetFunctions.EnterText(NewUserEmail(), userEmail);
                UserSetFunctions.EnterText(NewUserPassword(), passWord);
                UserSetFunctions.EnterText(NewUserConfirmPassword(), confirmPassword);
                UserSetFunctions.Click(NewUserSubmit());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on "OMS Admin" button
        public void SelectOMSAdminOption()
        {
            try
            {
                UserSetFunctions.Click(OMSAdminButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on UserMenu button from the right top corner
        public void UserMenuButton()
        {
            try
            {
                Thread.Sleep(3000);
                UserSetFunctions.Click(UserMenuSection());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UsersVerificationMenuBtn()
        {
            try
            {
                UserSetFunctions.Click(UserVerificationMenuLink());
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //This method will click on Trade Menu button
        public void SelectTradeMenu()
        {
            try
            {
                UserSetFunctions.Click(TradeMenuLink());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //This method will click on "AM Admin" button
        public void SelectAMAdimOption()
        {
            try
            {
                UserSetFunctions.Click(AMAdmin());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on "Operator Admin" button
        public void SelectOperatorAdminOption()
        {
            try
            {
                UserSetFunctions.Click(OperatorAdmin());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on Tickets Menu
        public void SelectTicketsMenu()
        {
            try
            {
                UserSetFunctions.Click(TicketsMenuLink());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on Accounts Menu
        public void SelectAccountsMenu()
        {
            try
            {
                UserSetFunctions.Click(AccountsTabMenuLink());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //This method will click on Withdraw Tab in Tickets
        public void NavigateToWithdrawTicketsTab()
        {
            try
            {
                UserSetFunctions.Click(WithdrawsTab());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on Deposits Tab in Tickets
        public void NavigateToDepositTicketsTab()
        {
            try
            {
                UserSetFunctions.Click(DepositsTab());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string VerifyWithdrawTickets(string userName)
        {
            //This will return status of the ticket.
            return null;
        }

        //This method will the withdraw Tab window
        public void CloseWithdrawTicketWindow()
        {
            UserSetFunctions.Click(WithdrawTicketWindow());
        }

        //This method will the Deposits Tab window
        public void CloseDepositsWindow()
        {
            UserSetFunctions.Click(DepositsWindow());
        }

        //This method will click on accept button in Deposite Tab
        public void ClickOnAcceptButtonFromDepositsTicketModal()
        {
            UserSetFunctions.Click(AcceptDepositeTicketButton());
        }

        //This method will click on decline button in Deposite Tab
        public void ClickOnDeclineButtonFromDepositsTicketModal()
        {
            UserSetFunctions.Click(DeclineDepositeTicketButton());
        }

        //This method will click on pending button in Deposite Tab
        public void ClickOnPendingButtonFromDepositsTicketModal()
        {
            UserSetFunctions.Click(PendingDepositeTicketButton());
        }

        //This method will click on the first ticket in withdraw Tab
        public void ClickOnTicketFromWithdrawTicketList(string ticketID)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[1]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(ticketID))
                    {
                        Thread.Sleep(2000);
                        Actions actions = new Actions(driver);
                        actions.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on the first ticket in Deposits Tab
        public void ClickOnTicketFromDepositTicketList(string depositsID)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[1]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(depositsID))
                    {
                        Thread.Sleep(2000);
                        Actions actions = new Actions(driver);
                        actions.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will click on the Users menu button in Admin
        public void SelectUserFromUsersList(string userName)
        {          
            try
            {               
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(userName))
                    {
                        Thread.Sleep(2000);

                        Actions action = new Actions(driver);
                        action.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
      
        //This method will close the Loyality window
        public void CloseLoyaltyTokenWindow()
        {
            UserSetFunctions.Click(ConfigureLoyaltyTokenWindow());
        }

        //This method will close the Order History of block trade window section
        public void CloseBlockTradeWindow()
        {
            try
            {
                UserSetFunctions.Click(CloseOrderHistoryModalWindow());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This method will close the Edit OMS window
        public void CloseEditOMSWindow()
        {
            UserSetFunctions.Click(EditOMSWindow());
        }

        //This method will edit/update the username of particular user and submit the information
        public void EditUserAccountInformation(string userName)
        {
            UserSetFunctions.Click(EditUserInformationLink());
            UserSetFunctions.EnterText(Usernnme(), userName);
            UserSetFunctions.Click(SubmitUserDetailsButton());
        }

        //This method will click on username and check whether fee is enabled or disabled
        public void OMSAdminstrationLoyalityFee()
        {
            try
            {
                Actions action = new Actions(driver);
                action.DoubleClick(ApexQa20msRowButtonLink()).Build().Perform();
                UserSetFunctions.Click(UpdateLoyalityFeeConfigButton());
                if (IsEnabledButton().Selected)
                {
                    logger.Info("Loyality fee is Enabled");
                }
                else
                {
                    logger.Info("Loyality fee is Disabled");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        //This method will get this Message "Badge already exists"
        public IWebElement BadgeExitCreationMsg()
        {
            return driver.FindElement(badgeExitCreationMsg);
        }

        public IWebElement CloseAddNewBadgeWindow()
        {
            return driver.FindElement(closeAddNewBadgeWindow);
        }

        //This method will get this Message "Badge Already Exists on another account"
        public IWebElement BadgeExitOnAontherAccountMsg()
        {
            return driver.FindElement(badgeExitOnAontherAccountMsg);
        }       

        //This method will create badge for user
        public void AddAccountBadge(string userName, string badgeNumber)
        {
            try
            {
                int accountArrayList = driver.FindElements(accountList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement accountNameElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string accountName = accountNameElement.Text;
                    if (accountName.Equals(userName))
                    {
                        Actions action = new Actions(driver);
                        action.DoubleClick(accountNameElement).Build().Perform();
                        break;
                    }
                }
                UserSetFunctions.Click(OpenAddNewBadgeButton());
                Thread.Sleep(2000);
                UserSetFunctions.EnterText(BadgeTextField(), badgeNumber);
                Thread.Sleep(2000);
                UserSetFunctions.Click(SubmitCreateAccountBadgeButton());
                Thread.Sleep(2000);
                CheckAndCreateBadge(userName, badgeNumber);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ArrayList GetListOfBlockTradeReports()
        {
            ArrayList tradeReportsOrderList = new ArrayList();
            int countOfTradeReports = driver.FindElements(By.XPath("//div[@class='ReactVirtualized__Table__row']")).Count;

            for (int i = 1; i <= countOfTradeReports; i++)
            {
                String textFinal = "";
                int countItems = driver.FindElements(By.XPath("(//div[@class='ReactVirtualized__Table__row'])[" + i + "]/div")).Count;
                for (int j = 2; j <= (countItems)-2; j++)
                {
                    String text = driver.FindElement(By.XPath("(//div[@class='ReactVirtualized__Table__row'])[" + i + "]/div[" + j + "]")).Text;
                    if (j == 2)
                    {
                        textFinal = text;
                    }
                    else
                    {
                        textFinal = textFinal + " || " + text;
                    }

                }
                tradeReportsOrderList.Add(textFinal);
            }
            return tradeReportsOrderList;
        }

        //This method return the buy block trade list and perfom click action on it
        public bool BuyBlockTradeList(string accountTypeID, string counterPartyID, string instrument, string originalQuantity, string quantityExecuted)
        {
            try
            {
                bool flag = false;
                string originalQtyValue;
                string quantityExecutedValue;

                originalQtyValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(originalQuantity));
                quantityExecutedValue = GenericUtils.ConvertToDoubleFormat(Double.Parse(quantityExecuted));

                string expectedRow_1 = accountTypeID + " || " + counterPartyID + " || " + "FullyExecuted" + " || " + instrument + " || " + "Buy" + " || " + originalQtyValue + " || " + quantityExecutedValue;
                string expectedRow_2 = counterPartyID + " || " + accountTypeID + " || " + "FullyExecuted" + " || " + instrument + " || " + "Sell" + " || " + originalQtyValue + " || " + quantityExecutedValue;

                var tradeReportsOrderList = GetListOfBlockTradeReports();
                if (tradeReportsOrderList.Contains(expectedRow_1) && tradeReportsOrderList.Contains(expectedRow_2))
                {
                    flag = true;
                }
                if (flag)
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifyTradesOK,"Buy"));
                }
                else
                {
                    logger.LogCheckPoint(string.Format(LogMessage.VerifyTradesNotOK, "Sell"));
                }
                return flag;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void CreateBadgeAccount()
        {
            UserSetFunctions.Click(SubmitCreateAccountBadgeButton());
        }

        //This method will check same badge created or not
        public void CheckAndCreateBadge(string userName, string badgeNumber)
        {
            try
            {
                if(BadgeExitOnAontherAccountMsg().Displayed || BadgeExitCreationMsg().Displayed)
                {
                    Thread.Sleep(2000);
                    UserSetFunctions.Click(CloseAddNewBadgeWindow());
                }
            }
            catch (NoSuchElementException)
            {
                
            }
        }

        //This method will click on the Block Trade Tab
        public void BlockTradeBtn()
        {
            try
            {
               // UserSetFunctions.Click(BlockTradesButton());
                UserSetFunctions.Click(BlockTradesTabButton());
            }
            catch (Exception e)
            {
                throw e;
            }
        }        

        
        public void TradeUserDetails(string executionID)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(counterPartyID))
                    if (webElementtext.Equals(executionID))
                    {
                        Thread.Sleep(2000);
                        Actions action = new Actions(driver);
                        action.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(2000);                        
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //This will click on Users Tab under User Menu
        public void UsersTabBtn()
        {
            UserSetFunctions.Click(UsersTab());
        }


        /*public void UserNameTabUnderUserVerification(string username)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(executionID))
                    if (webElementtext.Equals(username))
                    {
                        Thread.Sleep(2000);
                        Actions action = new Actions(driver);
                        action.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public void VerifyStatus(IWebDriver driver, string statusid, String status)
        {
            ArrayList myList = new ArrayList();
            try
            {
                int count = driver.FindElements(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div")).Count;
                for (int i = 1; i <= count - 1; i++)
                {
                    string statusId = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[5]")).Text;
                    myList.Add(statusId);
                    if (statusId.Equals(statusId))
                    {
                        string actualStatus = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Grid__innerScrollContainer']/div[" + i + "]/div[7]")).Text;
                        Assert.Equal(status, actualStatus);
                        break;
                    }
                }
                Assert.True(myList.Contains(statusid), Const.StatusIdNotFound);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        //This method will click in the order in tradeOrder History list
        public void UserNameTabUnderUserVerification(string username)
        {
            try
            {
                int accountArrayList = driver.FindElements(elementList).Count;
                for (int i = 1; i <= accountArrayList; i++)
                {
                    IWebElement webElement = driver.FindElement(By.XPath("//div[@class='ReactVirtualized__Table__row'][" + i + "]/div[2]"));
                    string webElementtext = webElement.Text;
                    if (webElementtext.Equals(username))
                    if (webElementtext.Equals(counterPartyID))
                    {
                        Thread.Sleep(2000);
                        Actions action = new Actions(driver);
                        action.DoubleClick(webElement).Build().Perform();
                        Thread.Sleep(2000);
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
