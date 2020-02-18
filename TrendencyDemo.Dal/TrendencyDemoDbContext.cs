using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrendencyDemo.Dal.Entities;

namespace DrendencyDemo.Web.Data
{
    public class TrendencyDemoDbContext : IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Email> Emails { get; set; }
        public TrendencyDemoDbContext(
            DbContextOptions<TrendencyDemoDbContext> options) : base(options)
        {
        }
    }
}
