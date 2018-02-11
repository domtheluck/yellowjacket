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

using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Services.Interfaces
{
    public interface IJobInstanceService
    {
        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><see cref="ValidationResult"/>.</returns>
        ValidationResult Validate(JobInstanceModel model);

        /// <summary>
        /// Adds the specified job instance to the repository.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <see cref="JobInstanceModel" />.
        /// </returns>
        Task<JobInstanceModel> Add(JobInstanceModel model);

        /// <summary>
        /// Gets the first job instance available.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>
        ///   <see cref="JobInstanceModel" />.
        /// </returns>
        Task<JobInstanceModel> GetFirstAvailable(string agentId);

        /// <summary>
        /// Gets all job instances from the repository.
        /// </summary>
        /// <returns>
        ///   <see cref="List{JobInstanceModel}" />.
        /// </returns>
        Task<List<JobInstanceModel>> GetAll();

        /// <summary>
        /// Finds a job instance by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <see cref="JobInstanceModel" />.
        /// </returns>
        Task<JobInstanceModel> Find(string id);

        /// <summary>
        /// Removes the specified job instancefrom the repository.
        /// </summary>
        /// <param name="id">The id of the job instance to remove.</param>
        /// <returns><see cref="Task" />.</returns>
        Task Remove(string id);

        /// <summary>
        /// Updates the specified job instance.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><see cref="JobInstanceModel"/>.</returns>
        Task<JobInstanceModel> Update(JobInstanceModel model);
    }
}
