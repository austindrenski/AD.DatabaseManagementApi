using System.Data.Entity;
using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    /// <summary>
    /// Encapsulates a <see cref="DbManagementService"/>.
    /// </summary>
    [PublicAPI]
    public sealed class DbManagementServiceContainer
    {
        [NotNull]
        private readonly DbManagementService _service;

        /// <summary>
        /// The name of this service container.
        /// </summary>
        [NotNull]
        public string Name { get; }

        /// <summary>
        /// Constructs a <see cref="DbManagementServiceContainer"/> to encapsulate a <see cref="DbManagementService"/>.
        /// </summary>
        /// <param name="service">The <see cref="DbManagementService"/> to execute against the database.</param>
        /// <param name="name">The name of the service.</param>
        public DbManagementServiceContainer([NotNull] DbManagementService service, [NotNull] string name)
        {
            _service = service;
            Name = name;
        }

        /// <summary>
        /// Executes the contained service against a <see cref="DbContext"/>.
        /// </summary>
        /// <returns>The count of records affected by the service.</returns>
        public int Execute()
        {
            return _service.Execute();
        }
    }
}