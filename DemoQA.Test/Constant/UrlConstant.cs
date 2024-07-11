using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Configuration;

namespace DemoQA.Test.Constant
{
    public class UrlConstant
    {
        private static string baseUrl => ConfigurationHelper.GetConfiguration()["baseUrl"];
        public static string ResgisterStudentUrl => $"{baseUrl}/automation-practice-form";
        public static string BookStoreUrl => $"{baseUrl}/books";
        public static string LoginUrl => $"{baseUrl}/login";
        public static string ProfileUrl => $"{baseUrl}/profile";
    }
}