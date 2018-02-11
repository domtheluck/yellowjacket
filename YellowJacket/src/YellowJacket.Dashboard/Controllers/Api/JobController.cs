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
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YellowJacket.Dashboard.Services.Interfaces;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/v1/job")]
    public class JobController : BaseController
    {
        #region Private Members

        private readonly IJobService _jobService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JobController"/> class.
        /// </summary>
        /// <param name="jobService">The job service.</param>
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        #endregion

        #region Public Methods

        [HttpGet("{id}", Name = "GetJobById")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                JobModel model = await _jobService.Find(id);

                if (model == null)
                    return StatusCode(404);

                return Ok(model);
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500, id);
            }
        }

        [HttpGet(Name = "GetAllJobs")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _jobService.GetAll());
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500);
            }
        }

        [HttpPut("{id}", Name = "PutJob")]
        public async Task<IActionResult> Put(string id, [FromBody] JobModel model)
        {
            try
            {
                model = await _jobService.Update(model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500, model);
            }
        }

        [HttpPost(Name = "PostJob")]
        public async Task<IActionResult> Post([FromBody]JobModel model)
        {
            try
            {
                if (!_jobService.Validate(model).IsValid)
                    return StatusCode(400, model);

                PostProcessModel(model);

                model = await _jobService.Add(model);

                return StatusCode(200, model);
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500, model);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Prepares the model before sending it to the repository.
        /// </summary>
        /// <param name="model">The model.</param>
        private void PostProcessModel(JobModel model)
        {
            model.SerializedFeatures = JsonConvert.SerializeObject(model.Features);
        }

        #endregion
    }
}