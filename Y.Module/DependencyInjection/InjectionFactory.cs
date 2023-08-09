﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.Module.Extensions;

namespace Y.Module.DependencyInjection
{
    public class InjectionFactory
    {
        public void InjectionAssembly(IServiceCollection service, Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                Injection(service, type);
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

        }
    }
}
