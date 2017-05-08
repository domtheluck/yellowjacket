using Microsoft.EntityFrameworkCore;
using YellowJacket.Dashboard.Entities.Agent;

namespace YellowJacket.Dashboard.Entities
{
    public class YellowJacketContext : DbContext
    {
        public YellowJacketContext(DbContextOptions<YellowJacketContext> options)
            : base(options)
        {
        }

        public DbSet<AgentEntity> Agents { get; set; }

    }
}
