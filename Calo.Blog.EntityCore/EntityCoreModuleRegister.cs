﻿using Autofac;
using Calo.Blog.Extenions.DependencyInjection.AutoFacDependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore
{
    public class EntityCoreModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IocManager>().As<IIocManager>().SingleInstance();

        }
    }
}