using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Reports;
using Core.Utilities;
using DemoQA.Test.Constant;
using DemoQA.Test.DataObject;
using DemoQA.Test.Page;

namespace DemoQA.Test.StepDefinitions
{
    [Binding]
    public class LoginStep
    {
        private LoginPage _loginPage;
        public static Dictionary<string, User> Users;
        public LoginStep(){
            _loginPage =new();
            Users = JsonUtils.ReadDictionaryJson<Dictionary<string, User>>(FileConstant.UserFilePath);
        }
        
        [Given(@"User logged in the DemoQA")]
        public void GivenUserloggedintheDemoQA()
        {
            var user = Users["user"];
            _loginPage.GoToUrl(UrlConstant.LoginUrl);
            _loginPage.Login(user.Username, user.Password);
        }
    }
}