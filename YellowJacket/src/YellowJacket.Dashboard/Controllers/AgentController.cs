using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YellowJacket.Dashboard.Entities.Agent;
using YellowJacket.Dashboard.Repositories;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Controllers
{
    [Produces("application/json")]
    [Route("api/agent")]
    public class AgentController : BaseController
    {
        #region Private Members

        private readonly IAgentRepository _agentRepository;

        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public AgentController(IMapper mapper, IAgentRepository agentRepository)
        {
            _mapper = mapper;
            _agentRepository = agentRepository;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<AgentEntity>, IEnumerable<AgentModel>>(await _agentRepository.GetAll()));
            }
            catch (Exception ex)
            {
                HandleError(ex);

                return StatusCode(500);
            }   
        }

        [HttpPost]
        public IActionResult Post([FromBody]AgentModel model)
        {
            return Ok();
        }
    }
}