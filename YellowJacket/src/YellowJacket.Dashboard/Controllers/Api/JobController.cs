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
using Microsoft.AspNetCore.Mvc;
using YellowJacket.Dashboard.Entities.Job;
using YellowJacket.Dashboard.Models.Job;
using YellowJacket.Dashboard.Repositories.Interfaces;

namespace YellowJacket.Dashboard.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/v1/job")]
    public class JobController : BaseController
    {
        #region Private Members

        private readonly IJobRepository _jobRepository;

        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JobController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="jobRepository">The job repository.</param>
        public JobController(IMapper mapper, IJobRepository jobRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        #endregion

        #region Public Methods

        [HttpGet("{id}", Name = "GetJobById")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                JobEntity entity = await _jobRepository.Find(id);

                if (entity == null)
                    return StatusCode(404);

                return Ok(_mapper.Map<JobEntity, JobModel>(entity));
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500, id);
            }
        }

        [HttpGet(Name="GetAllJobs")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<JobEntity>, IEnumerable<JobModel>>(await _jobRepository.GetAll()));
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500);
            }
        }

        [HttpPut("{id}", Name="PutJob")]
        public async Task<IActionResult> Put(string id, [FromBody] JobModel model)
        {
            try
            {
                JobEntity entity = await _jobRepository.Update(_mapper.Map<JobModel, JobEntity>(model));

                return Ok(_mapper.Map<JobEntity, JobModel>(entity));
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500, model);
            }
        }

        [HttpPost(Name="PostJob")]
        public async Task<IActionResult> Post([FromBody]JobModel model)
        {
            try
            {
              JobEntity entity = await _jobRepository.Find(model.Id);

                if (entity != null)
                    await _jobRepository.Remove(entity.Id);

                entity = await _jobRepository.Add(_mapper.Map<JobModel, JobEntity>(model));

                return CreatedAtRoute(
                    "Get",
                    new { id = entity.Id },
                    _mapper.Map<JobEntity, JobModel>(entity));
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500, model);
            }
        }

        #endregion
    }
}