// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories;
using YellowJacket.Dashboard.Repositories.Interfaces;
using YellowJacket.Dashboard.Services;
using YellowJacket.Dashboard.Services.Interfaces;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Test.Services
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    [Category("Service")]
    public class JobServiceTests : TestBase
    {
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        #region Test Methods

        [Test]
        public async Task AddJob_ValidJob_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("AddJob_ValidJob_NoError")
                .Options;

            const string jobName = "MyJob";

            JobModel model = new JobModel
            {
                Name = jobName,
                Features = new List<string>(),
                PackageName = "MyPackage.zip",
            };

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                await service.Add(model);
            }

            // Assert
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                const int expectedCount = 1;

                Assert.That(
                    expectedCount, Is.EqualTo(Convert.ToInt32(context.Jobs.Count())),
                    $"The job count should be {expectedCount}.");

                Assert.That(
                    jobName,
                    Is.EqualTo(context.Jobs.Single().Name),
                    $"The expected job name {jobName} doesn't match the actual value {context.Jobs.Single().Name}");
            }

        }

        [Test]
        public void ValidateJob_EmptyName_ValidationError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("AddJob_EmptyName_ValidationError")
                .Options;

            JobModel model = new JobModel
            {
                Features = new List<string>(),
                PackageName = "MyPackage.zip",
            };

            // Act
            ValidationResult validationResult;

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                validationResult = service.Validate(model);
            }

            // Assert
            const int expectedValidationErrorCount = 1;
            const string errorMessage = "'Name' should not be empty.";

            Assert.That(validationResult.Errors.Count, Is.EqualTo(expectedValidationErrorCount),
                $"The validation error count should be {expectedValidationErrorCount}");

            Assert.That(validationResult.Errors.First().ErrorMessage, Is.EqualTo(errorMessage),
                $"The expected error message '{errorMessage}' doesn't match the actual one {validationResult.Errors.First().ErrorMessage}");
        }

        [Test]
        public void ValidateJob_NameLongerThan25Characters_ValidationError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("ValidateJob_NameLongerThan25Characters_ValidationError")
                .Options;

            const string jobName = "abcdefghijklmnopqrstuvwxyz";

            JobModel model = new JobModel
            {
                Name = jobName,
                Features = new List<string>(),
                PackageName = "MyPackage.zip",
            };

            // Act
            ValidationResult validationResult;

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                validationResult = service.Validate(model);
            }

            // Assert
            const int expectedValidationErrorCount = 1;
            const string errorMessage = "'Name' must be between 1 and 25 characters. You entered 26 characters.";

            Assert.That(validationResult.Errors.Count, Is.EqualTo(expectedValidationErrorCount),
                $"The validation error count should be {expectedValidationErrorCount}");

            Assert.That(validationResult.Errors.First().ErrorMessage, Is.EqualTo(errorMessage),
                $"The expected error message '{errorMessage}' doesn't match the actual one {validationResult.Errors.First().ErrorMessage}");
        }

        [Test]
        public async Task GetAllJobs_ExistingJobs_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("GetAllJob_ExistingJobs_NoError")
                .Options;

            List<JobModel> expectedJobs = new List<JobModel>
            {
                new JobModel
                {
                    Name = "JobA"
                },
                new JobModel
                {
                    Name = "JobB"
                }
            };

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                foreach (JobModel model in expectedJobs)
                {
                    await service.Add(model);
                }

                context.SaveChanges();
            }

            List<JobModel> actualJobs;

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                actualJobs = await service.GetAll();
            }

            Assert.That(
                actualJobs.Count,
                Is.EqualTo(2),
                $"The actual job list count {actualJobs.Count} should be equal to the expected one {expectedJobs.Count}");
        }

        [Test]
        public async Task FindJob_ExistingJob_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("FindJob_ExistingJob_NoError")
                .Options;

            const string jobName = "MyJob";

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                context.Jobs.Add(new JobEntity
                {
                    Name = jobName
                });

                context.SaveChanges();
            }

            List<JobModel> models;

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                models = await service.GetAll();
            }

            JobModel model = models.First();

            // Assert
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                model = await service.Find(model.Id);

                Assert.That(model, !Is.Null, "The job shouldn't be null.");
            }
        }

        [Test]
        public async Task RemoveJob_ExistingJob_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("RemoveJob_ExistingJob_NoError")
                .Options;

            const string jobName = "MyJob";

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                context.Jobs.Add(new JobEntity
                {
                    Name = jobName
                });

                context.SaveChanges();
            }

            List<JobModel> models;

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                models = await service.GetAll();
            }

            JobModel model = models.First();

            // Assert
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IJobRepository jobRepository = new JobRepository(context);

                IJobService service = new JobService(jobRepository, GetMapper());

                await service.Remove(model.Id);

                const int expectedCount = 0;

                Assert.That(
                    expectedCount,
                    Is.EqualTo(Convert.ToInt32(context.Jobs.Count())),
                    $"The jobs count should be {expectedCount}.");
            }
        }

        #endregion
    }
}
