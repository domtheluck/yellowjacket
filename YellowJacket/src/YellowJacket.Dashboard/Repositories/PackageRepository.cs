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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;

namespace YellowJacket.Dashboard.Repositories
{
    /// <summary>
    /// PackageRepository implementation.
    /// </summary>
    /// <seealso cref="IJobRepository" />
    public class PackageRepository : IPackageRepository
    {
        #region Constants

        private const string PackageExtension = ".zip";
        private const string PackageConfigurationExtension = ".json";

        private const string PackageFolder = "Test";

        #endregion

        #region Private Members

        private readonly ConfigurationSettings _configuration;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public PackageRepository(IOptions<ConfigurationSettings> configuration)
        {
            _configuration = configuration.Value;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Finds a package by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <see cref="PackageEntity" />.
        /// </returns>
        public async Task<PackageEntity> Find(string id)
        {
            string testPackagePath = Path.Combine(_configuration.PackagesRootPath, PackageFolder);

            PackageEntity entity = null;

            await Task.Run(() =>
            {
                FileInfo package =
                    new DirectoryInfo(testPackagePath)
                    .GetFiles($"*{PackageExtension}")
                    .ToList()
                    .FirstOrDefault(x => x.Name.ToLowerInvariant().Equals($"{id}.zip"));

                if (package == null)
                    return;

                entity = GetPackageInformation(package);
            });

            return entity;
        }

        /// <summary>
        /// Gets all package from the repository.
        /// </summary>
        /// <returns>
        /// <see cref="IEnumerable{PackageEntity}"/>.
        /// </returns>
        public async Task<IEnumerable<PackageEntity>> GetAll()
        {
            string testPackagePath = Path.Combine(_configuration.PackagesRootPath, PackageFolder);

            List<PackageEntity> results = new List<PackageEntity>();

            await Task.Run(() =>
            {
                List<FileInfo> packages =
                    new DirectoryInfo(testPackagePath).GetFiles($"*{PackageExtension}").ToList();

                results.AddRange(packages.Select(GetPackageInformation));
            });

            return results;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the package information.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <returns><see cref="PackageEntity"/>.</returns>
        private static PackageEntity GetPackageInformation(FileInfo package)
        {
            string packageConfigurationPath =
                $"{Path.Combine(package.DirectoryName, Path.GetFileNameWithoutExtension(package.Name))}{PackageConfigurationExtension}";

            PackageEntity entity = JsonConvert.DeserializeObject<PackageEntity>(File.ReadAllText(packageConfigurationPath));

            entity.Name = Path.GetFileNameWithoutExtension(package.Name);

            return entity;
        }

        #endregion
    }
}