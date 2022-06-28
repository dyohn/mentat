// Startup.cs
// dyohn
// 6/25/2022
//
// See: https://github.com/pengweiqhca/Xunit.DependencyInjection
// for more information about usage.

using System;
using Microsoft.Extensions.DependencyInjection;

using Mentat.Domain.Interfaces;
// using Mentat.Domain.Models;

namespace Mentat.Domain.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IBashTestConfig, BashTestConfig>();
            //services.AddTransient<IBashTestDriver, BashTestDriver>();
        }
    }
}

