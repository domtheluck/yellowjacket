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
using Microsoft.AspNetCore.Mvc;
using YellowJacket.Dashboard.Services.Interfaces;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/v1/agent")]
    public class AgentController : BaseController
    {
        #region Private Members

        private readonly IAgentService _agentService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentController"/> class.
        /// </summary>
        /// <param name="agentService">The agent service.</param>
        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        #endregion

        #region Public Methods

        [HttpGet("{id}", Name = "GetAgentById")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                AgentModel model = await _agentService.Find(id);

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

        [HttpGet(Name="GetAllAgents")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _agentService.GetAll());
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500);
            }
        }

        [HttpPut("{id}", Name = "PutAgent")]
        public async Task<IActionResult> Put(string id, [FromBody] AgentModel model)
        {
            try
            {
                model = await _agentService.Update(model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500, model);
            }
        }

        [HttpPost(Name = "PostAgent")]
        public async Task<IActionResult> Post([FromBody]AgentModel model)
        {
            try
            {
                model = await _agentService.Find(model.Id);

                if (model != null)
                    await _agentService.Remove(model.Id);

                model = await _agentService.Add(model);

                return CreatedAtRoute(
                    "GetAgentById",
                    new { id = model.Id },
                    model);
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