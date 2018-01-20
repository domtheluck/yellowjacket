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
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using YellowJacket.Dashboard.Repositories;
using YellowJacket.Dashboard.Repositories.Interfaces;
using YellowJacket.Dashboard.Services;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Test.Services
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
   // [Parallelizable(ParallelScope.All)]
    public class PackageServiceTests: TestBase
    {
        #region Private Members

        private string _packageRootPath;

        #endregion

        [SetUp]
        public void Setup()
        {
            _packageRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Packages");
        }

        [TearDown]
        public void TearDown()
        {

        }

        #region Test Methods

        [Test]
        public async Task GetAllPackage_ExistingPackage_NoError()
        {
            // Arrange
            const int expectedPackageCount = 1;

            ConfigurationSettings configurationsSettings =
                new ConfigurationSettings { PackagesRootPath = _packageRootPath };

            IOptions<ConfigurationSettings> options = Options.Create(configurationsSettings);

            // Act
            IPackageRepository packageRepository = new PackageRepository(options);

            PackageService service = new PackageService(packageRepository, GetMapper());

            List<PackageModel> packages = await service.GetAll();

            // Assert
            Assert.That(packages, !Is.Null, "The package list shouldn't be null.");

            Assert.That(
                packages.Count,
                Is.EqualTo(expectedPackageCount),
                $"The actual package count {packages.Count} is not equal to the expected value {expectedPackageCount}.");
        }

        [Test]
        public async Task FindPackage_ExistingPackage_NoError()
        {
            // Arrange
            const string packageName = "SimpleFeature";

            ConfigurationSettings configurationsSettings =
                new ConfigurationSettings { PackagesRootPath = _packageRootPath };

            IOptions<ConfigurationSettings> options = Options.Create(configurationsSettings);

            // Act
            IPackageRepository packageRepository = new PackageRepository(options);

            PackageService service = new PackageService(packageRepository, GetMapper());

            PackageModel package = await service.Find(packageName);

            // Assert
            Assert.That(package, !Is.Null, "The package shouldn't be null.");

            Assert.That(
                package.Name,
                Is.EqualTo(packageName),
                $"The actual package name {package.Name} is not equal to the expected value {packageName}.");
        }

        #endregion
    }
}
