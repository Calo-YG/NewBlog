using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void TryAddObjectAccessor<T>(this IServiceCollection services, T obj)
        {
            if (services.Any(p => p.ServiceType == typeof(T))) return;

        }
    }
}
