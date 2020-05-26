namespace LoggingLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NLog;

    using PostSharp.Aspects;
    using PostSharp.Aspects.Advices;
    using PostSharp.Extensibility;
    using PostSharp.Serialization;

    [PSerializable]
    public class LogPostSharpAspect : TypeLevelAspect
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [OnMethodEntryAdvice]
        [MulticastPointcut(Targets = MulticastTargets.Method)]
        public void OnEntry(MethodExecutionArgs args)
        {
            logger.Info($"Method name - '{args.Method.Name}'");

            if (args.Arguments.Any())
            {
                this.LogMethodArguments(args.Arguments);
            } 
        }

        [OnMethodExitAdvice]
        [MulticastPointcut(Targets = MulticastTargets.Method)]
        public void OnExit(MethodExecutionArgs args)
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
