using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YellowJacket.Dashboard.Models;
using YellowJacket.Dashboard.Models.Agent;

namespace YellowJacket.Dashboard.Repositories
{
    public class AgentRepository: IAgentRepository
    {
        private readonly YellowJacketContext _context;

        public AgentRepository(YellowJacketContext context)
        {
            _context = context;

            if (!_context.Agents.Any())
                Add(new AgentModel { Name = "Item1" });
        }

        public void Add(AgentModel entity)
        {
            _context.Agents.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get a list of all entities.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AgentModel>> GetAll()
        {
            return await _context.Agents.ToListAsync();
        }

        public AgentModel Find(long key)
        {
            return _context.Agents.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(long key)
        {
            AgentModel entity = _context.Agents.First(t => t.Key == key);

            _context.Agents.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(AgentModel item)
        {
            _context.Agents.Update(item);
            _context.SaveChanges();
        }
    }
}
