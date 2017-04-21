using Microsoft.EntityFrameworkCore;
using YellowJacket.Dashboard.Models.Agent;

namespace YellowJacket.Dashboard.Models
{
    public class YellowJacketContext : DbContext
    {
        public YellowJacketContext(DbContextOptions<YellowJacketContext> options)
            : base(options)
        {
        }

        public DbSet<AgentModel> Agents { get; set; }

    }
}
