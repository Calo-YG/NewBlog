using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Y.Module.Modules
{
    public class YModule : IYModule, IPreInitApplication
    {
        protected internal ConfigerServiceContext ConfigerServiceContext
        {
            get
            {
                if (_configserviceContext is null)
                {
                    throw new ArgumentException("注册服务时未对_ConfigServiceContext赋值");
                }
                return _configserviceContext;
            }
            internal set => _configserviceContext = value;
        }
        private ConfigerServiceContext _configserviceContext;
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

        public static bool IsModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IYModule).GetTypeInfo().IsAssignableFrom(type);
        }

        internal static void CheckModuleType(Type type)
        {
            if (!IsModule(type))
            {
                throw new ArgumentNullException($"{type.Name}没有继承YModule");
            }
        }

        private void Configure<TOptions>(Action<TOptions> action) where TOptions : class
        {
            ConfigerServiceContext.Services.Configure<TOptions>(action);
        }
    }
}
