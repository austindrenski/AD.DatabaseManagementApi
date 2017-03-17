using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    /// <summary>
    /// Encapsulates a <see cref="DbContext"/> and a collection of <see cref="DbManagementServiceContainer"/> objects.
    /// </summary>
    [PublicAPI]
    public class DbManagementController
    {
        [NotNull]
        [ItemNotNull]
        private readonly IEnumerable<DbManagementServiceContainer> _services;

        /// <summary>
        /// Constructs a <see cref="DbManagementController"/> for a given <see cref="DbContext"/> and a collection of <see cref="DbManagementServiceContainer"/> objects.
        /// </summary>
        /// <param name="services">An enumerable collection of <see cref="DbManagementServiceContainer"/> objects, each of which encapsulate a service to execute against the database.</param>
        public DbManagementController([NotNull][ItemNotNull]  IEnumerable<DbManagementServiceContainer> services) 
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
            foreach (DbManagementServiceContainer service in _services)
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