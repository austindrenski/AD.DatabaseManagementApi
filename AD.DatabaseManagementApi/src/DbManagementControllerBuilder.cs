using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    [PublicAPI]
    public class DbManagementControllerBuilder
    {
        private readonly IList<Func<DbManagementServiceContainer>> _messages = new List<Func<DbManagementServiceContainer>>();

        public DbManagementControllerBuilder()
        {
        }

        public DbManagementController Build(DbContext context)
        {
            return new DbManagementController(context, _messages);
        }


        public Func<DbManagementServiceContainer> AddMessage(string message, DbManagementServiceContainer serviceContainer)
        {
            Func<DbManagementServiceContainer> action =  () =>
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
                    break;
                }
                return serviceContainer;
            };
            return action;
        }

        public Func<DbManagementServiceContainer> AddMessage(string message, string year, DbManagementServiceContainer serviceContainer)
        {
            Func<DbManagementServiceContainer> action = () =>
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
                    break;
                }
                return serviceContainer;
            };
            return action;
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

        private bool TestResponse(string response)
        {
            response = response ?? "";
            return !response.Equals("Y", StringComparison.OrdinalIgnoreCase)
                && !response.Equals("N", StringComparison.OrdinalIgnoreCase);
        }
    }
}
