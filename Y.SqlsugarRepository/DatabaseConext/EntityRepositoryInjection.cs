using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.Entensions;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class EntityRepositoryInjection : IEntityRepositoryInjection, IEntityContainer
    {
        public IReadOnlyList<EntityTypeInfo> EntityTypes { get; set; }
        public IServiceCollection Services { get; set; }

        public EntityRepositoryInjection(IServiceCollection services, Type contextType)
        {
            Services = services;
            EntityTypes = FindEntities(contextType);

            services.AddSingleton<IEntityRepositoryInjection>(this);
            services.AddSingleton<IEntityContainer>(this);
        }

        protected virtual EntityTypeInfo[] FindEntities(Type contextType)
        {
            return new DbEntityFinder().GetEntityTypeInfos(contextType).ToArray();
        }
    }
}
