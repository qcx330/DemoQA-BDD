using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Reports;
using DemoQA.Test.Constant;
using DemoQA.Test.Page;

namespace DemoQA.Test.StepDefinitions
{
    [Binding]
    public class BookStoreStep
    {
        private BookStorePage _bookStorePage;
        private ProfilePage _profilePage;
        private readonly ScenarioContext _scenarioContext;
        public BookStoreStep(ScenarioContext scenarioContext)
        {
            _bookStorePage = new();
            _profilePage = new();
            _scenarioContext = scenarioContext;
        }

        [Given(@"User navigates to the book store page")]
        public void GivenUsernavigatestothebookstorepage()
        {
            _bookStorePage.GoToUrl(UrlConstant.BookStoreUrl);
        }

        [When(@"User inputs search criteria ""(.*)""")]
        public void WhenUserinputssearchcriteria(string criteria)
        {
            _bookStorePage.Search(criteria);
            _scenarioContext["criteria"] = criteria;
        }

        [Then(@"All books match with input criteria will be displayed")]
        public void ThenAllbooksmatchwithinputcriteriawillbedisplayed()
        {
            _bookStorePage.VerifySearchResult(_scenarioContext["criteria"].ToString());
        }

        [When(@"User selects a book")]
        public void WhenUserselectsabook(Table table)
        {
            var bookTitle = table.Rows[0]["book"];
            _bookStorePage.SelectBook(bookTitle);
            _scenarioContext["title"] = bookTitle;
        }

        [When(@"User clicks on Add To Your Collection")]
        public void GivenUserclicksonAddToYourCollection()
        {
            _bookStorePage.AddBookToCollection();
        }


        [Then(@"an alert ""(.*)"" is shown")]
        public void Thenanalertisshown(string alert)
        {
            _bookStorePage.VerifyAlertMessage();
        }


    }
}