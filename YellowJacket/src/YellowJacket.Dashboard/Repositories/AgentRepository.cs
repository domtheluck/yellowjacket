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
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Entities.Agent;

namespace YellowJacket.Dashboard.Repositories
{
    /// <summary>
    /// AgentRepository implementation.
    /// </summary>
    /// <seealso cref="IAgentRepository" />
    public class AgentRepository : IAgentRepository
    {
        #region Private Members

        private readonly YellowJacketContext _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AgentRepository(YellowJacketContext context)
        {
            _context = context;

            if (_context.Agents.Any())
                return;

           // Task.Run(async () =>
           //{
           //    await Add(new AgentEntity { Id = "VM0002", Name = "Agent 2", Status = "Running", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now});
           //    await Add(new AgentEntity { Id = "VM0003", Name = "Agent 3", Status = "Idle", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0004", Name = "Agent 4", Status = "Running", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0005", Name = "Agent 5", Status = "Idle", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0006", Name = "Agent 6", Status = "Idle", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0007", Name = "Agent 7", Status = "Running", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0008", Name = "Agent 8", Status = "Idle", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0009", Name = "Agent 9", Status = "Running", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0010", Name = "Agent 10", Status = "Idle", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0011", Name = "Agent 11", Status = "Idle", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0012", Name = "Agent 12", Status = "Running", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0013", Name = "Agent 13", Status = "Idle", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0014", Name = "Agent 14", Status = "Running", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //    await Add(new AgentEntity { Id = "VM0015", Name = "Agent 15", Status = "Idle", RegisteredOn = DateTime.Now, LastUpdateOn = DateTime.Now });
           //});   
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <see cref="AgentEntity" />.
        /// </returns>
        public async Task<AgentEntity> Add(AgentEntity entity)
        {
            await _context.Agents.AddAsync(entity);
            await RemoveInvalidAgents(entity.Id);

            await _context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Get a list of all entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AgentEntity>> GetAll()
        {
            return await _context.Agents.ToListAsync();
        }

        /// <summary>
        /// Finds an agent by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <see cref="AgentEntity" />.
        /// </returns>
        public async Task<AgentEntity> Find(string id)
        {
            return await _context.Agents.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="id">The id of the entity to remove.</param>
        public async Task Remove(string id)
        {
            AgentEntity entity = await _context.Agents.FirstAsync(t => t.Id == id);

            _context.Agents.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<AgentEntity> Update(AgentEntity entity)
        {
             AgentEntity currentEntity = await Find(entity.Id);

            currentEntity.LastUpdateOn = entity.LastUpdateOn;

            _context.Agents.Update(currentEntity);

            await RemoveInvalidAgents(currentEntity.Id);
            await _context.SaveChangesAsync();

            return entity;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Removes the invalid agents.
        /// </summary>
        /// <param name="currentAgentId">The current agent identifier.</param>
        /// <returns></returns>
        public async Task RemoveInvalidAgents(string currentAgentId)
        {
            // TODO: the difference threshold need to be dynamic
            List<AgentEntity> entities = await _context.Agents
                .Where(x => x.Id != currentAgentId && (x.LastUpdateOn - x.RegisteredOn).TotalSeconds >= 60) // TODO: need to be dynamic
                .ToListAsync();

            foreach (AgentEntity entity in entities)
                _context.Agents.Remove(entity);
        }

        #endregion
    }
}
