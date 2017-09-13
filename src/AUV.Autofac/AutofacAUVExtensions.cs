using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.AUV.Entityframework6
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterAUVEntityframework(this ContainerBuilder builder,params Assembly[] assemblies)
        {
            assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
            builder.RegisterAssemblyTypes(assemblies)
                .Where(m=>m.IsAssignableFrom())
                ;
            return builder;
        }
    }
}
