using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AventStack.ExtentReports.Utils;
using Core;
using DemoQA.Test.DataObject;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DemoQA.Test.Page
{
    public class RegisterPage : BasePage
    {
        private Element _txtField(string field) => new(By.Id(field));
        private Element _chkField(string field) => new(By.XPath($"//label[text()='{field}']"));
        private Element _ddlDate(string field) => new(By.CssSelector($"select[class*='{field}-select']"));
        private Element _btnUploadPicture = new(By.Id("uploadPicture"));
        private Element _ddlStateCity(string stateCity) => new(By.CssSelector($"div#{stateCity}"));
        private Element _optStateCity(string stateCity) => new(By.XPath($"//div[contains(text(),'{stateCity}')]"));
        private Element _btnSubmit = new(By.Id("submit"));
        private Element _optSubject(string subject) => new(By.XPath($"//div[contains(@class,'option') and contains(text(),'{subject}')]"));
        private Element _popupRegister = new(By.ClassName("modal-content"));
        private Element _rowData(string row) => new(By.XPath($"//td[text()='{row}']/following-sibling::td"));
        private Element _txtBoD = new(By.Id("dateOfBirthInput"));
        private Element _lblDay(string day) => new(By.XPath($"//div[contains(@class,'datepicker') and .='{day}']"));
        public void VerifyStateCity(string state, string city)
        {
            if (state.IsNullOrEmpty() || city.IsNullOrEmpty())
            {
                return;
            }
            Assert.That($"{state} {city}", Is.EqualTo(_rowData("State and City").GetTextFromElement()));
        }
        public void RegisterStudent(Student student)
        {
            _txtField("firstName").InputText(student.FirstName);
            _txtField("lastName").InputText(student.LastName);
            InputEmail(student.Email);
            SelectGender(student.Gender);
            _txtField("userNumber").InputText(student.Mobile);
            InputDateOfBirth(student.DateOfBirth);
            InputSubjects(student.Subjects);
            SelectHobby(student.Hobbies);
            UploadPicture(student.Picture);
            InputCurrentAddress(student.CurrentAddress);
            SelectState(student.State);
            SelectCity(student.City);
        }
        public void ClickOnSubmitButton()
        {
            _btnSubmit.ScrollToElement();
            _btnSubmit.ClickOnElement();
        }
        public void VerifyRegisterStudent(Student expectedStudent)
        {
            Assert.That(_popupRegister, Is.Not.Null);
            VerifyPopupData(expectedStudent);
        }
        public void VerifyPopupData(Student expectedStudent)
        {
            Assert.That($"{expectedStudent.FirstName} {expectedStudent.LastName}", Is.EqualTo(_rowData("Student Name").GetTextFromElement()));
            VerifyEmail(expectedStudent.Email);
            Assert.That(expectedStudent.Gender, Is.EqualTo(_rowData("Gender").GetTextFromElement()));
            Assert.That(expectedStudent.Mobile, Is.EqualTo(_rowData("Mobile").GetTextFromElement()));
            VerifyDateOfBirth(expectedStudent.DateOfBirth);
            VerifySubjects(expectedStudent.Subjects);
            VerifyHobbies(expectedStudent.Hobbies);
            VerifyPicture(expectedStudent.Picture);
            VerifyAddress(expectedStudent.CurrentAddress);
            VerifyStateCity(expectedStudent.State, expectedStudent.City);
        }
        public void VerifyAddress(string address)
        {
            if (address.IsNullOrEmpty())
            {
                return;
            }
            Assert.That(address, Is.EqualTo(_rowData("Address").GetTextFromElement()));
        }
        public void VerifyPicture(string picture)
        {
            if (picture.IsNullOrEmpty())
            {
                return;
            }
            Assert.That(Path.GetFileName(picture), Is.EqualTo(_rowData("Picture").GetTextFromElement()));
        }
        public void VerifyHobbies(List<string> hobbies)
        {
            if (hobbies.IsNullOrEmpty())
            {
                return;
            }
            var hobbiesText = string.Join(", ", hobbies);
            Assert.That(hobbiesText, Is.EqualTo(_rowData("Hobbies").GetTextFromElement()));
        }
        public void VerifySubjects(List<string> subjects)
        {
            if (subjects.IsNullOrEmpty())
            {
                return;
            }
            var actualSubjects = string.Join(", ", subjects.Select(s => s));
            Assert.That(actualSubjects, Is.EqualTo(_rowData("Subjects").GetTextFromElement()));
        }
        public void VerifyDateOfBirth(string dob)
        {
            if (dob.IsNullOrEmpty())
            {
                return;
            }
            Assert.That(ConvertDateFormat(dob), Is.EqualTo(_rowData("Date of Birth").GetTextFromElement()));
        }
        public static string ConvertDateFormat(string inputDate)
        {
            DateTime dateTime = DateTime.ParseExact(inputDate, "d-MMMM-yyyy", CultureInfo.InvariantCulture);
            return dateTime.ToString("dd MMMM,yyyy");
        }
        public void VerifyEmail(string email)
        {
            if (email.IsNullOrEmpty())
            {
                return;
            }
            Assert.That(email, Is.EqualTo(_rowData("Student Email").GetTextFromElement()));
        }
        public void InputCurrentAddress(string address)
        {
            if (address.IsNullOrEmpty())
                return;
            _txtField("currentAddress").InputText(address);
        }
        public void InputEmail(string email)
        {
            if (email.IsNullOrEmpty())
                return;
            _txtField("userEmail").InputText(email);
        }
        public void UploadPicture(string picturePath)
        {
            if (picturePath.IsNullOrEmpty())
                return;
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string combinedPath = Path.Combine(baseDirectory, @picturePath);
                string fullPath = Path.GetFullPath(combinedPath);

                _btnUploadPicture.InputText(fullPath);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Picture file does not exist.", picturePath);
            }
        }
        public void SelectCity(string city)
        {
            if (city.IsNullOrEmpty())
                return;
            _ddlStateCity("city").ClickOnElement();
            _optStateCity(city).ClickOnElement();
        }
        public void SelectState(string state)
        {
            if (state.IsNullOrEmpty())
                return;
            _ddlStateCity("state").ClickOnElement();
            _optStateCity(state).ClickOnElement();
        }
        public void SelectGender(string gender)
        {
            _chkField(gender).ScrollToElement();
            _chkField(gender).ClickOnElement();
        }
        public void InputSubjects(List<string> subjects)
        {
            if (subjects.IsNullOrEmpty())
                return;
            foreach (string subject in subjects)
            {
                _txtField("subjectsInput").InputText(subject);
                _optSubject(subject).ScrollToElement();
                _optSubject(subject).ClickOnElement();
            }
        }
        public void InputDateOfBirth(string date)
        {
            if (date.IsNullOrEmpty())
                return;
            string[] dateParts = date.Split('-', ' ');
            string targetDay = dateParts[0];
            string targetMonth = dateParts[1];
            string targetYear = dateParts[2];

            _txtBoD.ScrollToElement();
            _txtBoD.ClickOnElement();
            _ddlDate("year").SelectByText(targetYear);
            _ddlDate("month").SelectByText(targetMonth);
            _lblDay(targetDay).ClickOnElement();
        }
        public void SelectHobby(List<string> hobbies)
        {
            if (hobbies.IsNullOrEmpty())
                return;
            foreach (string hobby in hobbies)
            {
                _chkField(hobby).ClickOnElement();
            }
        }
    }
}