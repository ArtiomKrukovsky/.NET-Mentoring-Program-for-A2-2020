namespace WebShop.LoggingCastelCoreLib
{
    using System.Collections.Generic;
    using System.Linq;

    using Castle.DynamicProxy;

    using NLog;

    public class LogMethodCastelCore : IInterceptor
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static IInvocation _invocation;

        public void Intercept(IInvocation invocation)
        {
            _invocation = invocation;
            this.LogDetailsOfMethodOnEnter();
            invocation.Proceed();
            LogDetailsOfMethodOnExit();
        }

        private static void LogDetailsOfMethodOnExit()
        {
            if (_invocation.ReturnValue != null)
            {
                logger.Info($"_return value - {_invocation.ReturnValue}");
            }
        }

        private void LogDetailsOfMethodOnEnter()
        {
            logger.Info($"Method name - '{_invocation.Method.Name}'");

            if (_invocation.Arguments.Any())
            {
                this.LogMethodArguments(_invocation.Arguments);
            }
        }

        private void LogMethodArguments(IEnumerable<object> arguments)
        {
            foreach (var argument in arguments)
            {
                logger.Info($"- parametr: '{argument}'");
            }
        }
    }
}
