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
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Repositories.Interfaces;
using YellowJacket.Dashboard.Services.Interfaces;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Services
{
    public class AgentService : IAgentService
    {
        #region Private Members

        private readonly IAgentRepository _agentRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public AgentService(IAgentRepository repository, IMapper mapper)
        {
            _agentRepository = repository;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Adds the specified agent to the repository.
        /// </summary>
        /// <param name="model">The model to add.</param>
        /// <returns>
        ///   <see cref="AgentModel" />.
        /// </returns>
        public async Task<AgentModel> Add(AgentModel model)
        {
            return _mapper.Map<AgentEntity, AgentModel>(
                await _agentRepository.Add(
                    _mapper.Map<AgentModel, AgentEntity>(model)));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all agents from the repository.
        /// </summary>
        /// <returns>
        ///   <see cref="List{AgentModel}" />.
        /// </returns>
        public async Task<List<AgentModel>> GetAll()
        {
            List<AgentEntity> entities = await _agentRepository.GetAll();

            return _mapper.Map<List<AgentEntity>, List<AgentModel>>(entities);
        }

        /// <inheritdoc />
        /// <summary>
        /// Finds an agent by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <see cref="AgentModel" />.
        /// </returns>
        public async Task<AgentModel> Find(string id)
        {
            return _mapper.Map<AgentEntity, AgentModel>(
                await _agentRepository.Find(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the specified agent from the repository.
        /// </summary>
        /// <param name="id">The id of the agent to remove.</param>
        /// <returns>
        ///   <see cref="Task" />.
        /// </returns>
        public async Task Remove(string id)
        {
            await _agentRepository.Remove(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the specified agent.
        /// </summary>
        /// <param name="model">The entity to update</param>
        /// <returns>
        ///   <see cref="AgentModel" />.
        /// </returns>
        public async Task<AgentModel> Update(AgentModel model)
        {
            return _mapper.Map<AgentEntity, AgentModel>(
                await _agentRepository.Update(
                    _mapper.Map<AgentModel, AgentEntity>(model)));
        }

        #endregion
    }
}
