using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{

    public abstract class YModule : IYModule, IPreApplicationInition
    {
        protected internal ServiceConfigurationContext ServiceConfigurationContext
        {
            get
            {
                if (_ServiceConfigurationContext is null)
                {
                    throw new ArgumentException("_ServiceConfigurationContext is null");
                }
                return _ServiceConfigurationContext;
            }
            internal set => _ServiceConfigurationContext = value;
        }

        private ServiceConfigurationContext _ServiceConfigurationContext;
        /// <summary>
        /// 服务注册配置
        /// </summary>
        /// <param name="context"></param>
        public virtual void ServiceConfiguration(ServiceConfigurationContext context)
        {
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="serviceProvider"></param>
        public virtual void ApplictionInit(IServiceProvider serviceProvider)
        {

        }
        /// <summary>
        /// 预初始化
        /// </summary>
        /// <param name="serviceProvider"></param>
        public virtual void PreApplictionInition(IServiceProvider serviceProvider)
        {

        }

        public static bool IsModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IYModule).GetTypeInfo().IsAssignableFrom(type);
        }
    }
}
