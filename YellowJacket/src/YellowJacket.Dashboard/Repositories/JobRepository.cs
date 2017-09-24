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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;

namespace YellowJacket.Dashboard.Repositories
{
    /// <summary>
    /// JobRepository implementation.
    /// </summary>
    /// <seealso cref="IJobRepository" />
    public class JobRepository : IJobRepository
    {
        #region Private Members

        private readonly YellowJacketContext _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JobRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public JobRepository(YellowJacketContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified job to the repository.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns>
        ///   <see cref="JobEntity" />.
        /// </returns>
        public async Task<JobEntity> Add(JobEntity job)
        {
            await _context.Jobs.AddAsync(job);

            await _context.SaveChangesAsync();

            return job;
        }

        /// <summary>
        /// Gets all jobs from the repository.
        /// </summary>
        /// <returns>
        ///   <see cref="IEnumerable{JobEntity}" />.
        /// </returns>
        public async Task<IEnumerable<JobEntity>> GetAll()
        {
            return await _context.Jobs.ToListAsync();
        }

        /// <summary>
        /// Finds a job by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <see cref="JobEntity" />.
        /// </returns>
        public async Task<JobEntity> Find(string id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Removes the specified job from the repository.
        /// </summary>
        /// <param name="id">The id of the job to remove.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task Remove(string id)
        {
            JobEntity entity = await _context.Jobs.FirstAsync(t => t.Id == id);

            _context.Jobs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns><see cref="JobEntity"/>.</returns>
        public async Task<JobEntity> Update(JobEntity job)
        {
            JobEntity currentEntity = await Find(job.Id);

            _context.Jobs.Update(currentEntity);

            await _context.SaveChangesAsync();

            return job;
        }

        #endregion 
    }
}
