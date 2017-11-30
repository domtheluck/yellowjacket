using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using YellowJacket.Dashboard.Entities;

namespace YellowJacket.Dashboard.Test.Repositories
{
   [TestFixture]
    public class AgentRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            //var options = new DbContextOptionsBuilder<YellowJacketContext>()
            //    .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
            //    .Options;

            //// Run the test against one instance of the context
            //using (var context = new BloggingContext(options))
            //{
            //    var service = new BlogService(context);
            //    service.Add("http://sample.com");
            //}

            //// Use a separate instance of the context to verify correct data was saved to database
            //using (var context = new BloggingContext(options))
            //{
            //    Assert.AreEqual(1, context.Blogs.Count());
            //    Assert.AreEqual("http://sample.com", context.Blogs.Single().Url);
            //}
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void AddAgent_NameNotExist_NoError()
        {
            // arrange

            // assert

            // act

            //MethodName_StateUnderTest_ExpectedBehavior: There are arguments against this strategy that if method names change as part of code refactoring than test name like this should also change or it becomes difficult to comprehend at a later stage.Following are some of the example:

            //isAdult_AgeLessThan18_False
            //    withdrawMoney_InvalidAccount_ExceptionThrown
            //admitStudent_MissingMandatoryFields_FailToAdmit

        }
    }
}
