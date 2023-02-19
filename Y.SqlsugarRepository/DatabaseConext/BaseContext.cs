using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class BaseContext: SqlSugarClient
    {
        private readonly ILogger logger;
        private ConnectionConfigOptions Options { get; set; }
        public BaseContext(ConnectionConfigOptions options) :base(options)
        { 
           Options= options ?? throw new ArgumentException("数据库上下文注册optionsw为空");
        }

        /// <summary>
        /// 配置查询过滤器
        /// </summary>
        public virtual void ConfigureFilter() { }
        
        /// <summary>
        /// 配置Aop
        /// </summary>
        public virtual void ConfigureAop() { }
        /// <summary>
        /// 数据过滤器
        /// </summary>
        public virtual void ConfigureDataeFilter() { }
    }
}
