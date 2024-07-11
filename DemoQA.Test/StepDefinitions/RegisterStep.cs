using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Reports;
using Core.Utilities;
using DemoQA.Test.Constant;
using DemoQA.Test.DataObject;
using DemoQA.Test.Page;
using LivingDoc.Dtos;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemoQA.Test.StepDefinitions
{
    [Binding]
    public class RegisterStep
    {
        private RegisterPage _registerPage;
        private readonly ScenarioContext _scenarioContext;
        public RegisterStep(ScenarioContext scenarioContext)
        {
            _registerPage = new();
            _scenarioContext = scenarioContext;
        }

        [Given(@"User opens the DemoQA register Student form page")]
        public void GivenUseropenstheDemoQAregisterStudentformpage()
        {
            _registerPage.GoToUrl(UrlConstant.ResgisterStudentUrl);
        }


        [When(@"User enters valid data into all fields")]
        public void WhenUserentersvaliddataintoallfields(Table table)
        {
            var _expectedStudent = table.CreateInstance<Student>();
            _scenarioContext["Student"] = _expectedStudent;
            _registerPage.RegisterStudent(_expectedStudent);
        }


        [When(@"User clicks on Submit button")]
        public void WhenUserclicksonSubmitbutton()
        {
            _registerPage.ClickOnSubmitButton();
        }


        [Then(@"Student data shown on pop up should be correct")]
        public void ThenStudentdatashownonpopupshouldbecorrect()
        {
            _registerPage.VerifyRegisterStudent(_scenarioContext["Student"] as Student);
            
        }

    }
}