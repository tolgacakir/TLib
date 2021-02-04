using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Core.Ioc
{
    public abstract class DependencyResolver
    {
        protected readonly IContainer _container;
        protected DependencyResolver()
        {
            _container = BuildContainer();
        }

        protected abstract IContainer BuildContainer();

        public T GetService<T>()
            where T: class
        {
            var result = _container.TryResolve(out T instance);
            return instance ?? throw new Exception($"The service could not found: {nameof(T)}");
        }
    }
}
