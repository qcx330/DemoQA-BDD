using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Configuration;
using Core.Driver;
using Core.Reports;
using DemoQA.Test.Constant;
using NUnit.Framework;

namespace DemoQA.Test.Hook
{
    [Binding]
    public class Hook
    {
        private ScenarioContext _scenarioContext;

        public Hook(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("BeforeTestRun");
            var config = ConfigurationHelper.ReadConfiguration(FileConstant.AppSettingPath);
            ExtentReportManager.AddSystemInfo("Enviroment", config["enviroment"]);
            ExtentReportManager.AddSystemInfo("Browser", config["browser"]);
            
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext){
            ExtentTestManager.CreateParentTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Console.WriteLine("BeforeScenario");            
            var browser = ConfigurationHelper.GetValueByKey("browser");
            DriverManager.InitializeBrowser(browser);    
            ExtentTestManager.CreateScenarioContext(_scenarioContext);
        }


        [AfterScenario]
        public void AfterScenario()
        {
            ExtentTestManager.UpdateScenarioContext();
            Console.WriteLine("AfterScenario");
            ReportLog.Info("Close webdriver");
            DriverManager.CloseDriver();
        }

        [AfterTestRun]
        public static void AfterTestRun(){
            ExtentReportManager.GenerateReport();
        }

        [AfterStep]
        public static void AfterStep(){
            ExtentTestManager.UpdateStepContext();
        }

    }
}