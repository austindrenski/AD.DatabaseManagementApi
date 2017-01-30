using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace DatabaseManagementApi
{
    [PublicAPI]
    public class DbManagementController : IDisposable
    {
        private readonly DbContext _context;

        private readonly IList<DbManagementServiceContainer> _services = new List<DbManagementServiceContainer>();

        public DbManagementController(DbContext context) 
        {
            _context = context;
        }

        public void Invoke()
        {
            DateTime startTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            Console.WriteLine(); 
            Console.WriteLine($@"Executing services at {startTime.ToShortTimeString()}. Please wait...");
            for (int i = 0; i < _services.Count; i++)
            {
                DbManagementServiceContainer service = _services[i];
                Console.WriteLine();
                Console.WriteLine($@"> Starting service {i + 1} of {_services.Count} at {currentTime}: {service.Name}.");
                int count = service.Invoke(_context);
                Console.WriteLine($@"> {service.Name} completed with {count} records affected in {(DateTime.Now - currentTime).TotalMinutes:0.00} minutes.");
                currentTime = DateTime.Now;
            }
            Console.WriteLine();
            Console.WriteLine($@"Completed {_services.Count} services in {(DateTime.Now - startTime).TotalMinutes:0.00} minutes. Press enter to exit...");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }
        }

        public void Message(string message, DbManagementServiceContainer serviceContainer)
        {
            string response = null;
            while (TestResponse(response))
            {
                Console.WriteLine();
                Console.WriteLine($@"{message} [Y/N]");
                response = Console.ReadLine();
                if (!response?.Equals("Y", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    continue;
                }
                serviceContainer.Parameters = HandleParameters();
                _services.Add(serviceContainer);
            }
        }

        public void Message(string message, string year, DbManagementServiceContainer serviceContainer)
        {
            string response = null;
            while (TestResponse(response))
            {
                Console.WriteLine();
                Console.WriteLine(message);
                response = Console.ReadLine();
                if (!response?.Equals("Y", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    continue;
                }
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("year", year);
                serviceContainer.Parameters = parameters;
                _services.Add(serviceContainer);
            }
        }

        private IDictionary<string, string> HandleParameters()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            string response = null;
            while (TestResponse(response))
            {
                Console.WriteLine();
                Console.WriteLine(@"Does this service require parameters? [Y/N]");
                response = Console.ReadLine();
                if (!response?.Equals("Y", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    continue;
                }
                Console.WriteLine();
                Console.WriteLine(@"Enter parameters in the form: [name1 = value1, value2, value3; name2 = value1]");
                string input = Console.ReadLine();
                parameters = input?.Replace(" ", null)
                                  .Split(';')
                                  .Select(x => x.Split('='))
                                  .ToDictionary(x => x[0], x => x[1]);
            }
            return parameters;
        }

        void IDisposable.Dispose()
        {
            _context.Dispose();
        }

        private bool TestResponse(string response)
        {
            response = response ?? "";
            return !response.Equals("Y", StringComparison.OrdinalIgnoreCase)
                && !response.Equals("N", StringComparison.OrdinalIgnoreCase);
        }
    }
}
