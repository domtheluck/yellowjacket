﻿// ***********************************************************************
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
using YellowJacket.Dashboard.Entities;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Repositories.Interfaces
{
    /// <summary>
    /// Job Instance repository interface definition.
    /// </summary>
    public interface IJobInstanceRepository
    {
        /// <summary>
        /// Adds the specified job instance to the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><see cref="JobInstanceEntity"/>.</returns>
        Task<JobInstanceEntity> Add(JobInstanceEntity entity);

        /// <summary>
        /// Gets all job instances from the repository.
        /// </summary>
        /// <returns><see cref="List{T}"/>.</returns>
        Task<List<JobInstanceEntity>> GetAll();

        /// <summary>
        /// Gets the first job instance available.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>
        ///   <see cref="JobInstanceEntity" />.
        /// </returns>
        Task<JobInstanceEntity> GetFirstAvailable(string agentId);

        /// <summary>
        /// Finds an job instance by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><see cref="JobInstanceEntity"/>.</returns>
        Task<JobInstanceEntity> Find(string id);

        /// <summary>
        /// Removes the specified job from the repository.
        /// </summary>
        /// <param name="id">The id of the job to remove.</param>
        Task Remove(string id);

        /// <summary>
        /// Updates the specified job instance.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task<JobInstanceEntity> Update(JobInstanceEntity entity);
    }
}
