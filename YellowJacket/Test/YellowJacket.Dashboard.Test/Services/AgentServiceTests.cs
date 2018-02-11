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
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using YellowJacket.Common.Enums;
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
    public class AgentServiceTests : TestBase
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
        public async Task AddAgent_ValidAgent_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("AddAgent_ValidAgent_NoError")
                .Options;

            const string agentName = "MyAgent";

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                AgentModel model = new AgentModel
                {
                    Name = agentName,
                    LastUpdateOn = DateTime.Now,
                    RegisteredOn = DateTime.Now,
                    Status = AgentStatus.Idle.ToString()
                };

                await service.Add(model);
            }

            // Assert
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                const int expectedCount = 1;

                Assert.That(
                    expectedCount, Is.EqualTo(Convert.ToInt32(context.Agents.Count())),
                    $"The agents count should be {expectedCount}.");

                Assert.That(
                    agentName,
                    Is.EqualTo(context.Agents.Single().Name),
                    $"The expected agent name {agentName} doesn't match the actual value {context.Agents.Single().Name}");
            }
        }

        [Test]
        public async Task GetAllAgents_ExistingAgents_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("GetAllAgent_ExistingAgents_NoError")
                .Options;

            List<AgentModel> expectedAgents = new List<AgentModel>
            {
                new AgentModel
                {
                    Name = "AgentA",
                    LastUpdateOn = DateTime.Now,
                    RegisteredOn = DateTime.Now,
                    Status = AgentStatus.Idle.ToString()
                },
                new AgentModel
                {
                    Name = "AgentB",
                    LastUpdateOn = DateTime.Now,
                    RegisteredOn = DateTime.Now,
                    Status = AgentStatus.Idle.ToString()
                }
            };

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                foreach (AgentModel model in expectedAgents)
                {
                    await service.Add(model);
                }

                context.SaveChanges();
            }

            List<AgentModel> actualAgents;

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                actualAgents = await service.GetAll();
            }

            Assert.That(
                actualAgents.Count, 
                Is.EqualTo(2), 
                $"The actual agent list count {actualAgents.Count} should be equal to the expected one {expectedAgents.Count}");
        }

        [Test]
        public async Task UpdateAgent_ExistingAgent_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("UpdateAgent_ExistingAgent_NoError")
                .Options;

            const string agentName = "MyAgent";

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                context.Agents.Add(new AgentEntity
                {
                    Name = agentName,
                    LastUpdateOn = DateTime.Now,
                    RegisteredOn = DateTime.Now,
                    Status = AgentStatus.Idle.ToString()
                });

                context.SaveChanges();
            }

            List<AgentModel> models;

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                models = await service.GetAll();
            }

            AgentModel model = models.First();

            string expectedStatus = AgentStatus.Running.ToString();

            // Assert
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                model.Status = AgentStatus.Running.ToString();

                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                model.Status = expectedStatus;

                model = await service.Update(model);

                Assert.That(
                    model.Status,
                    Is.EqualTo(expectedStatus),
                    $"The actual agent status {model.Status} should be equal to the expected value {expectedStatus}");
            }
        }

        [Test]
        public async Task RemoveAgent_ExistingAgent_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("RemoveAgent_ExistingAgent_NoError")
                .Options;

            const string agentName = "MyAgent";

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                context.Agents.Add(new AgentEntity
                {
                    Name = agentName,
                    LastUpdateOn = DateTime.Now,
                    RegisteredOn = DateTime.Now,
                    Status = AgentStatus.Idle.ToString()
                });

                context.SaveChanges();
            }

            List<AgentModel> models;

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                models = await service.GetAll();
            }

            AgentModel model = models.First();

            // Assert
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                await service.Remove(model.Id);

                const int expectedCount = 0;

                Assert.That(
                    expectedCount,
                    Is.EqualTo(Convert.ToInt32(context.Agents.Count())),
                    $"The agents count should be {expectedCount}.");
            }
        }

        [Test]
        public async Task FindAgent_ExistingAgent_NoError()
        {
            // Arrange
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("FindAgent_ExistingAgent_NoError")
                .Options;

            const string agentName = "MyAgent";

            // Act
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                context.Agents.Add(new AgentEntity
                {
                    Name = agentName,
                    LastUpdateOn = DateTime.Now,
                    RegisteredOn = DateTime.Now,
                    Status = AgentStatus.Idle.ToString()
                });

                context.SaveChanges();
            }

            List<AgentModel> models;

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                models = await service.GetAll();
            }

            AgentModel model = models.First();

            // Assert
            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                IAgentService service = new AgentService(agentRepository, GetMapper());

                model = await service.Find(model.Id);

                Assert.That(model, !Is.Null, "The agent shouldn't be null.");
            }
        }

        #endregion
    }
}
