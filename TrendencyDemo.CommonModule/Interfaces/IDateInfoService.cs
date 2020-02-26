using System;

namespace TrendencyDemo.CommonModule.Interfaces
{
    public interface IDateInfoService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
