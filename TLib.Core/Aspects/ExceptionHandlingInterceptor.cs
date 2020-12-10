using Castle.DynamicProxy;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Core.Aspects
{
    public class ExceptionHandlingInterceptor : IInterceptor
    {
        private readonly ILogger _logger;

        public ExceptionHandlingInterceptor(Serilog.ILogger logger)
        {
            _logger = logger;
        }
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in method: {0} with parameters: {1}...",
                invocation.Method.Name,
                string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));
            }
        }
    }
}
