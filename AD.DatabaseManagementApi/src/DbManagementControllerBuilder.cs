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
        private readonly IList<Func<IDbManagementService>> _messages = new List<Func<IDbManagementService>>();

        public DbManagementControllerBuilder()
        {
        }

        [NotNull]
        public DbManagementController Build([NotNull] DbContext context)
        {
            return new DbManagementController(_messages.Where(x => x != null).Select(x => x()));
        }

        [NotNull]
        public DbManagementControllerBuilder AddMessage([NotNull] string message, [NotNull] IDbManagementService service)
        {
            Func<IDbManagementService> action = () =>
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine(message);
                    string response = Console.ReadLine() ?? "";
                    if (response.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        return service;
                    }
                    if (response.Equals("N", StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
            };
            _messages.Add(action);
            return this;
        }
    }
}