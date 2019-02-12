using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AlphaPoint_QA.Common;
using AlphaPoint_QA.Utils;
using log4net;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Pages
{
    class TradeReportsPage
    {
        IWebDriver driver;
        ProgressLogger logger;
      

        By tradeReportsLink = By.XPath("//li[@data-test='Trade Reports']");
        By singleReportButton = By.XPath("//div[@class='activity-reporting__actions-holder']/button[1]");
        By cyclicReportButton = By.XPath("//div[@class='activity-reporting__actions-holder']/button[1]");
        By singleReportText = By.XPath("//div[@data-test='Single Report']");
        By cyclicReportText = By.XPath("//div[@data-test='Cyclical Report']");
        By singleReportsTab = By.XPath("//div[@class='ap-tab__menu reports-sidepane__menu']/div[1]");
        By cyclicReportsTab = By.XPath("//div[@class='ap-tab__menu reports-sidepane__menu']/div[2]");
        By singleReportsTabSelected = By.XPath("//div[@class='ap-tab__menu-item ap-tab__menu-item--active reports-sidepane__menu-item reports-sidepane__menu-item--active ap-tab__menu-item reports-sidepane__menu-item']");
        By accountsCheckbox = By.XPath("//div[@class='ap-checkbox__checkbox report-form__checkbox']");
        By reportType = By.XPath("//*[@class='form-control ap-select__select report-form__select']");
        By startDate = By.XPath("//input[@class='form-control ap-datepicker__input report-form__dpk-input' and @name='startDate']");
        By endDate = By.XPath("//input[@class='form-control ap-datepicker__input report-form__dpk-input' and @name='endDate']");
        By frequencyCyclicReport = By.XPath("//*[@class='form-control ap-select__select report-form__select' and @name='frequency']");
        By createTradeReportButton = By.XPath("//*[@class='ap-button__btn ap-button__btn--additive report-form__btn report-form__btn--additive']");

        public TradeReportsPage(IWebDriver driver, ProgressLogger logger)
        {
            this.driver = driver;
            this.logger = logger;
           
        }

        public IWebElement TradeReportsLink()
        {
            return driver.FindElement(tradeReportsLink);
        }

        public IWebElement SingleReportButton()
        {
            return driver.FindElement(singleReportButton);
        }

        public IWebElement CyclicReportButton()
        {
            return driver.FindElement(cyclicReportButton);
        }

        public IWebElement SingleReportText()
        {
            return driver.FindElement(singleReportText);
        }

        public IWebElement CyclicReportText()
        {
            return driver.FindElement(cyclicReportText);
        }

        public IWebElement SingleReportTab()
        {
            return driver.FindElement(singleReportsTab);
        }

        public IWebElement CyclicReportTab()
        {
            return driver.FindElement(cyclicReportsTab);
        }

        public IWebElement SingleReportsTabSelected()
        {
            return driver.FindElement(singleReportsTabSelected);
        }

        public IWebElement AccountsCheckbox()
        {
            return driver.FindElement(accountsCheckbox);
        }

        public IWebElement ReportType()
        {
            return driver.FindElement(reportType);
        }

        public IWebElement StartDate()
        {
            return driver.FindElement(startDate);
        }

        public IWebElement EndDate()
        {
            return driver.FindElement(endDate);
        }

        public IWebElement FrequencyCyclicReport()
        {
            return driver.FindElement(frequencyCyclicReport);
        }

        public IWebElement CreateTradeReportButton()
        {
            return driver.FindElement(createTradeReportButton);
        }

        
        public void CreateTradeDepositWithdraw()
        {
            //Pre-condition few trade, deposit and withdraw activities
        }

        public void CreateSingleReportButton()
        {

        }

        public void VerifySingleReportModal()
        {

        }

        public void VerifySingleReportModalDetails()
        {
            //"Verify below details on confirmation modal:Report type and dates from earlier page"
        }

        public void CreateSingleReportModalButton()
        {

        }

        public void VerifyCreateSingleReportSuccessMessage()
        {

        }


        /// <summary>
        /// Cyclic
        /// </summary>

        public void ClickCyclicReportButton()
        {

        }

        public void VerifyCyclicReportDetails()
        {
            

        }

        public void VerifyFrequencyDropdown()
        {
           

        }

        public void SelectCyclicReportType()
        {
            

        }

        public void SelectCyclicStartDate()
        {


        }

        public void SelectCyclicFrequency()
        {


        }

        public void ClickCreateCyclicReportButton()
        {


        }

        public void VerifyCyclicReportModal()
        {

        }

        public void VerifyCyclicReportModalDetails()
        {
            //"Verify below details on confirmation modal:Report type and dates from earlier page"
        }

        public void CreateCyclicReportModalButton()
        {

        }

        public void VerifyCreateCyclicReportSuccessMessage()
        {

        }


        /// <summary>
        /// Details Page
        /// </summary>
        /// 
        public void ClickRefreshReportsButton()
        {

        }

        public void VerifyLatestSingleReportGenerated()
        {
            //Verify Top most report is the latest created report

        }

        public void VerifyLatestSingleReportCreatedTime()
        {
            //Verify the created time
        }

        public void VerifySingleReportSummaryDetails()
        {
            //"Verify below details:Report summary is 'report type' , on demand and from and to date. "
        }

        public void VerifyDownloadSingleReportLink()
        {
            //"Verify the report is downloadable
        }

        public void VerifyCyclicReportsDetails()
        {
            //Verify Report is available under 'Cyclic reports' section
        }

        public void VerifyCyclicReportSummaryDetails()
        {
            
        }

        public void VerifyCyclicReportFrequency()
        {
           
        }

        public void VerifyCyclicReportCreatedDate()
        {
            
        }

        public void VerifyCyclicCancelReportButton()
        {
            
        }

        public void VerifyDownloadCyclicReportLink()
        {
            //"Verify the report is downloadable
        }

        public void VerifyCancelCyclicReportButton()
        {
           
        }

        public void VerifyCancelCyclicReportsSection()
        {
            //Verify that the report is removed from 'Cyclic reports' section

        }

        // This method verifies that the Single and Cyclic Report tabs are present
        // By default Single Reports tab is selected
        public bool VerifySingleAndCyclicReportText()
        {
            bool flag = false;
            if (SingleReportText().Text.Equals("Single Report") && CyclicReportText().Text.Equals("Cyclical Report"))
            {
                if (SingleReportsTabSelected().Displayed)
                {
                    flag = true;
                    logger.Info("Verification Successful: Single and Cyclic Report tabs are present, by default Single Reports tab is selected");
                    return flag;
                }
                else
                {
                    UserSetFunctions.Click(SingleReportTab());
                    logger.Error("Verification Failed: VerifySingleAndCyclicReportText");
                }
            }
            return flag;
        }

        // This method verifies that the "Verify below fields are present: accounts, report type and start date, end date"
        public bool VerifySingleReportFields()
        {
            bool flag = false;
            if (AccountsCheckbox().Displayed && ReportType().Displayed && StartDate().Displayed && EndDate().Displayed)
            {
                flag = true;
                logger.Info("Verification Successful: accounts, report type and start date, end date are present");
                return flag;
            }
            return flag;
        }

        // This method verifies Current Date - 1 and all previous dates are selectable 
        public bool VerifyStartDateSelectableDates()
        {
            int today;
            bool flag = false;
            today = GenericUtils.GetOnlyCurrentDate();
            UserSetFunctions.Click(StartDate());
            IWebElement dateWidgetFrom = driver.FindElement(By.XPath("//table[@class='pika-table']/tbody"));
            //This are the columns of the from date picker table
            List<IWebElement> columns = new List<IWebElement>();
            var dateColumn = dateWidgetFrom.FindElements(By.ClassName("is-disabled"));
            foreach (IWebElement fields in dateColumn)
            {
                string disabledFields = fields.Text;
                int fieldValueInInt = Int32.Parse(disabledFields);
                if (fieldValueInInt >= today)
                {
                    flag = true;
                    logger.Info("The Current Date - 1 and all previous dates are selectable");
                    return flag;
                }
                else
                {
                    logger.Error("Verification failed : The Current Date - 1 and all previous dates are not selectable");
                    return flag;
                }
            }
            return flag;
        }


        // This method verifies selectable dates are greater than start date and equal to current date
        public bool VerifyEndDateSelectableDates(string startDate)
        {
            int today;
            int startDateSelected = GenericUtils.GetOnlyDateFromDateString(startDate);
            bool flag = false;
            today = GenericUtils.GetOnlyCurrentDate();

            UserSetFunctions.Click(EndDate());
            IWebElement dateWidgetEnd = driver.FindElement(By.XPath("//table[@class='pika-table']/tbody"));
            //This are the columns of the from date picker table
            List<IWebElement> columns = new List<IWebElement>();
            var dateColumn = dateWidgetEnd.FindElements(By.ClassName("is-disabled"));
            foreach (IWebElement fields in dateColumn)
            {
                string disabledFields = fields.Text;
                int fieldValueInInt = Int32.Parse(disabledFields);
                if (fieldValueInInt > today)
                {
                    logger.LogCheckPoint ("Greater than today " + fieldValueInInt);
                }
                if (fieldValueInInt < startDateSelected)
                {
                    logger.LogCheckPoint("Less than startDateSelected" + fieldValueInInt);
                }


                //if ((fieldValueInInt <= today)&&(startDateSelected>fieldValueInInt))
                //{
                //    flag = true;
                //    logger.Info("The Current Date - 1 and all previous dates are selectable");
                //    return flag;
                //}
                //else
                //{
                //    logger.Error("Verification failed : The Current Date - 1 and all previous dates are not selectable");
                //    return flag;
                //}
            }
            return flag;
        }


        // 
        public bool VerifySingleReportData(string reportTypeValue, string startDate)
        {
            bool flag = false;
            UserCommonFunctions.DashBoardMenuButton(driver);
            UserCommonFunctions.NavigateToUserSetting(driver);
            Thread.Sleep(1000);
            UserSetFunctions.Click(TradeReportsLink());
            Thread.Sleep(1000);
            UserSetFunctions.Click(SingleReportButton());
            if (VerifySingleAndCyclicReportText())
            {
                if (VerifySingleReportFields())
                {
                    UserSetFunctions.Click(SingleReportsTabSelected());
                    UserSetFunctions.SelectDropdown(ReportType(), reportTypeValue);

                    if (VerifyStartDateSelectableDates())
                    {
                        UserSetFunctions.Click(StartDate());
                        if (startDate == null)
                        {
                            startDate = GenericUtils.GetCurrentDateMinusOne();
                        }
                        StartDate().SendKeys(startDate);
                        flag = true;
                    }
                    VerifyEndDateSelectableDates(startDate);
                }
            }
            else
            {
                logger.Error("Single Report : Element is not displayed");
                flag = false;
            }

            return flag;
        }

    }
}
