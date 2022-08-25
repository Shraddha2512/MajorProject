using EmployeeWorkScheduler.Web.Data;
using EmployeeWorkScheduler.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace EWSxUnitTestProject
{
    public static class DbContextMocker
    {
        public static ApplicationDbContext GetApplicationDbContext(string databasename)
        {
            // Create a fresh service provider for the InMemory Database instance.
            var serviceProvider = new ServiceCollection()
                                  .AddEntityFrameworkInMemoryDatabase()
                                  .BuildServiceProvider();

            // Create a new options instance,
            // telling the context to use InMemory database and the new service provider.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databasename)
                            .UseInternalServiceProvider(serviceProvider)
                            .Options;

            // Create the instance of the DbContext (would be an InMemory database)
            // NOTE: It will use the Scema as defined in the Data and Models folders
            var dbContext = new ApplicationDbContext(options);

            // Add entities to the inmemory database
            dbContext.SeedData();

            return dbContext;
        }
        internal static readonly AssignedTask[] TestData_Description
            = {
                new AssignedTask
                {
                    TaskId = 1,
                    Description = "First Description"

                },
                new AssignedTask
                {

                    TaskId= 2,
                     Description = "Second Description"

                },
                new AssignedTask
                {

                    TaskId = 3,
                     Description = "Third Description"

                },
                new AssignedTask
                {
                    TaskId = 4,
                     Description = "Fourth Description"

                }
            };

        private static void SeedData(this ApplicationDbContext context)
        {
            context.AssignedTasks.AddRange(TestData_Description);

            context.SaveChanges();
        }
    }
}
