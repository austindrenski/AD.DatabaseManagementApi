using System.Collections.Generic;
using System.Data.Entity;
using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    /// <summary>
    /// Encapsulates a service to be executed against a <see cref="DbContext"/>.
    /// </summary>
    [PublicAPI]
    public abstract class DbManagementService
    {
        /// <summary>
        /// The <see cref="DbContext"/> against which this service executes.
        /// </summary>
        [NotNull]
        protected readonly DbContext Context;

        /// <summary>
        /// The parameters used by this service.
        /// </summary>
        [CanBeNull]
        protected readonly IDictionary<string, string> Parameters;

        /// <summary>
        /// Constructs a <see cref="DbManagementService"/> encapsulating a series of database operations.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> against which the service is executed.</param>
        /// <param name="parameters">The parameters used by this service.</param>
        protected DbManagementService([NotNull] DbContext context, IDictionary<string, string> parameters = null)
        {
            Context = context;
            Parameters = parameters;
        }

        /// <summary>
        /// Executes the service.
        /// </summary>
        /// <returns></returns>
        public virtual int Execute()
        {
            return 0;
        }
    }
}