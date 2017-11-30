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
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using YellowJacket.Common.Enums;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Mapping;
using YellowJacket.Dashboard.Repositories;
using YellowJacket.Dashboard.Repositories.Interfaces;
using YellowJacket.Dashboard.Services;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Test.Services
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class AgentServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void AddAgent_NameNotExist_NoError()
        {
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("AddAgent_NameNotExist_NoError")
                .Options;

            const string agentName = "MyAgent";

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });

                Mapper mapper = new Mapper(config);

                AgentService service = new AgentService(agentRepository, mapper);

                AgentModel model = new AgentModel
                {
                    Name = agentName,
                    LastUpdateOn = DateTime.Now,
                    RegisteredOn = DateTime.Now,
                    Status = AgentStatus.Idle.ToString()

                };

                Task task = Task.Run(async () =>
                {
                    model = await service.Add(model);
                });

                task.Wait();
            }

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                const int expectedCount = 1;

                Assert.AreEqual(expectedCount, Convert.ToInt32(context.Agents.Count()), $"The agents count should be {expectedCount}.");

                Assert.AreEqual(
                    agentName, 
                    context.Agents.Single().Name, 
                    $"The expected agent name {agentName} doesn't match the actual one {context.Agents.Single().Name}");
            }
        }

        [Test]
        public void UpdateAgent_ExistingAgent_NoError()
        {
            DbContextOptions<YellowJacketContext> options = new DbContextOptionsBuilder<YellowJacketContext>()
                .UseInMemoryDatabase("UpdateAgent_ExistingAgent_NoError")
                .Options;

            const string agentName = "MyAgent";

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

            using (YellowJacketContext context = new YellowJacketContext(options))
            {
                IAgentRepository agentRepository = new AgentRepository(context);

                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });

                Mapper mapper = new Mapper(config);

                AgentService service = new AgentService(agentRepository, mapper);

                List<AgentModel> models = null;

                Task task = Task.Run(async () =>
                {
                    models = await service.GetAll();
                });

                task.Wait();

                const int expectedCount = 0;

                Assert.NotNull(models, "The agent model list shouldn't be empty.");

                Assert.Greater(
                    models.Count, 
                    0, 
                    $"The agent model list count {models.Count} must be greater than the expected value {expectedCount}.");
            }

            using (YellowJacketContext context = new YellowJacketContext(options))
            {

                Assert.AreEqual(1, context.Agents.Count());
                Assert.AreEqual(agentName, context.Agents.Single().Name);
            }
        }
    }
}
