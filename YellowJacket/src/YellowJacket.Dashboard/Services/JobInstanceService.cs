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
using AutoMapper;
using FluentValidation.Results;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;
using YellowJacket.Dashboard.Services.Interfaces;
using YellowJacket.Dashboard.Validators.JobInstance;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Services
{
    public class JobInstanceService : IJobInstanceService
    {
        #region Private Members

        private readonly IJobInstanceRepository _jobInstanceRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JobInstanceService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public JobInstanceService(IJobInstanceRepository repository, IMapper mapper)
        {
            _jobInstanceRepository = repository;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <see cref="T:FluentValidation.Results.ValidationResult" />.
        /// </returns>
        public ValidationResult Validate(JobInstanceModel model)
        {
            return new JobInstanceValidator().Validate(model);
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the specified job instance to the repository.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// <see cref="T:YellowJacket.Models.JobInstanceModel" />.
        /// </returns>
        public async Task<JobInstanceModel> Add(JobInstanceModel model)
        {
            return _mapper.Map<JobInstanceModel>(
                await _jobInstanceRepository.Add(
                    _mapper.Map<JobInstanceEntity>(model)));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the first job instance available.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>
        /// <see cref="T:YellowJacket.Models.JobInstanceModel" />.
        /// </returns>
        public async Task<JobInstanceModel> GetFirstAvailable(string agentId)
        {
            return _mapper.Map<JobInstanceModel>(
                await _jobInstanceRepository.GetFirstAvailable(agentId));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all job instances from the repository.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Collections.Generic.List`1" />.
        /// </returns>
        public async Task<List<JobInstanceModel>> GetAll()
        {
            List<JobInstanceEntity> entities = await _jobInstanceRepository.GetAll();

            return _mapper.Map<List<JobInstanceEntity>, List<JobInstanceModel>>(entities);
        }

        /// <inheritdoc />
        /// <summary>
        /// Finds a job instance by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// <see cref="T:YellowJacket.Models.JobInstanceModel" />.
        /// </returns>
        public async Task<JobInstanceModel> Find(string id)
        {
            return _mapper.Map<JobInstanceEntity, JobInstanceModel>(
                await _jobInstanceRepository.Find(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the specified job instancefrom the repository.
        /// </summary>
        /// <param name="id">The id of the job instance to remove.</param>
        /// <returns>
        ///   <see cref="T:System.Threading.Tasks.Task" />.
        /// </returns>
        public async Task Remove(string id)
        {
            await _jobInstanceRepository.Remove(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the specified job instance.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <see cref="T:YellowJacket.Models.JobInstanceModel" />.
        /// </returns>
        public async Task<JobInstanceModel> Update(JobInstanceModel model)
        {
            return _mapper.Map<JobInstanceEntity, JobInstanceModel>(
                await _jobInstanceRepository.Update(
                    _mapper.Map<JobInstanceModel, JobInstanceEntity>(model)));
        }

        #endregion
    }
}
