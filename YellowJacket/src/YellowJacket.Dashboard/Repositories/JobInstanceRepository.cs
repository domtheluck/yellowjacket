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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using YellowJacket.Common.Enums;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;

namespace YellowJacket.Dashboard.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// JobInstanceRepository implementation.
    /// </summary>
    /// <seealso cref="IJobInstanceRepository" />
    public class JobInstanceRepository : IJobInstanceRepository
    {
        #region Private Members

        private readonly YellowJacketContext _context;

        private readonly AsyncLock _lock = new AsyncLock();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JobInstanceRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public JobInstanceRepository(YellowJacketContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Adds the specified job instance to the repository.
        /// </summary>
        /// <param name="jobInstance">The job instance.</param>
        /// <returns>
        ///   <see cref="JobInstanceEntity" />.
        /// </returns>
        public async Task<JobInstanceEntity> Add(JobInstanceEntity jobInstance)
        {
            await _context.JobInstances.AddAsync(jobInstance);

            await _context.SaveChangesAsync();

            return jobInstance;
        }

        /// <summary>
        /// Gets the first job instance available.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>
        /// <see cref="JobInstanceEntity" />.
        /// </returns>
        public async Task<JobInstanceEntity> GetFirstAvailable(string agentId)
        {
            using (await _lock.LockAsync())
            {
                JobInstanceEntity entity = await _context.JobInstances
                    .OrderBy(x => x.CreatedOn)
                    .FirstOrDefaultAsync(x => x.Status == JobInstanceStatus.New.ToString());

                if (entity == null)
                    return null;

                entity.AgentId = agentId;

                entity.Status = JobInstanceStatus.InProgress.ToString();

                await Update(entity);

                return entity;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Finds an job instance by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <see cref="JobInstanceEntity" />.
        /// </returns>
        public async Task<JobInstanceEntity> Find(string id)
        {
            return await _context.JobInstances.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Updates the specified job instance.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<JobInstanceEntity> Update(JobInstanceEntity entity)
        {
            JobInstanceEntity currentEntity = await Find(entity.Id);

            _context.JobInstances.Update(currentEntity);

            await _context.SaveChangesAsync();

            return entity;
        }

        #endregion 
    }
}
