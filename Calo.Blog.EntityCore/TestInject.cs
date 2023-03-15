﻿using Microsoft.Extensions.Logging;
using Y.Module.DependencyInjection;

namespace Calo.Blog.EntityCore
{
    [Injection(InjectionEnum.Scoped,true,typeof(ITestInject))]
    public class TestInject : ITestInject
    {
        private readonly ILogger<TestInject> _logger;
        public TestInject(ILogger<TestInject> logger)
        {
            _logger = logger;
        }

        public void LogInfo()
        {
            _logger.LogInformation("注入成功");
        }
    }
}
