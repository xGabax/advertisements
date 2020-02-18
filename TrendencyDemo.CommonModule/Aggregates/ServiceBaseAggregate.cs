using DrendencyDemo.Web.Data;

namespace TrendencyDemo.CommonModule.Aggregates
{
    public class ServiceBaseAggregate
    {
        public TrendencyDemoDbContext Context { get; set; }

        public ServiceBaseAggregate(TrendencyDemoDbContext context)
        {
            Context = context;
        }
    }
}
