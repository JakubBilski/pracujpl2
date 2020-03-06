using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace AutomatedUITests
{
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        public AutomatedUITests()
        {
            _driver = new ChromeDriver(@"C:\Users\Kuba\source\repos\HRMiniProject\MainApp\AutomatedUITests\bin\Debug\netcoreapp2.1");
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void Home_WhenExecuted_ReturnsGoodTitle()
        {
            _driver.Navigate().GoToUrl("https://localhost:44391/home");
            Assert.Equal("Home Page - MainApp", _driver.Title);
        }

        [Fact]
        public void Home_WhenExecuted_ContainsWelcome()
        {
            _driver.Navigate().GoToUrl("https://localhost:44391/home");
            var element =  _driver.FindElement(By.ClassName("display-4"));
            Assert.Equal("Welcome", element.Text);
        }
        
        [Fact]
        public void Home_WhenExecuted_ContainsNavbar()
        {
            _driver.Navigate().GoToUrl("https://localhost:44391/home");
            var element = _driver.FindElement(By.CssSelector("ul.navbar-nav.flex-grow-1"));
            var navItems = element.FindElements(By.ClassName("nav-item"));
            Assert.Equal(5, navItems.Count);
        }
        
        [Fact]
        public void Home_WhenExecuted_ContainsSignIn()
        {
            _driver.Navigate().GoToUrl("https://localhost:44391/home");
            var element = _driver.FindElement(By.CssSelector("a.nav-link.text-dark"));
            Assert.Equal("Sign in", element.Text);
        }
        
        [Fact]
        public void Home_WhenExecuted_NavbarBrandNotOverlap()
        {
            _driver.Navigate().GoToUrl("https://localhost:44391/home");
            var navbarElementList = _driver.FindElement(
                By.CssSelector("div.navbar-collapse.collapse.d-sm-inline-flex.flex-sm-row-reverse"));
            var navbarBrand = _driver.FindElement(
                By.CssSelector("a.navbar-brand"));
            bool overlap = navbarElementList.Location.X < navbarBrand.Location.X + navbarBrand.Size.Width;
            Assert.False(overlap);
        }

        [Fact]
        public void Privacy_WhenExecuted_ReturnsGoodTitle()
        {
            _driver.Navigate().GoToUrl("https://localhost:44391/home/privacy");
            Assert.Equal("Privacy Policy - MainApp", _driver.Title);
        }
    }
}