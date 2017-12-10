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
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;
using YellowJacket.Dashboard.Services.Interfaces;
using YellowJacket.Dashboard.Validators.Job;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Services
{
    public class JobService : IJobService
    {
        #region Private Members

        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JobService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public JobService(IJobRepository repository, IMapper mapper)
        {
            _jobRepository = repository;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><see cref="ValidationResult"/>.</returns>
        public ValidationResult Validate(JobModel model)
        {
            return new JobValidator().Validate(model);
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the specified job to the repository.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns>
        /// <see cref="JobModel" />.
        /// </returns>
        public async Task<JobModel> Add(JobModel job)
        {
            return _mapper.Map<JobModel>(
                await _jobRepository.Add(
                    _mapper.Map<JobEntity>(job)));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all jobs from the repository.
        /// </summary>
        /// <returns>
        /// <see cref="List{JobModel}" />.
        /// </returns>
        public async Task<List<JobModel>> GetAll()
        {
            List<JobEntity> entities = await _jobRepository.GetAll();

            return _mapper.Map<List<JobEntity>, List<JobModel>>(entities);
        }

        /// <inheritdoc />
        /// <summary>
        /// Finds a job by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// <see cref="JobModel" />.
        /// </returns>
        public async Task<JobModel> Find(string id)
        {
            return _mapper.Map<JobEntity, JobModel>(
                await _jobRepository.Find(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the specified job from the repository.
        /// </summary>
        /// <param name="id">The id of the job to remove.</param>
        /// <returns>
        ///   <see cref="Task" />.
        /// </returns>
        public async Task Remove(string id)
        {
            await _jobRepository.Remove(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the specified job.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <see cref="JobModel" />.
        /// </returns>
        public async Task<JobModel> Update(JobModel model)
        {
            return _mapper.Map<JobEntity, JobModel>(
                await _jobRepository.Update(
                    _mapper.Map<JobModel, JobEntity>(model)));
        }

        #endregion
    }
}
