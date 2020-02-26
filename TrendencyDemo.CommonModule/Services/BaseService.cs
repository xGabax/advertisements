using DrendencyDemo.Web.Data;
using TrendencyDemo.CommonModule.Aggregates;
using TrendencyDemo.CommonModule.Interfaces;

namespace TrendencyDemo.CommonModule.Services
{
    public abstract class BaseService
    {
        protected readonly TrendencyDemoDbContext _context;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IDateInfoService _dateInfoService;

        public BaseService(ServiceBaseAggregate aggregate)
        {
            _context = aggregate.Context;
            _currentUserService = aggregate.CurrentUserService;
            _dateInfoService = aggregate.DateInfoService;
        }
    }
}
