using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    /// <summary>
    /// Encapsulates a <see cref="DbContext"/> and an enumerable collection of <see cref="IDbManagementService"/> objects.
    /// </summary>
    [PublicAPI]
    public class DbManagementController
    {
        [NotNull]
        [ItemNotNull]
        private readonly IEnumerable<IDbManagementService> _services;

        /// <summary>
        /// Constructs a <see cref="DbManagementController"/> for a given enumerable collection of <see cref="IDbManagementService"/> objects.
        /// </summary>
        /// <param name="services">An enumerable collection of <see cref="IDbManagementService"/> objects encapsulating services to execute against the database.</param>
        public DbManagementController([NotNull][ItemNotNull] IEnumerable<IDbManagementService> services) 
        {
            _services = services;
        }

        /// <summary>
        /// Call this method to begin executing services.
        /// </summary>
        public void Invoke()
        {
            DateTime startTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            Console.WriteLine(); 
            Console.WriteLine($"Executing services at {startTime.ToShortTimeString()}. Please wait...");
            int total = _services.Count();
            int index = 1;
            foreach (IDbManagementService service in _services)
            {
                Console.WriteLine();
                Console.WriteLine($"> Starting service {index++} of {total} at {currentTime}: {service.Name}.");

                int count = service.Execute();

                Console.WriteLine($"> {service.Name} completed with {count} records affected in {(DateTime.Now - currentTime).TotalMinutes:0.00} minutes.");

                currentTime = DateTime.Now;
            }

            Console.WriteLine();
            Console.WriteLine($"Completed {total} services in {(DateTime.Now - startTime).TotalMinutes:0.00} minutes. Press enter to exit...");

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }
        }
    }
}