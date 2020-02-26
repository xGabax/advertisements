using System;
using TrendencyDemo.CommonModule.Interfaces;

namespace TrendencyDemo.CommonModule.Services
{
    public class DateInfoService : IDateInfoService
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
