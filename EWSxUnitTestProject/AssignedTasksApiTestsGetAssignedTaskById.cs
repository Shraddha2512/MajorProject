using Castle.Core.Logging;
using EmployeeWorkScheduler.Web.Controllers;
using EmployeeWorkScheduler.Web.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;



namespace EWSxUnitTestProject
{
    public partial class AssignedTasksApiTests
    {
        [Fact]
        public void GetCategoryByID_NotFoundResult()
        {
            var dbName = nameof(AssignedTasksApiTests.GetCategoryByID_NotFoundResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);
            int findTaskId = 900;

            IActionResult actionresult = controller.GetAssignedTask(findTaskId).Result;

            Assert.IsType<NotFoundResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound; //404
            var actualStatusCode = (actionresult as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetAssignedTaskByID_BadFoundResult()
        {
            var dbName = nameof(AssignedTasksApiTests.GetAssignedTaskByID_BadFoundResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);
            int? findTaskId = null;

            IActionResult actionresult = controller.GetAssignedTask(findTaskId).Result;

            Assert.IsType<BadRequestResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest; //404
            var actualStatusCode = (actionresult as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }



        [Fact]
        public void GetAssignedTaskById_OkResult()
        {
            var dbName = nameof(AssignedTasksApiTests.GetAssignedTaskByID_BadFoundResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);
            int findTaskId = 2;

            IActionResult actionresult = controller.GetAssignedTask(findTaskId).Result;

            Assert.IsType<OkObjectResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK; //200
            var actualStatusCode = (actionresult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetAssignedTaskById_CorrectResult()
        {

            var dbName = nameof(AssignedTasksApiTests.GetAssignedTaskById_CorrectResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);
            int findTaskId = 2;

            AssignedTask expectedAssignedTask = DbContextMocker.TestData_Description
                                            .SingleOrDefault(c => c.TaskId == findTaskId);



            IActionResult actionresult = controller.GetAssignedTask(findTaskId).Result;

            OkObjectResult result = actionresult.Should().BeOfType<OkObjectResult>().Subject;

            Assert.IsType<AssignedTask>(result.Value);

            AssignedTask pc = result.Value.Should().BeAssignableTo<AssignedTask>().Subject;//actual category
            _outputHelper.WriteLine($"Found: Task Id : {pc.TaskId}, Task Description : {pc.Description}");

            Assert.NotNull(pc);



            Assert.Equal<int>(expected: expectedAssignedTask.TaskId, actual: pc.TaskId);


            Assert.Equal(expected: expectedAssignedTask.Description, actual: pc.Description);





        }

    }
}