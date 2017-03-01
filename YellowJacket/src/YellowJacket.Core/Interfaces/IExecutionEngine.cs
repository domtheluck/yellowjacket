using System.Collections.Generic;
using YellowJacket.Core.Engine.Events;

namespace YellowJacket.Core.Interfaces
{
    public delegate void ExecutionStartHandler(object sender, ExecutionStartEventArgs eventArgs);
    public delegate void ExecutionStopHandler(object sender, ExecutionStopEventArgs eventArgs);
    public delegate void ExecutionCompletedHandler(object sender, ExecutionCompletedEventArgs eventArgs);
    public delegate void ExecutionProgressHandler(object sender, ExecutionProgressEventArgs eventArgs);

    public interface IExecutionEngine
    {
        event ExecutionStartHandler ExecutionStart;
        event ExecutionStopHandler ExecutionStop;
        event ExecutionCompletedHandler ExecutionCompleted;
        event ExecutionProgressHandler ExecutionProgress;

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        void Execute(string assemblyPath, string feature);

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        /// <param name="logger"><see cref="ILogger"/>.</param>
        void Execute(string assemblyPath, string feature, ILogger logger);

        /// <summary>
        /// Execute the specified Feature contains in the related assembly.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="feature">The feature.</param>
        /// <param name="loggers">The loggers.</param>
        void Execute(string assemblyPath, string feature, List<ILogger> loggers);
    }
}
