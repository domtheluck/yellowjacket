using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YellowJacket.Dashboard.Entities;
using YellowJacket.Dashboard.Entities.Agent;

namespace YellowJacket.Dashboard.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly YellowJacketContext _context;

        public AgentRepository(YellowJacketContext context)
        {
            _context = context;

            if (_context.Agents.Any())
                return;

            Add(new AgentEntity { Id = "VM0001", Name = "Agent 1", Status = "Idle" });
            Add(new AgentEntity { Id = "VM0002", Name = "Agent 2", Status = "Running" });
            Add(new AgentEntity { Id = "VM0003", Name = "Agent 3", Status = "Idle" });
            Add(new AgentEntity { Id = "VM0004", Name = "Agent 4", Status = "Running" });
            Add(new AgentEntity { Id = "VM0005", Name = "Agent 5", Status = "Idle" });
            Add(new AgentEntity { Id = "VM0006", Name = "Agent 6", Status = "Idle" });
            Add(new AgentEntity { Id = "VM0007", Name = "Agent 7", Status = "Running" });
            Add(new AgentEntity { Id = "VM0008", Name = "Agent 8", Status = "Idle" });
            Add(new AgentEntity { Id = "VM0009", Name = "Agent 9", Status = "Running" });
            Add(new AgentEntity { Id = "VM0010", Name = "Agent 10", Status = "Idle" });
            Add(new AgentEntity { Id = "VM0011", Name = "Agent 11", Status = "Idle" });
            Add(new AgentEntity { Id = "VM0012", Name = "Agent 12", Status = "Running" });
            Add(new AgentEntity { Id = "VM0013", Name = "Agent 13", Status = "Idle" });
            Add(new AgentEntity { Id = "VM0014", Name = "Agent 14", Status = "Running" });
            Add(new AgentEntity { Id = "VM0015", Name = "Agent 15", Status = "Idle" });
        }

        public void Add(AgentEntity entity)
        {
            _context.Agents.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get a list of all entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AgentEntity>> GetAll()
        {
            return await _context.Agents.ToListAsync();
        }

        public AgentEntity Find(string id)
        {
            return _context.Agents.FirstOrDefault(t => t.Id == id);
        }

        public void Remove(string id)
        {
            AgentEntity entity = _context.Agents.First(t => t.Id == id);

            _context.Agents.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(AgentEntity entity)
        {
            _context.Agents.Update(entity);
            _context.SaveChanges();
        }
    }
}
