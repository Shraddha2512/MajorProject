using EmployeeWorkScheduler.Web.Models;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using EmployeeWorkScheduler.Web.Controllers;

namespace EWSxUnitTestProject
{
    public partial class AssignedTasksApiTests
    {
        [Fact]
        public void GetAssignedTasks_OkResult()
        {
            var dbName = nameof(AssignedTasksApiTests.GetAssignedTasks_OkResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);

            IActionResult actionresult = controller.GetAssignedTasks().Result;

            Assert.IsType<OkObjectResult>(actionresult);

            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionresult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetAssignedTasks_CheckCorrectResult()    // second 
        {
            var dbName = nameof(AssignedTasksApiTests.GetAssignedTasks_CheckCorrectResult);
            var logger = Mock.Of<ILogger<AssignedTasksController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var controller = new AssignedTasksController(dbContext, logger);

            IActionResult actionresult = controller.GetAssignedTasks().Result;

            Assert.IsType<OkObjectResult>(actionresult);

            var okResult = actionresult.Should().BeOfType<OkObjectResult>().Subject;

            Assert.IsAssignableFrom<List<AssignedTask>>(okResult.Value); //error can be found

            var description = okResult.Value.Should().BeAssignableTo<List<AssignedTask>>().Subject;

            Assert.NotNull(description);

            Assert.Equal(expected: DbContextMocker.TestData_Description.Length,
                        actual: description.Count);


            int ndx = 0;
            foreach (AssignedTask AssignedTask in DbContextMocker.TestData_Description)
            {
                Assert.Equal<int>(expected: AssignedTask.TaskId,
                    actual: description[ndx].TaskId);

                Assert.Equal(expected: AssignedTask.Description,
                    actual: description[ndx].Description);

                _outputHelper.WriteLine($"Row # {ndx} Okay !!! Issue Id - {AssignedTask.TaskId} Issue - {AssignedTask.Description}");
                ndx++;
            }

        }
    }
}

