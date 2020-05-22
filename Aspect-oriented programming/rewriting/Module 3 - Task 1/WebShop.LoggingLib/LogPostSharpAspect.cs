namespace LoggingLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NLog;

    using PostSharp.Aspects;

    [Serializable]
    public class LogPostSharpAspect : OnMethodBoundaryAspect
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnEntry(MethodExecutionArgs args)
        {
            logger.Info($"Method name - '{args.Method.Name}'");

            if (args.Arguments.Any())
            {
                this.LogMethodArguments(args.Arguments);
            } 
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (args.ReturnValue != null)
            {
                logger.Info($"_return value - {args.ReturnValue}");
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
