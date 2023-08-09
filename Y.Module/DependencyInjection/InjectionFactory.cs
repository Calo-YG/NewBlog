﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.Module.Extensions;

namespace Y.Module.DependencyInjection
{
    public class InjectionFactory
    {
        public readonly Type[] InjectionTypes = new Type[]
        {
            typeof(ISingletonInjection),
            typeof(IScopedInjection),
            typeof(ITransientInjection)
        };
        public void InjectionAssembly(IServiceCollection service, Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                Injection(service, type);

                InjectionWithInterface(service, type);
            }
        }
        protected virtual void Injection(IServiceCollection services, Type type)
        {
            var attributes = type.GetCustomAttributes().Where(p=>p.GetType()==typeof(InjectionAttribute));
            if (!attributes.Any())
            {
                return;
            }
            var injectionAttribute = attributes.FirstOrDefault() as InjectionAttribute;
            if (!attributes.Any() || injectionAttribute is null || injectionAttribute.InterfaceType is null)
            {
                if (!services.IsExists(type))
                {
                    AddToInjection(services, type, injectionAttribute.InjectionEnum);
                }
                return;
            }

            if (injectionAttribute is null && !services.IsExists(injectionAttribute.InterfaceType))
            {
                AddIoInjectionWithInterface(services, type, injectionAttribute.InterfaceType, injectionAttribute.InjectionEnum);
            }
        }
        private void AddToInjection(IServiceCollection services, Type type, InjectionEnum injectionEnum)
        {
            if (injectionEnum == InjectionEnum.Singleton)
            {
                services.AddSingleton(type);
            }
            if (injectionEnum == InjectionEnum.Scoped)
            {
                services.AddScoped(type);
            }
            if (injectionEnum == InjectionEnum.Transient)
            {
                services.AddTransient(type);
            }
        }
        private void AddIoInjectionWithInterface(IServiceCollection services, Type type, Type? interfaeType, InjectionEnum injectionEnum)
        {
            if (injectionEnum == InjectionEnum.Singleton)
            {
                if(interfaeType is null)
                {
                    services.AddSingleton(type);
					return;
				}
                services.AddSingleton(interfaeType, type);
            }
            if (injectionEnum == InjectionEnum.Scoped)
            {
                if(interfaeType is null)
                {
                    services.AddScoped(type);
                    return;
                }
                services.AddScoped(interfaeType, type);
            }
            if (injectionEnum == InjectionEnum.Transient)
            {
                if(interfaeType is null)
                {
                    services.AddTransient(type);
                    return;
                }
                services.AddTransient(interfaeType, type);
            }
        }

        /// <summary>
        /// 使用接口注入
        /// </summary>
        protected virtual void InjectionWithInterface(IServiceCollection services, Type type)
        {
            var interfasces = type.GetInterfaces();

            if(interfasces.Length <= 1)
            {
                return;
            }

            var hasInjectionInterface = interfasces.Any(p => InjectionTypes.Any(x => x == p));

            if (!hasInjectionInterface)
            {
                return;
            }

            var injectionInterface = interfasces.FirstOrDefault(p => InjectionTypes.Contains(p));

            Type interfaceImplete = null;

            var firstInterface = interfasces[0];

            if(firstInterface != injectionInterface)
            {
                interfaceImplete = firstInterface;
            }
            else
            {
                interfaceImplete = interfasces[1];
            }

            ///判断是否注入容器中、
            if(interfaceImplete is  null || services.IsExists(interfaceImplete)) 
            {
                return;
            }

            AddServieWithInterface(services, interfaceImplete, type, injectionInterface); ;
        }

        private void AddServieWithInterface(IServiceCollection services, Type interfaces,Type implete,Type injection) 
        {
            if(injection == typeof(ISingletonInjection))
            {
                services.AddSingleton(interfaces,implete);
                return;
            }
            if(injection == typeof(IScopedInjection))
            {
                services.AddScoped(interfaces, implete);
                return;
            }
            if(injection == typeof(ITransientInjection))
            {
                services.AddTransient(interfaces, implete);
                return;
            }
        }
    }
}
