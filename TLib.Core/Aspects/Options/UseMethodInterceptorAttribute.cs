using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Core.Aspects.Options
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    sealed class UseInterceptorAttribute : Attribute
    {
        public UseInterceptorAttribute() //TODO: Interceptor tipleri parametre olarak alınıp hangi interceptorlerin kullanılacağı seçilebilir ya da interceptor seçimi makalesine bakılabilir.
        {
        }
    }
}
