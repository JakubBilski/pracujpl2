using System;
using Xunit;
using MainApp;
using MainApp.Models;
using MainApp.Controllers;
using MainApp.EntityFramework;
using MainApp.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UnitTests
{
    public class HomeControllerUnitTest : UnitTestWithEmptyContext
    {
        [Fact(DisplayName = "Index should return default view")]
        public void IndexShouldReturnDefaultView()
        {
            DataContext context = GetContext();
            HomeController controller = new HomeController(context);
            var result = controller.Index().Result as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [Fact(DisplayName = "About should return default view")]
        public void AboutShouldReturnDefaultView()
        {
            DataContext context = GetContext();
            HomeController controller = new HomeController(context);
            var result = controller.About() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "About");
        }

        [Fact(DisplayName = "About should have proper message")]
        public void AboutShouldHaveProperMessage()
        {
            DataContext context = GetContext();
            HomeController controller = new HomeController(context);
            var result = controller.About() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Your application description page.", result.ViewData["Message"]);
        }
        [Fact(DisplayName = "About should have role CANDIDATE")]
        public void AboutShouldHaveRoleCandidate()
        {
            DataContext context = GetContext();
            HomeController controller = new HomeController(context);
            var result = controller.About() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(Role.CANDIDATE, result.ViewData["role"]);
        }
        [Fact(DisplayName = "Contact should return default view")]
        public void ContactShouldReturnDefaultView()
        {
            DataContext context = GetContext();
            HomeController controller = new HomeController(context);
            var result = controller.Contact() as ViewResult;

            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "About");
        }

        [Fact(DisplayName = "Contact should have proper message")]
        public void ContactShouldHaveProperMessage()
        {
            DataContext context = GetContext();
            HomeController controller = new HomeController(context);
            var result = controller.Contact() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Your contact page.", result.ViewData["Message"]);
        }
    }
    public class ApplicationControllerUnitTest : UnitTestWithEmptyContext
    {
        [Fact(DisplayName = "Index without user should raise null reference exception")]
        public async System.Threading.Tasks.Task IndexWithoutUserShouldRaiseNullReferenceException()
        {
            DataContext context = GetContext();
            ApplicationController controller = new ApplicationController(context, null, null);
            await Assert.ThrowsAsync<NullReferenceException>(() => controller.Index(null));
        }
    }
    public class UnitTestWithEmptyContext
    {
        protected DataContext GetContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            return new DataContext(options);
        }
    }
}
