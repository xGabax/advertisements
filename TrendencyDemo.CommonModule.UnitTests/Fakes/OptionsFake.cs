using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrendencyDemo.CommonModule.UnitTests.Fakes
{
    public class OptionsFake<T> : IOptions<T>
        where T : class, new()
    {
        private readonly T _config;
        public OptionsFake(T config)
        {
            _config = config;
        }

        public T Value
        {
            get
            {
                return _config;
            }
        }
    }
}
