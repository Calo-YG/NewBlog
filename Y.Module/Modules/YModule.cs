using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Modules
{
    public class YModule : IYModule , IPreInitApplication
    {
        /// <summary>
        /// 预处理程序
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void PreInitApplication(ConfigerServiceContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 服务注册与配置
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        void IYModule.ConfigerService(ConfigerServiceContext context)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        void IYModule.InitApplication(InitApplicationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
