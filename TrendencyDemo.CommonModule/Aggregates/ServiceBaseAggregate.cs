using DrendencyDemo.Web.Data;
using TrendencyDemo.CommonModule.Interfaces;

namespace TrendencyDemo.CommonModule.Aggregates
{
    public class ServiceBaseAggregate
    {
        public TrendencyDemoDbContext Context { get; set; }
        public IDateInfoService DateInfoService { get; set; }
        public ICurrentUserService CurrentUserService { get; set; }

        public ServiceBaseAggregate(TrendencyDemoDbContext context,
            IDateInfoService dateInfoService,
            ICurrentUserService currentUserService)
        {
            Context = context;
            DateInfoService = dateInfoService;
            CurrentUserService = currentUserService;
        }
    }
}
