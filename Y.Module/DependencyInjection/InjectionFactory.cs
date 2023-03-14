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
                var firstInterface = type.GetInterfaces().FirstOrDefault();
                if (firstInterface is null || service.IsExists(firstInterface)) continue;


            }
        }
        protected virtual void InjectionAttribute(IServiceCollection services, Type type)
        {
            var attributes = type.GetCustomAttributes().Cast<InjectionAttribute>();
            var injectionAttribute = attributes.FirstOrDefault();
            if (!attributes.Any() || injectionAttribute is null || injectionAttribute.InterfaceType is null)
            {
                if (!services.IsExists(type))
                {
                    AddToInjection(services, type, injectionAttribute.InjectionEnum);
                }
            }
            if (injectionAttribute != null && !services.IsExists(injectionAttribute.InterfaceType))
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

        private void AddIoInjectionWithInterface(IServiceCollection services, Type type, Type interfaeType, InjectionEnum injectionEnum)
        {
            if (injectionEnum == InjectionEnum.Singleton)
            {
                services.AddSingleton(interfaeType, type);
            }
            if (injectionEnum == InjectionEnum.Scoped)
            {
                services.AddScoped(interfaeType, type);
            }
            if (injectionEnum == InjectionEnum.Transient)
            {
                services.AddTransient(interfaeType, type);
            }
        }
    }
}
