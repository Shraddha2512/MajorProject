using EmployeeWorkScheduler.Web.Data;
using EmployeeWorkScheduler.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace EWSxUnitTestProject
{
    public partial class AssignedTasksApiTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public AssignedTasksApiTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
    }
}