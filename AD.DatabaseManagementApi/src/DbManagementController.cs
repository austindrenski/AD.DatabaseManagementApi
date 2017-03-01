using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    [PublicAPI]
    public class DbManagementController
    {
        private readonly DbContext _context;

        private readonly IEnumerable<DbManagementServiceContainer> _services;

        public DbManagementController(DbContext context, IEnumerable<Func<DbManagementServiceContainer>> services) 
        {
            _context = context;
            _services = services.Select(x => x());
        }

        public void Invoke()
        {
            DateTime startTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            Console.WriteLine(); 
            Console.WriteLine($@"Executing services at {startTime.ToShortTimeString()}. Please wait...");
            int total = _services.Count();
            int index = 1;
            foreach (DbManagementServiceContainer service in _services)
            {
                Console.WriteLine();
                Console.WriteLine($@"> Starting service {index++} of {total} at {currentTime}: {service.Name}.");

                int count = service.Invoke(_context);

                Console.WriteLine($@"> {service.Name} completed with {count} records affected in {(DateTime.Now - currentTime).TotalMinutes:0.00} minutes.");

                currentTime = DateTime.Now;
            }

            Console.WriteLine();
            Console.WriteLine($@"Completed {total} services in {(DateTime.Now - startTime).TotalMinutes:0.00} minutes. Press enter to exit...");

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }
        }
    }
}
