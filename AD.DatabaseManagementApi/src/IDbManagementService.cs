using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    /// <summary>
    /// Defines a service operation.
    /// </summary>
    [PublicAPI]
    public interface IDbManagementService
    {
        /// <summary>
        /// The name of this service.
        /// </summary>
        [NotNull]
        string Name { get; }

        /// <summary>
        /// Executes the service.
        /// </summary>
        /// <returns>The number of records affected by this service.</returns>
        int Execute();
    }
}