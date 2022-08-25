using EmployeeWorkScheduler.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EWSxUnitTestProject
{
    public partial class AssignedTasksApiTests
    {
        [Fact]
        public void DeleteAssignedTask_NotFoundResult()
        {
            var dbName = nameof(AssignedTasksApiTests.GetCategoryByID_NotFoundResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);
            int findTaskId = 900;

            IActionResult actionresultDelete = controller.DeleteAssignedTask(findTaskId).Result;

            Assert.IsType<NotFoundResult>(actionresultDelete);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound; //404
            var actualStatusCode = (actionresultDelete as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteAssignedTask_BadRequestResult()
        {


            var dbName = nameof(AssignedTasksApiTests.GetAssignedTaskByID_BadFoundResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);
            int? findTaskId = null;

            IActionResult actionresultDelete = controller.DeleteAssignedTask(findTaskId).Result;

            Assert.IsType<BadRequestResult>(actionresultDelete);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest; //400
            var actualStatusCode = (actionresultDelete as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteAssignedTask_OkResult()
        {

            var dbName = nameof(AssignedTasksApiTests.GetAssignedTaskById_OkResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);
            int findTaskId = 1;

            IActionResult actionresultDelete = controller.DeleteAssignedTask(findTaskId).Result;

            Assert.IsType<OkObjectResult>(actionresultDelete);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK; //400
            var actualStatusCode = (actionresultDelete as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }
    }
}