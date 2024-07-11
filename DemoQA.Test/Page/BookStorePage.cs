using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Driver;
using DemoQA.Test.DataObject;
using NuGet.Frameworks;
using NUnit.Framework;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface.InvokeExt;

namespace DemoQA.Test.Page
{
    public class BookStorePage : BasePage
    {
        private Element _txtSearch = new(By.Id("searchBox"));
        private Element _btnNext = new(By.XPath("//button[text()='Next']"));
        private Element _tblResult = new(By.CssSelector("[class*='table']"));
        private Element _rowBook(string title) => new(By.XPath($"//a[.='{title}']"));
        public void Search(string criteria)
        {
            _txtSearch.InputText(criteria);
        }
        public void VerifyAlertMessage()
        {
            
        }
        public void AddBookToCollection()
        {
            
        }
        public void SelectBook(string title)
        {
            _rowBook(title).ClickOnElement();
        }
        public void VerifySearchResult(string criteria)
        {
            List<Book> results = GetResults();
            results.Should().Contain(book => book.Title.ToLower().Contains(criteria.ToLower()) ||
                                            book.Author.ToLower().Contains(criteria.ToLower()) ||
                                            book.Publisher.ToLower().Contains(criteria.ToLower()));
        }
        public List<Book> GetResults()
        {
            List<Book> lstBook = new List<Book>();
            List<string> columnHeaders = new List<string>();
            IList<IWebElement> headers = DriverManager.WebDriver.FindElements(By.CssSelector("div[role='columnheader']"));
            foreach (var item in headers)
            {
                columnHeaders.Add(item.Text);
            }
            int titleIndex = columnHeaders.IndexOf("Title") + 1;
            int authorIndex = columnHeaders.IndexOf("Author") + 1;
            int publisherIndex = columnHeaders.IndexOf("Publisher") + 1;
            var rowElements = _tblResult.FindElements(By.CssSelector("div[role='rowgroup'] > div"));
            foreach (var row in rowElements)
            {
                try
                {
                    Book book = new Book
                    {
                        Title = row.FindElement(By.TagName("a")).Text,
                        Author = row.FindElements(By.CssSelector("div"))[authorIndex].Text,
                        Publisher = row.FindElements(By.CssSelector("div"))[publisherIndex].Text
                    };
                    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Publisher: {book.Publisher}");
                    lstBook.Add(book);
                }
                catch (NoSuchElementException)
                {
                    break;
                }
            }
            return lstBook;
        }

    }
}