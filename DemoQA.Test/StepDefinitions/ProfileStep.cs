using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.API;
using Core.Configuration;
using Core.Reports;
using Core.Utilities;
using DemoQA.Service.Service;
using DemoQA.Test.Constant;
using DemoQA.Test.DataObject;
using DemoQA.Test.Page;

namespace DemoQA.Test.StepDefinitions
{
    [Binding]
    public class ProfileStep
    {
        private ProfilePage _profilePage;
        private readonly ScenarioContext _scenarioContext;
        private UserService _userServices;
        private BookService _bookServices;
        protected static APIClient ApiClient;
        public static Dictionary<string, User> Users;
        User user;
        public ProfileStep(ScenarioContext scenarioContext)
        {
            ApiClient = new APIClient(ConfigurationHelper.GetConfiguration()["baseUrl"]);
            _profilePage = new();
            _userServices = new UserService(ApiClient);
            _bookServices = new BookService(ApiClient);
            Users = JsonUtils.ReadDictionaryJson<Dictionary<string, User>>(Constant.FileConstant.UserFilePath);
            user = Users["user"];
            _scenarioContext = scenarioContext;
        }

        [Given(@"User already added book")]
        public async void GivenUseralreadyaddedbook(Table table)
        {
            ReportLog.Info("Added book to collection");
            var response = _userServices.GenerateToken(user.Username, user.Password);
            var token = response.Data.Token;
            foreach (var row in table.Rows)
            {
                var bookId = row["Id"];
                _scenarioContext["Title"] = row["Title"];
                await _bookServices.AddBookToCollectionAsync(user.UserId, bookId, token);
            }
        }

        [Given(@"User navigate to the profile page")]
        public void GivenUsernavigatetotheprofilepage()
        {
            _profilePage.WaitForNavigateProfilePage();
        }

        [When(@"User search for added book")]
        public void WhenUsersearchforaddedbook()
        {
            _profilePage.SearchBook(_scenarioContext["Title"].ToString());
        }

        [When(@"User clicks on Delete icon")]
        public void WhenUserclicksonDeleteicon()
        {
            _profilePage.ClickOnDeleteButton(_scenarioContext["Title"].ToString());
        }

        [When(@"User clicks on OK button")]
        public void WhenUserclicksonOKbutton()
        {
            _profilePage.ClickOnOKButton();
        }

        [When(@"User clicks on OK button of alert “Book deleted.”")]
        public void WhenUserclicksonOKbuttonofalertBookdeleted()
        {
            _profilePage.HandleAlert();
        }

        [Then(@"the book is not shown")]
        public void Thenthebookisnotshown()
        {
            _profilePage.VerifyBookRemoved(_scenarioContext["Title"].ToString());
        }
        
        [Then(@"the book is shown in your profile")]
        public void Thenthebookisshowninyourprofile()
        {
            string title = _scenarioContext["title"].ToString();
            _profilePage.VerifyBookAdd(title);
        }
    }
}