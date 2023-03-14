using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectionAttribute : Attribute
    {
        public Type? InterfaceType { get; private set; }
        public bool UseInterface { get; private set; }
        public InjectionEnum InjectionEnum { get; private set; }

        public InjectionAttribute(InjectionEnum injectionEnum, bool userinterface = false, Type? interfaceType = null)
        {
            InjectionEnum = injectionEnum;
            UseInterface = userinterface;
            InterfaceType = interfaceType;
            Check();
        }

        private void Check()
        {
            if (UseInterface && InterfaceType is null)
            {
                throw new ApplicationException("在使用InjectionAttribute特性时，useinterface为true,interfaceType不能为null");
            }
        }
    }
}
