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

using System.Linq;
using System.Threading.Tasks;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace YellowJacket.Dashboard.Repositories
{
    /// <summary>
    /// AgentRepository implementation.
    /// </summary>
    /// <seealso cref="IConfigurationRepository" />
    public class ConfigurationRepository : IConfigurationRepository
    {
        #region Private Members

        private readonly YellowJacketContext _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ConfigurationRepository(YellowJacketContext context)
        {
            _context = context;

            if (_context.Agents.Any())
                return;

            Task.Run(async () =>
            {
                await _context.Configurations.AddAsync(new ConfigurationEntity { Id = "main" });

                await _context.SaveChangesAsync();
            });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns>
        ///   <see cref="T:YellowJacket.Dashboard.Entities.ConfigurationEntity" />.
        /// </returns>
        public async Task<ConfigurationEntity> Get()
        {
            return await _context.Configurations.FirstOrDefaultAsync(t => true);
        }

        public async Task<ConfigurationEntity> Update(ConfigurationEntity configuration)
        {
            ConfigurationEntity currentEntity = await Get();

            _context.Configurations.Update(currentEntity);

            await _context.SaveChangesAsync();

            return configuration;
        }

        #endregion
    }
}
