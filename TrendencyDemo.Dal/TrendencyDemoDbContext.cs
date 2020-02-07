using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendencyDemo.Dal.Entities;

namespace DrendencyDemo.Web.Data
{
    public class TrendencyDemoDbContext : IdentityDbContext<User, Role, int>
    {
        public TrendencyDemoDbContext(
            DbContextOptions<TrendencyDemoDbContext> options) : base(options)
        {
        }
    }
}
