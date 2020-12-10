using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Core.Aspects
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly Serilog.ILogger _logger;
        public LoggingInterceptor(Serilog.ILogger logger)
        {
            _logger = logger;
        }
        public void Intercept(IInvocation invocation)
        {
            _logger.Verbose("Calling method: {0} with parameters: {1}... ",
                   invocation.Method.Name,
                   string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

            invocation.Proceed();
            _logger.Verbose("The method: {methodName} return value was {@Return}.", invocation.Method.Name, invocation.ReturnValue);

        }
    }
}
