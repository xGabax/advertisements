using DrendencyDemo.Web.Data;
using TrendencyDemo.CommonModule.Aggregates;

namespace TrendencyDemo.CommonModule.Services
{
    public abstract class BaseService
    {
        protected readonly TrendencyDemoDbContext _context;

        public BaseService(ServiceBaseAggregate aggregate)
        {
            _context = aggregate.Context;
        }
    }
}
