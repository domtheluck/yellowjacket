using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YellowJacket.Dashboard.Models.Agent;
using YellowJacket.Dashboard.Repositories;

namespace YellowJacket.Dashboard.Controllers
{
    [Produces("application/json")]
    [Route("api/agent")]
    public class AgentController : Controller
    {
        #region Private Members

        private IAgentRepository _agentRepository;

        #endregion

        #region Constructors

        public AgentController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _agentRepository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }   
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AgentModel model)
        {
            return Ok();
        }
    }
}